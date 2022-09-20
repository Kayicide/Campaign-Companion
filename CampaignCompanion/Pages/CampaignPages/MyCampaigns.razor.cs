using System.Security.Claims;
using CampaignCompanion.Data.Entities;
using CampaignCompanion.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CampaignCompanion.Pages.CampaignPages
{
    public partial class MyCampaigns
    {
        [Inject] public ICampaignService CampaignService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public List<Campaign> CampaignList { get; set; } = new List<Campaign>();
        private string _userId { get; set; }

        private bool _dmedCampaignsExpanded { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            _userId = authState.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;


            CampaignList = await CampaignService.GetUserCampaigns(_userId);
            if (CampaignList.Count >= 1)
                _dmedCampaignsExpanded = true;
        }

        private async void CreateCampaign(string campaignName)
        {
            var createdCampaign = await CampaignService.CreateCampaign(campaignName, _userId);
            CampaignList.Add(createdCampaign);
            StateHasChanged();
        }
    }
}
