using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Event = AK.Wwise.Event;

namespace CodeBase
{
    public class TestWindow : EditorWindow
    {
        public AK.Wwise.Event _event = null;

        private static List<AkBankManagerAsync.AsyncLoadingBankResult> _banks = new();

        private void CreateGUI()
        {
            DrawInspector();
        }

        private void DrawInspector()
        {
            var root = rootVisualElement;
            var temp = new SerializedObject(this);
            var property = temp.FindProperty("_event");
            var propertyField = new PropertyField(property);
            propertyField.Bind(temp);
            root.Add(propertyField);

            var button = new Button(OnButtonClicked)
            {
                text = "Play"
            };
            
            var pauseButton = new Button(OnPauseButtonClicked)
            {
                text = "Play"
            };
            root.Add(button);
            root.Add(pauseButton);
        }

        private void OnPauseButtonClicked()
        {
            AkSoundEngine.StopAll();
        }

        private void OnButtonClicked()
        {
            Play(_event);
            //AkWaapiUtilities.TogglePlayEvent(WwiseObjectType.Event, _event.WwiseObjectReference.Guid);
        }

        private async void Play(Event sound)
        {
            //var akInitializers = FindObjectsByType<AkInitializer>(FindObjectsSortMode.None).First();
            var akInitializers = Resources.Load<AkInitializer>("WwiseGlobal");
            AkSoundEngineController.Instance.Init(akInitializers);
            AkSoundEngine.StopAll(akInitializers.gameObject);

            if (!IsBankLoaded("Main")) 
                await LoadBank("Main");

            _event.Post(akInitializers.gameObject);
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