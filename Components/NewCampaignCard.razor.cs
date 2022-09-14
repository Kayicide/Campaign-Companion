using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DnD_Combat_Turn_Tracker.Components
{
    public partial class NewCampaignCard
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public EventCallback<string> OnClickCallback { get; set; }
        public int ElevationLevel { get; set; } = 1;
        private void OnMouseOver() => ElevationLevel = 4;
        private void OnMouseOut() => ElevationLevel = 1;

        private void OnClickCampaign()
        {
            OnClickCallback.InvokeAsync(CampaignName);
            CampaignName = "";
        }
        public string CampaignName { get; set; }
    }
}
