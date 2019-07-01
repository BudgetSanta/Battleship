using System;

namespace Battleship
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("[INFO] Game started. Set up your game board");
            Game game = new Game();
            game.SetupGame();
            game.PlayGame();

        }
    }
}
