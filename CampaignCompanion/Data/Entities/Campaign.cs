using System.Diagnostics.Metrics;

namespace CampaignCompanion.Data.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
        public List<Encounter>? Encounters { get; set; }
    }
}
