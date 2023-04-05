using System;

namespace AntsLife
{
    public static class Battle
    {
        public static void Fight(IAttack fir,ICanGoToHeap sec)
        {
            
            if (fir.ModificatorsReturn().Contains(Modificator.IgnoreDef))
            {
                IAttack temp = (IAttack) fir;
                sec.TakeDamage(temp.Attack(),true);
            }
            else
            {
                IAttack temp = (IAttack) fir;
                sec.TakeDamage(temp.Attack(),false);
            }

            if ((sec is IAttack)&&(!fir.ModificatorsReturn().Contains(Modificator.CantBeAttacked)))
            {
                if (sec.ModificatorsReturn().Contains(Modificator.IgnoreDef))
                {
                    IAttack temp = (IAttack) sec;
                    fir.TakeDamage(temp.Attack(), true);
                }
                else
                {
                    IAttack temp = (IAttack) sec;
                    fir.TakeDamage(temp.Attack(), false);
                }
            }

            
        }
        
    }
}