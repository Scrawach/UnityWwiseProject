using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Editor
{
    public class AudioWwiseService
    {
        private const string EventGuid = "63074727-ACD1-4754-A758-15A77B458B62";
        private const uint EventId = 1784581504;
        
        private readonly List<AkBankManagerAsync.AsyncLoadingBankResult> _banks;

        private GameObject _audioListener;

        public AudioWwiseService() => 
            _banks = new List<AkBankManagerAsync.AsyncLoadingBankResult>();

        public async void Initialize()
        {
            var akInitializers = Resources.Load<AkInitializer>("WwiseGlobal");
            AkSoundEngineController.Instance.Init(akInitializers);
            AkSoundEngine.RegisterGameObj(akInitializers.gameObject);
            
            if (!IsBankLoaded("Main")) 
                await LoadBank("Main");

            _audioListener = akInitializers.gameObject;
        }
        
        public float GetDuration(uint playingId)
        {
            var eventId = AkSoundEngine.GetEventIDFromPlayingID(playingId);
            var data = AkWwiseProjectInfo.GetData().GetEventInfo(eventId);
            var result = AkSoundEngine.GetSourcePlayPosition(playingId, out var playPosition);
            var playPositionInSeconds = playPosition / 1000f;

            if (data == null)
                return 0f;
            
            return playPositionInSeconds / data.maxDuration;
        }
        
        public uint Play()
        {
            var flags = (uint)AkCallbackType.AK_MusicSyncAll 
                        | (uint)AkCallbackType.AK_EnableGetMusicPlayPosition 
                        | (uint)AkCallbackType.AK_EnableGetSourcePlayPosition
                        | (uint)AkCallbackType.AK_EnableGetSourceStreamBuffering 
                        | (uint)AkCallbackType.AK_CallbackBits;
            
            return AkSoundEngine.PostEvent(EventId, _audioListener, flags, OnCallback, null);
        }
        
        private void OnCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            Debug.Log($"Callback: {in_info.gameObjID}");
        }
        
        private async Task LoadBank(string bankName)
        {
            var result = await AkBankManagerAsync.LoadBankAsync(bankName);
            _banks.Add(result);
            Debug.Log($"Load bank {bankName} with result {result.Result}");
        }

        private bool IsBankLoaded(string bankName) =>
            _banks.Any(bank => bank.Name == bankName);
    }
}