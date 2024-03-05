public class Game
{
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public int CurrentRound { get; set; } = 1;
    public int MaxRounds { get; set; } = 3;
    public int Player1RoundsWon { get; set; } = 0;
    public int Player2RoundsWon { get; set; } = 0;
    public int Player1TotalPoints { get; set; } = 0;
    public int Player2TotalPoints { get; set; } = 0;
    public static List<Card> CartasDisponibles = new List<Card>
    {
        //crearme el conjunto de cartas avaladas
    };

    public Game(Player player1, Player player2)
    {
        Player1 = player1;
        Player2 = player2;
    }

    public void StartGame()
    {
        // Inicializar el juego
        InitializeGame();

        // Mientras el juego no haya terminado
        while (!IsGameOver())
        {
            HandlePlayerTurn(Player1);
            HandlePlayerTurn(Player2);

            // Verificar si ambos jugadores han pasado sus turnos
            if (Player1.HasPassed && Player2.HasPassed)
            {
                // Determinar el ganador de la ronda
                DetermineWinner();

                // Incrementar el número de ronda y robar cartas adicionales si es necesario
                CurrentRound++;
                if (CurrentRound == 2)
                {
                    Player1.DrawCard(2);
                    Player2.DrawCard(2);
                }
                else if (CurrentRound == 3)
                {
                    Player1.DrawCard(2);
                    Player2.DrawCard(2);
                }

                // Reiniciar el estado de paso de los jugadores
                Player1.HasPassed = false;
                Player2.HasPassed = false;
            }
        }

        DetermineGameWinner();
    }

    private void InitializeGame()
    {
        // Inicializar el mazo de cada jugador
        Player1.deck.InitializeDeck(CartasDisponibles);
        Player2.deck.InitializeDeck(CartasDisponibles);

        // Robar 10 cartas para cada jugador

            Player1.DrawCard(10);
            Player2.DrawCard(10);
    }

    private void HandlePlayerTurn(Player player)
{
    // Solicitar al jugador que elija una acción (jugar una carta, usar una habilidad de líder, o pasar)
    Console.WriteLine("Elige una acción:");
    Console.WriteLine("1. Jugar una carta");
    Console.WriteLine("2. Usar una habilidad de líder");
    Console.WriteLine("3. Pasar");

    int actionChoice = Convert.ToInt32(Console.ReadLine());

    switch (actionChoice)
    {
        case 1: // Jugar una carta
            Card cardToPlay = SelectCardFromHand(player); // Este método es un placeholder y necesita ser implementado
            UnitCard.AttackType position = SelectAttackType(); // Este método es un placeholder y necesita ser implementado
            player.PlayCard(cardToPlay, position);
            break;
        case 2: // Usar una habilidad de líder
            player.UseLeaderAbility(player.Leader);
            break;
        case 3: // Pasar
            player.HasPassed = true;
            Console.WriteLine("ha pasado su turno.");
            break;
        default:
            Console.WriteLine("Opción inválida. Intenta de nuevo.");
            HandlePlayerTurn(player); // Vuelve a llamar al método para que el jugador elija una acción válida
            break;
    }
}

    private bool IsGameOver()
{
    // Verificar si alguno de los jugadores ha ganado el juego (dos rondas ganadas)
    if (Player1RoundsWon == 2 || Player2RoundsWon == 2)
    {
        return true;
    }

    // Verificar si el juego ha alcanzado el número máximo de rondas
    if (CurrentRound > MaxRounds)
    {
        return true;
    }

    // Si ninguna de las condiciones anteriores se cumple, el juego continúa
    return false;
}

private int CalculateTotalPower(Player player)
{
    return player.BattleField.MeleeCards.OfType<UnitCard>().Sum(card => card.Power) +
           player.BattleField.RangedCards.OfType<UnitCard>().Sum(card => card.Power) +
           player.BattleField.SiegeCards.OfType<UnitCard>().Sum(card => card.Power);
}

private void DetermineWinner()
{
    int player1TotalPower = CalculateTotalPower(Player1);
    int player2TotalPower = CalculateTotalPower(Player2);

    if (player1TotalPower > player2TotalPower)
    {
        Player1RoundsWon++;
        Player1TotalPoints += player1TotalPower;
    }
    else if (player2TotalPower > player1TotalPower)
    {
        Player2RoundsWon++;
        Player2TotalPoints += player2TotalPower;
    }
    // No se realiza ninguna acción si la ronda termina en empate
}

public void DetermineGameWinner()
{
    // Caso 1: Si un jugador ha ganado dos rondas, ese jugador es el ganador del juego.
    if (Player1RoundsWon == 2)
    {
        Console.WriteLine("El Jugador 1 ha ganado el juego.");
        return;
    }
    if (Player2RoundsWon == 2)
    {
        Console.WriteLine("El Jugador 2 ha ganado el juego.");
        return;
    }

    // Caso 2: Si ambos jugadores ganan una ronda y la tercera es empate, el ganador es el jugador con más puntos en la ronda ganada.
    if (Player1RoundsWon == 1 && Player2RoundsWon == 1)
    {
        if (Player1TotalPoints > Player2TotalPoints)
        {
            Console.WriteLine("El Jugador 1 ha ganado el juego.");
        }
        else if (Player2TotalPoints > Player1TotalPoints)
        {
            Console.WriteLine("El Jugador 2 ha ganado el juego.");
        }
        else
        {
            Console.WriteLine("El juego termina en empate.");
        }
        return;
    }

    // Caso 3: Si un jugador gana una ronda y el otro no, el ganador es el jugador que ganó la ronda.
    if (Player1RoundsWon == 1 && Player2RoundsWon == 0)
    {
        Console.WriteLine("El Jugador 1 ha ganado el juego.");
        return;
    }
    if (Player2RoundsWon == 1 && Player1RoundsWon == 0)
    {
        Console.WriteLine("El Jugador 2 ha ganado el juego.");
        return;
    }

    // Caso 4: Si el juego termina en empate (ningún jugador gana dos rondas y todas las rondas son empate), se declara un empate.
    if (Player1RoundsWon == 0 && Player2RoundsWon == 0)
    {
        Console.WriteLine("El juego termina en empate.");
    }
}

private Card SelectCardFromHand(Player player)
{
    Console.WriteLine("Cartas en mano:");
    for (int i = 0; i < player.Hand.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {player.Hand[i].Name}");
    }

    Console.WriteLine("Elige una carta para jugar:");
    int cardChoice = Convert.ToInt32(Console.ReadLine());
    return player.Hand[cardChoice - 1];
}

private UnitCard.AttackType SelectAttackType()
{
    Console.WriteLine("Elige un tipo de ataque:");
    Console.WriteLine("1. Cercano");
    Console.WriteLine("2. A distancia");
    Console.WriteLine("3. Asedio");

    int attackTypeChoice = Convert.ToInt32(Console.ReadLine());
    switch (attackTypeChoice)
    {
        case 1:
            return UnitCard.AttackType.Melee;
        case 2:
            return UnitCard.AttackType.Ranged;
        case 3:
            return UnitCard.AttackType.Siege;
        default:
            Console.WriteLine("Opción inválida. Intenta de nuevo.");
            return SelectAttackType(); // Vuelve a llamar al método para que el usuario elija un tipo de ataque válido
    }
}

}