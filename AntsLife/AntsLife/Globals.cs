using System;
using System.Collections.Generic;

namespace AntsLife
{
    public static class Globals
    {
        public static Random _random = new Random();
        //
        //   WORKERS CREATION
        //  
        public static Worker Create_Usual_Worker(Colony colony)
        {
            return new Worker("Обычный", 1, 0, 0,colony, WorkerType.USUAL,
                new Resourses(1,0,0,0),true);
        }

        public static Worker Create_Older_Worker(Colony colony)
        {
            return new Worker("Старший", 2, 1, 0,colony, WorkerType.OLDER, new Resourses(1,0,0,0),true);
                
        }

        public static Worker Create_OlderUnique_Worker(Colony colony)
        {
            return new Worker("Старший неповторимый", 2, 1, 0,colony,WorkerType.OLDER_UNIQUE,new Resourses(0,0,1,1),false,new List<Modificator>()
                {
             Modificator.Immortal,
          Modificator.ModIgnoting,Modificator.FindResAnywhere});
        }

        public static Worker Create_Legendary_Worker(Colony colony)
        {
            return new Worker("Легендарный", 10, 6, 0,colony,WorkerType.LEGENDARY,new Resourses(0,1,1,1),true);
        }

        public static Worker Create_LegendaryRunner_Worker(Colony colony)
        {
            return new Worker("Легендарный спринтер", 10, 6, 0,colony,WorkerType.LEGENDARY_RUNNER,new Resourses(0,1,1,1),true,new List<Modificator>()
                {
             Modificator.CantBeAttackedFirst
                });
        }

        //
        //   WARIOUR CREATIONS
        //
        public static Wariour Create_Usual_Wariour(Colony colony)
        {
            return new Wariour("Обычный", 1, 0, 1,colony,WariourType.USUAL, 1, 1);
        }

        public static Wariour Create_Advanced_Wariour(Colony colony)
        {
            return new Wariour("Продвинутый",6,2,4,colony,WariourType.ADVANCED,2,1);
        }

        public static Wariour Create_AdvancedRevengefull_Wariour(Colony colony)
        {
            return new Wariour("Продвинутый мстительный",6,2,4,colony,WariourType.ADVANCED_REVENGEFULL,2,1,new List<Modificator>()
                {
                    Modificator.KillKiller
                });
        }

        public static Wariour Create_Older_Wariour(Colony colony)
        {
            return new Wariour("Старший", 2, 1, 2,colony,WariourType.OLDER ,1, 1);
        }

        public static Wariour Create_Elite_Wariour(Colony colony)
        {
            return new Wariour("Элитный",8,4,3,colony,WariourType.ELITE,2,2);
        }

        public static Wariour Create_LegendaryFat_Wariour(Colony colony)
        {
            return new Wariour("Легендарный толстый",10,6,4,colony,WariourType.LEGENDARY_FAT,3,1,new List<Modificator>()
                {
                    Modificator.Provocation
                ,Modificator.DoubleHPDEF});
        }
        //
        //   SPECIALINSECT CREATION
        //
        public static SpecialInsect Create_SpecialInsec_Orange(Colony colony)
        {
            return new SpecialInsect("Ленивый обычный агрессивный настойчивый - Шмель", 25, 6, 8, colony, 2, 1,
                new List<Modificator>()
                {
                    Modificator.BytesAnyway
                });

        }
        public static SpecialInsect Create_SpecialInsec_Black(Colony colony)
        {
            return new SpecialInsect("Ленивый неуязвимый агрессивный точный - Толстоножка", 25, 6, 8, colony, 2, 3,
                new List<Modificator>()
                {
                    Modificator.CantBeAttacked,
                    Modificator.IgnoreDef,
                    Modificator.CanDamageImmortals
                });
        }
        

        //
        //   QUEENS CREATION
        //
        public static Queen Create_Orange_Queen(Colony colony)
        {
            return new Queen("Шарлотта", 17, 8, 29, colony,
                new int[2] {2, 3}, new int[2] {1, 5},new List<WariourType>() {WariourType.USUAL,WariourType.ADVANCED,WariourType.ADVANCED_REVENGEFULL},
                new List<WorkerType>() {WorkerType.USUAL,WorkerType.OLDER,WorkerType.OLDER_UNIQUE});
        }

        public static Queen Create_Black_Queen(Colony colony)
        {
            return new Queen("Шарлотта", 16, 9, 23, colony,new int[2] {3, 4}, new int[2] {2, 5},
                new List<WariourType>() {WariourType.OLDER,WariourType.ELITE,WariourType.LEGENDARY_FAT},
                new List<WorkerType>() {WorkerType.USUAL,WorkerType.LEGENDARY,WorkerType.LEGENDARY_RUNNER});
        }
        //
        //   HEAPS CREATION
        //

