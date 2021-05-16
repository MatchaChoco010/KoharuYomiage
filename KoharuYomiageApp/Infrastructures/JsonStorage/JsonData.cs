using System.Collections.Generic;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;

namespace KoharuYomiageApp.Infrastructures.JsonStorage
{
    public class JsonData
    {
        public List<MastodonAccountSaveData> MastodonAccountData { get; set; } = new();
        public List<MastodonClientSaveData> MastodonClientData { get; set; } = new();
        public List<VoiceProfileSaveData> VoiceProfileData { get; set; } = new();
        public double? GlobalVolume { get; set; }
    }
}
