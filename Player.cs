using System;

namespace Battleship {

    class Player {

        Ship[] _ships;                  // Player's placed Ships
        int _shipsSunk;                 // Number of sunk ships

        public Player(int numShips) {
            _ships = new Ship[numShips];
            _shipsSunk = 0;
        }

        public Ship[] GetShips() {
            return _ships;
        }

        public Ship GetShip(Point point) {
            foreach (Ship ship in _ships) {
                if (ship.IsPointOnShip(point)) {
                    return ship;
                }
            }
            return null;
        }

        public int GetNumRemainingShips() {
            return _ships.Length - _shipsSunk;
        }

        public int GetSunkShips() {
            return _shipsSunk;
        }

        // CHECKS

        public bool IsFinishedSetup() {
            // If no null elements can be found, all ships placed
            return (Array.IndexOf(_ships, null) == -1);
        }

        public bool IsAHit(Point p) {
            foreach (Ship s in _ships) {
                if (s.IsPointOnShip(p)) {
                    return true;
                }
            }
            return false;
        }
        
        // ACTIONS
        
        public void AddShip(Ship ship) {
            int i = Array.IndexOf(_ships, null); 
            _ships[i] = ship;
        }
        
        public void AddHit() {
            _shipsSunk++;
        }

    }


}