        public static List<Heap> Create_Heaps()
        {
            return new List<Heap>()
            {
                new Heap(10, 17, 31, 45,1),
                new Heap(12, 18, 0, 29,2),
                new Heap(33, 18, 23, 0,3),
                new Heap(29, 0, 0, 15,4),
                new Heap(13, 34, 0, 39,5)
            };
        }
        //
        //    COLONY FIRST CREATION
        //
        public static Colony Create_Orange_Colony()
        {
            Colony colony = new Colony();
            colony.Queen = Create_Orange_Queen(colony);
            //Wariours
            for (int i = 0; i < 8; i++)
            {
                int tmp = _random.Next(0, 3);
                switch (tmp)
                {
                    case 0: colony.units_Wariour.Add(Create_Usual_Wariour(colony));
                        break;
                    case 1:colony.units_Wariour.Add((Create_Advanced_Wariour(colony)));
                        break;
                    case 2: colony.units_Wariour.Add(Create_AdvancedRevengefull_Wariour(colony));
                        break;
                }
            }
            //Workers
            for (int i = 0; i < 14; i++)
            {
                int tmp = _random.Next(0, 3);
                switch (tmp)
                {
                    case 0: colony.units_Worker.Add(Create_Usual_Worker(colony));
                        break;
                    case 1: colony.units_Worker.Add(Create_Older_Worker(colony));
                        break;
                    case 2: colony.units_Worker.Add(Create_OlderUnique_Worker(colony));
                        break;
                }
            }

            colony.ColonyType = ColonyTypes.Orange;
            colony.SpecialInsect.Add(Create_SpecialInsec_Orange(colony));
            colony.name = "РЫЖИЕ";
            return colony;
        }
        public static Colony Create_Black_Colony()
        {
            Colony colony = new Colony();
            colony.Queen = Create_Black_Queen(colony);
            //Wariours
            
            for (int i = 0; i < 5; i++)
            {
                int tmp = _random.Next(0, 3);
                switch (tmp)
                {
                    case 0: colony.units_Wariour.Add(Create_Older_Wariour(colony));
                        break;
                    case 1:colony.units_Wariour.Add(Create_Elite_Wariour(colony));
                        break;
                    case 2: colony.units_Wariour.Add(Create_LegendaryFat_Wariour(colony));
                        break;
                }
            }
            //Workers
            for (int i = 0; i < 15; i++)
            {
                int tmp = _random.Next(0, 3);
                switch (tmp)
                {
                    case 0: colony.units_Worker.Add(Create_Usual_Worker(colony));
                        break;
                    case 1: colony.units_Worker.Add(Create_Legendary_Worker(colony));
                        break;
                    case 2: colony.units_Worker.Add(Create_LegendaryRunner_Worker(colony));
                        break;
                }
            }

            colony.ColonyType = ColonyTypes.Black;
            colony.SpecialInsect.Add(Create_SpecialInsec_Black(colony));
            colony.name = "ЧЕРНЫЕ";
            return colony;
        }
        //
        //  FRIENDLY COLONY CREATIONS
        //
        public static int amountColoniesOrange = 1;
        public static int amountColoniesBlack = 1;
        public static Colony FriendlyColony_Creation(Colony _colony)
        {
            Colony colony = new Colony();
            if (_colony.ColonyType==ColonyTypes.Orange)
            {
                colony.name = $"РЫЖИЕ {amountColoniesOrange}";
                amountColoniesOrange++;
                colony.Queen = Create_Orange_Queen(colony);
                colony.Queen.Queens = new int[2] {0, 0};
                colony.ColonyType = _colony.ColonyType;
                colony.FriendlyColonies.Add(_colony);
            }
            else
            {
                colony.name = $"ЧЕРНЫЕ {amountColoniesBlack}";
                amountColoniesBlack++;
                colony.Queen = Create_Black_Queen(colony);
                colony.Queen.Queens = new int[2] {0, 0};
                colony.ColonyType = _colony.ColonyType;
            }
            _colony.FriendlyColonies.Add(colony);
            return colony;
        }

