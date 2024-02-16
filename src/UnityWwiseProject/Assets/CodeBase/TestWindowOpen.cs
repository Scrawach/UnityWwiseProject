using UnityEditor;

namespace CodeBase
{
    public static class TestWindowOpen
    {
        [MenuItem("Test/Open")]
        public static void OpenWindow() =>
            EditorWindow.GetWindow<TestWindow>(nameof(TestWindow));
    }
}