using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman;

internal class Game
{
    public int TurnsLeft { get; set; }
    public bool GameOver { get; set; }
    public bool PlayerWin { get; set; }
    public List<string> Phrases { get; set; } = new List<string>()
    {
        "JAZZ",
        "WOOLY MAMMOTH",
        "HEAVY METAL",
        "BEACH VACATION",
        "HELLO NOEL",
        "ROCKET LEAGUE",
        "I AM REALLY COOL",
        "PEACH AND PEPPER",
        "BARKING DOG",
        "BID LIGHT",
        "ROCK BAND",
        "GUITAR HERO",
        "ANOTHER ONE",
        "HOW IS THE WEATHER",
        "GET READY TO RUMBLE",
        "WHO DO YOU THINK YOU ARE I AM",
        "BLINDED BY THE LIGHT",
        "LAND OF THE FREE",
        "HOME OF THE WHOPPER",
        "HAVE IT YOUR WAY",
        "YOU RULE",
        "HELLO MY BABY",
        "ANOTHER BRICK IN THE WALL",
        "NEVER GONNA GIVE YOU UP",
        "PRIMARY COLORS",
        "ANIMAL CROSSING",
        "HARRY POTTER AND THE PRISONER OF AZKABAN",
        "CALL OF DUTY",
        "HYUNDAI",
        "KIA FORTE",
        "MISSISSIPPI",
        "CALL ME MAYBE",
        "DROP IT LOW GIRL",
        "OUR HOUSE IN THE MIDDLE OF OUR STREET",
        "SUPER MARIO BROS",
        "BEEP BOOP BOP",
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
        "GUMMY WORMS"
    };
    public string Phrase { get; set; }
    public List<string> LettersGuessed { get; set; }
    public string ValidCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public Dictionary<int, string> Drawings = new Dictionary<int, string>()
    {
        { 0,
        @"
   ______
   |    |
   |    O
   |   /|\
   |   / \
   |
 __|__
" },
        { 1,
        @"
   ______
   |    |
   |    O
   |   /|\
   |   / 
   |
 __|__
" },
        { 2,
        @"
   ______
   |    |
   |    O
   |   /|\
   |   
   |
 __|__
" },
        { 3,
        @"
   ______
   |    |
   |    O
   |   /|
   |   
   |
 __|__
" },
        { 4,
        @"
   ______
   |    |
   |    O
   |    |
   |   
   |
 __|__
" },
        { 5,
        @"
   ______
   |    |
   |    O
   |   
   |    
   |
 __|__
" },
        { 6,
        @"
   ______
   |    |
   |    
   |   
   |   
   |
 __|__
" }
    };

    
    public void Configure()
    {
        TurnsLeft = 6;
        GameOver = false;
        PlayerWin = false;
        LettersGuessed = new List<string>() { " " };

        Random rnd = new Random();
        int randomIndex = rnd.Next(Phrases.Count);
        string randomPhrase = Phrases[randomIndex];
        Phrase = randomPhrase;
    }
    
    public void Run()
    {
        // draw picture
        Console.WriteLine("HANGMAN");
        Console.WriteLine();
        Console.WriteLine(Drawings[TurnsLeft]);

        // show phrase
        string sentence = "";
        foreach (char letter in Phrase)
        {
            var foundLetter = LettersGuessed.FirstOrDefault(x => x.Equals(letter.ToString()), "");
            if (foundLetter == "")
            {
                sentence += "_";
            }
            else
            {
                sentence += letter;
            }
        }
        Console.WriteLine(sentence);
        Console.WriteLine();

        // show letter guessed
        Console.Write("LETTERS GUESSED:");
        foreach (string letter in LettersGuessed)
        {
            if (letter != " ")
            {
                Console.Write($"{letter} ");
            }
        }
        Console.WriteLine();

        // show turns left
        Console.WriteLine($"Turns left: {TurnsLeft}");

        // take turn
        TakeTurn();

        CheckForGameOver();

        Console.Clear();
    }

    public void TakeTurn()
    {
        string? guess;
        bool guessIsValid;
        do
        {
            Console.Write("Guess a letter: ");
            guess = Console.ReadLine().ToUpper();
            guessIsValid = CheckIfGuessIsValid(guess);
        }
        while (!guessIsValid);

        LettersGuessed.Add(guess);

        bool guessIsInPhrase = Phrase.Contains(guess);
        if (!guessIsInPhrase)
        {
            TurnsLeft -= 1;
        }
    }

    public bool CheckIfGuessIsValid(string guess)
    {
        bool guessIsValidCharacter = false;
        bool guessHasBeenGuessedAlready = LettersGuessed.Contains(guess);

        foreach (char c in ValidCharacters)
        {
            if (c.ToString() == guess)
            {
                guessIsValidCharacter = true;
            }
        }

        var guessIsValid = guessIsValidCharacter && !guessHasBeenGuessedAlready;
        return guessIsValid;
    }

    public void CheckForGameOver()
    {
        bool phraseCompleted = true;
        foreach (char c in Phrase)
        {
            int letterFound = LettersGuessed.IndexOf(c.ToString());
            if (letterFound == -1)
            {
                phraseCompleted = false;
            }
        }
        PlayerWin = phraseCompleted;

        if (TurnsLeft <= 0 || phraseCompleted)
        {
            GameOver = true;
        }
    }

    public void GameOverScreen()
    {
        // draw picture
        Console.WriteLine("HANGMAN");
        Console.WriteLine();
        Console.WriteLine(Drawings[TurnsLeft]);

        // show phrase
        string sentence = "";
        foreach (char letter in Phrase)
        {
            var foundLetter = LettersGuessed.FirstOrDefault(x => x.Equals(letter.ToString()), "");
            if (foundLetter == "")
            {
                sentence += "_";
            }
            else
            {
                sentence += letter;
            }
        }
        Console.WriteLine(sentence);

        // show letter guessed
        Console.Write("LETTERS GUESSED:");
        foreach (string letter in LettersGuessed)
        {
            if (letter != " ")
            {
                Console.Write($"{letter} ");
            }
        }
        Console.WriteLine();

        // show turns left
        Console.WriteLine($"GAME OVER. Turns left: {TurnsLeft}");
    }
}
