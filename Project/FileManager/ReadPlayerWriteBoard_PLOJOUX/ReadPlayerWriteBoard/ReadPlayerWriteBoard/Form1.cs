using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ReadPlayerWriteBoard
{
    public partial class Form1 : Form
    {
        XmlDocument doc = new XmlDocument();
        int[,] contentTab = new int[10, 10];
        int TABSIZE = 10;
        string[] players = new string[2];
        string FILE = "..\\..\\..\\BN-V02-2016-01-13-15-37-00.bnav";

        public Form1()
        {
            InitializeComponent();
            doc.Load(FILE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitTab(contentTab);
            ReadPlayers();
            WriteBattleBoard();
        }

        public void InitTab(int[,] tab)
        {
            for (int i = 0; i < TABSIZE; i++)
            {
                for (int j = 0; j < TABSIZE; j++)
                {
                    tab[i, j] = 1;
                }
            }
        }

        public string ConvertTabLineToString(int[,] tab, int i)
        {
            string content = "";
            for (int j = 0; j < TABSIZE; j++)
            {
                content += tab[i, j].ToString() + ";"; 
            }
            return content;
        }

        public void ReadPlayers()
        {
            string player;
            XmlNode node; 
            node = doc.DocumentElement.SelectSingleNode("/NAV/JOUEURS/P1");
            player = node.InnerText;
            players[0] = player;
            node = doc.DocumentElement.SelectSingleNode("/NAV/JOUEURS/P2");
            player = node.InnerText;
            players[1] = player;
        }

        public void WriteBattleBoard()
        {
            XmlElement element1 = doc.CreateElement(string.Empty, "PLATEAU-P1", string.Empty);
            doc.AppendChild(element1);

            XmlText empty = doc.CreateTextNode("\n");
            element1.AppendChild(empty);

            for (int i = 0; i < 10; i++)
            {
                string content;
                content = ConvertTabLineToString(contentTab, i);
                XmlText ligne = doc.CreateTextNode(content+"\n");
                element1.AppendChild(ligne);
            }

            doc.Save("C:\\Users\\PLOJOUXR_INFO\\Documents\\ReadPlayerWriteBoard\\XmlTest\\board.bnav");
        }
    }
}
