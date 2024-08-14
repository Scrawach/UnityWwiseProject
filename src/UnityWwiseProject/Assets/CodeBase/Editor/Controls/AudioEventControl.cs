using System;
using UnityEngine.UIElements;

namespace CodeBase.Editor.Controls
{
    public class AudioEventControl : VisualElement
    {
        private readonly Button _selectEventButton;
        private readonly Button _playButton;
        private readonly Button _stopButton;
        private readonly Slider _progressSlider;

        public AudioEventControl()
        {
            _selectEventButton = this.Q<Button>("select-button");
            _playButton = this.Q<Button>("play-button");
            _stopButton = this.Q<Button>("stop-button");
            _progressSlider = this.Q<Slider>("progress-slider");

            _progressSlider.RegisterValueChangedCallback(OnProgressValueChanged);
        }
        
        public event Action PlayClicked
        {
            add => _playButton.clicked += value;
            remove => _playButton.clicked -= value;
        }
        
        public event Action StopClicked
        {
            add => _stopButton.clicked += value;
            remove => _playButton.clicked -= value;
        }
        
        public event Action SelectEventClicked
        {
            add => _selectEventButton.clicked += value;
            remove => _selectEventButton.clicked -= value;
        }

        public event Action<ChangeEvent<float>> ProgressChanged;
        
        private void OnProgressValueChanged(ChangeEvent<float> evt)
        {
            ProgressChanged?.Invoke(evt);
        }
    }
}