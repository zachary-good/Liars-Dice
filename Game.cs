using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Liars_Dice
{
    class Game
    {
        //Generates random rolls for a player
        public static int[] DoRoll(int D)
        {
            //Initializes variables
            int dice = D;
            int i = 0;
            int[] hand = new int[dice];

            //generates random number array from 1-7
            while (i < dice)
            {
                Random r = new Random();
                int die = r.Next(1, 7);
                hand[i] = die;
                i++;
            }

            //Returns value to call
            return hand;
        }

        //Function to count the number of certain type of dice
        public static int DoCount(int[] hand, int DieNum)
        {
            //Initialize variables
            int j = 0;
            int count = 0;
            int length = hand.Length;
                                 
            //Checks for asked die number as well as wild ones
            while (j < length)
            {
                if (hand[j] == DieNum || hand[j] == 1)
                {
                    count++;
                }
                j++;
            }
            
            //Returns values
            return count;
        }

        //Checks to see if dice numbers matched asked numbers
        public static bool DoCheck(int RealCount, int DesiredCount)
        {
            //Checks values and returns 
            if (RealCount < DesiredCount)
            {
                Console.WriteLine("Congratulations, you were correct, it was a lie!");
                return true;
            }
            else
                Console.WriteLine("Sorry, it was not a lie.");
                return false;   
        }

        //Unused early prototype for guess gameplay
        public static int[] DoGuess(int[] roll)
        {
            int[] guess;
                        
            Console.WriteLine("What is your guess?");
            Console.WriteLine("How many dice?");
            int NumOfDie = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Of what number?");
            int NumOnDie = Convert.ToInt32(Console.ReadLine());
                          
            return guess = new int[] { NumOfDie, NumOnDie };
        }

        //Unused prototype to call bluff
        public static void DoCallBluff(int[] guess, int[] roll)
        {
            //Gets count of how many dice of the desired number were rolled
            int ActualCount = Game.DoCount(roll, guess[1]);
            
            //Checks if the number rolled was greater than or equal to the number guessed
            Game.DoCheck(ActualCount, guess[0]);
        }

        //Prints the roll to screen
        public static void DoPrint(int[] roll)
        {
            for (int i = 0; i < roll.Length; i++)
            {
                Console.Write(roll[i] + " ");
            }
            Console.WriteLine();
        }
    }
    //class to write to text files
    class WriteAllText
    {
        public static async Task ExampleAsync()
        {
            string text = "CONGRATS YOU WERE A BIG WINNER, thanks for playing";
                
            await File.WriteAllTextAsync("WriteText.txt", text);
        }
    }
}
