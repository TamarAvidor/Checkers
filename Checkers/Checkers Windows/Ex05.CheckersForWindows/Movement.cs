namespace Ex05.CheckersForWindows
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

        public static bool IsTwoPositionsEquale(CellPosition i_firstCellPosition, CellPosition i_secondCellPosition)
        {
            bool isTwoPositionsEquale = false;
            if ((i_firstCellPosition.X == i_secondCellPosition.X) && (i_firstCellPosition.Y == i_secondCellPosition.Y))
            {
                isTwoPositionsEquale = true;
            }

            return isTwoPositionsEquale;
        }
    }
}