using System;
using System.Collections.Generic;

namespace Ex05.CheckersForWindows
{
    public class Logic
    {
        public struct RegularAndSkipOverMovementsLists
        {
            internal List<Movement> RegularMovementList;
            internal List<Movement> SkipOverMovementList;
        }

        public static Movement ChooseRandomlyMovementFromStructOfLists(RegularAndSkipOverMovementsLists i_structOfPossibleMovementsForPlayer)
        {
            Movement movementToRet = new Movement();
            Random randomGenerator = new Random();
            int indexInList;
            int sizeOfList;

            sizeOfList = i_structOfPossibleMovementsForPlayer.SkipOverMovementList.Count;

            if (sizeOfList != 0)
            {
                indexInList = randomGenerator.Next(i_structOfPossibleMovementsForPlayer.SkipOverMovementList.Count);
                movementToRet = i_structOfPossibleMovementsForPlayer.SkipOverMovementList[indexInList];
            }
            else
            {
                sizeOfList = i_structOfPossibleMovementsForPlayer.RegularMovementList.Count;
                if (sizeOfList != 0)
                {
                    indexInList = randomGenerator.Next(i_structOfPossibleMovementsForPlayer.RegularMovementList.Count);
                    movementToRet = i_structOfPossibleMovementsForPlayer.RegularMovementList[indexInList];
                }
            }

            return movementToRet;
        }

        public static void MakeListOfPossibleMovementsForPlayer(ref Board i_board, ref RegularAndSkipOverMovementsLists io_structOfPossibleMovementsForPlayer, Player.e_PlayerPosition i_playerPosition)
        {
            io_structOfPossibleMovementsForPlayer.RegularMovementList.Clear();
            io_structOfPossibleMovementsForPlayer.SkipOverMovementList.Clear();

            List<CoinInformation> listOfCoinForActivePlayer;
            if (i_playerPosition == Player.e_PlayerPosition.Top)
            {
                listOfCoinForActivePlayer = i_board.TopPlayerListOfCoins;
            }
            else
            {
                listOfCoinForActivePlayer = i_board.BottomPlayerListOfCoins;
            }

            foreach (CoinInformation currentCoin in listOfCoinForActivePlayer)
            {
                AddPossibleMovementsToStructFromSpecificCell(ref i_board, ref io_structOfPossibleMovementsForPlayer, currentCoin, i_playerPosition);
            }
        }

        public static void AddPossibleMovementsToStructFromSpecificCell(ref Board i_board, ref RegularAndSkipOverMovementsLists i_structOfLists, CoinInformation i_coin, Player.e_PlayerPosition i_playerPosition)

        // Will be called from inside "for"- which means each cell of the list will be checked for possible moves and the found possible move will be put into the relevant list in the struct
        {
            int horizontalStepSize = 1; // growing direction, topPlayer moving downwards

            if (i_playerPosition == Player.e_PlayerPosition.Bottom)
            {
                horizontalStepSize = -1; // bottomPlayer moving upwards
            }

            CellPosition targetCell = new CellPosition();

            targetCell.X = /*need to make the x and y in cellposition to int and not a uint, 
                            * due to this change need to remove the force casting to uing here 
                            * and the same few lines later in this code*//*(uint)*/(int)i_coin.CellPosition.X + horizontalStepSize;
            targetCell.Y = i_coin.CellPosition.Y - 1;
            AddPossibleMovementsToStructFromSpecificCellToSpecificDirection(ref i_board, ref i_structOfLists, i_coin, i_playerPosition, targetCell);
            targetCell.Y = i_coin.CellPosition.Y + 1;
            AddPossibleMovementsToStructFromSpecificCellToSpecificDirection(ref i_board, ref i_structOfLists, i_coin, i_playerPosition, targetCell);
            if ((i_coin.CoinType == MyButton.e_CellOccupation.topPlayerSpecialCoin) || (i_coin.CoinType == MyButton.e_CellOccupation.bottomPlayerSpecialCoin))
            {
                ////also downwards
                targetCell.X = /*(uint)*/(int)i_coin.CellPosition.X - horizontalStepSize;
                targetCell.Y = i_coin.CellPosition.Y - 1;
                AddPossibleMovementsToStructFromSpecificCellToSpecificDirection(ref i_board, ref i_structOfLists, i_coin, i_playerPosition, targetCell);
                targetCell.Y = i_coin.CellPosition.Y + 1;
                AddPossibleMovementsToStructFromSpecificCellToSpecificDirection(ref i_board, ref i_structOfLists, i_coin, i_playerPosition, targetCell);
            }
        }

