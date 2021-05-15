using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;

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

        public async Task<MastodonAccountSaveData?> FindMastodonAccountData(string identifier)
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonAccountData.Find(data => data.Username + "@" + data.Instance == identifier);
        }

        public async Task SaveMastodonAccountData(MastodonAccountSaveData accountSaveData)
        {
            var storage = await GetOrCreateSettings();

            var index = storage.MastodonAccountData.FindIndex(data =>
                data.Username == accountSaveData.Username && data.Instance == accountSaveData.Instance);
            if (index is not -1)
            {
                storage.MastodonAccountData[index] = accountSaveData;
            }
            else
            {
                storage.MastodonAccountData.Add(accountSaveData);
            }

            await SaveSettings(storage);
        }

        public async Task<IEnumerable<MastodonAccountSaveData>> GetMastodonAccountData()
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonAccountData;
        }

        public async Task<MastodonClientSaveData?> FindMastodonClientData(string instance)
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonClientData.Find(data => data.Instance == instance);
        }

        public async Task SaveMastodonClientData(MastodonClientSaveData clientSaveData)
        {
            var storage = await GetOrCreateSettings();

            var index = storage.MastodonClientData.FindIndex(data => data.Instance == clientSaveData.Instance);
            if (index is not -1)
            {
                storage.MastodonClientData[index] = clientSaveData;
            }
            else
            {
                storage.MastodonClientData.Add(clientSaveData);
            }

            await SaveSettings(storage);
        }

        async Task<JsonData> GetOrCreateSettings()
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

        async Task SaveSettings(JsonData storage)
        {
            using var json = File.OpenWrite(SettingsPath);
            await JsonSerializer.SerializeAsync(json, storage);
        }
    }
}
