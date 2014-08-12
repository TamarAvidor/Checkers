using System;

namespace Checkers
{
    public class ConsoleUI
    {
        public static string AskForPlayerName()
        {
            Console.WriteLine("Please enter your name (20 characters maximum without any spaces).\nPress 'Enter' to continue");
            string strName = Console.ReadLine();

            while (IsPlayerNameLegal(strName) == false)
            {
                Console.WriteLine("The name you have entered is not legal, please enter 20 characters maximum without any spaces.\nPress 'Enter' to continue");
                strName = Console.ReadLine();
            }

            return strName;
        }

        // $G$ CSS-028 (0) method shouldn't include more then one return command.
        public static bool IsPlayerNameLegal(string i_inputName)
        {
            if (i_inputName.Length > 20 || i_inputName.Contains(" "))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static uint AskForBoardSize()
        {
            string boardSizeStr;
            uint boardSize = 0;
            bool allGood = false;

            Console.WriteLine("Please enter the prefered board size.\nFor 6x6 enter 6, for 8x8 enter 8 and for 10x10 enter 10. press 'Enter' to continue");

            while (allGood == false)
            {
                boardSizeStr = Console.ReadLine();
                allGood = uint.TryParse(boardSizeStr, out boardSize);

                if ((allGood == false) || (IsBoardSizeLegal(boardSize) == false))
                {
                    Console.WriteLine("The size you have entered is not legal, please choose 6/8/10 only.\nPress 'Enter' to continue");
                    allGood = false;
                }
            }

            return boardSize;
        }

        // $G$ CSS-028 (0) method shouldn't include more then one return command.
        public static bool IsBoardSizeLegal(uint i_boardSize)
        {
            if (i_boardSize == 6 || i_boardSize == 8 || i_boardSize == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static e_GameType AskForGameType()
        {
            string typeOfGameStr;
            e_GameType userChoiceGameType = e_GameType.pVc;
            bool allGood = false;
            Console.WriteLine("Please enter which type of game you would like to have: 0 for playing with computer, 1 for playing with a friend.\nPress 'Enter' to continue");
            /* == 0 means strings are equals*/
            while (allGood == false)
            {
                typeOfGameStr = Console.ReadLine();
                /* == 0 means strings are equals*/
                if (typeOfGameStr.CompareTo("0") == 0)
                {
                    allGood = true;
                }
                else if (typeOfGameStr.CompareTo("1") == 0)
                {
                    userChoiceGameType = e_GameType.pVp;
                    allGood = true;
                }
                else
                {
                    Console.WriteLine("You did not enter a valid game type, please enter 0/1 only.\nPress 'Enter' to continue");
                }
            }

            return userChoiceGameType;
        }

        // $G$ CSS-028 (0) method shouldn't include more then one return command.
        public static bool AskForAnotherGameDesire()
        {
            string userDesireString;
            Console.WriteLine("Would you like to play another game? Please enter {0}YES {1}NO.\nPress 'Enter' to continue");
            userDesireString = Console.ReadLine();

            if (userDesireString.CompareTo("0") == 0)
            {
                return true;
            }
            else if (userDesireString.CompareTo("1") == 0)
            {
                return false;
            }
            else
            {
                Console.WriteLine("You did not enter a valid option.");
                return AskForAnotherGameDesire();
            }
        }

        public static void PrintBoard(ref Board i_boardForPrint)
        {
            char letterForPrint = 'A';

            string spaceAmount = "   ";
            string lineBufferStr = " =";
            string equalCharAmount = "====";
            char cellBufferString = '|';
            string notOccupiedCellStr = "   ";
            string topPlayerRegularCoinCellStr = " O ";
            string topPlayerSpecialCoinCellStr = " Q ";
            string bottomPlayerRegularCoinCellStr = " X ";
            string bottomPlayerSpecialCoinCellStr = " K ";

            Ex02.ConsoleUtils.Screen.Clear();

            for (int i = 0; i < i_boardForPrint.BoardSize; i++)
            {
                Console.Write("{0}{1}", spaceAmount, letterForPrint);
                letterForPrint++;

                lineBufferStr += equalCharAmount;
            }

            Console.WriteLine();
            Console.WriteLine(lineBufferStr);

            letterForPrint = 'a';
            for (int i = 0; i < i_boardForPrint.BoardSize; i++)
            {
                Console.Write("{0}{1}", letterForPrint, cellBufferString);
                letterForPrint++;

                for (int j = 0; j < i_boardForPrint.BoardSize; j++)
                {
                    switch (i_boardForPrint.GameBoard[i, j].GetCellOccupation())
                    {
                        case Cell.e_CellOccupation.notOccupied:
                            Console.Write(notOccupiedCellStr);
                            break;
                        case Cell.e_CellOccupation.topPlayerRegularCoin:
                            Console.Write(topPlayerRegularCoinCellStr);
                            break;
                        case Cell.e_CellOccupation.topPlayerSpecialCoin:
                            Console.Write(topPlayerSpecialCoinCellStr);
                            break;
                        case Cell.e_CellOccupation.bottomPlayerRegularCoin:
                            Console.Write(bottomPlayerRegularCoinCellStr);
                            break;
                        case Cell.e_CellOccupation.bottomPlayerSpecialCoin:
                            Console.Write(bottomPlayerSpecialCoinCellStr);
                            break;
                    }

                    Console.Write(cellBufferString);
                }

                Console.WriteLine();
                Console.WriteLine(lineBufferStr);
            }
        }

        public static Movement AskForPlayerMove(uint i_boardSize, ref e_GameStatus i_gameStatus)
        {
            Movement playerMovement = new Movement();
            string playerMovementStr = string.Empty;
            playerMovementStr = Console.ReadLine();
            bool allGoodMovementStr = false;
            while (allGoodMovementStr == false)
            {
                if ((playerMovementStr.Length == 5) && (playerMovementStr[2] == '>'))
                {
                    uint inputSuccessCounter = 0;
                    CellPosition tempCellPos = new CellPosition();

                    if ((playerMovementStr[0] >= 'A') && (playerMovementStr[0] <= ('A' + i_boardSize - 1)))
                    {
                        tempCellPos.Y = /*(uint)*/playerMovementStr[0] - 'A';
                        inputSuccessCounter++;
                    }

                    if ((playerMovementStr[1] >= 'a') && (playerMovementStr[1] <= ('a' + i_boardSize - 1)))
                    {
                        tempCellPos.X = /*(uint)*/playerMovementStr[1] - 'a';
                        inputSuccessCounter++;
                    }

                    playerMovement.SourcePosition = tempCellPos;

                    if ((playerMovementStr[3] >= 'A') && (playerMovementStr[3] <= ('A' + i_boardSize - 1)))
                    {
                        tempCellPos.Y = /*(uint)*/playerMovementStr[3] - 'A';
                        inputSuccessCounter++;
                    }

                    if ((playerMovementStr[4] >= 'a') && (playerMovementStr[4] <= ('a' + i_boardSize - 1)))
                    {
                        tempCellPos.X = /*(uint)*/playerMovementStr[4] - 'a';
                        inputSuccessCounter++;
                    }

                    playerMovement.DestinationPosition = tempCellPos;

                    if (inputSuccessCounter == 4)
                    {
                        allGoodMovementStr = true;
                    }
                    else
                    {
                        Console.WriteLine("You did not enter a valid input, the format should be: COLrow>COLrow.\nPress 'Enter' to continue");
                        playerMovementStr = Console.ReadLine();
                    }
                }
                else if ((playerMovementStr.Length == 1) && (playerMovementStr[0] == 'Q'))
                {
                    allGoodMovementStr = true;
                    i_gameStatus = e_GameStatus.QuitByUser;
                }
                else
                {
                    allGoodMovementStr = false;
                    Console.WriteLine("You did not enter a valid input, the format should be: COLrow>COLrow.\nPress 'Enter' to continue");
                    playerMovementStr = Console.ReadLine();
                }
            }

            return playerMovement;
        }

        public static void MovmentInstruction()
        {
            Console.WriteLine("\nPlease enter your desired movements from now on in the format of: COLrow>COLrow.For example: Gf>He\nPress 'Enter' after each move.\n");
        }

        public static void SkipOverRequiredAnnouncement()
        {
            Console.WriteLine("There is a valid Skip Over move you must implement!, enter your move again:");
        }

        public static void InvalidLogicallyMovementAnnouncement()
        {
            Console.WriteLine("The movement you have entered is illogical!, enter your move again:");
        }

        public static void NoMoreMovesAnnouncement()
        {
            Console.WriteLine("You have no more valid moves:");
        }

        public static void PrintCurrentPlayerNameAndCoin(Player i_activePlayer)
        {
            char activePlayerChar = 'X';
            if (i_activePlayer.PlayerPosition == Player.e_PlayerPosition.Top)
            {
                activePlayerChar = 'O';
            }

            Console.WriteLine("{0}'s turn ({1}):", i_activePlayer.Name, activePlayerChar);
        }

        public static void PrintLastMovementOfActivePlayer(Player i_activePlayer, Movement i_lastMovement)
        {
            Console.WriteLine("{0}'s turn was ({1}):", i_activePlayer.Name, i_lastMovement.ToString());
        }

        public static void GoodByeMessege()
        {
            Console.WriteLine("We hope you had alot of fun! Good Bye");
        }

        public static void LastRoundPointsMessege(Player i_topPlayer, int i_topPlayerPoints, Player i_bottomPlayer, int i_bottomPlayerPoints)
        {
            Console.WriteLine(
                "{0}'s points: {1},\n{2}'s points: {3}.\n{4} points were added to {5}.",
                i_topPlayer.Name,
                i_topPlayerPoints,
                i_bottomPlayer.Name,
                i_bottomPlayerPoints,
                Math.Abs(i_topPlayerPoints - i_bottomPlayerPoints),
                (i_topPlayerPoints == i_bottomPlayerPoints) ? "both players" : ((i_topPlayerPoints > i_bottomPlayerPoints) ? i_topPlayer.Name : i_bottomPlayer.Name));
        }

        public static void SoFarTotalPointsMessege(Player i_topPlayer, Player i_bottomPlayer)
        {
            Console.Write("So far ");
            TotalPointsMessege(i_topPlayer, i_bottomPlayer);
        }

        public static void EndOfGameTotalPointsMessege(Player i_topPlayer, Player i_bottomPlayer)
        {
            Console.Write("The game had ended. ");
            TotalPointsMessege(i_topPlayer, i_bottomPlayer);
        }

        public static void TotalPointsMessege(Player i_topPlayer, Player i_bottomPlayer)
        {
            Console.WriteLine(" {0} earned {1} points and {2} earned {3} points.", i_topPlayer.Name, i_topPlayer.Points, i_bottomPlayer.Name, i_bottomPlayer.Points);
        }

        public static void DeclairWinnerMessege(Player i_winnerPlayer)
        {
            Console.WriteLine("{0} has won the last round.", i_winnerPlayer.Name);
        }

        public static void DeclairTieMessege()
        {
            Console.WriteLine("The round was ended with a tie. No points added to either player");
        }
    }
}
