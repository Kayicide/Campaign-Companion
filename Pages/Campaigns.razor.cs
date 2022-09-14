using DnD_Combat_Turn_Tracker.Data.Entities;
using DnD_Combat_Turn_Tracker.Data.Services;
using Microsoft.AspNetCore.Components;

namespace DnD_Combat_Turn_Tracker.Pages
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
