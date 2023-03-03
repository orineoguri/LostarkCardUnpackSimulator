using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orineoguri.Loa.CardUnpack
{
    class CardSet
    {
        private int[] _slots;
        private int[] _quantity;
        private int[] _selectionPacks;
        private int _targetAwakeLevel;
        private int _minimumEquipAmount = 0;

        private static readonly HashSet<int>[] _selectionPackContents =
        {
            new HashSet<int>() 
            { //0번 일반전선팩
                (int)CardList.Silian, (int)CardList.Shandi, (int)CardList.GingerWale, (int)CardList.waye, (int)CardList.Illiakan,
                (int)CardList.Beatrice, (int)CardList.Azena, (int)CardList.Bahunturr, (int)CardList.EstherLuteran, 
                (int)CardList.EstherSien, (int)CardList.EstherGalaturr, (int)CardList.Ninave, (int)CardList.SaneKoukuSaton, 
                (int)CardList.Aman, (int)CardList.LordSilian, (int)CardList.DelainAman, (int)CardList.Kharmine, (int)CardList.GuardianLu
            },
            new HashSet<int>() 
            { //1번 군단장 선택팩
                (int)CardList.Valtan, (int)CardList.Biackiss, (int)CardList.KoukuSaton, 
                (int)CardList.Abrelshud, (int)CardList.Kamen, (int)CardList.Illiakan
            },
            new HashSet<int>()
            { //2번 로아온 전선팩
                (int)CardList.Valtan, (int)CardList.Biackiss, (int)CardList.KoukuSaton,
                (int)CardList.Abrelshud, (int)CardList.Kamen, (int)CardList.Kadan,
                (int)CardList.Silian, (int)CardList.Shandi, (int)CardList.GingerWale, (int)CardList.waye, (int)CardList.Illiakan,
                (int)CardList.Beatrice, (int)CardList.Azena, (int)CardList.Bahunturr, (int)CardList.EstherLuteran,
                (int)CardList.EstherSien, (int)CardList.EstherGalaturr, (int)CardList.Ninave, (int)CardList.SaneKoukuSaton,
                (int)CardList.Aman, (int)CardList.LordSilian, (int)CardList.DelainAman, (int)CardList.Kharmine, (int)CardList.GuardianLu
            }
        };
        public CardSet( //메인폼용 생성자
            int slot1, int slot2, int slot3, int slot4, int slot5, int slot6, int slot7, //슬롯별 카드 번호, 빈슬롯0
            int quan1, int quan2, int quan3, int quan4, int quan5, int quan6, int quan7, //슬롯별 카드 개수, 없는카드 0
            int relicSelection, int commanderSelection, int loaonSelection, int target) //전선팩
        {
            this._slots = new int[7] { slot1, slot2, slot3, slot4, slot5, slot6, slot7 };
            this._quantity = new int[7] { quan1, quan2, quan3, quan4, quan5, quan6, quan7 };
            this._selectionPacks = new int[3] { relicSelection, commanderSelection, loaonSelection };
            this._targetAwakeLevel = target;
            for(int i = 0; i<_slots.Length; i++) 
            {
                if(_slots[i] != 0 && _minimumEquipAmount < 6) { _minimumEquipAmount++; } //빈슬롯 아니면 최소제한매수 증가
            }
        }

        public CardSet(int[] slots, int[] quantity, int[] selectionPacks, int target)
        { //내부용 생성자
            this._slots = slots;
            this._quantity = quantity;
            this._selectionPacks = selectionPacks;
            this._targetAwakeLevel = target;
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] != 0 && _minimumEquipAmount < 6) { _minimumEquipAmount++; } //빈슬롯 아니면 최소제한매수 증가
            }
        }

        private int GetSingleAwakeLevel(int slot)
        {
            int singleAwakeLevel;
            if (_quantity[slot] < 2) { singleAwakeLevel = 0; }
            else if (_quantity[slot] < 4) { singleAwakeLevel = 1; }
            else if (_quantity[slot] < 7) { singleAwakeLevel = 2; }
            else if (_quantity[slot] < 11) { singleAwakeLevel = 3; }
            else if (_quantity[slot] < 16) { singleAwakeLevel = 4; }
            else { singleAwakeLevel = 5; } //16장 이상은 무조건 5각으로 간주

            return singleAwakeLevel;
        }

        public int GetCurrentAwakeLevel()
        {
            int sum = 0; //각성레벨 합계
            int min = 5; //각성레벨 최소값인 카드의 각성레벨
            int equipped = 0; //장착중인 카드 매수

            for (int index = 0; index < 7; index++)
            {
                if(_slots[index] == 0) { min = 0; } //빈슬롯 하나라도 있으면 최소값은 0으로 간주
                else //빈슬롯 아니면
                {
                    if(_quantity[index] > 0) { equipped++; } //한장이라도 등록했으면 장착매수에 합산

                    int singleAwakeLevel = GetSingleAwakeLevel(index);
                    sum += singleAwakeLevel; //각성레벨 합계에 합산
                    min = (min > singleAwakeLevel) ? singleAwakeLevel : min; //현재 최소레벨보다 각성레벨이 낮을경우 최소레벨 교체
                }
            }

            if(equipped < _minimumEquipAmount) { return 0; } //최소장착매수보다 적게 장착했으면 무조건 0각

            return (sum - min); //7장을 장착할 수는 없으므로 각성레벨 가장낮은 카드의 레벨 빼고 반환
        }

        private int getRequiredQuantityToLevelUp(int currentQuantity)
        { //다음각성레벨 가려면 몇장이나 더 필요하죠?
            int required = 0;
            switch (currentQuantity)
            {
                case 0: case 1: case 3: case 6: case 10: case 15:
                    required = 1; break;
                case 2: case 5: case 9: case 14:
                    required = 2; break;
                case 4: case 8: case 13:
                    required = 3; break;
                case 7: case 12:
                    required = 4; break;
                case 11:
                    required = 5; break;
                default:
                    required = 0; break;
            }
            return required;
        }

        public CardSet GetLevelupCardSet(int slot)
        {
            if(_quantity[slot] > 15) { return null; } //풀각이면 각성 불가
            if (_slots[slot] == 0) { return null; } //빈슬롯이면 각성 불가

            int currentQuantity = _quantity[slot]; //현재 수집 개수
            int required = getRequiredQuantityToLevelUp(currentQuantity); //각성레벨 올리려면 몇장 더 필요한가
            int gainedFromSelectionPack = 0;
            int[] currentQuantityForRegenerate = (int[])_quantity.Clone(); //원본 안건드리고 클래스 재생성 하기 위한 수량 복사본
            int[] currentSelectionPack = (int[])_selectionPacks.Clone(); //전선팩 리스트 복제

            for (int index = 0; index < _selectionPackContents.Length; index++)
            {
                if (!_selectionPackContents[index].Contains(_slots[slot])) { continue; } //현재 선택팩으로 선택할 수 없는 카드라면
                else if (required <= currentSelectionPack[index])
                { //현재 요구량이 선택팩 보유량보다 적거나 같다면
                    currentQuantityForRegenerate[slot] += required; //남은 요구량 적립
                    currentQuantityForRegenerate[slot] += gainedFromSelectionPack; //지금까지 얻은 적립량 반영
                    currentSelectionPack[index] -= required; //남은 요구량 만큼 전선팩 차감

                    return new CardSet(this._slots, currentQuantityForRegenerate, currentSelectionPack, this._targetAwakeLevel);
                }
                else //현재 요구량이 선택팩 보유량보다 많다면
                {
                    required -= currentSelectionPack[index]; //일단 가진 전선팩 전부 까고 다음 우선순위의 선택팩으로
                    gainedFromSelectionPack += currentSelectionPack[index]; //깐만큼 적립
                    currentSelectionPack[index] = 0;
                }
            }

            return null;
        }

        public bool CanBeTargetLevelWithSelectionPack()
        {
            if(this.GetCurrentAwakeLevel() >= _targetAwakeLevel) { return true; } //이미 목표각성 달성했으면 성공

            Queue<CardSet> BFSQueue = new Queue<CardSet>(); //너비우선탐색용 큐
            for (int index = 0; index < _slots.Length; index++)
            {
                CardSet currentNode = this.GetLevelupCardSet(index);
                if (currentNode is null) { continue; } //레벨업 가능한 슬롯이면 일단 큐에 넣어보기
                else { BFSQueue.Enqueue(currentNode); }
            }

            while (BFSQueue.Count > 0) //큐에 노드 하나라도 들어있으면
            {
                CardSet currentNode = BFSQueue.Dequeue();
                if(currentNode.GetCurrentAwakeLevel() >= _targetAwakeLevel) { return true; } //첫번째 노드 목표각성 달성했으면 성공
                else
                {
                    for(int index=0; index< _slots.Length; index++) //목표각성 달성 실패했으면 자식노드 7종 테스트후 큐에 삽입
                    {
                        CardSet nextNode = currentNode.GetLevelupCardSet(index);
                        if(nextNode is null) { continue; }
                        else { BFSQueue.Enqueue(nextNode); }
                    }
                }
            }
            //큐에 노드 하나도 안남을때 까지 반복문 돌았으면 실패
            return false;
        }

        public string TestState()
        {
            string result = "";
            for(int i=0; i<_quantity.Length; i++)
            {
                result += _quantity[i].ToString();
                result += ", ";
            }
            result += "\n";
            for(int i=0; i<_selectionPacks.Length; i++)
            {
                result += _selectionPacks[i];
                result += ", ";
            }

            return result;
        }

        
    }
}
