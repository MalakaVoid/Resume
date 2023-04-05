using System;
using System.Collections.Generic;

namespace AntsLife
{
    public class SpecialInsect:Ant,ICanGoToHeap,IAttack
    {
        //------------------Значения--------------
        public List<Modificator> Modificators;
        public Heap _heap;
        public int targets;
        public int bites;
        public int targetsInHeak;
        //----------------Конструкторы----------------
        public SpecialInsect(string name, int hp, int def, int damage, Colony _colony,  int targets, int bites,List<Modificator> modificators) : base(name, hp, def, damage, _colony)
        {
            Modificators = modificators;
            _heap = null;
            this.targets = targets;
            this.bites = bites;
            targetsInHeak = targets;
            DayController.heapGoing += GoToHeap;
            DayController.DeleteDead_Borning_print+=DeleteDeadBorningPrint;
        }
        //-------------------Получение урона--------------------
        public override void TakeDamage(int dam,bool ignoreDef)
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
        //---------------------Назначение кучи---------------------
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
        //------------------Удаление мертвых------------------
        public void DeleteDeadBorningPrint(int none)
        {
            targetsInHeak = targets;
            if (IsAlive == false)
            {
                DayController.heapGoing -= GoToHeap;
                DayController.DeleteDead_Borning_print -= DeleteDeadBorningPrint;
            }
        }
        //---------------Функции для ИНТЕРФЕЙСОВ------------------
        public int TargetsOnHeap()
        {
            return targetsInHeak;
        }
        public void Death()
        {
            IsAlive = false;
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
        public void AddModificatorDeath(Modificator mods)
        {
            this.Modificators.Add(mods);
        }
        public Colony MyColony()
        {
            return _colony;
        }
        //-------------------------ВЫВОД ИНФОРМАЦИИ---------------------
        public void tellAboutYou()
        {
            
            Console.Write($"Тип: {name} \n" +
                              $"---Параметры: здоровье=25, защита={def}, урон={damage} наносит {bites} укус(а) и атакует {targets} за раз \n" +
                              $"---Модификаторы: ");
            foreach (string modificator in Globals.print_Modificators(this))
            {
                Console.Write(modificator+", ");
            }

            Console.WriteLine();
        }
    }
}