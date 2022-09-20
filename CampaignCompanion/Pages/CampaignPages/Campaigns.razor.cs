using CampaignCompanion.Data.Entities;
using CampaignCompanion.Data.Services;
using Microsoft.AspNetCore.Components;

namespace CampaignCompanion.Pages.CampaignPages
{
    public partial class Campaigns
    {
        [Inject] public ICampaignService CampaignService { get; set; }

        public List<Campaign> CampaignList { get; set; } = new List<Campaign>();

        protected override async Task OnInitializedAsync()
        {
            CampaignList = await CampaignService.GetAllCampaigns();
        }
    }
}
