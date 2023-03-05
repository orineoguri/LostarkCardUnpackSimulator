using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Orineoguri.Loa.CardUnpack
{
    public partial class MainForm : Form
    {
        private const int NUMBER_OF_SLOTS = 7;
        private readonly Size SLOT_IMG_SIZE = new Size(124, 181);
        private readonly Point SLOT_IMG_LOCATION = new Point(0, 0);

        private ComboBox[] _cardNames;
        private CheckBox[] _cardCollected;
        private NumericUpDown[] _cardAwakeLevel;
        private NumericUpDown[] _cardRemains;

        private PictureBox[] _slotImages;
        private PictureBox[] _slotAwakes;
        private PictureBox[] _slotRemains;

        public MainForm()
        {
            InitializeComponent();
            _cardNames = new[] { nameSlot0, nameSlot1, nameSlot2, nameSlot3, nameSlot4, nameSlot5, nameSlot6 };
            _cardCollected = new[] { collectedSlot0, collectedSlot1, collectedSlot2, collectedSlot3, collectedSlot4, collectedSlot5, collectedSlot6 };
            _cardAwakeLevel = new[] { awakeSlot0, awakeSlot1, awakeSlot2, awakeSlot3, awakeSlot4, awakeSlot5, awakeSlot6 };
            _cardRemains = new[] { remainingSlot0, remainingSlot1, remainingSlot2, remainingSlot3, remainingSlot4, remainingSlot5, remainingSlot6 };

            _slotImages = new[] { imgSlot0, imgSlot1, imgSlot2, imgSlot3, imgSlot4, imgSlot5, imgSlot6 };
            _slotAwakes = new PictureBox[NUMBER_OF_SLOTS];
            _slotRemains = new PictureBox[NUMBER_OF_SLOTS];

            Preset.DataSource = InitializePresets().Clone();

            for(int index = 0; index < NUMBER_OF_SLOTS; index++)
            {
                _slotAwakes[index] = new PictureBox
                {
                    Size = SLOT_IMG_SIZE,
                    Location = SLOT_IMG_LOCATION,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                _slotRemains[index] = new PictureBox
                {
                    Size = SLOT_IMG_SIZE,
                    Location = SLOT_IMG_LOCATION,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                _slotImages[index].Controls.Add(_slotAwakes[index]);
                _slotAwakes[index].Controls.Add(_slotRemains[index]);

                _cardNames[index].DataSource = _cardNameList.Clone();
                _cardNames[index].SelectedIndex = 0;
            }
        }


    }
}
