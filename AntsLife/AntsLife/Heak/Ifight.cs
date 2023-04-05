using System.Collections.Generic;

namespace AntsLife
{
    public interface IAttack:ITakeDamage,ICanGoToHeap
    {
        int Attack();
        int TargetsOnHeap();
    }
    public interface ITakeDamage
    {
        void TakeDamage(int dam,bool ignoreDef);
    }
}