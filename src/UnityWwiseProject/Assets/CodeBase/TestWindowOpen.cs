using System;
using UnityEditor;
using UnityEngine;

namespace CodeBase
{
    public static class TestWindowOpen
    {
        [MenuItem("Test/Open")]
        public static void Open()
        {
            const string testSoundGuid = "63074727-acd1-4754-a758-15a77b458b62";
            Debug.Log($"OPEN!");
            AkWaapiUtilities.TogglePlayEvent(WwiseObjectType.Event, Guid.Parse(testSoundGuid));
        }
    }
}