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
        private bool _isDataInitialized = false; //데이터소스 초기화 전 이벤트 핸들러 호출 방지용

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

            Preset.DataSource = InitializePresets().Clone(); //데이터소스 초기화
            _isDataInitialized = true; //데이터소스 초기화 전 이벤트 핸들러 호출 방지용
            InitializeSlotImages(); //이미지 슬롯 초기화
            
        }

        private void CardSlotUpdated(object sender, EventArgs e)
        {
            int index = Int32.Parse((sender as Control).Tag.ToString());
            ReloadSlotImg(index);
        }

        private void Preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!_isDataInitialized) { return; } //데이터소스 초기화 전에 호출되는것을 방지
            var selectedCards = _presets.Rows[Preset.SelectedIndex].Field<int[]>("cards");
            var defaultGoal = _presets.Rows[Preset.SelectedIndex].Field<int>("defaultGoal");
            this.targetAwakeLevel.Value = defaultGoal;
            for (int index = 0; index < NUMBER_OF_SLOTS; index++)
            {
                _cardNames[index].SelectedIndex = selectedCards[index];
            }
        }
    }
}
