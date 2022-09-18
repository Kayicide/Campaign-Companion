using System;
using DnD_Combat_Turn_Tracker.Data.Entities;
using DnD_Combat_Turn_Tracker.Data.HttpRequestObjects;

namespace DnD_Combat_Turn_Tracker.Data.Services
{
    public interface IUserService
    {
        public Task<User> FindOrCreateUser(string discordId);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _campaignHttpClient;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _campaignHttpClient = httpClientFactory.CreateClient("CampaignService");
        }

        public async Task<User> FindOrCreateUser(string discordId)
        {
            var response = await _campaignHttpClient.GetAsync($"User/{discordId}");
            var user = await response.Content.ReadFromJsonAsync<User>();
            if (user != null)
                return user;

            var body = new CreateUserHttpRequest() { DiscordId = discordId };
            response = await _campaignHttpClient.PostAsJsonAsync("User", body);
            user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }
    }
}

