using System.Collections.Generic;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;

namespace KoharuYomiageApp.Infrastructures.JsonStorage
{
    public class JsonData
    {
        public JsonData()
        {
            MastodonAccountData = new List<MastodonAccountSaveData>();
            MastodonClientData = new List<MastodonClientSaveData>();
        }

        public List<MastodonAccountSaveData> MastodonAccountData { get; set; }
        public List<MastodonClientSaveData> MastodonClientData { get; set; }
    }
}
