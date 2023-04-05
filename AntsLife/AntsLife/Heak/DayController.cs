
namespace AntsLife
{
    public static class DayController
    {
        public delegate void Screen_1_print();

        public static event Screen_1_print screen_1;

        public static void Screen_1()
        {
            screen_1.Invoke();
        }
        public delegate void Screen_2_print();

        public static event Screen_2_print screen_2;

        public static void Screen_2()
        {
            screen_2.Invoke();
        }
        public delegate void Screen_3_HeakStart();

        public static event Screen_3_HeakStart screen_3_HeakStart;

        public static void Screen_3_Start()
        {
            screen_3_HeakStart.Invoke();
        }
        //-------------GoToHeap------------
        public delegate void HeapGoing();

        public static event HeapGoing heapGoing;

        public static void GoToHeapStart()
        {
            heapGoing.Invoke();
        }
        //-------------HeapEvent(Fights,TakeRes)---------------
        public delegate void StartHeapEVENT();

        public static event StartHeapEVENT startHeapEvent;

        public static void HeapEvent()
        {
            startHeapEvent.Invoke();
        }
        //---------------DeleteDeadAnts------------------------
        public delegate void DeleteDeadUNITS_BornIns_Screen3(int day);

        public static event DeleteDeadUNITS_BornIns_Screen3 DeleteDead_Borning_print;
        
        public static void DeletingAnts_BornIns_printScreen3(int day)
        {
            DeleteDead_Borning_print.Invoke(day);
            
        }
    }
}