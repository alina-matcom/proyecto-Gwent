public class UnitCard : Card
{
    public enum AttackType
{
    Melee, // Ataque cuerpo a cuerpo
    Ranged, // Ataque a distancia
    Siege // Ataque de asedio
}

    public enum UnitType
{
    Plata, // Unidad de plata
    Oro // Unidad de oro
}

    public enum SpecialAbility
{
     IncreaseAttack, // Aumenta el poder de ataque
    WeatherEffect, // Aplica un efecto de clima
    RemoveHighestPower, // Elimina la carta con más poder del campo
    RemoveLowestPower, // Elimina la carta con menos poder del rival
    DrawCard, // Roba una carta
    MultiplierAttack, // Multiplica el poder de ataque
    ClearRow, // Limpia la fila del campo con menos unidades
    AveragePower // Calcula el promedio de poder y ajusta el poder de todas las cartas
}
    public UnitType UnitTypes { get;} // Enum para plata o oro
    public SpecialAbility SpecialAbilitys { get;} // Enum para las habilidades especiales
    public int Power { get; set; }
    public List<AttackType> AttackTypes { get;} // Lista de tipos de ataque
    public override void UseCardEffect()
    {
        // Implementación de la habilidad especial
    }
}