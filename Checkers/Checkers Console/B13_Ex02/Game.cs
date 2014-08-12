using System;

namespace Checkers
{
    public enum e_GameType
    {
        pVc = 0,
        pVp = 1
    }

    public enum e_GameStatus
    {
        QuitByUser = 0,
        NoMovesForActivePlayer = 1,
        ActiveGame = 2
    }

    public class Game
    {
        private e_GameType m_gameType;
        private Player m_topPlayer, m_bottomPlayer;
        private Board m_gameBoard;
        private Movement m_currentMovement;
        private e_GameStatus m_gameStatus;

        public Game()
        {
            m_bottomPlayer = new Player();
            /*in order to make bottom player as a computer. for checking and debugging matters*/
            //m_bottomPlayer.PlayerType = Player.e_PlayerType.Computer;
            m_gameBoard = new Board();
            m_gameType = ConsoleUI.AskForGameType();
            if (m_gameType == e_GameType.pVp)
            {
                m_topPlayer = new Player();
                m_topPlayer.PlayerPosition = Player.e_PlayerPosition.Top;
            }
            else
            {
                m_topPlayer = new Player("Computer");
            }

            // Added or old code that was replaced
            m_gameStatus = e_GameStatus.ActiveGame;
        }

        public void Start()
        {
            int topPlayerPoints, bottomPlayerPoints;

            bool keepPlaying = true;

            while (keepPlaying)
            {
                Play();

                m_gameBoard.CalculatePointsOfPlayers(out topPlayerPoints, out bottomPlayerPoints);

                if (topPlayerPoints > bottomPlayerPoints)
                {
                    ConsoleUI.DeclairWinnerMessege(m_topPlayer);
                    m_topPlayer.Points = m_topPlayer.Points + topPlayerPoints - bottomPlayerPoints;
                }
                else if (topPlayerPoints < bottomPlayerPoints)
                {
                    ConsoleUI.DeclairWinnerMessege(m_bottomPlayer);
                    m_bottomPlayer.Points = m_bottomPlayer.Points + bottomPlayerPoints - topPlayerPoints;
                }
                else
                {
                    ConsoleUI.DeclairTieMessege();
                }

                ConsoleUI.LastRoundPointsMessege(m_topPlayer, topPlayerPoints, m_bottomPlayer, bottomPlayerPoints);
                ConsoleUI.SoFarTotalPointsMessege(m_topPlayer, m_bottomPlayer);

                keepPlaying = ConsoleUI.AskForAnotherGameDesire();

                /*for automated start again and delay for cheking
                DateTime dt = DateTime.Now + TimeSpan.FromSeconds(10);
                do { } while (DateTime.Now < dt);
                keepPlaying = true;*/

                if (keepPlaying == true)
                {
                    m_gameBoard = new Board(m_gameBoard.BoardSize);
                    m_gameStatus = e_GameStatus.ActiveGame;
                }
            }

            ConsoleUI.GoodByeMessege();
        }

        // $G$ DSN-003 (-10) This method is too long. 
        private void Play()
        {
            bool isLastMovementWasSkipOver = false;
            CellPosition lastDestinatioonCellPosition = new CellPosition();

            ConsoleUI.PrintBoard(ref m_gameBoard);
            ConsoleUI.MovmentInstruction();

            Player activePlayer = m_bottomPlayer;

            while (m_gameStatus == e_GameStatus.ActiveGame)
            {
                Logic.RegularAndSkipOverMovementsLists movementsListsStruct = new Logic.RegularAndSkipOverMovementsLists();
                movementsListsStruct.RegularMovementList = new System.Collections.Generic.List<Movement>();
                movementsListsStruct.SkipOverMovementList = new System.Collections.Generic.List<Movement>();

                Logic.MakeListOfPossibleMovementsForPlayer(ref m_gameBoard, ref movementsListsStruct, activePlayer.PlayerPosition);

                if (Logic.IsStructOfListsEmpty(movementsListsStruct) == false)
                {
                    if (activePlayer.PlayerType == Player.e_PlayerType.Human)
                    {
                        ConsoleUI.PrintCurrentPlayerNameAndCoin(activePlayer);
                        m_currentMovement = ConsoleUI.AskForPlayerMove(m_gameBoard.BoardSize, ref m_gameStatus);
                    }
                    else
                    {
                        m_currentMovement = Logic.ChooseMaximalPointsMovementForPlayerFromStructOfLists(m_gameBoard, movementsListsStruct, activePlayer);
                    }

                    if ((isLastMovementWasSkipOver == false) ||
                            ((isLastMovementWasSkipOver == true) && (Movement.IsTwoPositionsEquale(lastDestinatioonCellPosition, m_currentMovement.SourcePosition) == true)))
                    {
                        if (Logic.IsMovementExistsInStructOfLists(movementsListsStruct, m_currentMovement, out isLastMovementWasSkipOver) == true)
                        {
                            if (((movementsListsStruct.SkipOverMovementList.Count != 0) &&
                                (Logic.IsMovementExistsInSpecificList(movementsListsStruct.RegularMovementList, m_currentMovement) == true)) == false)
                            {
                                m_gameBoard.ExcuteMovement(m_currentMovement);

                                ConsoleUI.PrintBoard(ref m_gameBoard);
                                ConsoleUI.PrintLastMovementOfActivePlayer(activePlayer, m_currentMovement);
                                lastDestinatioonCellPosition = m_currentMovement.DestinationPosition;
                                if (isLastMovementWasSkipOver == false)
                                {
                                    TogglePlayersTurns(ref activePlayer);
                                }
                                else
                                {
                                    Logic.MakeListOfPossibleMovementsForPlayer(ref m_gameBoard, ref movementsListsStruct, activePlayer.PlayerPosition);
                                    if (Logic.IsMovementFromSpecificCellExistInList(movementsListsStruct.SkipOverMovementList, lastDestinatioonCellPosition) == false)
                                    {
                                        isLastMovementWasSkipOver = false;
                                        TogglePlayersTurns(ref activePlayer);
                                    }
                                }
                            }
                            else
                            {
                                ConsoleUI.SkipOverRequiredAnnouncement();
                            }
                        }
                        else
                        {
                            if (m_gameStatus != e_GameStatus.QuitByUser)
                            {
                                ConsoleUI.InvalidLogicallyMovementAnnouncement();
                            }
                        }
                    }
                }
                else
                {
                    ConsoleUI.NoMoreMovesAnnouncement();
                    m_gameStatus = e_GameStatus.NoMovesForActivePlayer;
                }
            }
        }

        private Movement TakeCurrentPlayerActions(Player i_activePlayer)
        {
            ConsoleUI.PrintCurrentPlayerNameAndCoin(i_activePlayer);

            return ConsoleUI.AskForPlayerMove(m_gameBoard.BoardSize, ref m_gameStatus);
        }

        private void TogglePlayersTurns(ref Player io_formerActivePlayer)
        {
            if (io_formerActivePlayer.PlayerPosition == Player.e_PlayerPosition.Top)
            {
                io_formerActivePlayer = m_bottomPlayer;
            }
            else
            {
                io_formerActivePlayer = m_topPlayer;
            }
        }
    }
}
