namespace Checkers
{
    public struct CoinInformation
    {
        private CellPosition cellPosition;
        private Cell.e_CellOccupation coinType;

        public CellPosition CellPosition
        {
            get
            {
                return cellPosition;
            }

            set
            {
                cellPosition = value;
            }
        }

        public Cell.e_CellOccupation CoinType
        {
            get
            {
                return coinType;
            }

            set
            {
                coinType = value;
            }
        }
    }
}
