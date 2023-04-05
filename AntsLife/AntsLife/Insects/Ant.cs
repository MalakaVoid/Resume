namespace AntsLife
{
    public class Ant:ITakeDamage
    {
        public int hp;
        public int def;
        public int damage;
        public string name;
        public bool IsAlive;
        public Colony _colony;
        public Ant(string name, int hp, int def, int damage,Colony _colony)
        {
            this.hp = hp;
            this.def = def;
            this.damage = damage;
            this.name = name;
            IsAlive = true;
            this._colony = _colony;
        }
        public virtual void TakeDamage(int dam,bool ignoreDef)
        {
            IsAlive = false;
        }
    }
    //---------------Список типов муравьев----------------------
    public enum WorkerType
    {
        USUAL,
        OLDER,
        OLDER_UNIQUE,
        LEGENDARY,
        LEGENDARY_RUNNER
    }
    public enum WariourType
    {
        USUAL,
        ADVANCED,
        ADVANCED_REVENGEFULL,
        OLDER,
        ELITE,
        LEGENDARY_FAT
    }
}