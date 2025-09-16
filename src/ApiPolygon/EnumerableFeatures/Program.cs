namespace EnumerableFeatures;

class Program
{

    
    static async Task Main(string[] args)
    {
        // var list = new List<int>();
        //
        // foreach (var item in list)
        // {
        //     Console.WriteLine(item);
        // }
        //
        // foreach (var item in GetData())
        // {
        //     Console.WriteLine(item);
        // }  
        
        await foreach (var item in GetDataAsync())
        {
            Console.WriteLine(item);
        }
        
        
        Console.WriteLine("Hello, World!");
    }


    public static IEnumerable<int> GetData()
    {
        Console.WriteLine($"Start {nameof(GetData)}");
        for (var i = 0; i < 10; i++)
        {
            Console.WriteLine($"return {i}");
            yield return i;
        }

        Console.WriteLine("end");
    }

    public static async IAsyncEnumerable<int> GetDataAsync()
    {
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            yield return i;
        }
    }
}