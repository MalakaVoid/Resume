using System;
using System.Collections.Generic;

namespace AntsLife
{
    public class Colony
    {
        //----------------Юниты--------------
        public Queen Queen;
        public List<Wariour> units_Wariour;
        public List<Worker> units_Worker;
        public List<SpecialInsect> SpecialInsect;
        //--------------Значения колонии-------------
        public string name;
        public ColonyTypes ColonyType;
        public Resourses ColonyRes;
        public List<Colony> FriendlyColonies;
        //--------------------Конструктор-------------------
        public Colony()
        {
            this.units_Wariour = new List<Wariour>();
            this.units_Worker = new List<Worker>();
            this.SpecialInsect = new List<SpecialInsect>();
            this.ColonyRes = new Resourses(0, 0, 0, 0);
            DayController.screen_1 += print_Screen_1;
            DayController.screen_2 += print_Screen_2;
            DayController.screen_3_HeakStart += Screen_3_HeakStart;
            DayController.DeleteDead_Borning_print += DelDeadPrintAnts;
            DayController.DeleteDead_Borning_print += GetResourses_BornInsects;
            FriendlyColonies = new List<Colony>();
        }
        //-------------------------Удаление мертвых муравьев-------------------
        public void DelDeadPrintAnts(int day)
        {
            int deadWariours = 0;
            int deadWorkers = 0;
            int deadSpecIns = 0;
            for (int i = 0; i < units_Wariour.Count; i++)
            {
                if (!(units_Wariour[i].IsAlive))
                {
                    units_Wariour.RemoveAt(i);
                    deadWariours++;
                }
            }

            for (int i = 0; i < units_Worker.Count; i++)
            {
                if (!(units_Worker[i].IsAlive))
                {
                    units_Worker.RemoveAt(i);
                    deadWorkers++;
                }
            }
            for (int i = 0; i < SpecialInsect.Count; i++)
            {
                if (SpecialInsect[i].IsAlive == false)
                {
                    SpecialInsect.RemoveAt(i);
                    deadSpecIns++;
                }
            }

            Console.WriteLine($"С колонии {name} вернулись: \n" +
                              $"---р={units_Worker.Count}, в={units_Wariour.Count}, о={SpecialInsect.Count} \n" +
                              $"---Потери: р={deadWorkers}, в={deadWariours}, о={deadSpecIns}");
        }
        //----------------------Получение ресурсов от рабочих------------------------
        public void GetResourses_BornInsects(int day)
        {
            int[] gotRes = new int[4] {0, 0, 0, 0};
            for (int i = 0; i < units_Worker.Count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    this.ColonyRes.amountOfRes[k] += units_Worker[i].Backpack.amountOfRes[k];
                    gotRes[k]+=units_Worker[i].Backpack.amountOfRes[k];
                }

                units_Worker[i].Backpack.amountOfRes = new int[4] {0, 0, 0, 0};
            }

            Console.WriteLine($"---Добыто ресурсов: в={gotRes[0]}, л={gotRes[1]}, к={gotRes[2]}, р={gotRes[3]}");
            Queen.BornInsects(day);
        }
        //----------------------Вывод количества всех ресурсов-------------------
        public int WinnerPart()
        {
            int amount = 0;
            foreach (int i in ColonyRes.amountOfRes)
            {
                amount += i;
            }

            return amount;
        }
        //---------------------------ВЫВОД ИНФОРМАЦИИ--------------------------------
        public void print_Screen_1()
        {
            Console.WriteLine($"Колония {this.name}: \n" +
                              $"---Королева \"{Queen.name}\", личинок: {Queen.eggs} \n" +
                              $"---Ресурсы: в={ColonyRes.amountOfRes[0]}, л={ColonyRes.amountOfRes[1]}, к={ColonyRes.amountOfRes[2]}, р={ColonyRes.amountOfRes[3]} \n" +
                              $"---Популяция {units_Wariour.Count+units_Worker.Count+SpecialInsect.Count}: р={units_Wariour.Count} , в={units_Worker.Count} , о={SpecialInsect.Count};");
            Console.WriteLine();
        }

        public void print_Screen_2()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"Колония {name} \n" +
                              $"---Королева {Queen.name}: здоровье={Queen.hp}, защита={Queen.def} , урон={Queen.damage}");
            Console.WriteLine();
            if (units_Worker.Count != 0)
            {
                Console.WriteLine("<<<<<<<<<<Рабочие>>>>>>>>>");
            }

            foreach (WorkerType type in Queen.workersType)
            {
                int count = 0;
                
                foreach (Worker worker in units_Worker) 
                { 
                    if (worker._Type == type)
                    {
                        count++;
                    }
                }
                if (count != 0)
                {
                    Globals.print_About_Workers(type);
                    Console.WriteLine($"---Количество: {count}");
                    Console.WriteLine();
                }
            }

            if (units_Wariour.Count != 0)
            {
                Console.WriteLine("<<<<<<<<<<<Воины>>>>>>>>>>");
            }

            foreach (WariourType type in Queen.warioursType)
            {
                int count = 0;
                foreach (Wariour wariour in units_Wariour)
                {
                    if (wariour._Type == type)
                    {
                        count++;
                    }
                }

                if (count != 0)
                {
                    Globals.print_About_Wariours(type);
                    Console.WriteLine($"---Количество: {count}");
                    Console.WriteLine();
                }
            }

            if (SpecialInsect.Count != 0)
            {
                Console.WriteLine("<<<<<<<<<<<<Особое насекомое>>>>>>>>>");
            }

            foreach (SpecialInsect specialInsect in SpecialInsect)
            {
                specialInsect.tellAboutYou();
            }
            Console.WriteLine("------------------------------------------------");
        }

        public void Screen_3_HeakStart()
        {
            //Workers
            foreach (Heap heap in Heaps.heaps)
            {
                int workersCount = 0;
                foreach (Worker worker in units_Worker)
                {
                    if (worker._heap == heap)
                    {
                        workersCount++;
                    }
                }
                int warioursCount = 0;
                foreach (Wariour wariour in units_Wariour)
                {
                    if (wariour._heap == heap)
                    {
                        warioursCount++;
                    }
                }

                int specInsCount = 0;
                foreach (SpecialInsect specialInsect in SpecialInsect)
                {
                    specInsCount++;
                }
                Console.WriteLine($"С колонии {name} отправились: р={workersCount}, в={warioursCount}, о={specInsCount} на кучу {heap.HeapNum}");
            }
        }
    }

    public enum ColonyTypes
    {
        Orange,
        Black
    }
}