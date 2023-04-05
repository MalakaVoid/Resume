using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AntsLife
{
    public class Queen:Ant
    {
        public int[] Borning;
        public int[] Queens;
        public List<WariourType> warioursType;
        public List<WorkerType> workersType;
        public int DayOfEggBirth;
        public int eggs;
        public int BorningTime;
        public int BornedQueens;
        public Queen(string name, int hp, int def, int damage,Colony _colony, int[] borning, int[] queens,List<WariourType> warioursType,List<WorkerType> workersType) : base(name,hp, def, damage,_colony)
        {
            this.warioursType = warioursType;
            this.workersType = workersType;
            Borning = borning;
            Queens = queens;
            eggs = 0;
            BornedQueens = 0;
        }
        //-------------------Рождение муравьев------------------
        public void BornInsects(int Day)
        {
            if (eggs == 0)
            {
                eggs = Globals._random.Next(2, 10);
                DayOfEggBirth = Day;
                BorningTime = Globals._random.Next(Borning[0], Borning[1]+1);
                Console.WriteLine($"---Новые личинки: {eggs} (еще {BorningTime-Day+DayOfEggBirth} дней)");
            }
            else if (Day == (DayOfEggBirth + BorningTime))
            {
                int newWorkers = 0;
                int newWariours = 0;
                int newQueen = 0;
                int CanBornQueens = 3;
                while (eggs != 0)
                {
                    if (BornedQueens >= Queens[1])
                    {
                        CanBornQueens = 2;
                    }
                    switch (Globals._random.Next(0, CanBornQueens))
                    {
                        case 0:
                        {
                            WariourType tmp = warioursType[Globals._random.Next(0, warioursType.Count)];
                            switch (tmp)
                            {
                                case WariourType.USUAL: _colony.units_Wariour.Add(Globals.Create_Usual_Wariour(_colony));
                                    break;
                                case WariourType.ELITE: _colony.units_Wariour.Add(Globals.Create_Elite_Wariour(_colony));
                                    break;
                                case WariourType.OLDER: _colony.units_Wariour.Add(Globals.Create_Older_Wariour(_colony));
                                    break;
                                case WariourType.ADVANCED: _colony.units_Wariour.Add(Globals.Create_Advanced_Wariour(_colony));
                                    break;
                                case WariourType.LEGENDARY_FAT: _colony.units_Wariour.Add(Globals.Create_LegendaryFat_Wariour(_colony));
                                    break;
                                case WariourType.ADVANCED_REVENGEFULL: _colony.units_Wariour.Add(Globals.Create_AdvancedRevengefull_Wariour(_colony));
                                    break;
                            }

                            newWariours++;
                            break;
                        }
                        case 1:
                        {
                            WorkerType tmp = workersType[Globals._random.Next(0, workersType.Count)];
                            switch (tmp)
                            {
                                case WorkerType.OLDER: _colony.units_Worker.Add(Globals.Create_Older_Worker(_colony));
                                    break;
                                case WorkerType.USUAL: _colony.units_Worker.Add(Globals.Create_Usual_Worker(_colony));
                                    break;
                                case WorkerType.LEGENDARY: _colony.units_Worker.Add(Globals.Create_Legendary_Worker(_colony));
                                    break;
                                case WorkerType.OLDER_UNIQUE: _colony.units_Worker.Add(Globals.Create_OlderUnique_Worker(_colony));
                                    break;
                                case WorkerType.LEGENDARY_RUNNER: _colony.units_Worker.Add(Globals.Create_LegendaryRunner_Worker(_colony));
                                    break;
                            }
                            newWorkers++;
                            break;
                        }
                        case 2:
                        {
                            BornedQueens++;
                            int tmp = Globals._random.Next(0, 2);
                            if ((_colony.FriendlyColonies.Count < Queens[1])&&(tmp==0))
                            {
                                Globals.FriendlyColony_Creation(_colony);
                            }
                            newQueen++;
                            break;
                        }
                    }
                    eggs--;
                }
                eggs = Globals._random.Next(2, 10);
                DayOfEggBirth = Day;
                BorningTime = Globals._random.Next(Borning[0], Borning[1]+1);
                Console.WriteLine($"---Выросли: р={newWorkers}, в={newWariours}, к={newQueen} \n" +
                                  $"---Новые личинки: {eggs} (еще {BorningTime-Day+DayOfEggBirth} дней)");
            }
            else
            {
                Console.WriteLine($"---Личинки еще растут({BorningTime-Day+DayOfEggBirth})");
            }
        }
    }
}