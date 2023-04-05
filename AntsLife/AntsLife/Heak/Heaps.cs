using System.Collections.Generic;

namespace AntsLife
{
    public static class Heaps
    {
        public static List<Heap> heaps=Globals.Create_Heaps();
        public static void HeapDepletion()
        {
            for (int i = 0; i < heaps.Count;i++)
            {
                if ((heaps[i].amountOfRes[0] == 0) && (heaps[i].amountOfRes[1] == 0) &&
                    (heaps[i].amountOfRes[2] == 0) && (heaps[i].amountOfRes[3] == 0))
                {
                    heaps[i].DeleteHeap();
                }
            }
        }
        
    }
}