namespace AntsLife
{
    public class Resourses
    {
        public Resources[] typeOfRes;       //порядок: веточка -> листик -> камень -> капля
        public int[] amountOfRes;
        public Resourses(int branch,int leaf,int stone,int drop)
        {
            this.typeOfRes = new Resources[4] {Resources.Branch, Resources.Leaf, Resources.Stone, Resources.Drop};
            this.amountOfRes = new int[4] {branch, leaf, stone, drop};
        }
    }
    public enum Resources
    {
        Branch,
        Stone,
        Drop,
        Leaf
    }
}