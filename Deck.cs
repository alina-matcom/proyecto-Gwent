public class Deck
{
    public LeaderCard Leader { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    public List<Card> AvailableCards { get; set; } = new List<Card>(); // Lista de cartas disponibles


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

    public void AddCardFromCollection()
    {
        // Mostrar la colección de cartas disponibles al jugador
        Console.WriteLine("Cartas disponibles:");
        for (int i = 0; i < AvailableCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {AvailableCards[i].Name}");
        }

        // Permitir al jugador seleccionar una carta
        Console.Write("Seleccione una carta para agregar al mazo (número): ");
        int selectedIndex = int.Parse(Console.ReadLine()) - 1;

        // Verificar que la selección sea válida
        if (selectedIndex >= 0 && selectedIndex < AvailableCards.Count)
        {
            // Agregar la carta seleccionada al mazo
            AddCard(AvailableCards[selectedIndex]);
        }
        else
        {
            Console.WriteLine("Selección inválida. Intente de nuevo.");
        }
    }
        public void InitializeDeck(List<Card> availableCards)
    {
        // Limpiar el mazo actual
        Cards.Clear();

        // Añadir la carta de líder
        Leader = new LeaderCard("Líder de la Facción", "Habilidad Especial", "Nombre de la Faccion", "Descripción de la Habilidad Especial");

        // Asignar la lista de cartas disponibles
        AvailableCards = availableCards;

        // Permitir al jugador seleccionar y agregar cartas a su mazo
        while (Cards.Count < 25)
    {
        AddCardFromCollection();
    }
    }

}