using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orineoguri.Loa.CardUnpack
{
    public partial class MainForm
    {
        private readonly string[] _cardNameList =
        {
            "빈 슬롯", "발탄", "비아키스", "쿠크세이튼", "아브렐슈드", "카멘", "실리안", "샨디", "진저웨일", "웨이", "일리아칸", "베아트리스", "아제나&이난나", "바훈투르", "에스더 루테란", "에스더 시엔", "에스더 갈라투르", "니나브", "광기를 잃은 쿠크세이튼", "카단", "아만", "국왕 실리안", "데런 아만", "카마인", "가디언 루", "타나토스", "천둥날개", "슈헤리트", "알레그로", "마법사 로나운", "자간", "검은 이빨", "모카모카", "하백", "반다", "바스티안", "사샤", "카인", "마리 파우렌츠", "진화의 군주 카인", "진 매드닉", "라하르트", "아나벨", "지그문트", "나크라세나", "레바노스", "베르투스", "크로마니움", "헬가이아", "빙결의 레기오로스", "어둠의 레기오로스", "에페르니아", "그노시스", "운다트", "실페리온", "케이사르", "벨크루제", "카이슈르", "사이카", "칼도르", "페데리코", "칼트말루스", "혼돈의 사이카", "아델", "에아달린", "엔비스카", "루메루스", "우르닐", "칼벤투스", "용암 크로마니움", "중갑 나크라세나", "홍염의 요호", "혹한의 헬가이아", "타이탈로스", "흑야의 요호", "벨가누스", "아카테스", "이그렉시온", "미스틱", "카이슈테르", "엘버하스틱", "아르카디아", "피요르긴", "파르쿠나스", "나히니르", "신뢰의 아크 아스타", "창조의 아크 오르투스", "예지의 아크 아가톤", "희망의 아크 엘피스", "지혜의 아크 라디체", "헌신의 아크 카르타", "에르제베트", "크리스틴", "칼바서스", "니아", "샤나", "알비온", "아르고스", "데스칼루다", "쿤겔라니움", "바루투", "레온하트 네리아", "루드릭", "도굴단장 우고", "불칸", "역병 인도자", "하르잘", "소금거인", "천둥", "칼라도세", "미한", "릭투스", "하셀링크", "로블롬", "알리페르", "루테란 성 네리아", "윌리윌리", "세리아", "사교도 대제사장", "자히아", "리게아스", "하울로크", "칼스 모론토", "히바이크", "녹스", "세티노", "토토마", "하이비 집행관", "수호자 에오로", "붉은 남작 에디", "수호자 티르", "거신 카스피엘", "아크의 수호자 오셀", "파한", "만포", "미령", "원포", "길달", "절망의 레키엘", "역병군단 바르토", "도철", "혼재의 추오", "슈테른 네리아", "시그나투스", "에스", "제이", "안톤", "바에단", "타르실라", "루아브", "시안", "마네스", "기드온", "가룸", "페일린", "고르곤", "프록시마", "에라스모", "두키킹", "브레아레오스", "고르카그로스", "수신 아포라스", "실험체 타르마쿰", "솔 그랑데", "엘레노아", "아벤", "게르디아", "오렐다", "몽환의 퀸", "몽환의 킹", "위대한 성 네리아", "피에르", "나잔", "키즈라", "에이케르", "나베르", "비올레", "굴딩", "변절자 제페토", "아드모스", "아이히만 박사", "패자의 검", "진멸의 창", "갈기파도 항구 네리아", "아비시나", "판다 푸푸", "샐리", "크누트", "아드린느", "자하라", "리루", "나기", "세토", "키케라", "스텔라", "하이거", "루기네", "제레온", "베른 젠로드", "칸다리아 네리아", "신디", "한이 서린 여인", "다단", "자이언트 웜", "우르르", "베나르", "카도건", "몬테르크", "비슈츠", "베르하트", "푸름 전사 브리뉴", "아자란", "가비슈", "집행관 솔라스", "토토이끼", "창조의 알", "수호자 페오스", "여울", "하리", "한손", "호동", "금강", "삭월", "객주도사", "수령도사", "세비엘", "지휘관 솔", "자베른", "레이든", "나크슌", "포포", "도륙자 아르르", "나베갈", "레나", "프랭크", "헨리", "미카엘과 노메드", "몽환의 나이트", "몽환의 룩", "몽환의 비숍", "시이라", "루벤스타인 델 아르코", "이마르", "루티아", "모르페오", "모리나", "월향도사", "다쿠쿠", "표류소녀 엠마", "타냐 벤텀", "고비우스 24세", "용병 세이라", "하리야", "사트라", "킬리언", "오크 장로 질록", "루드벡", "파파", "리웰라", "비비안", "크란테루스", "멜피셔스", "나루니", "사자탈", "고블린 장로 발루", "테르나크", "카드리", "투란", "참크리", "몽환의 폰", "텔파", "이와르", "첼라", "나비", "데메타르", "두키칼리버", "벨리타", "네스", "난민 파밀리아", "다람쥐 욤", "여우 사피아노", "기자 마티아스", "베르베로", "알베르토", "키에사", "하템"
        };

        private DataTable _presets = new DataTable();

        private string[] InitializePresets()
        {
            //프리셋용 데이터테이블 필드 추가
            _presets.Columns.Add("index", typeof(int)); //인덱스
            _presets.Columns.Add("name", typeof(string)); //이름
            _presets.Columns.Add("cards", typeof(int[])); //카드리스트 - 길이7의 int배열
            _presets.Columns.Add("defaultGoal", typeof(int)); //목표각성수치 기본값

            //프리셋 
            _presets.Rows.Add(new object[] {0, "빈 프리셋", new[] {0,0,0,0,0,0,0 }, 0});

            _presets.Rows.Add(new object[] {1, "세상을 구하는 빛(18각)",
                new[] {(int)CardName.Shandi,(int)CardName.Azena, (int)CardName.Ninave, 
                    (int)CardName.Kadan, (int)CardName.Bahunturr, (int)CardName.Silian, (int)CardName.Waye }, 18});

            _presets.Rows.Add(new object[] {2, "세상을 구하는 빛(30각)",
                new[] {(int)CardName.Shandi,(int)CardName.Azena, (int)CardName.Ninave,
                    (int)CardName.Kadan, (int)CardName.Bahunturr, (int)CardName.Silian, (int)CardName.Waye }, 30});

            _presets.Rows.Add(new object[] {3, "남겨진 바람의 절벽(12각)",
                new[] {(int)CardName.Aman,(int)CardName.Seria, (int)CardName.ExecutionerSolas,
                    (int)CardName.LordSilian, (int)CardName.Kharmine, (int)CardName.DelainAman, 0 }, 12});

            _presets.Rows.Add(new object[] {4, "남겨진 바람의 절벽(30각)",
                new[] {(int)CardName.Aman,(int)CardName.Seria, (int)CardName.ExecutionerSolas,
                    (int)CardName.LordSilian, (int)CardName.Kharmine, (int)CardName.DelainAman, 0 }, 30});

            _presets.Rows.Add(new object[] {5, "카제로스의 군단장(18각)",
                new[] {(int)CardName.Valtan,(int)CardName.Illiakan, (int)CardName.Biackiss,
                    (int)CardName.Abrelshud, (int)CardName.Kamen, (int)CardName.KoukuSaton, 0 }, 18});

            _presets.Rows.Add(new object[] {6, "카제로스의 군단장(30각)",
                new[] {(int)CardName.Valtan,(int)CardName.Illiakan, (int)CardName.Biackiss,
                    (int)CardName.Abrelshud, (int)CardName.Kamen, (int)CardName.KoukuSaton, 0 }, 30});

            _presets.Rows.Add(new object[] {7, "창의 달인", //라하르트, 몽나, 앨버하스틱, 아르카디아, 하이비 집행관
                new[] {(int)CardName.Larhart,(int)CardName.GingerWale, (int)CardName.PhantomKnight,
                    (int)CardName.Alberhastic, (int)CardName.Arcadia, (int)CardName.HybeeExecutioner, 0 }, 30});

            _presets.Rows.Add(new object[] {8, "세 우마르 + 라제니스", //에이케르, 나베르, 알레그로
                new[] {(int)CardName.Eikerr,(int)CardName.Naber, (int)CardName.EstherGalaturr,
                    (int)CardName.Beatrice, (int)CardName.Alegro, 0, 0 }, 25});

            var nameArray = _presets.Select().Select(x => x["name"]).ToArray();

            return nameArray.Cast<string>().ToArray();

        }
    }
}