        public static void AddPossibleMovementsToStructFromSpecificCellToSpecificDirection(ref Board i_board, ref RegularAndSkipOverMovementsLists i_structOfLists, CoinInformation i_coin, Player.e_PlayerPosition i_playerPosition, CellPosition i_targetCell)
        {
            MyButton.e_CellOccupation regularCoinOfOpponent = MyButton.e_CellOccupation.bottomPlayerRegularCoin,
                                    specialCoinOfOpponent = MyButton.e_CellOccupation.bottomPlayerSpecialCoin; // assuming top player is the active one
            //// if top player is NOT the active one...change will be made

            if (i_playerPosition == Player.e_PlayerPosition.Bottom)
            {
                regularCoinOfOpponent = MyButton.e_CellOccupation.topPlayerRegularCoin;
                specialCoinOfOpponent = MyButton.e_CellOccupation.topPlayerSpecialCoin;
            }

            //// Checking weather target cell is inside the board bounderies

            if ((i_targetCell.X >= 0) &&
                    (i_targetCell.X <= i_board.BoardSize - 1) &&
                    (i_targetCell.Y >= 0) &&
                    (i_targetCell.Y <= i_board.BoardSize - 1))
            {
                MyButton.e_CellOccupation releventCellOccupation = i_board.GameBoard[i_targetCell.X, i_targetCell.Y].CellOccupation;

                Movement currentMovementToInsert = new Movement();
                currentMovementToInsert.SourcePosition = i_coin.CellPosition;
                if (releventCellOccupation == MyButton.e_CellOccupation.notOccupied)
                {
                    currentMovementToInsert.DestinationPosition = i_targetCell;

                    i_structOfLists.RegularMovementList.Add(currentMovementToInsert);
                }
                else if ((releventCellOccupation == regularCoinOfOpponent) || (releventCellOccupation == specialCoinOfOpponent))
                {
                    int sourceX = i_coin.CellPosition.X,
                        sourceY = i_coin.CellPosition.Y; // keeping the values of sourceX and sourceY to calculate the vector of movement together with i_tagerCell in order to calculate the skipOver cell
                    CellPosition skipOverTargetCell = new CellPosition();

                    skipOverTargetCell.X = i_targetCell.X + (i_targetCell.X - sourceX);
                    skipOverTargetCell.Y = i_targetCell.Y + (i_targetCell.Y - sourceY);

                    if ((skipOverTargetCell.X >= 0) &&
                        (skipOverTargetCell.X <= i_board.BoardSize - 1) &&
                        (skipOverTargetCell.Y >= 0) &&
                        (skipOverTargetCell.Y <= i_board.BoardSize - 1))
                    {
                        releventCellOccupation = i_board.GameBoard[skipOverTargetCell.X, skipOverTargetCell.Y].CellOccupation;
                        if (releventCellOccupation == MyButton.e_CellOccupation.notOccupied)
                        {
                            currentMovementToInsert.DestinationPosition = skipOverTargetCell;

                            i_structOfLists.SkipOverMovementList.Add(currentMovementToInsert);
                        }
                    }
                }
            }
        }

        public static bool IsStructOfListsEmpty(RegularAndSkipOverMovementsLists i_structOfLists)
        {
            bool isStructOfListsEmpty = true;
            if ((i_structOfLists.RegularMovementList.Count != 0) || (i_structOfLists.SkipOverMovementList.Count != 0))
            {
                isStructOfListsEmpty = false;
            }

            return isStructOfListsEmpty;
        }

