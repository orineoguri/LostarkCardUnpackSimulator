using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orineoguri.Loa.CardUnpack
{
    enum CardRank //카드 등급
    {
        Abyss = 0, //심연
        Relic = 1, //전설
        Heroic = 2, //영웅
        Rare = 3, //희귀
        High = 4, //고급
        Common = 5 //일반
    }

    public class CardPacks
    {
        private Dictionary<int, int> _rangedCardPacks1; //범위카드팩 1
        private Dictionary<int, int> _rangedCardPacks2; //범위카드팩 2(심연팩 포함)
        private Dictionary<int, int> _rawCardPacks; //개별 카드팩
        private Random _random; //난수발생기
        public CardPacks(int abyss, int relic, int heroic, int rare, int high, int common, int relic_heroic, int relic_rare, int relic_high, int entire) 
        {
            this._rangedCardPacks1 = new Dictionary<int, int>();
            this._rangedCardPacks2 = new Dictionary<int, int>();
            this._rawCardPacks = new Dictionary<int, int>();
            this._random = new Random();

            //범위 카드팩들 초기화
            _rangedCardPacks1[(int)CardRank.Heroic] = relic_heroic;
            _rangedCardPacks1[(int)CardRank.Rare] = relic_rare;
            _rangedCardPacks1[(int)CardRank.High] = relic_high;
            _rangedCardPacks1[(int)CardRank.Common] = entire;
            //개별 카드팩들 초기화
            _rawCardPacks[(int)CardRank.Abyss] = abyss;
            _rawCardPacks[(int)CardRank.Relic] = relic;
            _rawCardPacks[(int)CardRank.Heroic] = heroic;
            _rawCardPacks[(int)CardRank.Rare] = rare;
            _rawCardPacks[(int)CardRank.High] = high;
            _rawCardPacks[(int)CardRank.Common] = common;

        }

        private void UnpackRangedCardPacks()
        {
            for(int i = 0; i < _rangedCardPacks1[(int)CardRank.Heroic]; i++) //전영팩1 40:160 비율
            {
                int randomNumber = _random.Next(0, 199);
                if(randomNumber < 40) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                else { _rawCardPacks[(int)CardRank.Heroic] += 1; }
            }
            //_rangedCardPacks1[(int)CardRank.Heroic] = 0; //다 깐 카드팩은 0으로

            for (int i = 0; i < _rangedCardPacks1[(int)CardRank.Rare]; i++) //전희팩1 8:32:160 비율
            {
                int randomNumber = _random.Next(0, 199);
                if (randomNumber < 8) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                else if (randomNumber < 32) { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                else { _rawCardPacks[(int)CardRank.Rare] += 1; }
            }

            for (int i = 0; i < _rangedCardPacks1[(int)CardRank.High]; i++) //전고팩1 4:28:94:74 비율
            {
                int randomNumber = _random.Next(0, 199);
                if (randomNumber < 4) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                else if (randomNumber < 28) { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                else if (randomNumber < 94) { _rawCardPacks[(int)CardRank.Rare] += 1; }
                else { _rawCardPacks[(int)CardRank.High] += 1; }
            }

            for (int i = 0; i < _rangedCardPacks1[(int)CardRank.Common]; i++) //전체팩1 1:19:80:60:40 비율
            {
                int randomNumber = _random.Next(0, 199);
                if (randomNumber < 1) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                else if (randomNumber < 19) { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                else if (randomNumber < 80) { _rawCardPacks[(int)CardRank.Rare] += 1; }
                else if (randomNumber < 60) { _rawCardPacks[(int)CardRank.High] += 1; }
                else { _rawCardPacks[(int)CardRank.Common] += 1; }
            }
        }
    }
}
