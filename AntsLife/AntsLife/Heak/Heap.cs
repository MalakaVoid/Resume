using System;
using System.Collections.Generic;
using System.Text;

namespace AntsLife
{
    public class Heap: Resourses
    {
        public List<ICanGoToHeap> units;
        public int HeapNum;
        public bool IsOpen;
        public Heap(int branch, int leaf, int stone, int drop,int heapNum) : base(branch, leaf, stone, drop)
        {
            this.HeapNum = heapNum;
            units = new List<ICanGoToHeap>();
            IsOpen = true;
            DayController.startHeapEvent += Hike;
        }
        //----------------------Юниты на куче-------------------
        public void ComeIn(ICanGoToHeap unit)
        {
            units.Add(unit);
        }
        //-----------------------
        public void DeleteHeap()
        {
            if (IsOpen)
            {
                IsOpen = false;
                DayController.startHeapEvent -= Hike;
            }
        }
        //------------------------ПОХОД----------------------------
        public void Hike()
        {
            randomQueue();
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] is Worker)
                {
                    Worker worker = (Worker) units[i];
                    worker.TryTakeResources();
                }
            }
            int amountOfFights = 0;
            for (int i = 0; i < units.Count; i++)
            {
                List<int> IndexOfDeadInsects = new List<int>();
                if ((units[i] is IAttack))
                {
                    IAttack wariour = (IAttack) units[i];
                    bool flagOfNoTarget = false;
                    int targets = wariour.TargetsOnHeap();
                    while (targets > 0)
                    {
                        List<int> PossibleTargetsIndex = ExistenceOfEnemi(units, wariour, amountOfFights);
                        if (PossibleTargetsIndex.Count == 0)
                        {
                            flagOfNoTarget = true;
                            break;
                        }
                        bool flag = true;
                        foreach (int target in PossibleTargetsIndex)
                        {
                            if (units[target].ModificatorsReturn().Contains(Modificator.Provocation))
                            {
                                flag = false;
                                Battle.Fight(wariour, units[target]);
                                amountOfFights += 1;
                                if ((!units[target].IsDead())&&units[target].ModificatorsReturn().Contains(Modificator.KillKiller))
                                {
                                    wariour.Death();
                                }
                                if (!wariour.IsDead())
                                {
                                    if (wariour.ModificatorsReturn().Contains(Modificator.KillKiller))
                                    {
                                        units[target].Death();
                                    }
                                    units[i].AddModificatorDeath(Modificator.CantBeAttacked);
                                    if ((!units[i].ModificatorsReturn().Contains(Modificator.BytesAnyway))||wariour.TargetsOnHeap()==0)
                                    {
                                        IndexOfDeadInsects.Add(i);
                                        targets = 1;
                                    }
                                }
                                if (!units[target].IsDead())
                                {
                                    units[target].AddModificatorDeath(Modificator.CantBeAttacked);
                                    if ((!units[target].ModificatorsReturn().Contains(Modificator.BytesAnyway)))
                                    {
                                        IndexOfDeadInsects.Add(target);
                                    }
                                    else
                                    {
                                        if (((IAttack) units[target]).TargetsOnHeap() == 0)
                                        {
                                            IndexOfDeadInsects.Add(target);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        if (flag)
                        {
                            int targetInd = Globals._random.Next(0,PossibleTargetsIndex.Count);
                            int target = PossibleTargetsIndex[targetInd];
                            Battle.Fight(wariour, units[target]);
                            amountOfFights++;
                            if ((!units[target].IsDead())&&units[target].ModificatorsReturn().Contains(Modificator.KillKiller))
                            {
                                wariour.Death();
                            }
                            if (!wariour.IsDead())
                            {
                                if (wariour.ModificatorsReturn().Contains(Modificator.KillKiller))
                                {
                                    units[target].Death();
                                }
                                units[i].AddModificatorDeath(Modificator.CantBeAttacked);
                                if ((!units[i].ModificatorsReturn().Contains(Modificator.BytesAnyway))||wariour.TargetsOnHeap()==0)
                                {
                                    IndexOfDeadInsects.Add(i);
                                    targets = 1;
                                }
                            }
                            if (!units[target].IsDead())
                            {
                                units[target].AddModificatorDeath(Modificator.CantBeAttacked);
                                if ((!units[target].ModificatorsReturn().Contains(Modificator.BytesAnyway)))
                                {
                                    IndexOfDeadInsects.Add(target);
                                }
                                else
                                {
                                    if (((IAttack) units[target]).TargetsOnHeap() == 0)
                                    {
                                        IndexOfDeadInsects.Add(target);
                                    }
                                }
                            }
                            
                        }
                        targets--;
                    }
                    if (flagOfNoTarget)
                    {
                        continue;
                    }
                }
                IndexOfDeadInsects.Sort();
                for (int ind = IndexOfDeadInsects.Count - 1; ind >= 0; ind--)
                {
                    units.RemoveAt(IndexOfDeadInsects[ind]);
                    if (IndexOfDeadInsects[ind] < i)
                    {
                        i--;
                    }
                }
            }
            units = new List<ICanGoToHeap>();
        }
        //---------------------Возвращение возможных врагов для определенного юнита------------------------
        public List<int> ExistenceOfEnemi(List<ICanGoToHeap> units,ICanGoToHeap wariour,int amountOfFights)
        {
            List<int> Targets = new List<int>();
            for (int i = 0; i < units.Count; i++)
            {
                bool IsAgresive = true;
                foreach (Colony friendlyColony in wariour.MyColony().FriendlyColonies)
                {
                    if (friendlyColony == units[i].MyColony())
                    {
                        IsAgresive = false;
                    }
                }
                if ((units[i].MyColony() != wariour.MyColony()) &&
                    (!units[i].ModificatorsReturn().Contains(Modificator.CantBeAttacked))&&
                    ((!units[i].ModificatorsReturn().Contains(Modificator.CantBeAttackedFirst))||(units[i].ModificatorsReturn().Contains(Modificator.CantBeAttackedFirst)&&amountOfFights!=0)))
                {
                    Targets.Add(i);
                }
            }
            return Targets;
        }

        public void randomQueue()
        {
            for (int i = units.Count - 1; i >= 1; i--)
            {
                int j = Globals._random.Next(i + 1);
                ICanGoToHeap temp = units[j];
                units[j] = units[i];
                units[i] = temp;
            }
        }
    }
}