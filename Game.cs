using System;

namespace Battleship {

    class Game {

        Player _player;
        int _gridSize;
        int _maxGridValue;                          // Nicer value to reference than _gridSize - 1

        public Game() {

            int gridSize = GetUserInt("Enter board size (n*n) [0->]: ");
            _gridSize = gridSize;
            _maxGridValue = _gridSize - 1;

            int shipCount = GetUserInt(String.Format("Enter number of ships [1->{0}]: ", _gridSize*_gridSize));
            while (shipCount > _gridSize*_gridSize) {
                Console.WriteLine(String.Format("[ERROR] You can have a maximum of {0} ships with a board size of {1}*{1}. Please try again.", _gridSize*_gridSize, _gridSize));
                shipCount = GetUserInt(String.Format("Enter number of ships [1->{0}]: ", _gridSize*_gridSize));
            }
            _player = new Player(shipCount);

        }

        // GAME LOGIC

        // Creates game setup, board size, num ships, 2 x,y coords per ship with checking for valid positioning
        public void SetupGame() {

            while (!_player.IsFinishedSetup()) {

                Console.WriteLine("[INFO] Place a ship");

                int bowX = GetUserInt(String.Format("Enter X-Coord for Ships Bow [0-{0}]: ", _maxGridValue));
                int bowY = GetUserInt(String.Format("Enter Y-Coord for Ships Bow [0-{0}]: ", _maxGridValue));
                int sternX = GetUserInt(String.Format("Enter X-Coord for Ships Stern [0-{0}]: ", _maxGridValue));
                int sternY = GetUserInt(String.Format("Enter Y-Coord for Ships Stern [0-{0}]: ", _maxGridValue));
                Point bow = new Point(bowX, bowY);
                Point stern = new Point(sternX, sternY);

                Ship ship = new Ship(bow, stern);
                if (!PlaceShip(ship)) {
                    Console.WriteLine(String.Format("[ERROR] Bow: {0}, Stern: {1} is not valid. Try Again!", bow, stern));
                }
            }
        }

        public void PlayGame() {
            Console.WriteLine("Time to play!. Try find all ships!");

            while (!HasLostGame()){
                int x = GetUserInt(String.Format("Enter X-Coord for guess [0-{0}]: ", _maxGridValue));
                int y = GetUserInt(String.Format("Enter Y-Coord for guess [0-{0}]: ", _maxGridValue));
                Point guess = new Point(x, y);
                Fire(guess);
            }

        }
        
        // ACTIONS

        // Makes a guess on the board and reports back
        private void Fire(Point point) {
            if (_player.IsAHit(point)) {
                Console.WriteLine(String.Format("[INFO] Point: {0} hit", point));
                Ship hitShip = _player.GetShip(point);
                hitShip.MarkHit();
                if (hitShip.IsSunk()) {
                    _player.AddHit();
                    Console.WriteLine(String.Format("[INFO] Ship sunk! {0} ship(s) remain", _player.GetNumRemainingShips()));
                }
            } else {
                Console.WriteLine(String.Format("[INFO] Point: {0} missed", point));
            }

        }
        
        // CHECKS
        
        // When all player ships have sunk, they've lost
        private bool HasLostGame() {
            return (_player.GetShips().Length == _player.GetSunkShips());
        }

        // Places ship if it can be placed
        private bool PlaceShip(Ship ship) {
            if (IsValidShipPlacement(ship) && IsNotOverLapping(ship)) {
                _player.AddShip(ship);
                return true;
            }
            return false;
        }

        // Checks if both ends of the ship are valid board points 
        //  ensuring the whole boat is valid
        private bool IsValidShipPlacement(Ship s) {
            return (IsValidBoardPoint(s.GetBow()) && IsValidBoardPoint(s.GetStern()));
        }

        // Checks if point lies on the game's grid
        private bool IsValidBoardPoint(Point p) {
            bool xValid = p.GetX() >= 0 && p.GetX() < _gridSize;
            bool yValid = p.GetY() >= 0 && p.GetY() < _gridSize;
            return (xValid && yValid);
        }
        
        // Checks if the given ship shared a point with 
        //  any other ship in the players invetory
        private bool IsNotOverLapping(Ship newShip) {
            foreach (Point newPoint in newShip.GetPoints()) {
                foreach (Ship playerShip in _player.GetShips()) {
                    if (playerShip != null && playerShip.IsPointOnShip(newPoint)) {
                        return false;
                    }
                }
            }
            return true;
        }

        // HELPERS

        // Get an integer from the user checking that its a valid number
        private int GetUserInt(string prompt) {
            Console.Write(prompt);
            string input = Console.ReadLine();
            int number; 
            // Keep requesting number while input is NaN
            while (!Int32.TryParse(input, out number)) {
                Console.WriteLine("[ERROR]: {0} is not a valid input. Try again!", input);
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return number;
        }

    }

}
