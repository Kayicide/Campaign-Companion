using DnD_Combat_Turn_Tracker.Data.Entities;

namespace DnD_Combat_Turn_Tracker.Data.Services
{
    public interface ICampaignService
    {
        public Task<List<Campaign>> GetAllCampaigns();
        public Task<List<Campaign>> GetUserCampaigns(string userId);
    }
    public class CampaignService : ICampaignService
    {
        private readonly HttpClient _campaignHttpClient;
        public CampaignService(IHttpClientFactory httpClientFactory)
        {
            _campaignHttpClient = httpClientFactory.CreateClient("CampaignService");
        }

        public async Task<List<Campaign>> GetAllCampaigns()
        {
            var response = await _campaignHttpClient.GetAsync("Campaign");
            var result = await response.Content.ReadFromJsonAsync<List<Campaign>>();
            if (result == null)
                return new List<Campaign>();
            return result;
        }

        public async Task<List<Campaign>> GetUserCampaigns(string userId)
        {
            var response = await _campaignHttpClient.GetAsync($"Campaign/{userId}");
            var result = await response.Content.ReadFromJsonAsync<List<Campaign>>();
            if (result == null)
                return new List<Campaign>();
            return result;
        }
    }
}
