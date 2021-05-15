using System.Collections.Generic;
using KoharuYomiageApp.Application.Repositories.Interfaces;

namespace KoharuYomiageApp.Infrastructures.JsonStorage
{
    public class JsonData
    {
        public JsonData()
        {
            MastodonAccountData = new List<MastodonAccountData>();
            MastodonClientData = new List<MastodonClientData>();
        }

        public List<MastodonAccountData> MastodonAccountData { get; set; }
        public List<MastodonClientData> MastodonClientData { get; set; }
    }
}
