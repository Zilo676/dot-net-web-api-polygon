// // See https://aka.ms/new-console-template for more information
//
// using BasicsFetures;
//
// // Console.WriteLine("Hello, World!");
// //
// // OvverideAndNew test = new Child();
// // Child test2 = new Child();
// //
// // test.MakeSound();
// // test2.MakeSound();
// //
// // test.MakeHaha();
// // test2.MakeHaha();
// //
// // var test11 = new OperatorsOverride(1);
// //
// // int enumber = (int)test11;
// //
// // OperatorsOverride test22222 = enumber;
//
//
//
// class Program
// {
//     public static SemaphoreSlim _semaphore;
//
//     public static void Main()
//     {
//         _semaphore = new SemaphoreSlim(3);
//     }
//     
//     
//     public static async void Main(string[] args)
//     {
//         
//         var tasks = new[]
//         {
//             MakeRequestAsync("A"),
//             MakeRequestAsync("B"),
//             MakeRequestAsync("C"),
//             MakeRequestAsync("D"),
//             MakeRequestAsync("E"),
//             MakeRequestAsync("F"),
//         };
//
//         //var a = await Task.WaitAll(tasks);
//         ;
//     }
//     
//     
//     
//     public static async Task MakeRequestAsync(string id)
//     {
//         var semaphore = new SemaphoreSlim(3);
//         await semaphore.WaitAsync(); // Ждём своей очереди
//         try
//         {
//             Console.WriteLine($"Начало: {id} (поток {Thread.CurrentThread.ManagedThreadId})");
//             await Task.Delay(1000); // имитация работы
//             Console.WriteLine($"Конец: {id}");
//         }
//         finally
//         {
//             semaphore.Release(); // Возвращаем жетон
//         }
//     }
// }
//
//
//
//
//
//
//
// ;