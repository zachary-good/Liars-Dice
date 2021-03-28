using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Liars_Dice
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variable declarations
            int numOfDice = 1, numOnDice = 1;
            int[] finalGuess = new int[2];
            int count;
            int totalCount = 0, magicNum = 0;
            bool bluff;
            int gotBluffCalled = 0, CalledBluff = 0;
            string name;
            
            //Starts gameplay loop and keeps it looping untill condition is changed to false
            bool contPlay = true;
            while (contPlay == true)
            {
                //Greeting to user
                Console.WriteLine("Welcome to Liars Dice!");

                //Initializes list of players
                var players = new List<Player>();
                int NumPlayers = 0;

                //Asks for number of players
                Console.Write("Input the number of players: ");
                NumPlayers = Int32.Parse(Console.ReadLine());
                int countOfP = NumPlayers;

                //Creates players
                for (int i = 0; i < NumPlayers; i++)
                {
                    Player tempPlayer = new Player();
                    tempPlayer.numDice = 5;

                    Console.WriteLine("Input player name:");
                    tempPlayer.name = Console.ReadLine();

                    //Adds players to list
                    players.Add(tempPlayer);
                }

                //Privacy break to keep rolls hidden
                Console.WriteLine("Press ENTER when ready");
                Console.ReadKey();

                //Initializes gameplay loop until winner is true
                bool winner = false;
                while (!winner)
                {
                    numOfDice = 1;
                    numOnDice = 1;

                    //Rolling dice for loop
                    for (int i = 0; i < NumPlayers; i++)
                    {
                        var currPlayer = players[i];

                        currPlayer.roll = Game.DoRoll(currPlayer.numDice);
                    }
                    
                    //Allows guessing process to repeat until bluff is called. Goes until guess is false
                    bool guess = true;
                    while (guess)
                    {

                        //Guessing for loop
                        for (int i = magicNum; i < NumPlayers; i++)
                        {
                            //Initializes players variable
                            var currPlayer = players[i];
                            char callBluff = 'n';

                            //Shows players roll and asks for guess
                            Console.WriteLine(currPlayer.name + ", your roll was: ");
                            Game.DoPrint(currPlayer.roll);
                            Console.WriteLine(currPlayer.name + " enter the number of dice you guess:");
                            currPlayer.numOfDice = Int32.Parse(Console.ReadLine());
                            Console.WriteLine(currPlayer.name + " enter the value of the dice:");
                            currPlayer.numOnDice = Int32.Parse(Console.ReadLine());

                            //Checks that no rule violations were committed
                            if (numOnDice == 6)
                            {
                                while (currPlayer.numOfDice <= numOfDice)
                                {
                                    Console.WriteLine(currPlayer.name + " the last guess was of value 6, you must increase the number of your guess!");
                                    Console.WriteLine(currPlayer.name + " enter the number of dice you guess:");
                                    currPlayer.numOfDice = Int32.Parse(Console.ReadLine());
                                    Console.WriteLine(currPlayer.name + " enter the value of the dice:");
                                    currPlayer.numOnDice = Int32.Parse(Console.ReadLine());
                                }
                            }

                            //Checks for rule violations
                            while (currPlayer.numOfDice < numOfDice)
                            {
                                Console.WriteLine(currPlayer.name + " please enter a number greater than the previously guessed number:");
                                Console.WriteLine(currPlayer.name + " enter the number of dice you guess:");
                                currPlayer.numOfDice = Int32.Parse(Console.ReadLine());
                                Console.WriteLine(currPlayer.name + " enter the value of the dice:");
                                currPlayer.numOnDice = Int32.Parse(Console.ReadLine());
                            }

                            //Checks for rule violations
                            while (currPlayer.numOnDice < numOnDice)
                            {
                                if (numOnDice == 6)
                                {
                                    break;
                                }
                                else if (currPlayer.numOnDice < numOnDice && currPlayer.numOfDice > numOfDice)
                                {
                                    break;
                                }
                                else
                                Console.WriteLine(currPlayer.name + " please enter a value greater than the previously guessed value:");
                                Console.WriteLine(currPlayer.name + " enter the number of dice you guess:");
                                currPlayer.numOfDice = Int32.Parse(Console.ReadLine());
                                Console.WriteLine(currPlayer.name + " enter the value of the dice:");
                                currPlayer.numOnDice = Int32.Parse(Console.ReadLine());
                            }

                            //Assigns values of dice to be used later
                            numOfDice = currPlayer.numOfDice;
                            numOnDice = currPlayer.numOnDice;

                            //Clears screan and gives privacy break
                            Console.Clear();
                            Console.WriteLine("Press ENTER when ready");
                            Console.ReadKey();

                            Console.WriteLine("Previous guess: " + numOfDice + ", " + numOnDice);

                            //Displays players roll so they can decide to call bluff
                            try
                            {
                                Console.WriteLine(players[i + 1].name + ", your roll was: ");
                                Game.DoPrint(players[i + 1].roll);
                            }
                            catch(ArgumentOutOfRangeException)
                            {
                                Console.WriteLine(players[0].name + ", your roll was: ");
                                Game.DoPrint(players[0].roll);
                            }

                            //Assigns dice numbers to array to be passed into functions later
                            finalGuess[0] = numOfDice;
                            finalGuess[1] = numOnDice;

                            //Asks user if they want to call bluff                            
                            try
                            {
                                Console.WriteLine(players[i+1].name + " Do you want to call a bluff?(Y/N)");
                            }
                            catch(ArgumentOutOfRangeException)
                            {
                                Console.WriteLine(players[0].name + " Do you want to call a bluff?(Y/N)");
                            }
                            callBluff = char.Parse(Console.ReadLine());

                            //Checks if bluff was called and breaks from loop if so. Establishes new variables for later use
                            if (callBluff == 'y' || callBluff == 'Y')
                            {
                                guess = false;
                                gotBluffCalled = i;
                                CalledBluff = i + 1;
                                break;
                            }

                            //Privacy break, displays previous guess for next player
                            Console.Clear();
                            Console.WriteLine("Previous guess: " + numOfDice + ", " + numOnDice);
                        }
                    }

                    //Calculates the total number of the asked dice value that were showing
                    totalCount = 0;
                    for (int i = 0; i < NumPlayers; i++)
                    {
                        var currPlayer = players[i];
                        count = Game.DoCount(currPlayer.roll, finalGuess[1]);
                        totalCount += count;
                    }

                    //Displays total number of asked dice in the roll
                    Console.WriteLine("the total count of dice with number " + finalGuess[1] + " is: " + totalCount);

                    //Checks if it was a bluff
                    bluff = Game.DoCheck(totalCount, finalGuess[0]);
                     
                    //Determines who loses dice
                    if (bluff == true)
                    {
                        Console.WriteLine(players[gotBluffCalled].name + " loses a dice.");
                        players[gotBluffCalled].numDice--;
                        magicNum = gotBluffCalled;
                    }

                    //Determines who loses dice
                    if (bluff == false)
                    {
                        try
                        {
                            Console.WriteLine(players[CalledBluff].name + " loses a dice.");
                            players[CalledBluff].numDice--;
                            magicNum = CalledBluff;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine(players[0].name + " loses a dice.");
                            players[0].numDice--;
                            magicNum = 0;
                        }
                    }

                    //Break to maintain privacy of individuals dice roll
                    Console.WriteLine("Press ENTER when ready");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Press ENTER when ready");
                    Console.ReadKey();
                                      
                    //Checks to see how many players are remaining by checking if they have dice remaining
                    for (int i = 0; i < NumPlayers; i++)
                    {
                        var currPlayer = players[i];

                        if (currPlayer.numDice == 0)
                        {
                            countOfP--;
                        }
                    }

                    //Checks to find which player ahd 1 dice remaining and declares them winner
                    if (countOfP == 1)
                    {
                        if (players[0].numDice == 1)
                        {
                            Console.WriteLine(players[0].name + " YOU ARE THE WINNER!");
                        }
                        else if (players[gotBluffCalled].numDice == 1)
                        {
                            Console.WriteLine(players[gotBluffCalled].name + " YOU ARE THE WINNER!");
                        }
                        else if(players[CalledBluff].numDice == 1)
                        {
                            Console.WriteLine(players[CalledBluff].name + " YOU ARE THE WINNER!");
                        }

                        //Writes winner message to text file
                        WriteAllText.ExampleAsync();

                        //Changes value of winner to end game
                        winner = true;
                    }
                }

                //Asks players if thay want to play another game and terminates loop if answer is no
                Console.WriteLine("Do you want to play again?(Y/N)");
                char newGame = char.Parse(Console.ReadLine());
                if (newGame == 'n' || newGame == 'N')
                {
                    contPlay = false;
                }
            }         
        }
    }
}