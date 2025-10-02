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
        
        Animal animal = new Animal();

        Cat cat = (Cat)new Animal();
        Animal cat2 = new Cat();
        cat.MakeSound();
        // var a = cat as Animal;
        // a.MakeSound();

        var set = new HashSet<string>();
        set.Add("123");
        set.Contains("123");
        set.Remove("123");
        ;
        
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
        int[] nums = [5, 2, 3, 1];
        int left = 0;
        int right = nums.Length;
        MergeSort(nums, left, right);
        ;

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

    public static void QuickSort(int[] arr, int low, int h)
    {
        if (low < h)
        {
            int pivot = Partition(arr, low, h);
            QuickSort(arr, low, pivot - 1);
            QuickSort(arr, pivot + 1, h);
        }
    }

    public static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        //сдвиг все что больше пивота в право
        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        //ставим пивот на свое место
        Swap(arr, i + 1, high);
        return i + 1;
    }

    private static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    public static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    public static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        var L = new int[n1];
        var R = new int [n2];

        Array.Copy(arr, left, L, 0, n1);
        Array.Copy(arr, mid, R, 0, n2);

        int i = 0;
        int j = 0;
        int k = left;
        while (i < n1 && j < n2)
        {
            arr[k++] = L[i] <= R[j] ? L[i++] : R[j++];
        }

        while (i < n1) arr[k++] = L[i++];
        while (j < n2) arr[k++] = R[j++];
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
    
    
    public class Animal
    {
        public string Name { get; set; }

        public virtual void MakeSound()
        {
            Console.WriteLine("Animal.MakeSound()");
        }

        public void Eat() => Console.WriteLine("Animal.Eat()");
    }

// Наследник
    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Dog.MakeSound()");
        }

        public void Fetch() => Console.WriteLine("Принёс мяч!");
    }

// Ещё один наследник
    public class Cat : Animal
    {
        public new void MakeSound() => Console.WriteLine("Cat.MakeSound()");
        public void Eat() => Console.WriteLine("Cat.Eat()");
    }
}