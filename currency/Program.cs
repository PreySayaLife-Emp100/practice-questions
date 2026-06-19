namespace currency
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = {5,5,5,10,20};
            int[] arr2 = { 5, 10, 10, 20 };
            Console.WriteLine("Output for input 1");
            check(arr1);
            Console.WriteLine("Output fot input 2");
            check(arr2);
            void check(int[] arr)
            {
                int five = 0;
                int ten = 0;
                foreach (int a in arr)
                {
                    if (a == 5)
                    {
                        five++;
                    }
                    else if (a == 10)
                    {
                        if (five == 0)
                        {
                            Console.WriteLine("can not provide change");
                            return;
                        }
                        five--;
                        ten++;
                    }
                    else
                    {
                        if (ten > 0 && five > 0)
                        {
                            ten--;
                            five++;
                        }
                        else if (five >= 3)
                        {
                            five -= 3;
                        }
                        else
                        {
                            Console.WriteLine("can not provide change");
                            return;
                        }
                    }
                }
                Console.WriteLine("yes can provide change");
            }
            
        }
    }
}
