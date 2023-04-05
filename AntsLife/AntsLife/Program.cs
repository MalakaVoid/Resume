using System;
using System.Collections.Generic;
using System.Threading;

namespace AntsLife
{
    class Program
    {
        static void Main(string[] args)
        {
            //Начальные значения
            int Days = 1;
            List<Colony> colonies=new List<Colony>()
            {
                Globals.Create_Orange_Colony(),
                Globals.Create_Black_Colony()
            };
            Squirrel squirrel = new Squirrel();
            //основная часть игры
            while (Days != 14)
            {
                squirrel.SquirrelAttacky(Days);
                Screen_1(Days,squirrel);
                Screen_2();
                DayController.GoToHeapStart();  //распределение по кучам
                Screen_3_HeakStart();
                DayController.HeapEvent();
                DayController.DeletingAnts_BornIns_printScreen3(Days);
                Heaps.HeapDepletion();
                Days++;
                Console.WriteLine("\n\nНажмите ВВОД для перехода на следующий день...\n\n");
                Console.ReadKey();
            }

            Console.WriteLine("-----------НАСТУПИЛА ЗАСУХА------");
            Colony Winner = WinnerColony(colonies);
            Console.WriteLine($"Колония {Winner.name} выживает после засухи с {Winner.WinnerPart()} ресурсами!!!");
            Console.WriteLine("THE END!!!!");
            Console.ReadLine();
        }
        public static void Screen_1(int day,Squirrel squirrel)
        {
            Console.WriteLine("----------------Экран 1 - Начало хода----------------");
            Console.WriteLine($"День {day} (до засухи осталось {14-day} дней)");
            DayController.Screen_1();
            foreach (Heap heap in Heaps.heaps)
            {
                if (heap.IsOpen)
                {
                    Console.WriteLine(
                        $"Куча {heap.HeapNum}: в={heap.amountOfRes[0]} , л={heap.amountOfRes[1]} , к={heap.amountOfRes[2]} , р={heap.amountOfRes[3]}");
                }
                else
                {
                    Console.WriteLine(
                        $"Куча {heap.HeapNum}: истощена");
                }
            }
            if (squirrel.attacking)
            {
                Console.WriteLine($"Глобальный эффект: <Белка> ворует половину веточек со случайной кучи(в течении {squirrel.DayOfAttack+squirrel.Duration-day} дней)");
            }
        }

        public static void Screen_2()
        {
            Console.WriteLine("---------------Экран 2 - Информация по колонии----------------");
            DayController.Screen_2();
        }
        public static void Screen_3_HeakStart()
        {
            Console.WriteLine("----------------Экран 3 - Поход---------------");
            Console.WriteLine("Начало дня: ");
            DayController.Screen_3_Start();
        }
        public static Colony WinnerColony(List<Colony> colonies)
        {
            foreach (Colony friendlyColony in colonies[0].FriendlyColonies)
            {
                colonies.Add(friendlyColony);
            }
            foreach (Colony friendlyColony in colonies[1].FriendlyColonies)
            {
                colonies.Add(friendlyColony);
            }
            int tmp = 0;
            Colony winner = new Colony();
            foreach (Colony colony in colonies)
            {
                if (colony.WinnerPart() > tmp)
                {
                    winner = colony;
                    tmp = colony.WinnerPart();
                }
            }
            return winner;
        }
    }
}