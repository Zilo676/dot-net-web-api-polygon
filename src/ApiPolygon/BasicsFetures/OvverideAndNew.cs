namespace BasicsFetures;

public class OvverideAndNew
{
    public virtual void MakeSound()
    {
        Console.WriteLine("BONK!");
    }

    public virtual void MakeHaha()
    {
        Console.WriteLine("Haha");
    }
    
}

public class Child : OvverideAndNew
{
    public new void MakeSound()
    {
        Console.WriteLine("NEW BONK!");
        
    }

    public override void MakeHaha()
    {
        Console.WriteLine("HAHA with NEW BONK!");
    }
}


