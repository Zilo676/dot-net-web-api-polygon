namespace BasicsFetures;

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