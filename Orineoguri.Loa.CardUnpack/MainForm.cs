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

        private ComboBox[] _cardNames; //카드이름
        private CheckBox[] _cardCollected; //카드 수집여부
        private NumericUpDown[] _cardAwakeLevel; //카드 각성레벨
        private NumericUpDown[] _cardRemains; //카드 잔여량

        private PictureBox[] _slotImages; //슬롯별 카드 이미지
        private PictureBox[] _slotAwakeImages; //슬롯별 각성레벨 이미지
        private PictureBox[] _slotRemainsImages; //슬롯별 카드 잔여량 이미지

        private CardDeckChecker _cardSet;
        private CardUnpacker _cardPacks;
        private BackgroundWorker _asyncWorker;

        public MainForm()
        {
            InitializeComponent();
            //index로 쉽게 접근가능하도록 슬롯별 컨트롤을 배열에 삽입
            _cardNames = new[] { nameSlot0, nameSlot1, nameSlot2, nameSlot3, nameSlot4, nameSlot5, nameSlot6 };
            _cardCollected = new[] { collectedSlot0, collectedSlot1, collectedSlot2, collectedSlot3, collectedSlot4, collectedSlot5, collectedSlot6 };
            _cardAwakeLevel = new[] { awakeSlot0, awakeSlot1, awakeSlot2, awakeSlot3, awakeSlot4, awakeSlot5, awakeSlot6 };
            _cardRemains = new[] { remainingSlot0, remainingSlot1, remainingSlot2, remainingSlot3, remainingSlot4, remainingSlot5, remainingSlot6 };

            _slotImages = new[] { imgSlot0, imgSlot1, imgSlot2, imgSlot3, imgSlot4, imgSlot5, imgSlot6 };
            _slotAwakeImages = new PictureBox[NUMBER_OF_SLOTS];
            _slotRemainsImages = new PictureBox[NUMBER_OF_SLOTS];

            Preset.DataSource = InitializePresets().Clone(); //데이터소스 초기화
            _isDataInitialized = true; //데이터소스 초기화 전 이벤트 핸들러 호출 방지용
            InitializeSlotImages(); //이미지 슬롯 초기화
            InitializeAsyncWorker(); //시뮬레이션을 위한 비동기 쓰레드 초기화 관련 필드/메소드 초기화
            
        }

        private void CardSlotUpdated(object sender, EventArgs e)
        {
            int index = Int32.Parse((sender as Control).Tag.ToString()); //컨트롤별로 설정된 태그를 int로 변환
            ReloadSlotImg(index); //카드 이미지 새로고침
        }

        private void Preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!_isDataInitialized) { return; } //데이터소스 초기화 전에 호출되는것을 방지
            var selectedCards = _presets.Rows[Preset.SelectedIndex].Field<int[]>("cards"); //프리셋에 카드 리스트
            var defaultGoal = _presets.Rows[Preset.SelectedIndex].Field<int>("defaultGoal"); //프리셋에 목표 각성 수치
            this.targetAwakeLevel.Value = defaultGoal;
            for (int index = 0; index < NUMBER_OF_SLOTS; index++)
            {
                _cardNames[index].SelectedIndex = selectedCards[index];
            }
        }

        private void buttonOnce_Click(object sender, EventArgs e)
        {
            if (_asyncWorker.IsBusy) { return; } //결과창 충돌 방지

            _cardPacks = SetUpCardUnpacker();
            var unpackResult = _cardPacks.UnpackRawCardPacks();
            _cardSet = SetUpCardDeckChecker(unpackResult);
            textResultOutput.Clear();
            textResultOutput.AppendText("카드팩 해제 결과\n");
            textResultOutput.AppendText("------------------------------------------------------------");
            foreach (KeyValuePair<int, int> acquiredCard in unpackResult)
            {
                if(acquiredCard.Value == 0) { continue; } //한장도 없으면 스킵

                textResultOutput.AppendText(Environment.NewLine); //공백 삽입

                if(acquiredCard.Key <= 24) { textResultOutput.SelectionColor = Color.Orange; } //전설카드면 주황색
                else if(acquiredCard.Key <= 99) { textResultOutput.SelectionColor = Color.Violet; } //영웅카드면 보라색
                else if (acquiredCard.Key <= 200) { textResultOutput.SelectionColor = Color.Blue; } //희귀카드면 파란색
                else if (acquiredCard.Key <= 257) { textResultOutput.SelectionColor = Color.Green; } //고급카드면 초록색

                textResultOutput.AppendText($"{acquiredCard.Value}장 -\t[{_cardNameList[acquiredCard.Key]}]");
            }
            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.AppendText("------------------------------------------------------------");

            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.SelectionFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
            textResultOutput.AppendText($"현재 각성레벨 : {_cardSet.GetCurrentAwakeLevel()}");

            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.SelectionFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
            textResultOutput.AppendText($"목표레벨 달성가능 여부 : {(_cardSet.CanBeTargetLevelWithSelectionPack() ? "가능" : "불가능")}");

        }

        private void trackRepeatCount_ValueChanged(object sender, EventArgs e)
        {
            labelRepeatCount.Text = $"시뮬레이션 반복횟수 : {trackRepeatCount.Value}0,000";
            progressBar.Maximum = trackRepeatCount.Value * 10000; //프로그래스바 최대수치도 조정
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            if (_asyncWorker.IsBusy) { return; } //스레드 중복실행 방지
            textResultOutput.Clear();
            textResultOutput.AppendText($"시뮬레이션 시작 ({DateTime.Now.ToString("HH:mm:ss")})");
            _asyncWorker.RunWorkerAsync(argument: trackRepeatCount.Value * 10000);

        }
    }
}
