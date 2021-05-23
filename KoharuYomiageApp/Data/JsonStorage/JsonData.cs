using System.Collections.Generic;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.JsonStorage
{
    public class JsonData
    {
        public List<MastodonAccountData> MastodonAccountData { get; set; } = new();
        public List<MastodonClientData> MastodonClientData { get; set; } = new();
        public List<VoiceProfileData> VoiceProfileData { get; set; } = new();
        public double? GlobalVolume { get; set; }
    }
}
