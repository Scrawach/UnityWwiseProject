using CodeBase.Editor.Controls;
using UnityEngine.UIElements;

namespace CodeBase.Editor
{
    public class WwisePlayerRoot : VisualElement
    {
        private readonly WwisePlayerWindow _parent;

        public WwisePlayerRoot(WwisePlayerWindow parent)
        {
            _parent = parent;
            var eventsProvider = new AudioEventsProvider();
            var searchWindowProvider = new SearchWindowProvider(parent, eventsProvider);
        }
    }
}