using System;

namespace Battleship {

    class Game {

        Player _player;
        int _gridSize;
        int _maxGridValue;

        public Game() {

            int gridSize = GetUserInt("Enter board size (n*n) [0->]: ");
            int shipCount = GetUserInt("Enter number of ships [1->]: ");
            _player = new Player(shipCount);
            _gridSize = gridSize;
            _maxGridValue = _gridSize - 1;

        }

        // GAME LOGIC

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

            Console.WriteLine("[INFO] No ships remain! The game has ended. You have lost.");
        }
        
        // ACTIONS

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
        
        private bool HasLostGame() {
            return (_player.GetShips().Length == _player.GetSunkShips());
        }

        private bool PlaceShip(Ship ship) {
            if (IsValidShipPlacement(ship) && IsNotOverLapping(ship)) {
                _player.AddShip(ship);
                return true;
            }
            return false;
        }

        private bool IsValidShipPlacement(Ship s) {
            return (IsValidBoardPoint(s.GetBow()) && IsValidBoardPoint(s.GetStern()));
        }

        private bool IsValidBoardPoint(Point p) {
            bool xValid = p.GetX() >= 0 && p.GetX() < _gridSize;
            bool yValid = p.GetY() >= 0 && p.GetY() < _gridSize;
            return (xValid && yValid);
        }
        
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

        private int GetUserInt(string prompt) {
            Console.Write(prompt);
            string input = Console.ReadLine();
            int number; 
            // Keep requesting number while input is NaN and Not valid board point
            while (!Int32.TryParse(input, out number) 
                    && IsValidBoardPoint( new Point(number, 0)) ) {
                Console.WriteLine("[ERROR]: {0} is not a valid input. Try again!", input);
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return number;
        }

    }

}
