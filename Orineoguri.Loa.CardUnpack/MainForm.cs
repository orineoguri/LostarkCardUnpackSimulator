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
                (int)this.numericUpDown23.Value, (int)this.numericUpDown24.Value, (int)this.numericUpDown19.Value,
                (int)this.numericUpDown20.Value, (int)this.numericUpDown21.Value, (int)this.numericUpDown22.Value, //심연, 전설, 영웅, 희귀, 고급, 일반
                (int)this.numericUpDown8.Value, (int)this.numericUpDown9.Value, (int)this.numericUpDown10.Value, (int)this.numericUpDown11.Value,
                (int)this.numericUpDown12.Value, (int)this.numericUpDown13.Value, (int)this.numericUpDown14.Value, (int)this.numericUpDown15.Value);

            Dictionary<int, int> resultRanged = cardPacks.UnpackRawCardPacks();

            CardSet cardSet = new CardSet(
                7, 17, 13, 6, 12, 9, 19,
                (int)this.numericUpDown1.Value + resultRanged[7], (int)this.numericUpDown2.Value + resultRanged[17], 
                (int)this.numericUpDown3.Value + resultRanged[13], (int)this.numericUpDown4.Value + resultRanged[6],
                (int)this.numericUpDown5.Value + resultRanged[12], (int)this.numericUpDown6.Value + resultRanged[9], 
                (int)this.numericUpDown7.Value + resultRanged[19],
                (int)this.numericUpDown16.Value, 0, (int)this.numericUpDown17.Value, (int)this.numericUpDown18.Value);

            MessageBox.Show(cardSet.CanBeTargetLevelWithSelectionPack().ToString());
        }

        private void CheckAwakeLevel_Click(object sender, EventArgs e)
        {
            int maxCount = 100000;
            int counter = 0;

            for(int i = 0; i<maxCount; i++)
            {
                cardPacks = new CardPacks(
                    (int)this.numericUpDown23.Value, (int)this.numericUpDown24.Value, (int)this.numericUpDown19.Value,
                    (int)this.numericUpDown20.Value, (int)this.numericUpDown21.Value, (int)this.numericUpDown22.Value, //심연, 전설, 영웅, 희귀, 고급, 일반
                    (int)this.numericUpDown8.Value, (int)this.numericUpDown9.Value, (int)this.numericUpDown10.Value, 
                    (int)this.numericUpDown11.Value, (int)this.numericUpDown12.Value, (int)this.numericUpDown13.Value, 
                    (int)this.numericUpDown14.Value, (int)this.numericUpDown15.Value);

                Dictionary<int, int> resultRanged = cardPacks.UnpackRawCardPacks();

                CardSet cardSet = new CardSet(
                    7, 17, 13, 6, 12, 9, 19,
                    (int)this.numericUpDown1.Value + resultRanged[7], (int)this.numericUpDown2.Value + resultRanged[17],
                    (int)this.numericUpDown3.Value + resultRanged[13], (int)this.numericUpDown4.Value + resultRanged[6],
                    (int)this.numericUpDown5.Value + resultRanged[12], (int)this.numericUpDown6.Value + resultRanged[9],
                    (int)this.numericUpDown7.Value + resultRanged[19],
                    (int)this.numericUpDown16.Value, 0, (int)this.numericUpDown17.Value, (int)this.numericUpDown18.Value);

                if(cardSet.CanBeTargetLevelWithSelectionPack()) { counter++; }
            }

            MessageBox.Show(((double)counter / (double)maxCount * 100).ToString() + "%");

        }
    }
}
