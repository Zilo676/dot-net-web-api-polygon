namespace CollectionsFetures;

public class Animal
{
    public int Age { get; set; }
}

public class Dog : Animal
{
}

class AnimalComparer : IComparer<Animal>
{
    public int Compare(Animal x, Animal y)
    {
        return x.Age > y.Age ? 1 : 0;
    }
}

public interface ITest<out T1, in T2>
{
    public T1 TestMethod(T2 a);
    T2 Field { set; }
}