        public static List<string> print_Modificators(ICanGoToHeap unit)
        {
            List<string> mods = new List<string>();
            foreach (Modificator modificator in unit.ModificatorsReturn())
            {
                switch (modificator)
                {
                    case Modificator.Immortal:
                    {
                        mods.Add("полностью неуязвим для всех атак (даже смертельных для неуязвимых)");
                        break;
                    }
                    case Modificator.ModIgnoting:
                    {
                        mods.Add("игнорирует все модификаторы врагов");
                        break;
                    }
                    case Modificator.FindResAnywhere:
                    {
                        mods.Add("всегда находит нужный ресурс в куче, даже если его больше нет");
                        break;
                    }
                    case Modificator.CantBeAttackedFirst:
                    {
                        mods.Add("не может быть атакован первым");
                        break;
                    }
                    case Modificator.KillKiller:
                    {
                        mods.Add("убивает своего убийцу, даже если он неуязвим");
                        break;
                    }
                    case Modificator.Provocation:
                    {
                        mods.Add("принимает все атаки на себя");
                        break;
                    }
                    case Modificator.DoubleHPDEF:
                    {
                        mods.Add("здоровье и защита увеличены в двое");
                        break;
                    }
                    case Modificator.BytesAnyway:
                    {
                        mods.Add("всегда наносит укус, даже если был убит");
                        break;
                    }
                    case Modificator.CantBeAttacked:
                    {
                        mods.Add("не может быть атакован войнами");
                        break;
                    }
                    case Modificator.IgnoreDef:
                    {
                        mods.Add("игнорирует защиту");
                        break;
                    }
                    case Modificator.CanDamageImmortals:
                    {
                        mods.Add("может наносить урон неуязвимым насекомым");
                        break;
                    }
                }
            }

            return mods;
        }

        public static void print_About_Workers(WorkerType type)
        {
           switch (type)
           {
              case WorkerType.USUAL:
              {
                  Console.WriteLine(
                      $"Тип: Обычный \n"+
                      $"---Параметры: здоровье= 1, защита= 0, может брать 1 ресурс: 'веточка' за раз");
                  break;
              }
              case WorkerType.OLDER:
              {
                  Console.WriteLine(
                      $"Тип: Старший \n"+
                      $"---Параметры: здоровье= 2, защита= 1, может брать 1 ресурс: 'веточка или веточка' за раз");
                  break;
              }
              case WorkerType.OLDER_UNIQUE:
              {
                  Console.WriteLine(
                      $"Тип: Старший неповторимый \n"+
                      $"---Параметры: здоровье= 2, защита= 1, может брать 1 ресурс: 'камушек или росинка' за раз \n" +
                      $"---Модификаторы: полностью неуязвим от всех атак(даже смертельных для неуязвимых, игнорирует все модификаторы врагов, всегда находит нужный ресурс в куче(даже если его там нет) ");
                  break;
              }
              case WorkerType.LEGENDARY:
              {
                  Console.WriteLine(
                      $"Тип: Легендарный \n"+
                      $"---Параметры: здоровье= 10, защита= 6, может брать 3 ресурса: 'листик и камушек и росинка' за раз ");
                  break;
              }
              case WorkerType.LEGENDARY_RUNNER:
              {
                  Console.WriteLine(
                      $"Тип: Легендарный спринтер \n"+
                      $"---Параметры: здоровье= 10, защита= 6, может брать 3 ресурса: 'листик и камушек и росинка' за раз \n" +
                      $"---Модификаторы: не может быть атакован первым ");
                  break;
              }
           }
        }

        public static void print_About_Wariours(WariourType type)
        { 
            switch (type)
            {
               case WariourType.USUAL:
               {
                   Console.WriteLine(
                       $"Тип: Обычный \n"+
                       $"---Параметры: здоровье=1, защита=0, урон=1, может атаковать 1 цель за раз и наносит 1 укус");
                   break;
               }
               case WariourType.ADVANCED:
               {
                   Console.WriteLine(
                       $"Тип: Продвинутый \n"+
                       $"---Параметры: здоровье=6, защита=2, урон=4, может атаковать 2 цели за раз и наносит 1 укус");
                   break;
               }
               case WariourType.ADVANCED_REVENGEFULL:
               {
                   Console.WriteLine(
                       $"Тип: Продвинутый мстительный \n"+
                       $"---Параметры: здоровье=6, защита=2, урон=4, может атаковать 2 цели за раз и наносит 1 укус \n" +
                       $"---Модификаторы: убивает своего убийцу, даже если он неуязвим");
                   break;
               }
               case WariourType.OLDER:
               {
                   Console.WriteLine(
                       $"Тип: Старший \n"+
                       $"---Параметры: здоровье=2, защита=1, урон=2, может атаковать 1 цель за раз и наносит 1 укус");
                   break;
               }
               case WariourType.ELITE:
               {
                   Console.WriteLine(
                       $"Тип: Элитный \n"+
                       $"---Параметры: здоровье=8, защита=4, урон=3, может атаковать 2 цели за раз и наносит 2 укуса");
                   break;
               }
               case WariourType.LEGENDARY_FAT:
               {
                   Console.WriteLine(
                       $"Тип: Легендарный толстый \n"+
                       $"---Параметры: здоровье=10, защита=6, урон=4, может атаковать 3 цели за раз и наносит 1 укус \n" +
                       $"---Модификаторы: принимает все атаки на себя, здоровье и защита увеличены в двое");
                   break;
               }
            }
        }
    }
}