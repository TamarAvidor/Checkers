namespace Ex05.CheckersForWindows
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Board
    {
        private MyButton[,] m_gameBoard;
        private int m_boardSize;
        private List<CoinInformation> topPlayerListOfCoins;
        private List<CoinInformation> bottomPlayerListOfCoins;

        public Board(int i_boardSize) ////constructor
        {
            CellPosition temp = new CellPosition();
            m_boardSize = i_boardSize;
            m_gameBoard = new MyButton[m_boardSize, m_boardSize];

            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    m_gameBoard[i, j] = new MyButton();
                    temp.X = i;
                    temp.Y = j;
                    m_gameBoard[i, j].Position = temp;
                    m_gameBoard[i, j].Size = new Size(50, 50);
                    m_gameBoard[i, j].Visible = true;
                    if ((i + j) % 2 == 0)
                    {
                        m_gameBoard[i, j].Enabled = false;
                        m_gameBoard[i, j].BackColor = Color.Gray;
                    }
                }
            }

            topPlayerListOfCoins = new List<CoinInformation>();
            bottomPlayerListOfCoins = new List<CoinInformation>();
            InitializeBoard();
        }

        public void Clone(Board i_board)
        {
            m_boardSize = i_board.m_boardSize;

            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    m_gameBoard[i, j].CellOccupation = i_board.m_gameBoard[i, j].CellOccupation;
                }
            }

            foreach (CoinInformation currentCoin in i_board.topPlayerListOfCoins)
            {
                topPlayerListOfCoins.Add(currentCoin);
            }

            foreach (CoinInformation currentCoin in i_board.bottomPlayerListOfCoins)
            {
                bottomPlayerListOfCoins.Add(currentCoin);
            }
        }

        public List<CoinInformation> TopPlayerListOfCoins
        {
            get
            {
                return topPlayerListOfCoins;
            }
        }

        public List<CoinInformation> BottomPlayerListOfCoins
        {
            get
            {
                return bottomPlayerListOfCoins;
            }
        }

        public MyButton[,] GameBoard
        {
            get
            {
                return m_gameBoard;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_boardSize;
            }
        }

        private void InitializeBoard() ////builds the first board by the given size
        {
            CoinInformation currentCoinInformation = new CoinInformation();

            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    if (i < (m_boardSize / 2) - 1)
                    {
                        if ((j + i) % 2 != 0)
                        {
                            m_gameBoard[i, j].Enabled = true;
                            m_gameBoard[i, j].CellOccupation = MyButton.e_CellOccupation.topPlayerRegularCoin;

                            currentCoinInformation.CellPosition = m_gameBoard[i, j].Position;
                            currentCoinInformation.CoinType = MyButton.e_CellOccupation.topPlayerRegularCoin;

                            topPlayerListOfCoins.Add(currentCoinInformation); ////Ready list of a new board with each coin of the top player
                        }
                    }
                    else if (i > m_boardSize / 2)
                    {
                        if ((j + i) % 2 != 0)
                        {
                            m_gameBoard[i, j].Enabled = true;
                            m_gameBoard[i, j].CellOccupation = MyButton.e_CellOccupation.bottomPlayerRegularCoin;

                            currentCoinInformation.CellPosition = m_gameBoard[i, j].Position;
                            currentCoinInformation.CoinType = MyButton.e_CellOccupation.bottomPlayerRegularCoin;

                            bottomPlayerListOfCoins.Add(currentCoinInformation); ////Ready list of a new board with each coin of the bottom player
                        }
                    }
                }
            }
        }

        public void ExcuteMovement(Movement i_playerMove) //// gets legal move and updates the board(and update the player's lists) correspondetly
        {
            int sourceX = i_playerMove.SourcePosition.X,
                    sourceY = i_playerMove.SourcePosition.Y,
                    destinationX = i_playerMove.DestinationPosition.X,
                    destinationY = i_playerMove.DestinationPosition.Y;

            /*cases we are interested in: top or bottom*/
            switch (m_gameBoard[sourceX, sourceY].CellOccupation)
            {
                case MyButton.e_CellOccupation.topPlayerRegularCoin:
                case MyButton.e_CellOccupation.topPlayerSpecialCoin:
                    ChangeCoinPositionInListOfCoins(ref topPlayerListOfCoins, i_playerMove);
                    if (destinationX == (m_boardSize - 1))
                    {
                        m_gameBoard[sourceX, sourceY].CellOccupation = MyButton.e_CellOccupation.topPlayerSpecialCoin;
                    }

                    break;
                case MyButton.e_CellOccupation.bottomPlayerRegularCoin:
                case MyButton.e_CellOccupation.bottomPlayerSpecialCoin:
                    ChangeCoinPositionInListOfCoins(ref bottomPlayerListOfCoins, i_playerMove);
                    if (destinationX == 0)
                    {
                        m_gameBoard[sourceX, sourceY].CellOccupation = MyButton.e_CellOccupation.bottomPlayerSpecialCoin;
                    }

                    break;
            }

            m_gameBoard[destinationX, destinationY].CellOccupation = m_gameBoard[sourceX, sourceY].CellOccupation;
            m_gameBoard[sourceX, sourceY].CellOccupation = MyButton.e_CellOccupation.notOccupied;

            /*skip over was made. it has no meaning checking sourceY - destinationY...the same*/
            if (Math.Abs((int)sourceX - (int)destinationX) == 2)
            {
                CellPosition cellPositionToRemove = new CellPosition();
                cellPositionToRemove.X = (sourceX + destinationX) / 2;
                cellPositionToRemove.Y = (sourceY + destinationY) / 2;

                if (RemoveCoinFromListOfCoins(ref topPlayerListOfCoins, cellPositionToRemove) == false)
                {
                    RemoveCoinFromListOfCoins(ref bottomPlayerListOfCoins, cellPositionToRemove);
                }

                m_gameBoard[cellPositionToRemove.X, cellPositionToRemove.Y].CellOccupation = MyButton.e_CellOccupation.notOccupied;
            }
        }

        public bool RemoveCoinFromListOfCoins(ref List<CoinInformation> i_listOfCoins, CellPosition i_cellPosition)
        //// Will be used when skip over was made
        {
            for (int i = 0; i < i_listOfCoins.Count; i++)
            {
                if ((i_listOfCoins[i].CellPosition.X == i_cellPosition.X) && (i_listOfCoins[i].CellPosition.Y == i_cellPosition.Y))
                {
                    i_listOfCoins.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool ChangeCoinPositionInListOfCoins(ref List<CoinInformation> i_listOfCoins, Movement i_coinMovement)
        {
            int sourceX = i_coinMovement.SourcePosition.X,
                    sourceY = i_coinMovement.SourcePosition.Y,
                    destinationX = i_coinMovement.DestinationPosition.X;

            for (int i = 0; i < i_listOfCoins.Count; i++)
            {
                /*better use with the new methode:
                if(Logic.IsTwoPositionsEquale(i_listOfCoins[i].CellPosition, i_coinMovement.SourcePosition) == true)*/
                if ((i_listOfCoins[i].CellPosition.X == sourceX) && (i_listOfCoins[i].CellPosition.Y == sourceY))
                {
                    CoinInformation currentCoinUpdatedInformation = new CoinInformation();
                    currentCoinUpdatedInformation.CellPosition = i_coinMovement.DestinationPosition;

                    switch (m_gameBoard[sourceX, sourceY].CellOccupation)
                    {
                        case MyButton.e_CellOccupation.topPlayerRegularCoin:
                        case MyButton.e_CellOccupation.topPlayerSpecialCoin:
                            if (destinationX == (m_boardSize - 1))
                            {
                                currentCoinUpdatedInformation.CoinType = MyButton.e_CellOccupation.topPlayerSpecialCoin;
                            }
                            else
                            {
                                currentCoinUpdatedInformation.CoinType = i_listOfCoins[i].CoinType;
                            }

                            break;
                        case MyButton.e_CellOccupation.bottomPlayerRegularCoin:
                        case MyButton.e_CellOccupation.bottomPlayerSpecialCoin:
                            if (destinationX == 0)
                            {
                                currentCoinUpdatedInformation.CoinType = MyButton.e_CellOccupation.bottomPlayerSpecialCoin;
                            }
                            else
                            {
                                currentCoinUpdatedInformation.CoinType = i_listOfCoins[i].CoinType;
                            }

                            break;
                    }

                    i_listOfCoins.RemoveAt(i);
                    i_listOfCoins.Add(currentCoinUpdatedInformation);
                    return true;
                }
            }

            return false;
        }

        public void CalculatePointsOfPlayers(out int o_topPlayerPoints, out int o_bottomPlayerPoints)
        {
            o_topPlayerPoints = 0;
            o_bottomPlayerPoints = 0;

            foreach (CoinInformation currentCoin in topPlayerListOfCoins)
            {
                if (currentCoin.CoinType == MyButton.e_CellOccupation.topPlayerSpecialCoin)
                {
                    o_topPlayerPoints += 4;
                }
                else
                {
                    o_topPlayerPoints++;
                }
            }

            foreach (CoinInformation currentCoin in bottomPlayerListOfCoins)
            {
                if (currentCoin.CoinType == MyButton.e_CellOccupation.bottomPlayerSpecialCoin)
                {
                    o_bottomPlayerPoints += 4;
                }
                else
                {
                    o_bottomPlayerPoints++;
                }
            }
        }
    }
}
