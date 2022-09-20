namespace CampaignCompanion.Data.Entities
{
    public class Encounter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
