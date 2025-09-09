namespace BasicsFetures;

public class Animal
{
    public string Name { get; set; }

    public virtual void MakeSound()
    {
        Console.WriteLine("Animal.MakeSound()");
    }
}

// Наследник
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Гав!");
    }

    public void Fetch() => Console.WriteLine("Принёс мяч!");
}

// Ещё один наследник
public class Cat : Animal
{
    public override void MakeSound() => Console.WriteLine("Мяу!");
}