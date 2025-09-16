namespace BasicsFetures;

public class OperatorsOverride
{
    public int Num { get; set; }

    public OperatorsOverride()
    {

    }

    public OperatorsOverride(int num)
    {
        Num = num;
    }

    public static OperatorsOverride operator +(OperatorsOverride op1, OperatorsOverride op2)
    {
        return new OperatorsOverride(op1.Num + op2.Num);
    }
    public static implicit operator OperatorsOverride(int num) => new OperatorsOverride(num);
    public static explicit operator int( OperatorsOverride test) => test.Num;

}