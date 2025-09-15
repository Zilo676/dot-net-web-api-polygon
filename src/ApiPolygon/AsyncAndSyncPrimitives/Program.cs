using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AsyncAndSyncPrimitives;

class Program
{
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);
    private static int _counter = 0;

    public static void Process(int id)
    {
        // semaphoreSlim.Wait();
        // try
        // {
        //     Console.WriteLine($"SemaphoreSlim {id} {semaphoreSlim.CurrentCount}");
        //     Interlocked.Increment(ref _counter);
        //     Thread.Sleep(2000);
        // }
        // finally
        // {
        //     semaphoreSlim.Release();
        //     Console.WriteLine($"{id}:{semaphoreSlim.CurrentCount}");
        // }
    }

    static async Task Main(string[] args)
    {
        try
        {
            await Work();
            Console.WriteLine("test");
        }
        catch (Exception ex)
        {
            Console.WriteLine("catch");
        }

        ;
        // using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        // var result = await Work();
        // try
        // {
        //     await Task.Delay(1000, cts.Token);
        // }
        // Eat().GetAwaiter().GetResult();
        // var result = await new CustomAwaitable();
        // Parallel.For(0, 10, Process);
        // Console.WriteLine($"counter={_counter}");

        ;
        using var semaphore = new Semaphore(3, 3, "semaphore");

        if (semaphore.WaitOne(TimeSpan.FromSeconds(5)))
        {
            try
            {
            }
            finally
            {
                semaphore.Release();
            }
        }

        ;
    }

    public static async Task<string> Work()
    {
        await Task.Delay(2000);
        Console.WriteLine("result");
        throw new Exception("exception");
        return "result";
        
    }


    public class CustomAwaitable
    {
        public CustomAwaiter GetAwaiter() => new CustomAwaiter();
    }

    public struct CustomAwaiter : INotifyCompletion
    {
        public bool IsCompleted => false;

        public void OnCompleted(Action continuation)
        {
            ThreadPool.QueueUserWorkItem(_ => continuation());
        }

        public int GetResult() => 42;
    }
}