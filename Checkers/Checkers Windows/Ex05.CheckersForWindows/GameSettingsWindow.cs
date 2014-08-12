using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05.CheckersForWindows
{
    public partial class GameSettingsWindow : Form
    {
        private int m_BoardSize = 6;
        private string m_firstPlayerName;
        private string m_secondPlayerName = "Computer";
        private bool m_isComputerEnabled = true;

        public GameSettingsWindow()
        {
            this.InitializeComponent();
        }

        public bool ComputerEnabled
        {
            get
            {
                return this.m_isComputerEnabled;
            }
        }

        public int BoardSize
        {
            get
            {
                return this.m_BoardSize;
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return this.m_firstPlayerName;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return this.m_secondPlayerName;
            }
        }

        private void radioEightByEight_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardSize = 8;
        }

        private void radioTenByTen_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardSize = 10;
        }

        private void textBoxPlayer1_TextChanged(object sender, EventArgs e)
        {
            this.m_firstPlayerName = textBoxPlayer1.Text;
        }

        private void textBoxPlayer2_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxSecondPlayer.Checked == true)
            {
                this.m_secondPlayerName = textBoxPlayer2.Text;
            }
        }

        private void MyButtonDone_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer1.Text == string.Empty)
            {
                MessageBox.Show("Please Enter A Name For Player 1");
            }
            else if (checkBoxSecondPlayer.Enabled == true && textBoxPlayer2.Text == string.Empty)
            {
                MessageBox.Show("Please Enter A Name For Player 2");
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void checkBoxSecondPlayer_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxSecondPlayer.Checked == true)
            {
                this.m_isComputerEnabled = false;
                textBoxPlayer2.ForeColor = this.ForeColor;
                textBoxPlayer2.BackColor = Color.White;
                textBoxPlayer2.Text = string.Empty;
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                this.m_isComputerEnabled = true;
                textBoxPlayer2.BackColor = this.BackColor;
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.Enabled = false;
            }
        }
    }
}
