namespace AoC23;

public static class Mathx
{
    public static long GetLCMOfList(List<int> elements)
    {
        var lcm = 1L;
        var divisor = 2;
         
        while (true)
        {
            var counter = 0;
            var divisible = false;
            for (var i = 0; i < elements.Count; i++)
            {
                switch (elements[i])
                {
                    case 0:
                        return 0;
                    case < 0:
                        elements[i] *= -1;
                        break;
                }
                
                if (elements[i] == 1)
                {
                    counter++;
                }

                if (elements[i] % divisor != 0)
                {
                    continue;
                }
                
                divisible = true;
                elements[i] /= divisor;
            }
 
            if (divisible)
            {
                lcm *= divisor;
            }
            else
            {
                divisor++;
            }
 
            if (counter == elements.Count)
            {
                return lcm;
            }
        }
    }
}