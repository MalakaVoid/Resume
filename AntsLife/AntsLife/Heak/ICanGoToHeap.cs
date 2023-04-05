using System.Collections.Generic;

namespace AntsLife
{
    public interface ICanGoToHeap:ITakeDamage
    {
        Colony MyColony();
        bool IsDead();
        List<Modificator> ModificatorsReturn();
        void AddModificatorDeath(Modificator mods);
        void Death();
    }
}