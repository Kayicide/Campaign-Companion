using CampaignCompanion.Data.Entities;
using CampaignCompanion.Data.Services;
using Microsoft.AspNetCore.Components;

namespace CampaignCompanion.Components
{
    public partial class CampaignInfoHeader
    {
        [Inject] public ICampaignService CampaignService { get; set; }
        [Parameter] public Campaign Campaign { get; set; }
        [Parameter] public bool ReadOnly { get; set; }

        public string CampaignNameUpdate { get; set; }
        public string CampaignDescriptionUpdate { get; set; }

        private bool EditMode { get; set; } = false;

        protected override void OnParametersSet()
        {
            SetupFields();
        }


        private void ToggleEditMode()
        {
            if(!ReadOnly) //idk probably don't need this, just being safe.
                EditMode = !EditMode;
        }

        private async Task SaveChanges()
        {
            Campaign.Name = CampaignNameUpdate;
            Campaign.Description = CampaignDescriptionUpdate;

            Campaign = await CampaignService.UpdateCampaign(Campaign);
            SetupFields();
            StateHasChanged();

            ToggleEditMode();
        }

        private void CancelEdit()
        {
            SetupFields();
            ToggleEditMode();
        }

        private void SetupFields()
        {
            CampaignNameUpdate = Campaign.Name;
            CampaignDescriptionUpdate = Campaign.Description;
        }
    }
}
