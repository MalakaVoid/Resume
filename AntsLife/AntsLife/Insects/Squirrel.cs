using System.Diagnostics;

namespace AntsLife
{
    public class Squirrel
    {
        public int Duration;
        public int DayOfAttack;
        public bool attacking;

        public Squirrel()
        {
            Duration = 7;
            DayOfAttack = 0;
            attacking = false;
        }

        public void SquirrelAttacky(int Day)
        {
            if (!attacking)
            {
                int tmp = Globals._random.Next(0, 3);
                if (tmp == 0)
                {
                    attacking = true;
                    DayOfAttack = Day;
                }
            }
            if (DayOfAttack + Duration == Day && attacking)
            {
                attacking = false;
            }
            if (attacking)
            {
                bool open = true;
                while (open)
                {
                    int tmp = Globals._random.Next(0, Heaps.heaps.Count);
                    if (Heaps.heaps[tmp].IsOpen)
                    {
                        Heaps.heaps[tmp].amountOfRes[0] -= (Heaps.heaps[tmp].amountOfRes[0]) / 2;
                        open = false;
                    }
                }
            }
        }
    }
}