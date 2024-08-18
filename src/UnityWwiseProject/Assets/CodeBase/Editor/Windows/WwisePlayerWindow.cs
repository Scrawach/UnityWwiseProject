using CodeBase.Editor.Services;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Editor.Windows
{
    public class WwisePlayerWindow : EditorWindow
    {
        private WwisePlayerRoot _root;
        
        private void CreateGUI()
        {
            _root = new WwisePlayerRoot(this);
            _root.Initialize();
        }

        private void Update() => 
            _root.Update();
    }
}