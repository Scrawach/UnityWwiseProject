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
            root.Add(button);
        }

        private void OnButtonClicked()
        {
            Play(_event);
            //AkWaapiUtilities.TogglePlayEvent(WwiseObjectType.Event, _event.WwiseObjectReference.Guid);
        }

        private async void Play(Event sound)
        {
            var global = Resources.Load<AkInitializer>("WwiseGlobal");
            var instantiate = GameObject.Instantiate(global);
            AkSoundEngineController.Instance.Init(instantiate);
            Debug.Log($"{global}");
            var result = await AkBankManagerAsync.LoadBankAsync("Main");
            Debug.Log($"Result {result.Result}");
            _event.Post(instantiate.gameObject);
            Debug.Log($"POST");
        }
    }
}