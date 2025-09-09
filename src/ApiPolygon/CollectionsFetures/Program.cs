namespace CollectionsFetures;

class Program
{
    static void Main(string[] args)
    {
        // Array
        var array = new int[10];
        var array2 = new int[10,10,10];
        // Array.Resize(ref array2, 10);
        var jagged = new int[2][];
        
        DateTime now = DateTime.Now;           // Локальное время
        DateTime utcNow = DateTime.UtcNow;     // Время в UTC
        DateTime specific = new DateTime(2024, 5, 15, 10, 30, 0); // 15 мая 2024, 10:30

        var kind  = specific.Kind;
        ;
        
        // Ковариантность и контрвариантность
        Dog dog = new Dog();
        Animal animal = dog;
        
        List<Dog> dogs = new List<Dog>();
        //List<Animal> animals = dogs; - error because invariant
        
        //Covariant
        IEnumerable<Dog> dogs2 = new List<Dog>();
        IEnumerable<Animal> animals = dogs2;
        
        //Contravariant
        IComparer<Animal> comparer = new AnimalComparer();
        IComparer<Dog> dogComparer = comparer;

        Func<Animal, Dog> func1 = a => new Dog();
        //in - можно писать параметры метода
        //out - можно читать возвращение из метода
        Func<Dog, Animal> func2 = func1;
        
        
        // Func<Dog, Animal> func3 = a => new Dog();
        // //in - можно писать параметры метода
        // //out - можно читать возвращение из метода
        // Func<Animal,Dog> func4 = func3;

    }
}