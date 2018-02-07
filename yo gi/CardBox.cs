using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yo_gi
{
    [Serializable]
    public class CardBox : PictureBox
    {
        public Card c;
        public CardBox()
        {
        }
        public CardBox(Card c)
        {
            this.Image = c.image;
            this.c = c;
        }
    }
}
