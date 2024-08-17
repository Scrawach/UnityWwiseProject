using CodeBase.Editor.Windows;
using UnityEditor;

namespace CodeBase.Editor
{
    public static class WwisePlayerWindowOpen
    {
        [MenuItem("Wwise/Open Editor Player")]
        public static void OpenWindow() =>
            EditorWindow.GetWindow<WwisePlayerWindow>(nameof(WwisePlayerWindow));
    }
}