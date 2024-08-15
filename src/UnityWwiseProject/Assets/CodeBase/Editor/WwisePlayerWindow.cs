using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Editor
{
    public class WwisePlayerWindow : EditorWindow
    {
        private AudioWwiseService _wwiseSerice = new();
        private uint _playingId;

        private void CreateGUI()
        {
            DrawInspector();
            _wwiseSerice.Initialize();
        }

        private void Update()
        {
            if (_playingId != 0) 
                Debug.Log($"Progress: {_wwiseSerice.GetDuration(_playingId)}");
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

        private void OnPauseButtonClicked() => 
            AkSoundEngine.StopAll();

        private void OnButtonClicked() => 
            _playingId = _wwiseSerice.Play();
    }
}