using System.Diagnostics.Metrics;

namespace DnD_Combat_Turn_Tracker.Data.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public List<Encounter>? Encounters { get; set; }
    }
}
