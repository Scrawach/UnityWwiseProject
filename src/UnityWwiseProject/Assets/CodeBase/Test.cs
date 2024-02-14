using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase
{
    public class Test : MonoBehaviour
    {
        public AK.Wwise.Event Event;

        private async void Start()
        {
            Debug.Log($"Start...");
            var result = await LoadBankAsync("Main");
            Debug.Log($"Bank loaded {result.Result}");
            Event.Post(gameObject);
        }

        private static Task<BankResult> LoadBankAsync(string bank) =>
            Task.Run(() =>
            {
                var task = new TaskCompletionSource<BankResult>();
                
                AkBankManager.LoadBankAsync(bank, (bankId, pointer, result, cookie) => 
                    task.TrySetResult(new BankResult(bankId, pointer, result, cookie)));
                
                return task.Task;
            });

        public class BankResult
        {
            public uint BankId;
            public IntPtr InMemoryBankPtr;
            public AKRESULT Result;
            public object Cookie;

            public BankResult(uint bankId, IntPtr inMemoryBankPtr, AKRESULT result, object cookie)
            {
                BankId = bankId;
                InMemoryBankPtr = inMemoryBankPtr;
                Result = result;
                Cookie = cookie;
            }
        }
    }
}
