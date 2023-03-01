﻿using System;
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
        public CardSet( //메인폼용 생성자
            int slot1, int slot2, int slot3, int slot4, int slot5, int slot6, int slot7, //슬롯별 카드 번호, 빈슬롯0
            int quan1, int quan2, int quan3, int quan4, int quan5, int quan6, int quan7, //슬롯별 카드 개수, 없는카드 0
            int relicSelection, int commanderSelection, int loaonSelection) //전선팩
        {
            this._slots = new int[7] { slot1, slot2, slot3, slot4, slot5, slot6, slot7 };
            this._quantity = new int[7] { quan1, quan2, quan3, quan4, quan5, quan6, quan7 };
            this._selectionPacks = new int[3] { relicSelection, commanderSelection, loaonSelection };
        }

        public CardSet(int[] slots, int[] quantity, int[] selectionPacks)
        { //내부용 생성자
            this._slots = slots;
            this._quantity = quantity;
            this._selectionPacks = selectionPacks;
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

        public int GetCurrentAwakeLevel(int minimumEquipAmount)
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

            if(equipped < minimumEquipAmount) { return 0; } //최소장착매수보다 적게 장착했으면 무조건 0각

            return (sum - min); //7장을 장착할 수는 없으므로 각성레벨 가장낮은 카드의 레벨 빼고 반환
        }
    }
}