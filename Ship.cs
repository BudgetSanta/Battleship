using System;

namespace Battleship {

    class Ship {

        Point _bow;         // Front end of the ship
        Point _stern;       // Back end of the ship
        int _hits;          // Hits on the ship
        int _length;

        public Ship(Point bow, Point stern) {
            _bow = bow;
            _stern = stern;
            _length = CalcShipLength();
            _hits = 0;
        }

        public Point GetBow() {
            return _bow;
        }

        public Point GetStern() {
            return _stern;
        }

        public int GetLength() {
            return _length;
        }

        public bool IsSunk() {
            return (_hits == _length);
        } 

        public void MarkHit() {
            _hits++;
        }

        private int CalcShipLength() {
            if (_bow.GetX() == _stern.GetX()) {
                return Math.Abs(_bow.GetY() - _stern.GetY())+1;
            } else {
                return Math.Abs(_bow.GetX() - _stern.GetX())+1;
            }
        }
        
        public bool IsPointOnShip(Point p) {
            // Same vertical axis
            if (p.GetX() == _bow.GetX()) {
                // Checking if point lies between Y values
                if (_bow.GetY() <= _stern.GetY()) {
                    return (p.GetY() >= _bow.GetY() && p.GetY() <= _stern.GetY());
                } else {
                    return (p.GetY() >= _stern.GetY() && p.GetY() <= _bow.GetY());
                }
            }
            
            // Same horizontal axis
            if (p.GetY() == _bow.GetY()) {
                // Checking if point lies between X values
                if (_bow.GetX() <= _stern.GetX()) {
                    return (p.GetX() >= _bow.GetX() && p.GetX() <= _stern.GetX());
                } else {
                    return (p.GetX() >= _stern.GetX() && p.GetX() <= _bow.GetX());
                }
            }

            return false;
        }
        
        public Point[] GetPoints() {
            Point[] points = new Point[_length];

            // Share same horizontal axis
            if (_bow.GetY() == _stern.GetY()) {
                
                // Find lower x value and count up to the other
                if (_bow.GetX() <= _stern.GetX()) {
                    for (int i = _bow.GetX(); i <= _stern.GetX(); i++) {
                        points[i - _bow.GetX()] = new Point(i, _bow.GetY());
                    }
                } else {
                    for (int i = _stern.GetX(); i <= _bow.GetX(); i++) {
                        points[i - _stern.GetX()] = new Point(i, _stern.GetY());
                    }
                }

            } else {

                // Find lower y value and count up to the other
                if (_bow.GetY() <= _stern.GetY()) {
                    for (int i = _bow.GetY(); i <= _stern.GetY(); i++) {
                        points[i-_bow.GetY()] = new Point(_bow.GetX(), i);
                    }
                } else {
                    for (int i = _stern.GetY(); i <= _bow.GetY(); i++) {
                        points[i-_stern.GetY()] = new Point(_stern.GetX(), i);
                    }
                }

            }
            return points;
        }

        override public string ToString() {
            string ship;
            if (_length == 1) {
                ship = new String("<");
            } else if (_length == 2) {
                ship = new String("<>");
            } else {
                ship = String.Format("<{0}>", new String('#',_length-2));
            }
            return String.Format("H:{0} | L:{1} - {2}", _hits, _length, ship);
        }

    }

}
