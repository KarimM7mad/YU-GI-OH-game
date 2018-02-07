using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace yo_gi
{
    [Serializable]
    public  class Actionn
    {
        public Card[] p1MonsterCard = new Card[5];
        public Card[] p2MonsterCard = new Card[5];
        public Card[] p1MagicCard = new Card[5];
        public Card[] p2MagicCard = new Card[5];
        public bool MyOppTurn;
        public int p1Life;
        public int p2Life;
        public Card FieldCard=null;
        public string Win = null;

        public Actionn(bool MyOppTurn,Card[] p1MonsterCard,Card[] p2MonsterCard,Card[] p1MagicCard,Card[] p2MagicCard,int p1Life,int p2Life)
        {
            for (int i = 0; i < 5; i++)
            {
                this.p1MagicCard[i] = p1MagicCard[i];
                this.p1MonsterCard[i] = p1MonsterCard[i];
                this.p2MagicCard[i] = p2MagicCard[i];
                this.p2MonsterCard[i] = p2MonsterCard[i];
            }
            this.MyOppTurn = MyOppTurn;
            this.p1Life = p1Life;
            this.p2Life = p2Life;
            this.FieldCard = null;
        }
        public Actionn(bool MyOppTurn, Card[] p1MonsterCard, Card[] p2MonsterCard, Card[] p1MagicCard, Card[] p2MagicCard,Card Field, int p1Life, int p2Life)
        {
            for (int i = 0; i < 5; i++)
            {
                this.p1MagicCard[i] = p1MagicCard[i];
                this.p1MonsterCard[i] = p1MonsterCard[i];
                this.p2MagicCard[i] = p2MagicCard[i];
                this.p2MonsterCard[i] = p2MonsterCard[i];
            }
            this.MyOppTurn = MyOppTurn;
            this.p1Life = p1Life;
            this.p2Life = p2Life;
            this.FieldCard = Field;
        }
        public Actionn(bool MyOppTurn, Card[] p1MonsterCard, Card[] p2MonsterCard, Card[] p1MagicCard, Card[] p2MagicCard,string win, int p1Life, int p2Life)
        {
            for (int i = 0; i < 5; i++)
            {
                this.p1MagicCard[i] = p1MagicCard[i];
                this.p1MonsterCard[i] = p1MonsterCard[i];
                this.p2MagicCard[i] = p2MagicCard[i];
                this.p2MonsterCard[i] = p2MonsterCard[i];
            }
            this.MyOppTurn = MyOppTurn;
            this.p1Life = p1Life;
            this.p2Life = p2Life;
            this.FieldCard = null;
            win = "W";
        }
    }
}
