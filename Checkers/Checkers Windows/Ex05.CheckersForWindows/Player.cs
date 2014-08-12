using System.Collections.Generic;

namespace Ex05.CheckersForWindows
{
    public class Player
    {
        public enum e_PlayerType
        {
            Human = 0,
            Computer = 1
        }

        public enum e_PlayerPosition
        {
            Top = 0,
            Bottom = 1,
        }

        private string m_playerName;
        private int m_playerPoints;
        private e_PlayerType m_playerType;
        private e_PlayerPosition m_playerPosition;

        public Player(string i_inputName)
        {
            m_playerName = i_inputName;
            m_playerType = e_PlayerType.Human;
            m_playerPosition = e_PlayerPosition.Bottom;
            m_playerPoints = 0;
        }

        public Player(string i_inputName, bool i_computer)
        {
            m_playerName = i_inputName;
            m_playerType = e_PlayerType.Computer;
            m_playerPosition = e_PlayerPosition.Top;
            m_playerPoints = 0;
        }

        public string Name
        {
            get
            {
                return m_playerName;
            }
        }

        public int Points
        {
            get
            {
                return m_playerPoints;
            }

            set
            {
                m_playerPoints = value;
            }
        }

        public e_PlayerType PlayerType
        {
            get
            {
                return m_playerType;
            }

            set
            {
                m_playerType = value;
            }
        }

        public e_PlayerPosition PlayerPosition
        {
            get
            {
                return m_playerPosition;
            }

            set
            {
                m_playerPosition = value;
            }
        }
    }
}