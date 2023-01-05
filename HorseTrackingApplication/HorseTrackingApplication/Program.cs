using System;
using System.Collections.Generic;
using System.Linq;

namespace HorseTrackingApplication
{
    class Program
    {
        private static List<Horse> horseList = new List<Horse>();
        private static List<Inventory> inventoryList = new List<Inventory>();
        public static int horseNo = 0;
        public static int dispensing = 0;
        public static int payoutAmount = 0;

        static void Main(string[] args)
        {
            AddAllInventory();
            AddAllHorse();
            Display();
            UserInput();
        }

        public static void UserInput()
        {
            string input = "";
            while (true)
            {
                try
                {

                    input = Console.ReadLine();

                    if (input.Equals(""))
                    {
                        continue;
                    }
                    else if (input.Equals("q"))
                    {
                        break;
                    }
                    else if (input.Equals("r"))
                    {
                        RestockIngredients();
                    }
                    else
                    {
                        if (input != "" || input != null)
                        {
                            bool startWithNum = char.IsDigit(input[0]);

                            //User input checks
                            if (startWithNum == true)
                            {
                                //User enters decimal input
                                if (input.Contains("."))
                                {
                                    var res = input.Split(" ");
                                    if (res != null)
                                    {
                                        Console.WriteLine("Invalid Bet: " + res[1]);
                                    }
                                }
                                //user enter integer number
                                else
                                {

                                    var res = input.Split(" ");

                                    horseNo = Convert.ToInt32(res[0]);

                                    dispensing = Convert.ToInt32(res[1]);

                                    CalculatePayOut();
                                }
                            }
                            else
                            {
                                var res = input.Split(" ");

                                horseNo = Convert.ToInt32(res[1]);

                                SetWiningHorse(horseNo);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid selection: " + input + "\n");
                }
            }
        }

        public static void Display()
        {
            Console.WriteLine("Inventory:\n");

            foreach (var item in inventoryList)
            {
                Console.WriteLine("$" + item.GetName() + "," + item.GetStock() + "\n");
            }

            Console.WriteLine("Horses:\n");
            int count = 1;

            foreach (var item in horseList)
            {
                Console.WriteLine(item.GetId() + "," + item.GetName() + ", " + item.GetOdds() + ", " + item.GetRes());
                count++;
            }
        }

        public static void RestockIngredients()
        {
            foreach (var i in inventoryList)
            {
                i.SetStock(10);
            }
            horseList = new List<Horse>();
            AddAllHorse();
            Display();
        }

        public static void CalculatePayOut()
        {
            Console.WriteLine("----------------------------------");
            Display();
            var data = horseList.Where(x => x.Id == horseNo).FirstOrDefault();

            if (data != null)
            {
                payoutAmount = dispensing * data.Odds;

                if (data.Res.ToLower() == "won")
                {
                    Console.WriteLine("Payout: " + data.Name + ",$" + payoutAmount);
                    CalculateDispensing();
                }
                else
                {
                    Console.WriteLine("No Payout: " + data.Name);
                    Display();
                }
            }
            else
            {
                Console.WriteLine("Invalid Horse Number: " + horseNo);
            }
        }

        public static void CalculateDispensing()
        {
            int resAmount = payoutAmount;
            int note1 = 0, note5 = 0, note10 = 0, note20 = 0, note100 = 0;

            if (resAmount >= 100)
            {
                note100 = resAmount / 100;
                resAmount -= note100 * 100;

                var res = inventoryList.Where(x => x.Name == 100).FirstOrDefault();

                if (res != null)
                {
                    res.SetStock(res.GetStock() - note100);
                }
            }

            if (resAmount >= 20)
            {
                note20 = resAmount / 20;
                resAmount -= note20 * 20;

                var res = inventoryList.Where(x => x.Name == 20).FirstOrDefault();

                if (res != null)
                {
                    res.SetStock(res.GetStock() - note20);
                }
            }

            if (resAmount >= 10)
            {
                note10 = resAmount / 10;
                resAmount -= note10 * 10;

                var res = inventoryList.Where(x => x.Name == 10).FirstOrDefault();

                if (res != null)
                {
                    res.SetStock(res.GetStock() - note10);
                }
            }
            if (resAmount >= 5)
            {
                note5 = resAmount / 5;
                resAmount -= note5 * 5;

                var res = inventoryList.Where(x => x.Name == 5).FirstOrDefault();
                if (res != null)
                {
                    res.SetStock(res.GetStock() - note5);
                }
            }

            if (resAmount >= 1)
            {
                note1 = resAmount;

                var res = inventoryList.Where(x => x.Name == 1).FirstOrDefault();

                if (res != null)
                {
                    res.SetStock(res.GetStock() - note1);
                }
            }

            Console.WriteLine("Dispensing: ");
            Console.WriteLine("$1," + note1);
            Console.WriteLine("$5," + note5);
            Console.WriteLine("$10," + note10);
            Console.WriteLine("$20," + note20);
            Console.WriteLine("$100," + note100);

            Display();
        }

        public static void SetWiningHorse(int no)
        {
            horseList.ForEach(x =>
            {
                x.Res = "lost";
            });

            var finalHorse = horseList.Where(x => x.Id == no).FirstOrDefault();

            if (finalHorse != null)
            {
                finalHorse.SetRes("won");
            }
            else
            {
                Console.WriteLine("Invalid Horse Number: " + no);
            }
            Display();
        }

        //Master Data For Inventory
        public static void AddAllInventory()
        {
            inventoryList = new List<Inventory>()
            {
                new Inventory{Name=1,Stock=10 },
                new Inventory{Name=5, Stock=10},
                new Inventory{Name=10, Stock=10},
                new Inventory{Name=20, Stock=10},
                new Inventory{Name=100, Stock=10},
            };
        }

        //Master Data For Horse
        public static void AddAllHorse()
        {
            horseList = new List<Horse>()
            {
                new Horse{Id=1,Name="That Darn Gray Cat",Odds=5,Res="won"},
                new Horse{Id=2,Name="Fort Utopia",Odds=10,Res="lost"},
                new Horse{Id=3,Name="Count Sheep",Odds=9,Res="lost"},
                new Horse{Id=4,Name="Ms Traitour",Odds=4,Res="lost"},
                new Horse{Id=5,Name="Real Princess",Odds=3,Res="lost"},
                new Horse{Id=6,Name="Pa Kettle",Odds=5,Res="lost"},
                new Horse{Id=7,Name="Gin Stinger",Odds=6,Res="lost"},
            };
        }

    }
}
