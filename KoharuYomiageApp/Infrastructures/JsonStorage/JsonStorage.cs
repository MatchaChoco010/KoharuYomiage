using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces;

namespace KoharuYomiageApp.Infrastructures.JsonStorage
{
    public class JsonStorage : IMastodonAccountStorage, IMastodonClientStorage
    {
        string SettingsPath
        {
            get
            {
                var exePath = Assembly.GetEntryAssembly()?.Location ?? "";
                var settingFilePath = Path.Combine(Path.GetDirectoryName(exePath) ?? "", "settings.json");
                return settingFilePath;
            }
        }

        public async ValueTask<MastodonAccountData?> FindMastodonAccountData(string identifier)
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonAccountData.Find(data => data.Username + "@" + data.Instance == identifier);
        }

        public async ValueTask SaveMastodonAccountData(MastodonAccountData accountData)
        {
            var storage = await GetOrCreateSettings();

            var index = storage.MastodonAccountData.FindIndex(data =>
                data.Username == accountData.Username && data.Instance == accountData.Instance);
            if (index is not -1)
            {
                storage.MastodonAccountData[index] = accountData;
            }
            else
            {
                storage.MastodonAccountData.Add(accountData);
            }

            await SaveSettings(storage);
        }

        public async ValueTask<IEnumerable<MastodonAccountData>> GetMastodonAccountData()
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonAccountData;
        }

        public async ValueTask<MastodonClientData?> FindMastodonClientData(string instance)
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonClientData.Find(data => data.Instance == instance);
        }

        public async ValueTask SaveMastodonClientData(MastodonClientData clientData)
        {
            var storage = await GetOrCreateSettings();

            var index = storage.MastodonClientData.FindIndex(data => data.Instance == clientData.Instance);
            if (index is not -1)
            {
                storage.MastodonClientData[index] = clientData;
            }
            else
            {
                storage.MastodonClientData.Add(clientData);
            }

            await SaveSettings(storage);
        }

        async ValueTask<JsonData> GetOrCreateSettings()
        {
            try
            {
                using var json = File.OpenRead(SettingsPath);
                return await JsonSerializer.DeserializeAsync<JsonData>(json) ?? throw new FileNotFoundException();
            }
            catch (FileNotFoundException)
            {
                var storage = new JsonData();
                await SaveSettings(storage);
                return storage;
            }
        }

        async ValueTask SaveSettings(JsonData storage)
        {
            using var json = File.OpenWrite(SettingsPath);
            await JsonSerializer.SerializeAsync(json, storage);
        }
    }
}
