using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Event = AK.Wwise.Event;

namespace CodeBase.Editor
{
    public class EditorWwisePlayerWindow : EditorWindow
    {
        public AK.Wwise.Event _event = null;

        private const string EventGuid = "63074727-ACD1-4754-A758-15A77B458B62";
        private const uint EventId = 1784581504;

        private static List<AkBankManagerAsync.AsyncLoadingBankResult> _banks = new();

        private static uint _playingId;

        private void CreateGUI()
        {
            DrawInspector();
        }

        private void Update()
        {
            if (_playingId != 0)
            { 
                PrintDuration(_playingId);
            }
        }

        private void PrintDuration(uint playingId)
        {
            var data = AkWwiseProjectInfo.GetData().GetEventInfo(EventId);
            var result = AkSoundEngine.GetSourcePlayPosition(playingId, out var playPosition);
            var playPositionInSeconds = playPosition / 1000f;
            var progress = playPositionInSeconds / data.maxDuration;
            Debug.Log($"{result}: {progress}");
        }

        private void DrawInspector() => 
            rootVisualElement.Add(UserButtons());

        private VisualElement UserButtons()
        {
            var elements = new VisualElement();

            var playButton = new Button(OnButtonClicked) { text = "Play" };
            var stopButton = new Button(OnPauseButtonClicked) { text = "Stop" };
            
            elements.Add(playButton);
            elements.Add(stopButton);

            return elements;
        }

        private void OnPauseButtonClicked()
        {
            AkSoundEngine.StopAll();
        }

        private void OnButtonClicked()
        {
            Play(_event);
        }

        private async void Play(Event sound)
        {
            var akInitializers = Resources.Load<AkInitializer>("WwiseGlobal");
            AkSoundEngineController.Instance.Init(akInitializers);
            AkSoundEngine.RegisterGameObj(akInitializers.gameObject);
            if (!IsBankLoaded("Main")) 
                await LoadBank("Main");

            var flags = (uint)AkCallbackType.AK_MusicSyncAll 
                        | (uint)AkCallbackType.AK_EnableGetMusicPlayPosition 
                        | (uint)AkCallbackType.AK_EnableGetSourcePlayPosition
                        | (uint)AkCallbackType.AK_EnableGetSourceStreamBuffering 
                        | (uint)AkCallbackType.AK_CallbackBits;
            Debug.Log($"{flags}");
            
            _playingId = AkSoundEngine.PostEvent(EventId, akInitializers.gameObject, flags, OnCallback, null);
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