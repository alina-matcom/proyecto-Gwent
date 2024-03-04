public class Deck
{
    public LeaderCard Leader { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();

    public void AddCard(Card card)
    {
        // Verificar que el mazo tenga un líder
        if (Leader == null)
        {
            throw new InvalidOperationException("El mazo debe tener un líder.");
        }

        // Verificar que todas las cartas coincidan con la facción del líder
        if (card.Faction != Leader.Faction)
        {
            throw new InvalidOperationException("Todas las cartas del mazo deben coincidir con la facción del líder o ser neutrales.");
        }

        // Verificar que el mazo tenga solo 25 cartas
        if (Cards.Count==25)
        {
            throw new InvalidOperationException("El mazo debe contener solo 25 cartas.");
        }

        // Verificar el límite de cartas de unidad de tipo Plata
        if (card is UnitCard unitCardSilver && unitCardSilver.UnitTypes == UnitCard.UnitType.Plata)
        {
            var silverCards = Cards.OfType<UnitCard>()
                                    .Count(c => c.UnitTypes == UnitCard.UnitType.Plata && c.Name == unitCardSilver.Name);
            if (silverCards == 3)
            {
                throw new InvalidOperationException("No se pueden agregar más de 3 cartas de plata con el mismo nombre al mazo.");
            }

        }

        // Verificar el límite de cartas de unidad de tipo Oro
        if (card is UnitCard unitCardGold && unitCardGold.UnitTypes == UnitCard.UnitType.Oro)
        {
           var goldCards = Cards.OfType<UnitCard>()
                             .Count(c => c.UnitTypes == UnitCard.UnitType.Oro && c.Name == unitCardGold.Name);
        if (goldCards == 1)
        {
            throw new InvalidOperationException("No se pueden agregar más de 1 carta de oro con el mismo nombre al mazo.");
        }
        }

        // Agregar la carta al mazo
        Cards.Add(card);
    }
}