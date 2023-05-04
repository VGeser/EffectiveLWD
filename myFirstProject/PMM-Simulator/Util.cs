namespace SimulatorSubsystem;

public static class Util
{
    public static List<List<Int32>> GetEncodings(List<Int32> result, int n, int target, int radix)
    {
        if (n > 0 && target >= 0)
        {
            int d = 0;
            List<List<Int32>> total = new List<List<Int32>>();
            while (d < radix)
            {
                List<Int32> next = new List<Int32>(result) { d };
                List<List<Int32>> newRes = GetEncodings(next, n - 1, target - d, radix);
                total.AddRange(newRes);
                d++;
            }

            return total;
        }

        else if (n == 0 && target == 0)
        {
            List<List<Int32>> r = new List<List<int>> { result };
            return r;
        }
        else
        {
            return new List<List<Int32>>();
        }
    }
}