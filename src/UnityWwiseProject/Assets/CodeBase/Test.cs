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
            Debug.Log($"Bank loaded {result.Result}, {Event.ObjectReference.Guid}");
            Event.Post(gameObject);
        }

        private static Task<AsyncLoadingBankResult> LoadBankAsync(string bank) =>
            Task.Run(() =>
            {
                var task = new TaskCompletionSource<AsyncLoadingBankResult>();
                
                AkBankManager.LoadBankAsync(bank, (bankId, pointer, result, cookie) => 
                    task.TrySetResult(new AsyncLoadingBankResult(bankId, pointer, result, cookie)));
                
                return task.Task;
            });

        public class AsyncLoadingBankResult
        {
            public uint BankId;
            public IntPtr InMemoryBankPtr;
            public AKRESULT Result;
            public object Cookie;

            public AsyncLoadingBankResult(uint bankId, IntPtr inMemoryBankPtr, AKRESULT result, object cookie)
            {
                BankId = bankId;
                InMemoryBankPtr = inMemoryBankPtr;
                Result = result;
                Cookie = cookie;
            }
        }
    }
}
