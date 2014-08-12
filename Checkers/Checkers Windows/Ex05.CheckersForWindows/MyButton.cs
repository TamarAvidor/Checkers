namespace Ex05.CheckersForWindows
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;

    public class MyButton : Button
    {
        public enum e_CellOccupation
        {
            notOccupied = 0,
            topPlayerRegularCoin = 1,
            bottomPlayerRegularCoin = 2,
            topPlayerSpecialCoin = 3,
            bottomPlayerSpecialCoin = 4,
        }

        private CellPosition m_Position;
        private e_CellOccupation m_cellOccupation;

        public MyButton()
        {
            this.InitializeComponent();
            this.m_Position = new CellPosition();
            this.m_cellOccupation = e_CellOccupation.notOccupied;
        }

        public CellPosition Position
        {
            get
            {
                return this.m_Position;
            }

            set
            {
                this.m_Position = value;
            }
        }

        public e_CellOccupation CellOccupation
        {
            get
            {
                return this.m_cellOccupation;
            }

            set
            {
                this.m_cellOccupation = value;
            }
        }

        public override string ToString()
        {
            string occupation = string.Empty;
            switch (this.m_cellOccupation)
            {
                case e_CellOccupation.notOccupied:
                    occupation = string.Empty;
                    break;
                case e_CellOccupation.topPlayerRegularCoin:
                    occupation = "O";
                    break;
                case e_CellOccupation.bottomPlayerRegularCoin:
                    occupation = "X";
                    break;
                case e_CellOccupation.topPlayerSpecialCoin:
                    occupation = "Q";
                    break;
                case e_CellOccupation.bottomPlayerSpecialCoin:
                    occupation = "K";
                    break;
            }

            return occupation;           
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}