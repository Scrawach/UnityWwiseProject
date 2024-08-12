using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeBase.Editor.Controls
{
    public class StringSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private string _title;
        private string[] _choices;
        private string[] _descriptions;
        private Action<string> _onSelected;

        public void Configure(string title, string[] choices, string[] descriptions, Action<string> onSelected = null)
        {
            _title = title;
            _choices = choices;
            _descriptions = descriptions;
            _onSelected = onSelected;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry> { new SearchTreeGroupEntry(new GUIContent(_title)) };
            
            for (var i = 0; i < _choices.Length; i++)
            {
                var content = new GUIContent($"[{_choices[i]}] {_descriptions[i]}");
                tree.Add(new SearchTreeEntry(content) { level = 1, userData = _choices[i] });
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            if (entry.userData is not string choice)
                return false;
            
            _onSelected?.Invoke(choice);
            return true;
        }
    }
}