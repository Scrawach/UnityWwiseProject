using System;
using System.Threading.Tasks;

namespace CodeBase
{
    public static class AkBankManagerAsync
    {
        public static Task<AsyncLoadingBankResult> LoadBankAsync(string bank) =>
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