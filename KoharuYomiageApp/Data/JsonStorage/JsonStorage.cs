using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.JsonStorage
{
    public class JsonStorage : IMastodonAccountStorage, IMastodonClientStorage, IVoiceProfileStorage,
        IGlobalVolumeStorage, IReadingTextContainerStorage
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

        public async Task<double?> FindGlobalVolume(CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.GlobalVolume;
        }

        public async Task SaveGlobalVolume(double volume, CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);

            storage.GlobalVolume = volume;

            await SaveSettings(storage, cancellationToken);
        }

        public async Task<MastodonAccountData?> FindMastodonAccountData(string identifier,
            CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.MastodonAccountData.Find(data => data.Username + "@" + data.Instance == identifier);
        }

        public async Task SaveMastodonAccountData(MastodonAccountData accountSaveData,
            CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);

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

            await SaveSettings(storage, cancellationToken);
        }

        public async Task DeleteMastodonAccountData(string identifier, CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);

            var index = storage.MastodonAccountData.FindIndex(data => data.Username + "@" +  data.Instance == identifier);
            if (index is not -1)
            {
                storage.MastodonAccountData.RemoveAt(index);
            }

            await SaveSettings(storage, cancellationToken);
        }

        public async Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData(CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.MastodonAccountData;
        }

        public async Task<MastodonClientData?> FindMastodonClientData(string instance,
            CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.MastodonClientData.Find(data => data.Instance == instance);
        }

        public async Task SaveMastodonClientData(MastodonClientData clientSaveData, CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);

            var index = storage.MastodonClientData.FindIndex(data => data.Instance == clientSaveData.Instance);
            if (index is not -1)
            {
                storage.MastodonClientData[index] = clientSaveData;
            }
            else
            {
                storage.MastodonClientData.Add(clientSaveData);
            }

            await SaveSettings(storage, cancellationToken);
        }

        public async Task<int?> FindReadingTextContainerSize(CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.ReadingTextContainerSize;
        }

        public async Task SaveReadingTextContainerSize(int size, CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            storage.ReadingTextContainerSize = size;
            await SaveSettings(storage, cancellationToken);
        }

        public async Task<VoiceProfileData?> FindVoiceProfile(string accountIdentifier, string type,
            CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.VoiceProfileData.Find(d => d.AccountIdentifier == accountIdentifier && d.Type == type);
        }

        public async Task<IEnumerable<VoiceProfileData>> GetVoiceProfiles(string accountIdentifier,
            CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);
            return storage.VoiceProfileData.FindAll(d => d.AccountIdentifier == accountIdentifier);
        }

        public async Task SaveVoiceProfile(VoiceProfileData data, CancellationToken cancellationToken)
        {
            var storage = await GetOrCreateSettings(cancellationToken);

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

            await SaveSettings(storage, cancellationToken);
        }

        async Task<JsonData> GetOrCreateSettings(CancellationToken cancellationToken)
        {
            try
            {
                using var json = File.OpenRead(SettingsPath);
                return await JsonSerializer.DeserializeAsync<JsonData>(json, cancellationToken: cancellationToken) ??
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException)
            {
                var storage = new JsonData();
                await SaveSettings(storage, cancellationToken);
                return storage;
            }
        }

        async Task SaveSettings(JsonData storage, CancellationToken cancellationToken)
        {
            using var fs = File.Open(SettingsPath, FileMode.Create);
            await using var json = new Utf8JsonWriter(fs);
            cancellationToken.ThrowIfCancellationRequested();
            JsonSerializer.Serialize(json, storage);
        }
    }
}
