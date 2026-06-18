namespace atm
{
    internal class Program
    {
        static void Main(string[] args)
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
            
            bool withdraw = true;
            while (withdraw)
            {
                Console.WriteLine("Enter a for withdraw and b exit");
                switch (Console.ReadLine())
                {
                    case "a":
                        withdrawfromatm();
                        break;
                    case "b":
                        withdraw=false;
                        break;
                    default:
                        Console.WriteLine("Enter valid char");
                        break;
                }  
            }
            void withdrawfromatm()
            {
                Console.WriteLine("Enter withdraw amount");
                int num;
                while (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("Enter valid amount");
                }
                if (num > 0 && num % 100 == 0)
                {
                    
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
                        if (remainingamount == 0)
                        {
                            Console.WriteLine("Success");
                            foreach (KeyValuePair<int, int> k in despensenotes)
                            {
                                Console.WriteLine($"{k.Key} : {k.Value}");
                            }
                            Console.WriteLine("Remainig notes in atm");
                            foreach (KeyValuePair<int, int> k in noteinatm)
                            {
                                Console.WriteLine($"{k.Key} : {k.Value}");
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
}
