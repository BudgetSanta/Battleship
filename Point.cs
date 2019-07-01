using System;

namespace Battleship {

    class Point {

        int _x;
        int _y;

        public Point(int x, int y) {
            _x = x;
            _y = y;
        }

        public int[] GetPoint() {
            return new int[] {_x, _y};
        }

        public int GetX() {
            return _x;
        }

        public int GetY() {
            return _y;
        }
        
        override public string ToString() {
            return String.Format("{0},{1}", _x, _y);
        }
    }

}
