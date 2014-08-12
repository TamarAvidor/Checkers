namespace Checkers
{
    public class Cell
    {
        // $G$ DSN-999 (0) It's better to use a struct to keep Cell data.
        public enum e_CellOccupation
        {
            notOccupied = 0,
            topPlayerRegularCoin = 1,
            bottomPlayerRegularCoin = 2,
            topPlayerSpecialCoin = 3,
            bottomPlayerSpecialCoin = 4,
        }

        private e_CellOccupation m_cellOccupation;

        public Cell()
        {
            m_cellOccupation = e_CellOccupation.notOccupied;
        }

        public e_CellOccupation GetCellOccupation()
        {
            return m_cellOccupation;
        }

        public void SetCellOccupation(e_CellOccupation i_cellRepresentIntStr)
        {
            m_cellOccupation = i_cellRepresentIntStr;
        }
    }
}