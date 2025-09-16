namespace BasicsFetures;

public readonly struct Point
{
    public double X { get; init; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point Move(double deltaX, double deltaY)
    {
        return new Point(X + deltaX, Y + deltaY);
    }
}


public static class PointConverter
{
    public static Point NewPointAndModifyOriginal(ref Point p)
    {
        p = new Point(p.X*2, p.Y);
        return p;
    }
    
}