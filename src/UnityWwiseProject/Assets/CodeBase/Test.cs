using System;
using UnityEngine;

namespace CodeBase
{
    public class Test : MonoBehaviour
    {
        public AK.Wwise.Event Event;

        private void Start()
        {
            Debug.Log($"Start...");
            AkBankManager.LoadBankAsync("Main", OnBankCallback);
        }

        private void OnBankCallback(uint in_bankid, IntPtr in_inmemorybankptr, AKRESULT in_eloadresult, object in_cookie)
        {
            Debug.Log($"bankId: {in_bankid}; result: {in_eloadresult}");
            Event.Post(gameObject);
        }
    }
}
