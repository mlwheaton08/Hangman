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

    quit = game.AskToQuit();
}