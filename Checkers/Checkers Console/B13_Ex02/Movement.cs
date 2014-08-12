namespace Checkers
{
    public struct CellPosition
    {
        private int x;
        private int y;

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
    }

    public struct Movement
    {
        private CellPosition sourcePosition;
        private CellPosition destinationPosition;

        public CellPosition SourcePosition
        {
            get
            {
                return sourcePosition;
            }

            set
            {
                sourcePosition = value;
            }
        }

        public CellPosition DestinationPosition
        {
            get
            {
                return destinationPosition;
            }

            set
            {
                destinationPosition = value;
            }
        }

        public override string ToString()
        {
            char[] movement = new char[5];
            char currentLetter = char.MaxValue;
            currentLetter = (char)sourcePosition.Y;
            currentLetter += 'A';
            movement.SetValue(currentLetter, 0);
            currentLetter = (char)sourcePosition.X;
            currentLetter += 'a';
            movement.SetValue(currentLetter, 1);

            movement.SetValue('>', 2);

            currentLetter = (char)destinationPosition.Y;
            currentLetter += 'A';
            movement.SetValue(currentLetter, 3);
            currentLetter = (char)destinationPosition.X;
            currentLetter += 'a';
            movement.SetValue(currentLetter, 4);
            string m = new string(movement);
            return m;
        }

        // $G$ CSS-028 (0) method shouldn't include more then one return command.
        public static bool IsTwoPositionsEquale(CellPosition i_firstCellPosition, CellPosition i_secondCellPosition)
        {
            if ((i_firstCellPosition.X == i_secondCellPosition.X) && (i_firstCellPosition.Y == i_secondCellPosition.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}