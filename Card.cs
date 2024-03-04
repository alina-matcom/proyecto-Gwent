public abstract class Card
{
    public string Name { get;}
    public string Description { get;}
    //public Position Position { get; set; } 
    public string  Faction {get;}
    public abstract void UseCardEffect();
}