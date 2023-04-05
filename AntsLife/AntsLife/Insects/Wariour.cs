using System;
using System.Collections.Generic;

namespace AntsLife
{
    public class Wariour:Ant,IAttack,ICanGoToHeap
    {
        //------------Значения---------------
        public WariourType _Type;
        public int targets;
        public int bites;
        public Heap _heap;
        public List<Modificator> Modificators;
        public int targetsInHeak;
        //-----------Конструкторы----------------
        public Wariour(string name, int hp, int def, int damage,Colony _colony,WariourType _type, int targets, int bites) : base(name,hp, def, damage,_colony)
        {
            this._Type = _type;
            this.targets = targets;
            this.bites = bites;
            _heap = null;
            Modificators = new List<Modificator>();
            targetsInHeak = targets;
            DayController.heapGoing += GoToHeap;
            DayController.DeleteDead_Borning_print+=DeleteDeadBorningPrint;
        }
        public Wariour(string name, int hp, int def, int damage,Colony _colony,WariourType _type, int targets, int bites,List<Modificator> Modificators) : base(name,hp, def, damage,_colony)
        {
            this.Modificators = Modificators;
            this._Type = _type;
            this.targets = targets;
            this.bites = bites;
            _heap = null;
            targetsInHeak = targets;
            if (Modificators.Contains(Modificator.DoubleHPDEF))
            {
                this.hp *= 2;
                this.def *= 2;
            }
            DayController.heapGoing += GoToHeap;
            DayController.DeleteDead_Borning_print+=DeleteDeadBorningPrint;
        }
        //--------------Получение урона--------------
        public override void TakeDamage(int dam, bool ignoreDef)
        {
            if (ignoreDef)
            {
                hp -= dam;
                if (hp <= 0)
                {
                    IsAlive = false;
                }
            }
            else
            {
                if (dam < def)
                {
                    hp -= 1;
                }
                else if (dam == hp)
                {
                    hp -= dam / 2;
                }
                else
                {
                    hp -= dam - def / 2;
                }

                if (hp <= 0)
                {
                    IsAlive = false;
                    DayController.heapGoing -= GoToHeap;
                }
            }
        }
        //-----------------Назначение куче-----------------
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
        //---------------Удаление муравья если мертв, либо возвращение целей------------
        public void DeleteDeadBorningPrint(int none)
        {
            targetsInHeak = targets;
            if (IsAlive == false)
            {
                DayController.heapGoing -= GoToHeap;
                DayController.DeleteDead_Borning_print -= DeleteDeadBorningPrint;
            }
        }
        //------------------Функции для ИНТЕРФЕЙСОВ-------------------
        public int TargetsOnHeap()
        {
            return targetsInHeak;
        }

        public void Death()
        {
            IsAlive = false;
        }

        public void AddModificatorDeath(Modificator mods)
        {
            this.Modificators.Add(mods);
        }
        public List<Modificator> ModificatorsReturn()
        {
            return Modificators;
        }

        public int Attack()
        {
            targetsInHeak -= 1;
            return (damage*bites);
        }
        public bool IsDead()
        {
            return IsAlive;
        }
        public Colony MyColony()
        {
            return _colony;
        }
        //----------------Вывод информации---------------------
        public void tellAboutYourself()
        {
            switch (_Type)
            {
               case WariourType.USUAL:
               {
                   Console.WriteLine("Тип: Обычный");
                   break;
               }
               case WariourType.ADVANCED:
               {
                   Console.WriteLine("Тип: Продвинутый");
                   break;
               }
               case WariourType.ADVANCED_REVENGEFULL:
               {
                   Console.WriteLine("Тип: Продвинутый мстительный");
                   break;
               }
               case WariourType.OLDER:
               {
                   Console.WriteLine("Тип: Старший");
                   break;
               }
               case WariourType.ELITE:
               {
                   Console.WriteLine("Тип: Элитный");
                   break;
               }
               case WariourType.LEGENDARY_FAT:
               {
                   Console.WriteLine("Тип: Легендарный толстый");
                   break;
               }
            }
            Console.WriteLine($"---Параметры: здоровье={hp}, защита={def}, урон={damage}, может атаковать {targets} цель за раз и наносит {bites} укус");
            Console.Write($"---Модификаторы: ");
            foreach (string printModificator in Globals.print_Modificators(this))
            {
                Console.Write(printModificator+", ");
            }

            Console.WriteLine($"---Королева: {_colony.Queen.name}");
        }
    }
}