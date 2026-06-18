namespace atm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter amount");
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Enter valid amount");
            }
            if (num > 0 && num % 100 == 0)
            {
                Dictionary<int, int> noteinatm = new Dictionary<int, int>
                {
                    {500,20 },
                    {200,20 },
                    {100,50 }
                };
                Dictionary<int, int> despensenotes = new Dictionary<int, int>
                {
                    {500,0 },
                    {200,0 },
                    {100,0 }
                };
                int totalamountinatm = 0;
                foreach (KeyValuePair<int, int> k in noteinatm)
                {
                    totalamountinatm += (k.Value * k.Key);
                }
                if (num > totalamountinatm)
                {
                    Console.WriteLine("Insufficient cash");
                    return;
                }
                else
                {
                    int remainingamount = num;
                    {
                        List<int> denominations = new List<int> { 500, 200, 100 };

                        foreach (int denom in denominations)
                        {
                            if (remainingamount >= denom)
                            {
                                int notesNeeded = remainingamount / denom;

                                int notesAvailable = noteinatm[denom];

                                int notesToDispense = Math.Min(notesNeeded, notesAvailable);

                                remainingamount -= (notesToDispense * denom);
                                noteinatm[denom] -= notesToDispense;
                                despensenotes[denom] = notesToDispense;
                            }
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Enter positive amount and multiple of 100");
            }
        }
    }
}
