namespace AntsLife
{
    public enum Modificator
    {
        //Worker MOds
        Immortal,    //неуязвимость +
        ModIgnoting,   //Игнорирование модификаторов - невозможно представить
        FindResAnywhere,// всегда находит ресурс в куче +
        CantBeAttackedFirst, //не может быть атакован первым +
        
        //Wariours Mods
        KillKiller,  //убивает своего убийцу +
        Provocation,   //принимает все атаки на себя +
        DoubleHPDEF,   //здоровье и защита увеличены в 2 раза +
        
        //SpecialInsect Mods
        BytesAnyway, //Наносит укус даже если был убит +
        CantBeAttacked, //Не может быть атакован +
        IgnoreDef, //Игнорирует защиту +
        CanDamageImmortals  //Может наность урон неуязвимым врагам - невозможно представит
    }

   
}