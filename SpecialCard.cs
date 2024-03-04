public class SpecialCard : Card
{
    public enum SpecialType
    {
        Weather, // Clima
        Boost, // Aumento
        Clear, // Despeje
        Shield // Señuelo
    }

    public SpecialType SpecialTypes { get; set; }

public override void UseCardEffect()
{
    // Implementación del efecto especial
}
}