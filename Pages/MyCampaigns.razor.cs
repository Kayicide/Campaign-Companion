using System.Security.Claims;
using DnD_Combat_Turn_Tracker.Data.Entities;
using DnD_Combat_Turn_Tracker.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DnD_Combat_Turn_Tracker.Pages
{
    public partial class MyCampaigns
    {
        [Inject] public ICampaignService CampaignService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public List<Campaign> CampaignList { get; set; } = new List<Campaign>();
        private string _userId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            _userId = authState.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;


            CampaignList = await CampaignService.GetUserCampaigns(_userId);
        }

        private async void CreateCampaign(string campaignName)
        {
            var createdCampaign = await CampaignService.CreateCampaign(campaignName, _userId);
            CampaignList.Add(createdCampaign);
            StateHasChanged();
        }
    }
}
