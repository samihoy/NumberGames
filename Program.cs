using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NumberGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //whiele loop to run the guessing game
            while (true)
            {
                int MenyChoise = Input("\n 1................simpel gueesing number 1-20, no hints \n 2.........coustome game mode, set your own difficulty!\n 3........................................Quit Playiong");
                // menue with choises for game mode and other feutures    
                switch (MenyChoise)
                {
                    case 1: // RandomNumber method to generate the guesing Number, NumbergameNoHints methos to run a normal game
                        List<int> GuessedNumbers = new List<int>();
                        int RandomNumber1 = RandomNumber(1, 20);
                        GuessedNumbers = NumbergameNohints();
                        ChekGuess(GuessedNumbers, RandomNumber1);
                        break;
                    case 2: // Meney to make your own coustomegame where you can chose all aspects of difficulty, play to try it out : )
                        Console.WriteLine(" What interval of numbers would you like to guess from?, the higher the harder!\n");
                        int lowest = Input(" Lowest Number: \n");
                        int highest = Input(" Highest Number: \n");
                        int NumberofAtempts = Input(" how many Guesses would you like? \n");
                        int Hints = Input(" Would you like hints?\n 1.....no\n 2....Yes\n");
                        int WeryClose = 0;
                        int Close = 0;
                        // This if statment only runs if the user want's Hints
                        if (Hints == 2)
                        {
                            Console.WriteLine(" How close to a number would you like to be for a hint to apper? \n If you have hints on it wall ALWAYS tell you if your number was lower or higher!");
                            WeryClose = Input("\n If the number is wheery close, it shoulde be whitin: ");
                            Close = Input("\n If the number is close but not wherry close it should be whitin: ");
                        }
                        // loop to run the custome game
                        while (true)
                        {
                            int HiddenNumber = RandomNumber(lowest, highest);
                            bool YouLose = true;

                            for (int n = 1; n <= NumberofAtempts; n++)
                            {
                                int UserGuess = Input(" Guess a number:");
                                // Game that runs if the user chose no hints
                                if (Hints == 1)
                                {
                                    Console.WriteLine($" You have {NumberofAtempts - n} Numbers left to chose");
                                    if (UserGuess == HiddenNumber)
                                    {
                                        Console.WriteLine($" \n--------------------------------------------------------- \n----- Congrats yoy won!! the corect number was {UserGuess} !! -----\n---------------------------------------------------------\n");
                                        YouLose = false;
                                        break;
                                    }
                                }
                                // game that runs if user chose to play with hints
                                else if (Hints == 2)
                                {
                                    // the methods belowe takes the usernumbers, the generated number and compares them, if the number is close to it gives a custom hint
                                    if (UserGuess < HiddenNumber)
                                    {
                                        HintsOnHigher(UserGuess, HiddenNumber, WeryClose, Close);
                                    }
                                    else if (UserGuess > HiddenNumber)
                                    {
                                        HintsOnLower(UserGuess, HiddenNumber, WeryClose, Close);
                                    }
                                    else if (UserGuess == HiddenNumber)
                                    {
                                        Console.WriteLine($" Congrats yoy won!! the corect number was {UserGuess} !! \n");
                                        YouLose = false; // this is to make sure the you lose message dosent play after you exit the loop
                                        break;
                                    }
                                }
                                else // error handeling if input when making choises in costume mode was somhow wrong
                                {
                                    Console.WriteLine(" Somthing went wrong in the settup, try again\n");
                                    YouLose = false;
                                    break;
                                }
                            }
                            // plays by default if you never guees the number corect
                            if (YouLose)
                            {
                                Console.WriteLine($" Sorry Your out of attempts!\n Unfortunaly you picked the wrong numbers \n The right number was {HiddenNumber}");
                            }
                            break;
                        }
                        break;
                    case 3: // Exits the game, this thime with a nicer message
                        Console.WriteLine("\n---------------------------------------\n--------------Good Bye!----------------\n------Hope Your had fun playing--------\n---------------------------------------");
                        return;
                    default: //Error handeling, out of range menye choise
                        Console.WriteLine(" Not a valid Option try again \n");
                        break;
                }
            }
        }
        // A method to take in input from a user, converts them to int and also type a message. Helps clean up the code
        // the error handeling is placed inside the method as not to interfear with loops in the main, specificaly loops that count attempts
        public static int Input(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string y = Console.ReadLine();

                if (int.TryParse(y, out int x))
                {
                    return x;
                }
                else
                {
                    Console.WriteLine(" Not a number try again");
                }
            }
        }
        // A random number generator methos to simplefy the main
        public static int RandomNumber(int x, int y)
        {
            Random Generated = new Random();
            return Generated.Next(x, y);
        }
        // A method to enable hints if the number is close, points to lower
        public static void HintsOnLower(int UserGuess, int HiddenNumber, int WeryClose, int Close)
        {
            if ((UserGuess - HiddenNumber) < WeryClose)
            {
                Console.WriteLine(" Wherry close, A little bit Lower! \n");
            }
            else if ((UserGuess - HiddenNumber) < Close)
            {
                Console.WriteLine(" Close, But Lower! \n");
            }
            else
            {
                Console.WriteLine($" Lower, you had: {UserGuess} \n");
            }
        }
        // A method to enable hints if the number is close, points to higher
        public static void HintsOnHigher(int UserGuess, int HiddenNumber, int WeryClose, int Close)
        {
            if ((HiddenNumber - UserGuess) < WeryClose)
            {
                Console.WriteLine(" Wherry close, A little bit higher! \n");
            }
            else if ((HiddenNumber - UserGuess) < Close)
            {
                Console.WriteLine(" Close, But higher! \n");
            }
            else
            {
                Console.WriteLine($" Higher, you had: {UserGuess} \n");
            }
        }
        // metod to chek if user gueesed corect number
        public static void ChekGuess(List<int> GuessedNumbers, int RandomNumber)
        {
            if (GuessedNumbers.Contains(RandomNumber))
            {
                Console.WriteLine($"           ----CONGRATULATIONS!!!--- \n            The winning Number was {RandomNumber}\n    Your amazing guessing skills mabe you a winner! \n Be sure to chek out the other game modes if you \n found this one to easy, im sure it will be fun!");
            }
            else
            {
                Console.WriteLine(" Sorry it seams your number wasent the right one, try again?\n");
            }
        }
        // metod that runs a standard game of guess the number with 5 atampts, no hints and interval of 1-20
        public static List<int> NumbergameNohints()
        {
            List<int> GuessedNumbers = new List<int>();
            for (int n = 1; n <= 5; n++)
            {
                int x = Input(" Guess a number:");
                GuessedNumbers.Add(x);
                Console.WriteLine($" You have {5 - n} numbers left\n");
            }
            return GuessedNumbers;
        }
    }
}
