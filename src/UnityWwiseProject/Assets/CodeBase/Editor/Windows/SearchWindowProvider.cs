using System;
using System.Linq;
using CodeBase.Editor.Services;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Editor.Windows
{
    public class SearchWindowProvider : IDisposable
    {
        private readonly EditorWindow _parent;
        private readonly AudioEventsProvider _audioEvents;
        
        private StringSearchWindow _searchWindow;

        public SearchWindowProvider(EditorWindow parent, AudioEventsProvider audioEvents)
        {
            _parent = parent;
            _audioEvents = audioEvents;
        }

        public void OpenAudioEventSearch(Vector2 position, Action<string> onSelected = null)
        {
            const int windowWidth = 600;
            var point = _parent.position.position + position + new Vector2(windowWidth / 3f, 0);

            if (_searchWindow == null)
                _searchWindow = ScriptableObject.CreateInstance<StringSearchWindow>();

            var (choices, tooltips) = BuildAudioEventChoices();
            _searchWindow.Configure("Audio Events", choices, tooltips, onSelected);
            SearchWindow.Open(new SearchWindowContext(point, windowWidth), _searchWindow);
        }

        public void Dispose() => 
            Object.Destroy(_searchWindow);

        private (string[] choices, string[] tooltips) BuildAudioEventChoices()
        {
            var events = _audioEvents.AllWwiseEvents().ToList();
            var choices = new string[events.Count];
            var tooltips = new string[events.Count];

            for (var i = 0; i < events.Count; i++)
            {
                choices[i] = events[i].Guid.ToString();
                tooltips[i] = events[i].Name;
            }

            return (choices, tooltips);
        }
    }
}