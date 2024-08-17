using CodeBase.Editor.Services;
using UnityEngine.UIElements;

namespace CodeBase.Editor.Windows
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