using CampaignCompanion.Data.Entities;
using CampaignCompanion.Data.Services;
using Microsoft.AspNetCore.Components;

namespace CampaignCompanion.Pages.CampaignPages
{
    public partial class CampaignPage
    {
        [Parameter] public string CampaignId { get; set; }
        [Inject] public ICampaignService CampaignService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        private Campaign? campaign { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            if(!Guid.TryParse(CampaignId, out var campaignIdGuid))
                NavigationManager.NavigateTo("/mycampaigns");

            campaign = await CampaignService.GetCampaign(campaignIdGuid);

            if (campaign == null)
                NavigationManager.NavigateTo("/mycampaigns");
        }
    }
}
