using System;
using System.Collections.Generic;
using System.Text;

namespace Liars_Dice
{
    //Player class, stores all information for each player
    public class Player
    {
        //Initialization of variables of player class
        public int numDice { get; set; }
        public string name { get; set; }
        public int[] roll { get; set; }
        public int numOfDice { get; set; }
        public int numOnDice { get; set; }
       
    }
}
