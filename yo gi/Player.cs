using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace yo_gi
{
    public class Player
    {
        public FieldPanel MyField;
        public String name;
        public List<Card> deck;  
        public Monster [] MyFieldMonsters;
        public Spell[] MyFieldMagic;
        public List<Card> handCards;
        public List<Card> x;
        Random rnd;    // for creating a random deck 
        //public FieldPanel MyField { set; get; }
        public int LifePoints { set; get; }

        public bool hisTurn
        {
            set;
            get;
        }

        public bool isHost  // for host or guest ( zero for guest , one for host)
        {
            get;
            set;
        }
    public Player(String name, bool isHost)
        {
            /*if (isHost)
            {
               Form1.FieldPanel1.MyTurn = true;
            }
            else if(!isHost)
            {
               Form1.FieldPanel1.MyTurn = false;
            }*/
             this.deck = new List<Card>(10);
            x = new List<Card>(10);
            this.handCards = new List<Card>(6);
            this.name = name;
            this.isHost = isHost;
            this.LifePoints = 4000;
            rnd = new Random();
            this.MyFieldMagic = new Spell[5];
            this.MyFieldMonsters = new Monster[5];
            x.Add(new Spell("Dark Hole", "Spell", "Not", yo_gi.Properties.Resources.DarkHole));
            x.Add(new NormalMonster("Azir", "Monster", "NOT", yo_gi.Properties.Resources._1,3, 2500, 1000, TheState.Att));
            x.Add(new NormalMonster("BATTLECAST XERATH", "Monster", "NOT", yo_gi.Properties.Resources._2, 3, 1000, 0, TheState.Att));
            x.Add(new NormalMonster("BloodFury", "Monster", "NOT", yo_gi.Properties.Resources._3, 3, 2000, 2000, TheState.Att));
            x.Add(new NormalMonster("Fiddlesticks", "Monster", "NOT", yo_gi.Properties.Resources._4, 3, 1000, 0, TheState.Att));
            x.Add(new Spell("Bot Of Greed", "Spell", "Not", yo_gi.Properties.Resources.BotOfGreed));
            x.Add(new NormalMonster("Gurdian of the sand", "Monster", "NOT", yo_gi.Properties.Resources._5, 3, 500, 3000, TheState.Att));
            x.Add(new NormalMonster("Inffernal", "Monster", "NOT", yo_gi.Properties.Resources._6, 3, 2000, 2300, TheState.Att));
            x.Add(new NormalMonster("Risen_Fiddlesticks", "Monster", "NOT", yo_gi.Properties.Resources.Risen_Fiddlesticks, 3, 1800, 100, TheState.Att));
            x.Add(new Spell("Fissure", "Spell", "Not", yo_gi.Properties.Resources.Fissure));
            x.Add(new NormalMonster("Gudrdian of the sand Khazix", "Monster", "NOT", yo_gi.Properties.Resources._7, 3, 2200, 1000, TheState.Att));
            x.Add(new NormalMonster("Rammus", "Monster", "NOT", yo_gi.Properties.Resources.Rammus, 3, 0, 1500, TheState.Att));
            x.Add(new NormalMonster("Renekton", "Monster", "NOT", yo_gi.Properties.Resources.Renekton, 3, 1500, 500, TheState.Att));
            x.Add(new Spell("Legendry Sword", "Spell", "Not", yo_gi.Properties.Resources.LegendrySword));
            x.Add(new NormalMonster("Khazix XERATH", "Monster", "NOT", yo_gi.Properties.Resources._8, 3, 1300, 500, TheState.Att));
            x.Add(new NormalMonster("Nasus", "Monster", "NOT", yo_gi.Properties.Resources._9, 3, 1000, 700, TheState.Att));
            x.Add(new NormalMonster("Runeborn_Xerath", "Monster", "NOT", yo_gi.Properties.Resources.Runeborn_Xerath, 3, 1000, 0, TheState.Att));
            //x.Add(new Spell("Shurima", "Spell", "Not", yo_gi.Properties.Resources.ShurimaDesert));
            //x.Add(new Spell("DarkHole", "Spell","Not", yo_gi.Properties.Resources.ShurimaDesert));
            GenerateDeck();
        }
    public void start()// to drow 5 cards when start
        {
            for(int i = 0; i < 5; i++)
            {
                Card c = GetCard(0, this.deck);
                this.handCards.Add(c);
                FieldPanel.HCards[i].c = c;
                //FieldPanel.HCards[i].Image = c.image;
                this.deck.RemoveAt(0);
            }
            handCards.Insert(5, null);
        }
    public void drawCard() //GetCard dih method betreturn card data
        {
            
            //GUI
            Card c = GetCard(0, this.deck);
            for (int i = 0; i < 6; i++)
            {
                if (FieldPanel.HCards[i].c == null)
                {
                    FieldPanel.HCards[i].Location = new Point(FieldPanel.HCards[i].Location.X, 730);
                    handCards.Insert(i, c);
                    FieldPanel.HCards[i].c = c;
                    FieldPanel.HCards[i].Image = c.image;
                    this.deck.RemoveAt(0);
                    FieldPanel.Counter++;
                    break;
                }
            }
            
        }

    public void putCard(Card c)//put card
    {
        for (int i = 0; i < 6; i++)
        {
            if (FieldPanel.HCards[i].c == c)
            {
                handCards.Insert(i, null);
                break;
            }
        }
    }

    public Card getCard(int i)//take card from hand
    {
        return handCards.ElementAt(i);
    }
        //ther is add function in field it use to summon
        public void SummonMonster(String Choice) //neglecting level of monster   
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                if (this.MyFieldMonsters[i] != null)
                {
                    count++;
                    continue;
                }
                else if(count != 5)
                {
                    //GUI (event handling on selecting a monster)
                    // inside the monster nfso
                    this.MyFieldMonsters[i] = (Monster)GetCard(3/*card number fl 2eed*/, handCards);
                    switch (Choice)//choice da ele haeb2a fl menu ele htetla3 lma ydos 3la card
                    {
                        case "atack":
                            //GUI
                            this.MyFieldMonsters[i].state = TheState.Att; // 3la el enum the state ele fe class monster
                            break;
                        case "defence":
                            //GUI
                            this.MyFieldMonsters[i].state = TheState.def; // 3la el enum the state ele fe class monster
                            break;
                        case "Defence":
                            //GUI
                            this.MyFieldMonsters[i].state = TheState.defFlip; // 3la el enum the state ele fe class monster
                            break;
                    }
                    handCards.RemoveAt(3/*makan el card fl 2eed , ele howa index*/);
                    break;
                }
                else
                {
                    //GUI message
                    String message = "you can't summon a monster , field is full";
                }
            }
        }

    public void putSpellCard()
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                if (this.MyFieldMagic[i] != null)
                {
                    count++;
                    continue;
                }
             //else if(count!=5)
             //   {
                    //GUI (event handling on selecting a monster)
                    // inside the monster nfso
                 //   this.MyFieldMagic[i] = GetCard(/*card number fl 2eed*/, handCards);
                 //   handCards.RemoveAt(/*card Number fl 2eed*/);
                  //  break;
              //  }
                else
                {
                    //GUI message 
                    String message = "you can't put a spell card , field is full";
                }
                   
             }
        }
    public void DecreaseBy(int x)
        {
            this.LifePoints -= x;
        }
    private Card GetCard(int index,List<Card> AL)
        {
            
            return AL[index];
        }
    public void GenerateDeck()
        {
            //x hna = arrayList of Cards , 40 da 3adad elements ele fl arraylist of cards el2saseya (3adad el kroot y3ny)
            this.deck = x;    
            /*for (int i = 0; i < 29; i++){
                this.deck.Add(x[rnd.Next(x.Count)]);
                x.Remove(this.deck.ElementAt(i));
            }*/
            this.deck.Add(new Spell("Shurima", "Spell", "Not", yo_gi.Properties.Resources.ShurimaDesert));
        }

    public void EndTurn1(Player p) { this.hisTurn = false; 
        p.hisTurn=true; 
    }

    }//class end
}//namespace end