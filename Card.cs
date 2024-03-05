public abstract class Card
{
    public string Name { get;}
    public string Description { get;}
    //public Position Position { get; set; } 
    public string  Faction {get;}

    protected Card(string name, string description, string faction)
    {
        Name = name;
        Description = description;
        Faction = faction;
    }
    public abstract void UseCardEffect();
}