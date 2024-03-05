public class Game
{
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public int CurrentRound { get; set; } = 1;
    public int MaxRounds { get; set; } = 3;
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
            // Manejar el turno del jugador 1
            HandlePlayerTurn(Player1);

            // Manejar el turno del jugador 2
            HandlePlayerTurn(Player2);

            // Incrementar el número de ronda
            CurrentRound++;
        }

        // Determinar el ganador
        DetermineWinner();
    }

    private void InitializeGame()
    {
        

        // Inicializar el mazo de cada jugador
        Player1.deck.InitializeDeck(CartasDisponibles);
        Player2.deck.InitializeDeck(CartasDisponibles);

        // Robar 10 cartas para cada jugador
        for (int i = 0; i < 10; i++)
        {
            Player1.DrawCard();
            Player2.DrawCard();
        }
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
        // Lógica para determinar el ganador del juego
        // Esto podría basarse en el número de rondas ganadas o en alguna otra métrica
    }
}