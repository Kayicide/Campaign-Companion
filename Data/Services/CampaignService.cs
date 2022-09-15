using DnD_Combat_Turn_Tracker.Data.Entities;
using DnD_Combat_Turn_Tracker.Data.HttpRequestObjects;

namespace DnD_Combat_Turn_Tracker.Data.Services
{
    public interface ICampaignService
    {
        public Task<List<Campaign>> GetAllCampaigns();
        public Task<List<Campaign>> GetUserCampaigns(string userId);
        public Task<Campaign> CreateCampaign(string name, string userId);
        public Task<Campaign?> GetCampaign(Guid id);
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
            var response = await _campaignHttpClient.GetAsync($"Campaign/User/{userId}");
            var result = await response.Content.ReadFromJsonAsync<List<Campaign>>();
            if (result == null)
                return new List<Campaign>();
            return result;
        }

        public async Task<Campaign?> GetCampaign(Guid campaignId)
        {
            var response = await _campaignHttpClient.GetAsync($"Campaign/{campaignId}");
            var result = await response.Content.ReadFromJsonAsync<Campaign>();
            return result;
        }

        public async Task<Campaign> CreateCampaign(string name, string userId)
        {
            var body = new CreateCampaignHttpRequest {Name = name, UserId = userId};
            var response = await _campaignHttpClient.PostAsJsonAsync("Campaign", body);
            var result = await response.Content.ReadFromJsonAsync<Campaign>();
            if (result == null)
                return new Campaign();
            return result;
        }
    }
}
