using Hangman;

bool quit = false;

while (!quit)
{
    Console.Clear();
    var game = new Game();
    game.Configure();

    while (!game.GameOver)
    {
        game.Run();
    }

    game.GameOverScreen();
    Console.ReadLine();
    Console.WriteLine();

    bool validInput = false;

    do
    {
        Console.Clear();
        Console.WriteLine("Press 'y' to play again");
        Console.WriteLine("Press 'n' to quit");
        string playAgain = Console.ReadLine();

        if (playAgain == "y")
        {
            validInput = true;
        }
        if (playAgain == "n")
        {
            validInput = true;
            quit = true;
        }
    }
    while (!validInput);
}