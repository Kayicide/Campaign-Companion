using System;
namespace CampaignCompanion.Data.Entities
{
    public class DiscordUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string? Avatar_Decorations { get; set; }
        public string Discriminator { get; set; }
        public int Public_Flags { get; set; }
        public int Flags { get; set; }
        public string Banner { get; set; }
        public string Banner_Color { get; set; }
        public int Accent_Color { get; set; }
        public string Locale { get; set; }
        public bool Mfa_Enabled { get; set; }
        public int Premium_Type { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }

    }
}

