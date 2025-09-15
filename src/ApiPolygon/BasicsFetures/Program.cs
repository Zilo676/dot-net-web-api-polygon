// // See https://aka.ms/new-console-template for more information
//
using BasicsFetures;
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


class Program
{

    class A
    {
    }
    class B : A {}

    static void Print<T>(T t)
    {
        Console.WriteLine(typeof(T).Name);
        Console.WriteLine(t.GetType().Name);
    }
    // public static SemaphoreSlim _semaphore;

    // public static void Main()
    // {
    //     _semaphore = new SemaphoreSlim(3);
    // }


    public static void Main(string[] args)
    {
        
        // Animal dog = new Dog();
        // Animal cat = new Cat();
        // Animal animal = new Animal();
        //
        // dog.MakeSound(); // Dog.MakeSound()
        // cat.MakeSound(); // Animal.MakeSound()
        // animal.MakeSound(); // Animal.MakeSound()
        //
        // dog.Eat(); // Animal.Eat()
        // cat.Eat(); // Cat.MakeSound()
        // animal.Eat(); // Animal.Eat()
        //
        // Console.WriteLine("test2");
        // var dog2 = new Dog();
        // var cat2 = new Cat();
        // (dog2 as Animal).MakeSound(); // Dog.MakeSound()
        // (cat2 as Animal).MakeSound(); // Animal.MakeSound()
        //
        // (dog2 as Animal).Eat(); // Animal.Eat()
        // (cat2 as Animal).Eat(); // Animal.Eat()
        // cat2.Eat();

        // string a = "test";
        // string b = "test";
        //
        // Console.WriteLine(a==b);
        // Console.WriteLine(ReferenceEquals(a,b));
        // Console.WriteLine("###");
        // string c = "te" + "st1";
        // Console.WriteLine(a==c);
        // Console.WriteLine(Equals(a,c));
        // Console.WriteLine(ReferenceEquals(a,c));
        //
        // string d = "te" + "st1";
        //
        // Console.WriteLine(c==d);
        // Console.WriteLine(Equals(d,c));
        // Console.WriteLine(ReferenceEquals(d,c));
        //
        //
        // Console.WriteLine("############");
        // var s1 = "text1";
        // var s2 = s1;
        // s2+= "123";
        // ChangeString(s1);
        // Console.WriteLine(s1==s2);
        // Console.WriteLine(ReferenceEquals(s1,s2));
        
        A obj = new B();
        Print(obj);
        Print(new A[] { new B() });
        ;
        
        
        
        // var tasks = new[]
        // {
        //     MakeRequestAsync("A"),
        //     MakeRequestAsync("B"),
        //     MakeRequestAsync("C"),
        //     MakeRequestAsync("D"),
        //     MakeRequestAsync("E"),
        //     MakeRequestAsync("F"),
        // };

        //var a = await Task.WaitAll(tasks);
        ;
    }

    public static void ChangeString(string s)
    {
        s += "123";
    }
}


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