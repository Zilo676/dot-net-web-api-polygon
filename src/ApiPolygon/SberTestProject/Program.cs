namespace SberTestProject;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(PathHelper.GetPathCount(5,5));
        Console.WriteLine(PathHelper.GetPathCount(10,1));
        Console.WriteLine(PathHelper.GetPathCount(1,10));
        Console.WriteLine(PathHelper.GetPathCount(4,6));
        Console.WriteLine(PathHelper.GetPathCount(6,4));
        ;

    }
}

public static class PathHelper
{
    public static int GetPathCount(int m, int n)

    {
        if (n <= 0 || m <= 0) return 0;

        int[] dp = new int[m];
        Array.Fill(dp, 1);
        
        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < m; j++)
            {
                dp[j] += dp[j - 1];
            }
        }

        return dp[m - 1];
    }
}