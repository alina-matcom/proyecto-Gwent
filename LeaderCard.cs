public class LeaderCard : Card
{
    public string LeaderAbility { get; }

    public LeaderCard(string name, string description, string faction, string leaderAbility)
        : base(name, description, faction)
    {
        LeaderAbility = leaderAbility;
    }

    public override void UseCardEffect()
    {
        // Implementación de la habilidad del líder
    }
}