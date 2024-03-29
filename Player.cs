public class Player
{
    // Mazo del jugador
    public Deck deck { get; set; }

    // Cementerio del jugador
    public List<Card> Graveyard { get; set; } = new List<Card>();

    // Líder del jugador
    public LeaderCard Leader { get; set; }

    // Campo de batalla del jugador
    public BattleField BattleField { get; set; }

    // Cartas en la mano del jugador
    public List<Card> Hand { get; set; } = new List<Card>();
    public bool HasPassed { get; internal set; }

    // Métodos para interactuar con el jugador
    public void DrawCard(int numberOfCards)
{
    // Verificar si la mano del jugador tiene espacio para las cartas adicionales
    if (Hand.Count + numberOfCards <= 10)
    {
        // Verificar si hay suficientes cartas en el mazo
        if (deck.Cards.Count >= numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                // Robar la primera carta del mazo
                Card drawnCard = deck.Cards[0];
                deck.Cards.RemoveAt(0);

                // Agregar la carta robada a la mano del jugador
                Hand.Add(drawnCard);
            }
        }
        else
        {
            // Si no hay suficientes cartas en el mazo, lanzar una excepción
            throw new InvalidOperationException("No hay suficientes cartas en el mazo.");
        }
    }
    else
    {
        // Si la mano está llena, no se puede robar más cartas
        throw new InvalidOperationException("La mano está llena. No se puede robar más cartas hasta el final del turno.");
    }
}
    public void PlayCard(Card card, UnitCard.AttackType position)
{
    // Verificar si la carta está en la mano del jugador
    if (!Hand.Contains(card))
    {
        throw new InvalidOperationException("La carta no está en la mano del jugador.");
    }

    // Verificar si la carta es unitaria
    if (card is UnitCard unitCard)
    {
        // Verificar si la posición de la carta es válida para su tipo de ataque
        if (!unitCard.AttackTypes.Contains(position))
        {
            throw new InvalidOperationException("La posición de la carta no es válida para su tipo de ataque.");
        }
    }
    if(card is SpecialCard espCard && espCard.SpecialTypes==SpecialCard.SpecialType.Clear)
    {
        // Remover la carta de la mano del jugador
        Hand.Remove(card);
        card.UseCardEffect();
    }
    else
     {
        // Colocar la carta en el campo de batalla
        switch (position)
        {
            case UnitCard.AttackType.Melee:
                BattleField.MeleeCards.Add(card);
                break;
            case UnitCard.AttackType.Ranged:
                BattleField.RangedCards.Add(card);
                break;
            case UnitCard.AttackType.Siege:
                BattleField.SiegeCards.Add(card);
                break;
        }

        // Remover la carta de la mano del jugador
        Hand.Remove(card);

        // Llamar al método para usar el efecto especial de la carta, si lo tiene
        card.UseCardEffect();
     }   
}
    public void UseLeaderAbility(LeaderCard Leader)
    {
        Leader.UseCardEffect();
    }

}