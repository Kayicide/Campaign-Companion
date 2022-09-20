using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampaignCompanion.Pages
{
    public class LogoutModel : PageModel
    {
        private IHttpContextAccessor _accessor;


        public LogoutModel(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public async Task<IActionResult> OnGet()
        {
            var context = _accessor.HttpContext;
            if(context == null)
                return Redirect("~/error");

            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}
