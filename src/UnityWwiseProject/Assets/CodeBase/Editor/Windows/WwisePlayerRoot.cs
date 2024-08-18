using CodeBase.Editor.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Editor.Windows
{
    public class WwisePlayerRoot : VisualElement
    {
        private readonly WwisePlayerWindow _parent;
        private readonly AudioWwiseService _wwiseService;

        private uint _playingId;

        public WwisePlayerRoot(WwisePlayerWindow parent)
        {
            _parent = parent;
            var eventsProvider = new AudioEventsProvider();
            var searchWindowProvider = new SearchWindowProvider(parent, eventsProvider);
            _wwiseService = new AudioWwiseService();
            _parent.rootVisualElement.Add(CreateButtons());
        }

        public void Initialize()
        {
            _wwiseService.Initialize();
        }

        public void Update()
        {
            Debug.Log($"Progress: {_wwiseService.GetDuration(_playingId)}");
        }

        private VisualElement CreateButtons()
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
            _playingId = _wwiseService.Play();
    }
}