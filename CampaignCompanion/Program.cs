using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using CampaignCompanion.Data.Entities;
using CampaignCompanion.Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMudServices();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("CampaignService", c =>
{

    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ConnectionStrings:CampaignAPI"));

    c.DefaultRequestHeaders.Add("Accept", "application/json");

    //c.DefaultRequestHeaders.Add("Authorization", "Bearer " + GenToken(Configuration.GetValue<string>("AuthServiceSecret")));

});

builder.Services.AddTransient<ICampaignService, CampaignService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "Discord";
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOAuth("Discord",
    options =>
    {
        options.AuthorizationEndpoint = "https://discord.com/oauth2/authorize";
        options.Scope.Add("identify");
        options.CallbackPath = "/auth/oauthCallback";

        options.ClientId = builder.Configuration.GetValue<string>("Discord:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("Discord:ClientSecret");

        options.TokenEndpoint = "https://discord.com/api/oauth2/token";
        options.UserInformationEndpoint = "https://discord.com/api/users/@me";

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");

        options.AccessDeniedPath = "/DiscordFailedAuth";

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                    context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();

                var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                var userObject = await response.Content.ReadFromJsonAsync<DiscordUser>();

                var userService = builder.Services.BuildServiceProvider().GetService<IUserService>();
                var test = await userService.FindOrCreateUser(userObject.Id);

                context.RunClaimActions(user);
            }
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapWhen(ctx => ctx.Request.Path.Equals("/Login"), applicationBuilder =>
{
    applicationBuilder.Run(async context =>
    {
        await context.ChallengeAsync("Discord",
            properties: new AuthenticationProperties { RedirectUri = "/" });
    });
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
