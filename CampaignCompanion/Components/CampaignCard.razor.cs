using CampaignCompanion.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace CampaignCompanion.Components
{
    public partial class CampaignCard
    {
        [Parameter] public Campaign Campaign { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public int ElevationLevel { get; set; } = 1;
        private void OnMouseOver() => ElevationLevel = 4;
        private void OnMouseOut() => ElevationLevel = 1;
        private void OnClickCampaign() => NavigationManager.NavigateTo($"/Campaign/{Campaign.Id}");
    }
}
