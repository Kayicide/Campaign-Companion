using DnD_Combat_Turn_Tracker.Data.Entities;
using DnD_Combat_Turn_Tracker.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DnD_Combat_Turn_Tracker.Components
{
    public partial class CampaignsGrid
    {
        [Parameter] public List<Campaign> CampaignList { get; set; }
        [Parameter] public bool AllowNew { get; set; }
        [Inject] public ICampaignService CampaignService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private string _userId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            _userId = authState.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        private async void CreateCampaign(string campaignName)
        {
            var createdCampaign = await CampaignService.CreateCampaign(campaignName, _userId);
            CampaignList.Add(createdCampaign);
            StateHasChanged();
        }
    }
}
