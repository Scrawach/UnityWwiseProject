using UnityEditor;

namespace CodeBase.Editor
{
    public static class EditorWwisePlayerWindowOpen
    {
        [MenuItem("Wwise/Open Editor Player")]
        public static void OpenWindow() =>
            EditorWindow.GetWindow<EditorWwisePlayerWindow>(nameof(EditorWwisePlayerWindow));
    }
}