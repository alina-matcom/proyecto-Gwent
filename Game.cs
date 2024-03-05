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
        // Lógica para manejar el turno de un jugador
        // Esto podría incluir jugar una carta, usar una habilidad de líder, o pasar
    }

    private bool IsGameOver()
    {
        // Lógica para determinar si el juego ha terminado
        // Esto podría basarse en el número de rondas ganadas o en alguna otra condición
        return CurrentRound > MaxRounds;
    }

private void DetermineWinner()
{
    // Obtén la suma de los Power de las cartas unitarias en el campo de batalla para cada jugador
    int player1TotalPower = Player1.BattleField.MeleeCards.OfType<UnitCard>().Sum(card => card.Power) +
                             Player1.BattleField.RangedCards.OfType<UnitCard>().Sum(card => card.Power) +
                             Player1.BattleField.SiegeCards.OfType<UnitCard>().Sum(card => card.Power);

    int player2TotalPower = Player2.BattleField.MeleeCards.OfType<UnitCard>().Sum(card => card.Power) +
                             Player2.BattleField.RangedCards.OfType<UnitCard>().Sum(card => card.Power) +
                             Player2.BattleField.SiegeCards.OfType<UnitCard>().Sum(card => card.Power);

    // Determina el ganador de la ronda basado en la suma de los Power
    if (player1TotalPower > player2TotalPower)
    {
        Player1RoundsWon++;
        Player1TotalPoints += player1TotalPower; // Acumula los puntos ganados en la ronda
    }
    else if (player2TotalPower > player1TotalPower)
    {
        Player2RoundsWon++;
        Player2TotalPoints += player2TotalPower; // Acumula los puntos ganados en la ronda
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

}