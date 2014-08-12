namespace Ex05.CheckersForWindows
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public struct CoinInformation
    {
        private CellPosition cellPosition;
        private MyButton.e_CellOccupation coinType;

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

        public MyButton.e_CellOccupation CoinType
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
