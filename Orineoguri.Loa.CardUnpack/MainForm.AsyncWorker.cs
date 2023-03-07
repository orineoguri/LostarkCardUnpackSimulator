using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Orineoguri.Loa.CardUnpack
{
    public partial class MainForm
    {
        private void InitializeAsyncWorker()
        {
            _asyncWorker = new BackgroundWorker();
            _asyncWorker.WorkerReportsProgress = true;
            _asyncWorker.DoWork += DoSimulation;
            _asyncWorker.ProgressChanged += ProgressChecker;
            _asyncWorker.RunWorkerCompleted += CompleteSimuation;
        }

        private void DoSimulation(object sender, DoWorkEventArgs e) //시뮬레이션 실행하는 이벤트 핸들러(메인 스레드에서 실행 안됨)
        {
            int totalSimulationCount = (int)e.Argument;
            int succeedCount = 0;
            CardUnpacker unpacker;
            CardDeckChecker deckChecker;
            Dictionary<int, int> unpackResult;
            Font boldText = new Font(SystemFonts.DefaultFont, FontStyle.Bold);

            for (int simulationCount = 0; simulationCount < totalSimulationCount; simulationCount++)
            {
                CheckForIllegalCrossThreadCalls = false; //크로스 스레딩 문제 회피용
                unpacker = SetUpCardUnpacker();
                unpackResult = unpacker.UnpackRawCardPacks();
                deckChecker = SetUpCardDeckChecker(unpackResult);
                if (deckChecker.CanBeTargetLevelWithSelectionPack()) { succeedCount++; }
                _asyncWorker.ReportProgress(simulationCount); //프로그래스바 진행
            }

            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.SelectionFont = boldText;
            textResultOutput.AppendText($"총 {totalSimulationCount}회 시행중 {succeedCount}회 성공");

            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.SelectionFont = boldText;
            textResultOutput.AppendText($"목표레벨 달성 확률 : {(double)succeedCount / (double)totalSimulationCount * 100}%");

        }

        private void ProgressChecker(object sender, ProgressChangedEventArgs e) //시뮬레이션 진행상황 보고
        {
            progressBar.Value = e.ProgressPercentage + 1; //프로그래스바 진행
        }

        private void CompleteSimuation(object sender, RunWorkerCompletedEventArgs e) //시뮬레이션 종료 이후
        {
            progressBar.Value = 0;
            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.AppendText("------------------------------------------------------------");
            textResultOutput.AppendText(Environment.NewLine);
            textResultOutput.AppendText($"시뮬레이션 종료 ({DateTime.Now.ToString("HH:mm:ss")})");
        }

        private int GetCardAmount(int slot) //슬롯정보로 카드매수 구하기
        {
            if (!_cardCollected[slot].Checked) { return 0; } //명함수집도 못했으면 0
            int result = 1; //명함수집 확인했으니 1장부터 시작

            switch (_cardAwakeLevel[slot].Value) //각성수치 카드 장수에 반영
            {
                case 1: result += 1; break;
                case 2: result += 3; break;
                case 3: result += 6; break;
                case 4: result += 10; break;
                case 5: result += 15; break;
            }

            result += (int)_cardRemains[slot].Value; //여분카드 반영

            return result;
        }

        private CardUnpacker SetUpCardUnpacker() //카드팩 까기
        {
            CardUnpacker unpacker;
            if (checkNoAbyss.Checked)
            { //남바절 모드 켜져있으면 범위팩2 시리즈도 전부 범위팩1 위치에 합산
                unpacker = new CardUnpacker(
                    (int)numericRawAbyss.Value, (int)numericRawRelic.Value, (int)numericRawHeroic.Value,
                    (int)numericRawRare.Value, (int)numericRawHigh.Value, (int)numericRawCommon.Value,
                    (int)(numericRelicHeroic.Value + numericRelicHeroic2.Value),
                    (int)(numericRelicRare.Value + numericRelicRare2.Value), (int)(numericRelicHigh.Value + numericRelicHigh2.Value),
                    (int)(numericEntire.Value + numericCoin.Value + numericCoin2.Value + numericEntire2.Value),
                    0, 0, 0, 0);
            }
            else
            {
                unpacker = new CardUnpacker(
                    (int)numericRawAbyss.Value, (int)numericRawRelic.Value, (int)numericRawHeroic.Value,
                    (int)numericRawRare.Value, (int)numericRawHigh.Value, (int)numericRawCommon.Value,
                    (int)numericRelicHeroic.Value, (int)numericRelicRare.Value, (int)numericRelicHigh.Value,
                    (int)(numericEntire.Value + numericCoin.Value + numericCoin2.Value),
                    (int)numericRelicHeroic2.Value, (int)numericRelicRare2.Value, (int)numericRelicHigh2.Value, (int)numericEntire2.Value);
            }

            return unpacker;
        }

        private CardDeckChecker SetUpCardDeckChecker(Dictionary<int, int> unpackedCards)
        {
            var checker = new CardDeckChecker( //기존에 슬롯에 있던 카드에 새로 얻은 카드들 더해서 생성자에 반영
                    _cardNames[0].SelectedIndex, _cardNames[1].SelectedIndex, _cardNames[2].SelectedIndex, _cardNames[3].SelectedIndex,
                    _cardNames[4].SelectedIndex, _cardNames[5].SelectedIndex, _cardNames[6].SelectedIndex,
                    GetCardAmount(0) + unpackedCards[_cardNames[0].SelectedIndex], GetCardAmount(1) + unpackedCards[_cardNames[1].SelectedIndex],
                    GetCardAmount(2) + unpackedCards[_cardNames[2].SelectedIndex], GetCardAmount(3) + unpackedCards[_cardNames[3].SelectedIndex],
                    GetCardAmount(4) + unpackedCards[_cardNames[4].SelectedIndex], GetCardAmount(5) + unpackedCards[_cardNames[5].SelectedIndex],
                    GetCardAmount(6) + unpackedCards[_cardNames[6].SelectedIndex],
                    (int)numericRelicSelection.Value, (int)numericCommanderSelection.Value, (int)numericLoaonSelection.Value, (int)targetAwakeLevel.Value);

            return checker;
        }
    }
}
