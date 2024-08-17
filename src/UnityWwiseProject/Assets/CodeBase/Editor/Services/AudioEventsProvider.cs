using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Editor.Services
{
    public class AudioEventsProvider
    {
        public IEnumerable<string> AllEventNames() => 
            AllWwiseEvents().Select(audioEvent => audioEvent.Name);

        public IEnumerable<AkWwiseProjectData.Event> AllWwiseEvents() => 
            AkWwiseProjectInfo.GetData().EventWwu.SelectMany(workUnit => workUnit.List);
    }
}