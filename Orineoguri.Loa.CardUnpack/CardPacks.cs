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
        private Dictionary<int, int>[] _rangedCardPack = new Dictionary<int, int>[2];
        private Dictionary<int, int> _rawCardPacks; //개별 카드팩
        private Random _random; //난수발생기

        private const int ABYSS_START = 1; //심연팩 1번~19번(총 19종)
        private const int ABYSS_END = 19;
        private const int RELIC_START = 6; //전설팩 6번~24번(총 19종)
        private const int RELIC_END = 24;
        private const int HEROIC_START = 25; //영웅팩 22번~99번(총 75종)
        private const int HEROIC_END = 99;
        private const int RARE_START = 100; //희귀팩 100번~200번(총 101종)
        private const int RARE_END = 200; 
        private const int HIGH_START = 201; //고급팩 201번~257번(총 57종)
        private const int HIGH_END = 257;
        private const int COMMON_START = 258; //일반팩 258번~285번(총 28종)
        private const int COMMON_END = 285;

        public CardPacks(int abyss, int relic, int heroic, int rare, int high, int common, //심연팩, 전설팩, 영웅팩, 희귀팩, 고급팩, 일반팩
            int relic_heroic, int relic_rare, int relic_high, int entire, //전영팩1, 전희팩1, 전고팩1, 전체팩1
            int relic_heroic2, int relic_rare2, int relic_high2, int entire2) //전영팩2, 전희팩2, 전고팩2, 전체팩2
        {
            this._random = new Random();

            //범위 카드팩들 초기화
            _rangedCardPack[0][(int)CardRank.Heroic] = relic_heroic;
            _rangedCardPack[0][(int)CardRank.Rare] = relic_rare;
            _rangedCardPack[0][(int)CardRank.High] = relic_high;
            _rangedCardPack[0][(int)CardRank.Common] = entire;

            _rangedCardPack[1][(int)CardRank.Heroic] = relic_heroic2;
            _rangedCardPack[1][(int)CardRank.Rare] = relic_rare2;
            _rangedCardPack[1][(int)CardRank.High] = relic_high2;
            _rangedCardPack[1][(int)CardRank.Common] = entire2;

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
            for(int index = 0; index < _rangedCardPack.Length; index++)
            {
                //전영팩 40:160 비율
                for (int i = 0; i < _rangedCardPack[index][(int)CardRank.Heroic]; i++)
                {
                    int randomNumber = _random.Next(0, 199);
                    if (randomNumber < 40) 
                    {
                        if (index == 0) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                        else { _rawCardPacks[(int)CardRank.Abyss] += 1; }
                    }
                    else { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                }

                //전희팩 8:32:160 비율
                for (int i = 0; i < _rangedCardPack[index][(int)CardRank.Rare]; i++)
                {
                    int randomNumber = _random.Next(0, 199);
                    if (randomNumber < 8) 
                    {
                        if (index == 0) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                        else { _rawCardPacks[(int)CardRank.Abyss] += 1; }
                    }
                    else if (randomNumber < 32) { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                    else { _rawCardPacks[(int)CardRank.Rare] += 1; }
                }

                //전고팩 4:28:94:74 비율
                for (int i = 0; i < _rangedCardPack[index][(int)CardRank.High]; i++)
                {
                    int randomNumber = _random.Next(0, 199);
                    if (randomNumber < 4) 
                    {
                        if (index == 0) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                        else { _rawCardPacks[(int)CardRank.Abyss] += 1; }
                    }
                    else if (randomNumber < 28) { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                    else if (randomNumber < 94) { _rawCardPacks[(int)CardRank.Rare] += 1; }
                    else { _rawCardPacks[(int)CardRank.High] += 1; }
                }

                //전체팩 1:19:80:60:40 비율
                for (int i = 0; i < _rangedCardPack[index][(int)CardRank.Common]; i++)
                {
                    int randomNumber = _random.Next(0, 199);
                    if (randomNumber < 1) 
                    {
                        if (index == 0) { _rawCardPacks[(int)CardRank.Relic] += 1; }
                        else { _rawCardPacks[(int)CardRank.Abyss] += 1; }
                    }
                    else if (randomNumber < 19) { _rawCardPacks[(int)CardRank.Heroic] += 1; }
                    else if (randomNumber < 80) { _rawCardPacks[(int)CardRank.Rare] += 1; }
                    else if (randomNumber < 60) { _rawCardPacks[(int)CardRank.High] += 1; }
                    else { _rawCardPacks[(int)CardRank.Common] += 1; }
                }
            }
            
        }

        public Dictionary<int, int> UnpackRawCardPacks()
        {
            UnpackRangedCardPacks(); //범위 카드팩 일단 해체
            Dictionary<int, int> cardList = new Dictionary<int, int>();
            for(int i = 1; i <= COMMON_END; i++) { cardList[i] = 0; } //카드리스트 내용물 0으로 초기화

            for(int i = 0; i < _rawCardPacks[(int)CardRank.Abyss]; i++) //심연팩
            {
                int randomNumber = _random.Next(ABYSS_START, ABYSS_END);
                cardList[randomNumber] += 1;
            }
            
            for(int i = 0; i < _rawCardPacks[(int)CardRank.Relic]; i++) //전설팩
            {
                int randomNumber = _random.Next(RELIC_START, RELIC_END);
                cardList[randomNumber] += 1;
            }

            for (int i = 0; i < _rawCardPacks[(int)CardRank.Heroic]; i++) //영웅팩
            {
                int randomNumber = _random.Next(HEROIC_START, HEROIC_END);
                cardList[randomNumber] += 1;
            }

            for (int i = 0; i < _rawCardPacks[(int)CardRank.Rare]; i++) //희귀팩
            {
                int randomNumber = _random.Next(RARE_START, RARE_END);
                cardList[randomNumber] += 1;
            }

            for (int i = 0; i < _rawCardPacks[(int)CardRank.High]; i++) //고급팩
            {
                int randomNumber = _random.Next(HIGH_START, HIGH_END);
                cardList[randomNumber] += 1;
            }

            for (int i = 0; i < _rawCardPacks[(int)CardRank.Common]; i++) //일반팩
            {
                int randomNumber = _random.Next(COMMON_START, COMMON_END);
                cardList[randomNumber] += 1;
            }

            return new Dictionary<int, int>(cardList);
        }

        public Dictionary<int, int> RangedCardPackTest()
        {
            UnpackRangedCardPacks();
            return new Dictionary<int, int>(_rawCardPacks);
        }
    }
}
