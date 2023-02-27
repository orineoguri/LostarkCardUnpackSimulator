using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orineoguri.Loa.CardUnpack
{
    public partial class MainForm : Form
    {
        private CardPacks cardPacks;
        public MainForm()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            cardPacks = new CardPacks(
                0, 0, 0, 0, 0, 0, //심연, 전설, 영웅, 희귀, 고급, 일반
                (int)this.numericUpDown8.Value, (int)this.numericUpDown9.Value, (int)this.numericUpDown10.Value, (int)this.numericUpDown11.Value,
                (int)this.numericUpDown12.Value, (int)this.numericUpDown13.Value, (int)this.numericUpDown14.Value, (int)this.numericUpDown15.Value);

            Dictionary<int, int> resultRanged = cardPacks.UnpackRawCardPacks();
            string temp = "";
            for(int index = 1; index <= 50; index++)
            {
                temp += index.ToString();
                temp += " : ";
                temp += resultRanged[index].ToString();
                temp += "\n";
            }
            MessageBox.Show(temp);
            
        }

        private void CheckAwakeLevel_Click(object sender, EventArgs e)
        {
            CardSet cardSet = new CardSet(
                7, 17, 13, 6, 12, 9, 19,
                (int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, (int)this.numericUpDown3.Value, (int)this.numericUpDown4.Value,
                (int)this.numericUpDown5.Value, (int)this.numericUpDown6.Value, (int)this.numericUpDown7.Value,
                0, 0, 0);

            MessageBox.Show(cardSet.GetCurrentAwakeLevel(6).ToString());
        }
    }
}
