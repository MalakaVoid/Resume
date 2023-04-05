using System;
using System.Collections.Generic;

namespace AntsLife
{
    public class Worker:Ant,ICanGoToHeap
    {
        //-------------Значения---------------
        public Resourses TakableResourses;
        public Resourses Backpack;
        public bool TakeResOr_And;
        public Heap _heap;
        public WorkerType _Type;
        public List<Modificator> Modificators;
        //------------------Конструкторы-----------------------

        public Worker(string name, int hp, int def, int damage,Colony _colony,WorkerType _Type,Resourses takableResources,bool takeResOrAnd) : base(name,hp, def, damage,_colony)
        {
            this.TakableResourses = takableResources;
            this.Backpack = new Resourses(0, 0, 0, 0);
            this._Type = _Type;
            this.TakeResOr_And = takeResOrAnd;     //если true то and, если false то or!!!
            _heap = null;
            Modificators = new List<Modificator>();
            DayController.heapGoing+=GoToHeap;
            DayController.DeleteDead_Borning_print+=DeleteDeadBorningPrint;
        }
        public Worker(string name, int hp, int def, int damage,Colony _colony,WorkerType _Type,Resourses takableResources,bool takeResOrAnd,List<Modificator> Modificators) : base(name,hp, def, damage,_colony)
        {
            this.TakableResourses = takableResources;
            this.Backpack = new Resourses(0, 0, 0, 0);
            this._Type = _Type;
            this.TakeResOr_And = takeResOrAnd;     //если true то and, если false то or!!!
            _heap = null;
            this.Modificators = Modificators;
            DayController.heapGoing+=GoToHeap;
            DayController.DeleteDead_Borning_print+=DeleteDeadBorningPrint;
        }
        //-----------------Получение урона-----------------------
        public override void TakeDamage(int dam,bool ignoreDef)
        {
            if (!(this.Modificators.Contains(Modificator.Immortal)))
            {
                IsAlive = false;
            }
            
        }
        //----------------------Назначение кучи-------------------
        public void GoToHeap()
        {
            bool open = true;
            while (open)
            {
                _heap = Heaps.heaps[Globals._random.Next(0, Heaps.heaps.Count)];
                if (_heap.IsOpen)
                {
                    _heap.ComeIn(this);
                    open = false;
                }
            }
        }
        //-----------------Удаление при смерти----------------------
        public void DeleteDeadBorningPrint(int none)
        {
            if (IsAlive == false)
            {
                DayController.heapGoing -= GoToHeap;
                DayController.DeleteDead_Borning_print -= DeleteDeadBorningPrint;
            }
        }
        //-----------------Получение ресурсов--------------------
         public void TryTakeResources()
        {
            if (TakeResOr_And)
            {
                for (int i = 0; i < 4; i++)
                {
                    if ((TakableResourses.amountOfRes[i] > 0)&&(Modificators.Contains(Modificator.FindResAnywhere)))
                    {
                        if (_heap.amountOfRes[i] <= 0)
                        {
                            Backpack.amountOfRes[i] += TakableResourses.amountOfRes[i];
                        }
                        else
                        {
                            _heap.amountOfRes[i] -= TakableResourses.amountOfRes[i];
                            Backpack.amountOfRes[i] += TakableResourses.amountOfRes[i];
                        }
                    }
                    else if ((TakableResourses.amountOfRes[i] > 0)&&(_heap.amountOfRes[i]>0))
                    {
                        _heap.amountOfRes[i] -= TakableResourses.amountOfRes[i];
                        Backpack.amountOfRes[i] += TakableResourses.amountOfRes[i];
                    }
                }
            }
            else
            {
                List<int> amountTypes = new List<int>();
                
                for (int i = 0; i < 4; i++)
                {
                    if ((Modificators.Contains(Modificator.FindResAnywhere))&&(TakableResourses.amountOfRes[i] > 0))
                    {
                        amountTypes.Add(i);
                    }
                    else if ((TakableResourses.amountOfRes[i] > 0)&&(_heap.amountOfRes[i]>0))
                    {
                        amountTypes.Add(i);
                    }
                }

                if (amountTypes.Count != 0)
                {
                    int tmp = Globals._random.Next(0, amountTypes.Count);
                    if (_heap.amountOfRes[amountTypes[tmp]] <= 0)
                    {
                        Backpack.amountOfRes[amountTypes[tmp]] += TakableResourses.amountOfRes[amountTypes[tmp]];
                    }
                    else
                    {
                        _heap.amountOfRes[amountTypes[tmp]] -= TakableResourses.amountOfRes[amountTypes[tmp]];
                        Backpack.amountOfRes[amountTypes[tmp]] += TakableResourses.amountOfRes[amountTypes[tmp]];
                    }
                }

            }
        }
         //----------------------Функции для интерфейсов-------------
        public List<Modificator> ModificatorsReturn()
        {
            return Modificators;
        }
        
        public void AddModificatorDeath(Modificator mods)
        {
            this.Modificators.Add(mods);
        }

        public Colony MyColony()
        {
            return _colony;
        }

        public bool IsDead()
        {
            return IsAlive;
        }
        public void Death()
        {
            IsAlive = false;
        }
        //-------------Вывод информации--------------------
        public void tellAboutYourself()
        {
            switch (_Type)
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
            Console.WriteLine($"---Королева: {_colony.Queen.name}");
        }
    }
}