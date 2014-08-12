using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05.CheckersForWindows
{
    public partial class GameWindow : Form
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

        private Board m_Board;
        private Movement m_currentMovement;
        private e_GameType m_gameType;
        private Player m_topPlayer, m_bottomPlayer, m_activePlayer;
        private e_GameStatus m_gameStatus;
        private MyButton m_firstPressedButton;
        private MyButton m_secondPressedButton;
        private GameSettingsWindow m_settings;

        private bool m_isSecondClick = false;
        private bool m_isFirstClick = false;
        private bool m_isLastMovementWasSkipOver = false;
        private bool m_keepPlaying = true;
        private CellPosition m_lastDestinationCellPosition;
        private Logic.RegularAndSkipOverMovementsLists m_movementsListsStruct;

        public GameWindow()
        {
            this.InitializeComponent();
            this.m_settings = new GameSettingsWindow();
            if (this.m_settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.InitializeAllMembers();
                this.Start();
            }
        }

        private void InitializeAllMembers()
        {
            this.InitializeGameMembers();
            this.InitializeDesignMembers();
        }

        private void InitializeDesignMembers()
        {
            labelPlayerOne.Text = string.Format("{0}:", this.m_bottomPlayer.Name);
            labelPlayerTwo.Text = string.Format("{0}:", this.m_topPlayer.Name);
            AdjustLabelOfCurrentPlayerTheSignLabel(labelPlayerOne);
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        }

        private void AdjustLabelOfCurrentPlayerTheSignLabel(Label i_labelOfCurrentPlayer)
        {
            labelCurrentPlayerSign.Left = i_labelOfCurrentPlayer.Right +5;
            labelCurrentPlayerSign.Top = i_labelOfCurrentPlayer.Top;
            labelCurrentPlayerSign.Text = "<-";
        }

        private void InitializeGameMembers()
        {
            this.m_Board = new Board(this.m_settings.BoardSize);
            this.SetMyButtonsLocation(this.m_Board.BoardSize);
            this.m_lastDestinationCellPosition = new CellPosition();
            this.m_movementsListsStruct = new Logic.RegularAndSkipOverMovementsLists();
            this.m_movementsListsStruct.RegularMovementList = new System.Collections.Generic.List<Movement>();
            this.m_movementsListsStruct.SkipOverMovementList = new System.Collections.Generic.List<Movement>();
            this.m_currentMovement = new Movement();
            bool computerEnabled = this.m_settings.ComputerEnabled;
            this.m_bottomPlayer = new Player(this.m_settings.FirstPlayerName);
            if (computerEnabled == true)
            {
                this.m_gameType = e_GameType.pVc;
                this.m_topPlayer = new Player(this.m_settings.SecondPlayerName, computerEnabled);
            }
            else
            {
                this.m_gameType = e_GameType.pVp;
                this.m_topPlayer = new Player(this.m_settings.SecondPlayerName);
                this.m_topPlayer.PlayerPosition = Player.e_PlayerPosition.Top;
            }

            this.m_gameStatus = e_GameStatus.ActiveGame;
            this.m_activePlayer = this.m_bottomPlayer;
        }

        private void Start()
        {
            int topPlayerPoints, bottomPlayerPoints;
            while (this.m_keepPlaying)
            {
                this.Play();
                this.CalculatePoints(out topPlayerPoints, out bottomPlayerPoints);
                if (this.m_gameStatus != e_GameStatus.QuitByUser)
                {
                    string msgwon = string.Format("{0} won!!\nanother round?", this.m_activePlayer.Name);
                    if (MessageBox.Show(msgwon, "New Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.CreateNextGame(topPlayerPoints, bottomPlayerPoints);
                    }
                    else
                    {
                        this.Quit();
                    }
                }
                else
                {
                    this.Quit();
                }
            }
        }

        private void CreateNextGame(int i_topPlayerPoints, int i_bottomPlayerPoints)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            this.InitializeGameMembers();
            labelPlayerOne.Text = string.Format("{0}: {1}", this.m_bottomPlayer.Name, i_bottomPlayerPoints);
            labelPlayerTwo.Text = string.Format("{0}: {1}", this.m_topPlayer.Name, i_topPlayerPoints);
            AdjustLabelOfCurrentPlayerTheSignLabel(labelPlayerOne);
        }

        private void Play()
        {
            this.PrintTextButtons(this.m_Board.BoardSize);
            while (this.m_gameStatus == e_GameStatus.ActiveGame)
            {
                if (this.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    if (MessageBox.Show("Are You Sure You Want To Quit?", "Quit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.m_gameStatus = e_GameStatus.QuitByUser;
                    }
                }
            }
        }

        private void TogglePlayersTurns(ref Player io_formerActivePlayer)
        {
            if (io_formerActivePlayer.PlayerPosition == Player.e_PlayerPosition.Top)
            {
                io_formerActivePlayer = this.m_bottomPlayer;
                AdjustLabelOfCurrentPlayerTheSignLabel(labelPlayerOne);
            }
            else
            {
                io_formerActivePlayer = this.m_topPlayer;
                AdjustLabelOfCurrentPlayerTheSignLabel(labelPlayerTwo);
            }
        }

        private void PrintTextButtons(int i_BoardSize)
        {
            foreach (MyButton button in this.m_Board.GameBoard)
            {
                button.Text = button.ToString();
            }
        }

        private void SetMyButtonsLocation(int i_boardSize)
        {
            this.m_Board.GameBoard[0, 0].Location = new Point(labelPlayerTwo.Left, this.labelPlayerTwo.Bottom);
            this.Controls.Add(this.m_Board.GameBoard[0, 0]);
            for (int i = 1, j = 0; i < i_boardSize; i++)
            {
                this.m_Board.GameBoard[i, j].Location = new Point(this.m_Board.GameBoard[i - 1, j].Left, this.m_Board.GameBoard[i - 1, j].Bottom);
                this.Controls.Add(this.m_Board.GameBoard[i, j]);
            }

            for (int i = 0; i < i_boardSize; i++)
            {
                for (int j = 1; j < i_boardSize; j++)
                {
                    this.m_Board.GameBoard[i, j].Location = new Point(this.m_Board.GameBoard[i, j - 1].Right, this.m_Board.GameBoard[i, j - 1].Top);
                    this.Controls.Add(this.m_Board.GameBoard[i, j]);
                }
            }

            foreach (MyButton button in this.m_Board.GameBoard)
            {
                button.Click += new EventHandler(this.Button_Click);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MyButton b = sender as MyButton;
            this.SaveCurrentButton(b);
            this.ChangeColor();
            if (Logic.IsStructOfListsEmpty(this.m_movementsListsStruct) == false)
            {
                this.PositionUpdateForCurrentMovement(b);
                if (this.m_isFirstClick == true && this.m_isSecondClick == true)
                {
                    this.m_isFirstClick = false;
                    this.m_isSecondClick = false;
                    if ((this.m_isLastMovementWasSkipOver == false) ||
                            ((this.m_isLastMovementWasSkipOver == true) && (Movement.IsTwoPositionsEquale(this.m_lastDestinationCellPosition, this.m_currentMovement.SourcePosition) == true)))
                    {
                        this.IsMovementExistsInStructOfLists();
                    }
                }
            }
            else
            {
                this.NoMoreMovesForActivePlayerAndClose();
            }
        }

        private void IsMovementExistsInStructOfLists()
        {
            if (Logic.IsMovementExistsInStructOfLists(this.m_movementsListsStruct, this.m_currentMovement, out this.m_isLastMovementWasSkipOver) == true)
            {
                this.IsMustSkipOrHaveRegularMovement();
            }
            else
            {
                if (this.m_firstPressedButton == this.m_secondPressedButton)
                {
                    this.ChangeColor();
                }
                else
                {
                    this.MessegeBox("Invalid Movement");
                }
            }
        }

        private void IsMustSkipOrHaveRegularMovement()
        {
            if (((this.m_movementsListsStruct.SkipOverMovementList.Count != 0) &&
                (Logic.IsMovementExistsInSpecificList(this.m_movementsListsStruct.RegularMovementList, this.m_currentMovement) == true)) == false)
            {
                this.ExecuteMoves();
            }
            else
            {
                this.MessegeBox("Skip Over Required");
            }
        }

        private void ExecuteMoves()
        {
            this.m_Board.ExcuteMovement(this.m_currentMovement);
            this.PrintTextButtons(this.m_Board.BoardSize);
            this.m_firstPressedButton.BackColor = Color.Transparent;
            this.m_lastDestinationCellPosition = this.m_currentMovement.DestinationPosition;
            this.IsLastMovementWasSkipOver();
        }

        private void IsLastMovementWasSkipOver()
        {
            if (this.m_isLastMovementWasSkipOver == false)
            {
                this.TogglePlayersTurns(ref this.m_activePlayer);
                if (this.m_activePlayer.PlayerType == Player.e_PlayerType.Computer)
                {
                    this.ComputerMovement();
                }
            }
            else
            {
                this.CheckForAnotherTurnAfterSkipOver();
            }
        }

        private void CheckForAnotherTurnAfterSkipOver()
        {
            Logic.MakeListOfPossibleMovementsForPlayer(ref this.m_Board, ref this.m_movementsListsStruct, this.m_activePlayer.PlayerPosition);
            if (Logic.IsStructOfListsEmpty(this.m_movementsListsStruct) == false)
            {
                if (Logic.IsMovementFromSpecificCellExistInList(this.m_movementsListsStruct.SkipOverMovementList, this.m_lastDestinationCellPosition) == false)
                {
                    this.m_isLastMovementWasSkipOver = false;
                    this.TogglePlayersTurns(ref this.m_activePlayer);
                    if (this.m_activePlayer.PlayerType == Player.e_PlayerType.Computer)
                    {
                        this.ComputerMovement();
                    }
                }
            }
            else
            {
                this.NoMoreMovesForActivePlayerAndClose();
            }
        }

        private void ComputerMovement()
        {
            Logic.MakeListOfPossibleMovementsForPlayer(ref this.m_Board, ref this.m_movementsListsStruct, this.m_activePlayer.PlayerPosition);
            if (Logic.IsStructOfListsEmpty(this.m_movementsListsStruct) == false)
            {
                this.m_currentMovement = Logic.ChooseMaximalPointsMovementForPlayerFromStructOfLists(this.m_Board, this.m_movementsListsStruct, this.m_activePlayer); // for computer
                this.ExecuteMoves();
            }
            else
            {
                this.NoMoreMovesForActivePlayerAndClose();
            }
        }

        private void SaveCurrentButton(MyButton b)
        {
            if (this.m_isFirstClick == false)
            {
                this.m_firstPressedButton = b;
                Logic.MakeListOfPossibleMovementsForPlayer(ref this.m_Board, ref this.m_movementsListsStruct, this.m_activePlayer.PlayerPosition);
            }
            else
            {
                this.m_secondPressedButton = b;
                if (this.m_firstPressedButton == this.m_secondPressedButton)
                {
                    this.ChangeColor();
                }
            }
        }

        private void ChangeColor()
        {
            if (this.m_firstPressedButton.CellOccupation != MyButton.e_CellOccupation.notOccupied)
            {
                if (this.m_firstPressedButton.BackColor == Color.LightBlue)
                {
                    this.m_firstPressedButton.BackColor = Color.Transparent;
                }
                else
                {
                    this.m_firstPressedButton.BackColor = Color.LightBlue;
                }
            }
        }

        private void PositionUpdateForCurrentMovement(MyButton b)
        {
            if (this.m_isFirstClick == true)
            {
                this.m_currentMovement.DestinationPosition = b.Position;
                this.m_isSecondClick = true;
            }
            else
            {
                this.m_isFirstClick = true;
                this.m_currentMovement.SourcePosition = b.Position;
            }
        }

        private void CalculatePoints(out int i_topPlayerPoints, out int i_bottomPlayerPoints)
        {
            this.m_Board.CalculatePointsOfPlayers(out i_topPlayerPoints, out i_bottomPlayerPoints);

            if (i_topPlayerPoints > i_bottomPlayerPoints)
            {
                this.m_topPlayer.Points = this.m_topPlayer.Points + i_topPlayerPoints - i_bottomPlayerPoints;
                this.m_activePlayer = this.m_topPlayer;
            }
            else if (i_topPlayerPoints < i_bottomPlayerPoints)
            {
                this.m_bottomPlayer.Points = this.m_bottomPlayer.Points + i_bottomPlayerPoints - i_topPlayerPoints;
                this.m_activePlayer = this.m_bottomPlayer;
            }
        }

        private void NoMoreMovesForActivePlayerAndClose()
        {
            this.MessegeBox("No More Moves");
            this.m_gameStatus = e_GameStatus.NoMovesForActivePlayer;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void MessegeBox(string msg)
        {
            MessageBox.Show(msg);
            this.m_firstPressedButton.BackColor = Color.Transparent;
        }

        private void Quit()
        {
            this.m_keepPlaying = false;
            MessageBox.Show("Good Bye");
        }
    }
}
