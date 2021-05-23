using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using ImTools;
using KoharuYomiageApp.Data.Repository;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.JsonStorage
{
    public class JsonStorage : IMastodonAccountStorage, IMastodonClientStorage, IVoiceProfileStorage,
        IGlobalVolumeStorage
    {
        static string SettingsPath
        {
            get
            {
                var exePath = Assembly.GetEntryAssembly()?.Location ?? "";
                var settingFilePath = Path.Combine(Path.GetDirectoryName(exePath) ?? "", "settings.json");
                return settingFilePath;
            }
        }

        public async Task<double?> FindGlobalVolume()
        {
            var storage = await GetOrCreateSettings();
            return storage.GlobalVolume;
        }

        public async Task SaveGlobalVolume(double volume)
        {
            var storage = await GetOrCreateSettings();

            storage.GlobalVolume = volume;

            await SaveSettings(storage);
        }

        public async Task<MastodonAccountData?> FindMastodonAccountData(string identifier)
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonAccountData.Find(data => data.Username + "@" + data.Instance == identifier);
        }

        public async Task SaveMastodonAccountData(MastodonAccountData accountSaveData)
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

        public async Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData()
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonAccountData;
        }

        public async Task<MastodonClientData?> FindMastodonClientData(string instance)
        {
            var storage = await GetOrCreateSettings();
            return storage.MastodonClientData.Find(data => data.Instance == instance);
        }

        public async Task SaveMastodonClientData(MastodonClientData clientSaveData)
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

        public async Task<VoiceProfileData?> FindVoiceProfile(string accountIdentifier, string type)
        {
            var storage = await GetOrCreateSettings();
            return storage.VoiceProfileData.FindFirst(d => d.AccountIdentifier == accountIdentifier && d.Type == type);
        }

        public async Task<IEnumerable<VoiceProfileData>> GetVoiceProfiles(string accountIdentifier)
        {
            var storage = await GetOrCreateSettings();
            return storage.VoiceProfileData.FindAll(d => d.AccountIdentifier == accountIdentifier);
        }

        public async Task SaveVoiceProfile(VoiceProfileData data)
        {
            var storage = await GetOrCreateSettings();

            var index = storage.VoiceProfileData.FindIndex(d =>
                d.AccountIdentifier == data.AccountIdentifier && d.Type == data.Type);
            if (index is not -1)
            {
                storage.VoiceProfileData[index] = data;
            }
            else
            {
                storage.VoiceProfileData.Add(data);
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
            using var json = File.Open(SettingsPath, FileMode.Create);
            await JsonSerializer.SerializeAsync(json, storage);
        }
    }
}
