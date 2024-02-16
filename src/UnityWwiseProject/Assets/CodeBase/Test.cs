using UnityEngine;

namespace CodeBase
{
    public class Test : MonoBehaviour
    {
        public AK.Wwise.Event Event;

        private async void Start()
        {
            Debug.Log($"Start...");
            var result = await AkBankManagerAsync.LoadBankAsync("Main");
            Debug.Log($"Bank loaded {result.Result}, {Event.ObjectReference.Guid}");
            Event.Post(gameObject);
        }

    }
}
