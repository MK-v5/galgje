namespace galgje
{
    internal class Program
    {
        //Maak galgje, gebaseerd op een interne lijst met woorden.
        public static List<string> wordList = new List<string> { "school", "hangman", "fortnite", "xylofoon", "pilot", "unknown" };
        //1 speler, 10 kansen.
        static string playerName;
        static int attempts = 10;
        static string guess;
        static string chosenWord;
        static bool gameWon;
        static List<HangmanLetter> guessedLetters = new List<HangmanLetter>();

        public struct HangmanLetter
        {
            public char letter;
            public bool isInWord;
        }
        //Letter raden,
        //niet in het woord, dan kans eraf,
        //wel in het woord, dan letter tonen.
        //Alle letters geraden, dan gewonnen.
        //Kansen op, dan game over.
        static void Main(string[] args)
        {
            Random rnd = new Random();
            chosenWord = wordList[rnd.Next(0, wordList.Count)];
            //player naam vragen
            Console.WriteLine("Please input your name:");
            playerName = Console.ReadLine();
            //spel spelen
            PlayGame();
        }

        static void PlayGame()
        {
            //zo lang het woord niet geraden is of de attempts niet 0 zijn
            //voer raad functie uit
            while(attempts > 0 && gameWon == false)
            {
                Guess();
            }
        }

        static void Guess() 
        {
            Console.Clear();
            //geraden letter tonen
            DisplayGame();
            Console.WriteLine("\n\nPlease input a letter or guess the entire word:");
            //letter invoer uitlezen
            guess = Console.ReadLine().ToLower();
            //controleren of het een nummer is
            foreach(char c in guess)
            {
                if (!char.IsLetter(c))
                {
                    //geen valide input
                    Console.WriteLine("input invalid.");
                    //opnieuw proberen
                    return;
                }
            }
            //attempt weg of controleren of woord klaar is
            //controleren of letter in woord zit
            //letter opslaan als struct
            //controleren of het een letter of woord is
            if (guess.Length == 1)
            {
                //is letter
                //check of letter is al geraden
                foreach(HangmanLetter h in guessedLetters)
                {
                    if(h.letter == guess[0])
                    {
                        Console.WriteLine("Letter is already guessed");
                        return;
                    }
                }
                if (LetterInWord(guess[0], chosenWord))
                {
                    guessedLetters.Add(new HangmanLetter { letter = guess[0], isInWord = true });
                }
                else
                {
                    guessedLetters.Add(new HangmanLetter { letter = guess[0], isInWord = false });
                    attempts--;
                    return;
                }
                Console.WriteLine("Correct!");
            }
            else if(guess.Length > 1)
            {
                //is een woord
                if(string.Compare(guess, chosenWord) == 0)
                {
                    gameWon = true;
                    Console.WriteLine("You guessed correctly!");
                    return;
                }
            }
            else
            {
                //is invalide poging
                Console.WriteLine("Cannot process null. Please try again.");
                return;
            }
            if (WordComplete())//attempt weg of controleren of woord klaar is
            {
                gameWon = true;
                return;
            }
        }

        static void DisplayGame()
        {
            Console.WriteLine("Hello " + playerName);
            Console.WriteLine("You have " + attempts + " Left");
            foreach(char c in chosenWord)
            {
                char displayLetter = '_';
                foreach(HangmanLetter h in guessedLetters)
                {
                    if (h.letter == c)
                    {
                        displayLetter = h.letter;
                    }
                }
                Console.Write(displayLetter);
            }
            Console.WriteLine("\n\nGuessed Letters:");
            foreach(HangmanLetter h in guessedLetters)
            {
                if(h.isInWord == false)
                {
                    Console.Write(h.letter + " ");
                }
            }
        }

        static bool WordComplete()
        {
            int uniqueLetters = chosenWord.Distinct().Count();
            foreach(HangmanLetter h in guessedLetters) 
            {
                if(h.isInWord)
                {
                    uniqueLetters--;
                }
            }
            if(uniqueLetters == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool LetterInWord(char letter, string word)
        {
            foreach(char c in word)
            {
                if(c == letter)
                {
                    return true;
                }
            }
            return false;
        }
    }
}