        public static bool IsMovementExistsInStructOfLists(RegularAndSkipOverMovementsLists i_structOfLists, Movement i_currentMovement, out bool o_isSkipOver)
        {
            bool retVal = false;
            o_isSkipOver = false;

            if (IsMovementExistsInSpecificList(i_structOfLists.SkipOverMovementList, i_currentMovement) == true)
            {
                o_isSkipOver = true;
                retVal = true;
            }
            else if (IsMovementExistsInSpecificList(i_structOfLists.RegularMovementList, i_currentMovement) == true)
            {
                retVal = true;
            }

            return retVal;
        }

        public static bool IsMovementExistsInSpecificList(List<Movement> i_listOfMovements, Movement i_currentMovement)
        {
            bool isMovementExistsInSpecificList = false;
            foreach (Movement currentMovement in i_listOfMovements)
            {
                if (IsTwoMovementsEquale(currentMovement, i_currentMovement) == true)
                {
                    isMovementExistsInSpecificList = true;
                }
            }

            return isMovementExistsInSpecificList;
        }

        private static bool IsTwoMovementsEquale(Movement i_firstMovement, Movement i_secondMovement)
        {
            bool isTwoMovementsEquale = false;
            if ((Movement.IsTwoPositionsEquale(i_firstMovement.SourcePosition, i_secondMovement.SourcePosition) == true) &&
                (Movement.IsTwoPositionsEquale(i_firstMovement.DestinationPosition, i_secondMovement.DestinationPosition) == true))
            {
                isTwoMovementsEquale = true;
            }

            return isTwoMovementsEquale;
        }

        public static bool IsMovementFromSpecificCellExistInList(List<Movement> i_listOfMovements, CellPosition i_sourceCellPosition)
        {
            bool isMovementFromSpecificCellExistInList = false;
            foreach (Movement currentMovement in i_listOfMovements)
            {
                if (Movement.IsTwoPositionsEquale(currentMovement.SourcePosition, i_sourceCellPosition) == true)
                {
                    isMovementFromSpecificCellExistInList = true;
                }
            }

            return isMovementFromSpecificCellExistInList;
        }

        public static Movement ChooseMaximalPointsMovementForPlayerFromStructOfLists(Board i_board, RegularAndSkipOverMovementsLists i_structOfPossibleMovementsForPlayer, Player i_player)
        {
            Movement movementToRet = new Movement();
            int sizeOfList = i_structOfPossibleMovementsForPlayer.SkipOverMovementList.Count;

            if (sizeOfList != 0)
            {
                movementToRet = ChooseMaximalPointsMovementForPlayerFromList(i_board, i_structOfPossibleMovementsForPlayer.SkipOverMovementList, i_player);
            }
            else
            {
                movementToRet = ChooseRandomlyMovementFromStructOfLists(i_structOfPossibleMovementsForPlayer);
            }

            return movementToRet;
        }

        private static Movement ChooseMaximalPointsMovementForPlayerFromList(Board i_board, List<Movement> i_listOfMovements, Player i_player)
        {
            int maximalPoints = CalculateDifferencePointsAfterChosenMovement(i_board, i_listOfMovements[0], i_player);
            int currentPoints = 0;
            Movement maximalPointsMovement = i_listOfMovements[0];

            foreach (Movement currentMovement in i_listOfMovements)
            {
                currentPoints = CalculateDifferencePointsAfterChosenMovement(i_board, currentMovement, i_player);

                if (currentPoints > maximalPoints)
                {
                    maximalPointsMovement = currentMovement;
                }
            }

            return maximalPointsMovement;
        }

        private static int CalculateDifferencePointsAfterChosenMovement(Board i_board, Movement i_movement, Player i_player)
        {
            Board tempBoard = new Board(i_board.BoardSize);
            tempBoard.Clone(i_board);

            tempBoard.ExcuteMovement(i_movement);

            int topPlayerPoints, bottomPlayerPoints;

            tempBoard.CalculatePointsOfPlayers(out topPlayerPoints, out bottomPlayerPoints);

            int deltaPoints = topPlayerPoints - bottomPlayerPoints;

            if (i_player.PlayerPosition == Player.e_PlayerPosition.Bottom)
            {
                deltaPoints *= -1;
            }

            return deltaPoints;
        }
    }
}
