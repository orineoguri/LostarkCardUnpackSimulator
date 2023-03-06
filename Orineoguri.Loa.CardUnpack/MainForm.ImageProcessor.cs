using Orineoguri.Loa.CardUnpack.Properties;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Orineoguri.Loa.CardUnpack
{
    public partial class MainForm
    {
        private void ReloadSlotImg(int index)
        {
            if(_cardNames[index].SelectedIndex == 0) //빈 슬롯일 경우
            {
                _slotImages[index].Image = (Bitmap)Resources.ResourceManager.GetObject("Img000");

                //각성수치, 잔여량 안보이게
                _slotAwakeImages[index].Visible = false;
                _slotRemainsImages[index].Visible = false;

                return;
            }
            else if (!_cardCollected[index].Checked) //한장도 수집을 못한상태라면
            {                
                //슬롯에 해당하는 카드 로드
                Bitmap slotImage = (Bitmap)Resources.ResourceManager.GetObject("Img"+ _cardNames[index].SelectedIndex.ToString("D3"));
                //해당하는 카드 이미지가 리소스에 없으면 빈 슬롯 이미지로 대체
                if (slotImage is null) { slotImage = (Bitmap)Resources.ResourceManager.GetObject("Img000"); }
                //회색조로 변경해서 슬롯에 반영
                _slotImages[index].Image = MakeGrayScale(slotImage);

                //각성수치, 잔여량 안보이게
                _slotAwakeImages[index].Visible = false;
                _slotRemainsImages[index].Visible = false;

                return;
            }
            else //빈슬롯도 아니고, 한장이상 수집을 했다면
            {
                //슬롯에 해당하는 카드 로드
                Bitmap slotImage = (Bitmap)Resources.ResourceManager.GetObject("Img" + _cardNames[index].SelectedIndex.ToString("D3"));
                //해당하는 카드 이미지가 리소스에 없으면 빈 슬롯 이미지로 대체
                if (slotImage is null) { slotImage = (Bitmap)Resources.ResourceManager.GetObject("Img000"); }
                _slotImages[index].Image = slotImage;

                //각성레벨 반영
                _slotAwakeImages[index].Visible = true;
                Bitmap awakeImage = (Bitmap)Resources.ResourceManager.GetObject("awake" + _cardAwakeLevel[index].Value.ToString());
                _slotAwakeImages[index].Image = awakeImage;

                //잔여량 반영
                if(_cardRemains[index].Value == 0) { _slotRemainsImages[index].Visible = false; return; } //잔여량 0이면 표시안함
                _slotRemainsImages[index].Visible = true;
                Bitmap remainsImage = (Bitmap)Resources.ResourceManager.GetObject("remain" + ((int)_cardRemains[index].Value).ToString("D2"));
                _slotRemainsImages[index].Image = remainsImage;

            }
        }

        private Bitmap MakeGrayScale(Bitmap original) //수집안된 카드 이미지 회색조로 표시
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        private void InitializeSlotImages() //각성레벨, 잔여량 이미지 표시를 위한 pictureBox영역 초기화
        {
            for (int index = 0; index < NUMBER_OF_SLOTS; index++)
            {
                _slotAwakeImages[index] = new PictureBox
                {
                    Size = SLOT_IMG_SIZE,
                    Location = SLOT_IMG_LOCATION,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                _slotRemainsImages[index] = new PictureBox
                {
                    Size = SLOT_IMG_SIZE,
                    Location = SLOT_IMG_LOCATION,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                _slotImages[index].Controls.Add(_slotAwakeImages[index]);
                _slotAwakeImages[index].Controls.Add(_slotRemainsImages[index]);

                _cardNames[index].DataSource = _cardNameList.Clone();
                _cardNames[index].SelectedIndex = 0; //이미지 영역 초기화 직후 카드슬롯 빈 공간으로 통일
            }
        }
    }
}
