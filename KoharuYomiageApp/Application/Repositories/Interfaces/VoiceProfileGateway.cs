using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class VoiceProfileGateway : IVoiceProfileGateway
    {
        readonly IVoiceProfileStorage _storage;

        public VoiceProfileGateway(IVoiceProfileStorage storage)
        {
            _storage = storage;
        }

        public async Task<VoiceProfileData?> FindVoiceProfile(string accountIdentifier, string type)
        {
            var data = await _storage.FindVoiceProfile(accountIdentifier, type);

            if (data is null)
            {
                return null;
            }

            return new VoiceProfileData(data.AccountIdentifier, type, data.Volume, data.Speed, data.Tone, data.Alpha,
                data.ToneScale, data.ComponentNormal, data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow,
                data.ComponentCalmness);
        }

        public async Task<IEnumerable<VoiceProfileData>> GetVoiceProfiles(string accountIdentifier)
        {
            var data = await _storage.GetVoiceProfiles(accountIdentifier);
            return data.Select(d => new VoiceProfileData(d.AccountIdentifier, d.Type, d.Volume, d.Speed, d.Tone,
                d.Alpha, d.ToneScale, d.ComponentNormal, d.ComponentHappy, d.ComponentAnger, d.ComponentSorrow,
                d.ComponentCalmness));
        }

        public async Task SaveVoiceProfile(VoiceProfileData data)
        {
            await _storage.SaveVoiceProfile(new VoiceProfileSaveData(data.AccountIdentifier, data.Type, data.Volume,
                data.Speed, data.Tone,
                data.Alpha, data.ToneScale, data.ComponentNormal, data.ComponentHappy, data.ComponentAnger,
                data.ComponentSorrow,
                data.ComponentCalmness));
        }
    }
}
