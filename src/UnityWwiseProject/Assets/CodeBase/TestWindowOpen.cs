using UnityEditor;
using UnityEngine;

namespace CodeBase
{
    public static class TestWindowOpen
    {
        [MenuItem("Test/Open")]
        public static void Open()
        {
            Debug.Log($"OPEN!");
        }
    }
}