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

        // Returns the ship that is on that point
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

        // Check to see whether all ships have been placed
        public bool IsFinishedSetup() {
            // If no null elements can be found, all ships placed
            return (Array.IndexOf(_ships, null) == -1);
        }

        // Checks whether point was a hit on any player ship
        public bool IsAHit(Point p) {
            foreach (Ship s in _ships) {
                if (s.IsPointOnShip(p)) {
                    return true;
                }
            }
            return false;
        }
        
        // ACTIONS
        
        // Given a ship it adds it to the array in the first available index
        public void AddShip(Ship ship) {
            int i = Array.IndexOf(_ships, null); 
            _ships[i] = ship;
        }
        
        // Player hit is a sunken ship. Adds to tally
        public void AddHit() {
            _shipsSunk++;
        }

    }


}
