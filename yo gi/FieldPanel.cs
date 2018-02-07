using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Threading;
using System.IO;
using System.Media;


namespace yo_gi
{
    class Msgs : Form
    {
        public static Msgs msg1 = new Msgs();
        public static Msgs msg2 = new Msgs();
        public static Msgs msg3 = new Msgs();
        public static Msgs msg4 = new Msgs();
        public static Msgs msg5 = new Msgs();
        public Msgs()
        {
            CenterToScreen();
            this.TransparencyKey = Color.Black;
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;

        }


    }
    [Serializable]
    public class FieldPanel:Panel

    {
        public bool CanDraw = false;
        public Spell SpellActive;
        public bool SpellMode;
        public Card RefMonster;
        #region Sounds & Streams
        Stream str1 = Properties.Resources.draw;
        Stream str2 = Properties.Resources.Destroyed;
        Stream str3 = Properties.Resources.Summon;
        Stream str4 = Properties.Resources.Glow;
        Stream str5 = Properties.Resources.Phases;
        Stream str6 = Properties.Resources.LP_Wipeout;
        SoundPlayer sp1, sp2, sp3, sp4, sp5, sp6;
        #endregion
        public System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer t2 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer t3 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer t4 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer t5 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer tDraw = new System.Windows.Forms.Timer();
        public bool MyTurn=true;
        public bool AttMode;
        public bool FirstTurn = true;
        public NormalMonster MTA;//Monster To Attack
        public NormalMonster MAttacked;
        public Boolean isSummon = false;
        public Boolean exist = false;
        public static int Counter = 5;
        public Thread thread;
        #region Label
        public Label SurrLbl = new Label();
        public Label EndLbl = new Label();
        public Label AtkLbl = new Label();
        public Label ATkphLbl = new Label();
        public Label SurrphLbl = new Label();
        public Label TurnphLbl = new Label();
        public Label DrawphLbl = new Label();
        public Label WonLbl = new Label();
#endregion
        public CardBox FieldBox = new CardBox();
        #region Images
        public Image c1 = yo_gi.Properties.Resources._1;
        public Image c2 = yo_gi.Properties.Resources._2;
        public Image c3 = yo_gi.Properties.Resources._3;
        public Image c4 = yo_gi.Properties.Resources._4;
        public Image c5 = yo_gi.Properties.Resources._5;
        public Image c6 = yo_gi.Properties.Resources._6;
        public Image c7 = yo_gi.Properties.Resources._7;
        public Image c8 = yo_gi.Properties.Resources._8;
        public Image c9 = yo_gi.Properties.Resources._9;
        public Image c10 = yo_gi.Properties.Resources._10;
        public Image ShurimaCard = yo_gi.Properties.Resources.ShurimaDesert;
        public Image ShurimaField = yo_gi.Properties.Resources.Shurima;
        public Image surrImg = yo_gi.Properties.Resources.surrflag;
        public Image InitImg = yo_gi.Properties.Resources.TransparentImg;
        public Image endImg = yo_gi.Properties.Resources.endturn;
        public Image atkImg = yo_gi.Properties.Resources.Attack;
        public Image fieldFrame = yo_gi.Properties.Resources.frame;

        #endregion
        public Card[] p1MonsterCard = new Card[5];
        public Card[] p2MonsterCard = new Card[5];
        public Card[] p1MagicCard = new Card[5];
        public Card[] p2MagicCard = new Card[5];
        #region PictureBoxes
        public static CardBox[] HCards = new CardBox[6];
        public static PictureBox[] DeckCards = new PictureBox[10];
        public PictureBox[] p1MonsterBox = new PictureBox[5];
        public PictureBox[] p2MonsterBox = new PictureBox[5];
        public PictureBox[] p1MagicBox = new PictureBox[5];
        public PictureBox[] p2MagicBox = new PictureBox[5];
        #endregion
        public Bitmap BackGround, Fieldbg;
        public static Bitmap MainBackGround;
        Graphics g;

        public FieldPanel()
        {
            tDraw.Interval = 100;
            tDraw.Start();
            tDraw.Tick += tDraw_Tick;




            //Sounds
            sp1 = new SoundPlayer(str1);
            sp2 = new SoundPlayer(str2);
            sp3 = new SoundPlayer(str3);
            sp4 = new SoundPlayer(str4);
            sp5 = new SoundPlayer(str5);
            sp6 = new SoundPlayer(str6);


            //To stop flickering
            this.DoubleBuffered = true;

            InitHCards();
            InitDeck();

            //Drawing cards thread
            thread = new Thread(new ThreadStart(StartDrawing));
            thread.Start();

            //FieldFrame picturebox
            FieldBox.Image = fieldFrame;
            FieldBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            FieldBox.BackColor = Color.Transparent;
            FieldBox.SetBounds(410, 300, 131, 196);
            this.Controls.Add(FieldBox);


            //Attack Label
            AtkLbl.Image = atkImg;
            AtkLbl.Cursor = Cursors.Hand;
            AtkLbl.BackColor = Color.Transparent;
            AtkLbl.SetBounds(1250, 200, 120, 129);
            AtkLbl.MouseClick += Attack;
            this.Controls.Add(AtkLbl);


            //Surrender Label
            SurrLbl.Image = surrImg;
            SurrLbl.Cursor = Cursors.Hand;
            SurrLbl.BackColor = Color.Transparent;
            SurrLbl.SetBounds(1250, 350, 120, 129);
            SurrLbl.MouseClick += Surrender;
            this.Controls.Add(SurrLbl);


            //Endturn Label
            EndLbl.Image = endImg;
            EndLbl.Cursor = Cursors.Hand;
            EndLbl.BackColor = Color.Transparent;
            EndLbl.SetBounds(1270, 510, 80, 80);
            EndLbl.MouseClick += EndTurn;
            this.Controls.Add(EndLbl);

            MainBackGround = new Bitmap(yo_gi.Properties.Resources.BG);
            BackGround = MainBackGround;
            for (int i = 0; i < 5; i++)
            {
                p1MonsterBox[i] = new PictureBox();
                p2MonsterBox[i] = new PictureBox();
                p1MagicBox[i] = new PictureBox();
                p2MagicBox[i] = new PictureBox();
                p1MagicBox[i].Location = new Point((620 + 104 * i), (580));
                p1MonsterBox[i].Location = new Point((620 + 104 * i), (424));
                p2MagicBox[i].Location = new Point((620 + 104 * i), (106));
                p2MonsterBox[i].Location = new Point((620 + 104 * i), (260));
                /*p1MagicBox[i].Location = new Point((820 + 104 * i), (665));
                p1MonsterBox[i].Location = new Point((820 + 104 * i), (526));
                p2MagicBox[i].Location = new Point((820 + 104 * i), (206));
                p2MonsterBox[i].Location = new Point((820 + 104 * i), (360));*/
                p1MagicBox[i].Size = new Size(100, 158);
                p2MagicBox[i].Size = new Size(100, 158);
                p1MonsterBox[i].Size = new Size(100, 158);
                p2MonsterBox[i].Size = new Size(100, 158);
                p1MagicBox[i].Image = new Bitmap(yo_gi.Properties.Resources.frame);
                this.p1MagicBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                p2MagicBox[i].Image = new Bitmap(yo_gi.Properties.Resources.frame);
                this.p2MagicBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                p1MonsterBox[i].Image = new Bitmap(yo_gi.Properties.Resources.frame);
                this.p1MonsterBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                p2MonsterBox[i].Image = new Bitmap(yo_gi.Properties.Resources.frame);
                this.p2MonsterBox[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.Controls.Add(p1MagicBox[i]);
                this.Controls.Add(p2MagicBox[i]);
                this.Controls.Add(p1MonsterBox[i]);
                this.Controls.Add(p2MonsterBox[i]);
                p1MagicBox[i].MouseEnter += showCard;
                p2MagicBox[i].MouseEnter += showCard;
            }
            #region MonsterShow
            p1MonsterBox[0].MouseEnter += p1Monster0Show;
            p2MonsterBox[0].MouseEnter += p2Monster0Show;
            p1MonsterBox[1].MouseEnter += p1Monster1Show;
            p2MonsterBox[1].MouseEnter += p2Monster1Show;
            p1MonsterBox[2].MouseEnter += p1Monster2Show;
            p2MonsterBox[2].MouseEnter += p2Monster2Show;
            p1MonsterBox[3].MouseEnter += p1Monster3Show;
            p2MonsterBox[3].MouseEnter += p2Monster3Show;
            p1MonsterBox[4].MouseEnter += p1Monster4Show;
            p2MonsterBox[4].MouseEnter += p2Monster4Show;
            #endregion
            #region MonsterAttack
            p1MonsterBox[0].MouseClick += p1Monster0;
            p2MonsterBox[0].MouseClick += p2Monster0;
            p1MonsterBox[1].MouseClick += p1Monster1;
            p2MonsterBox[1].MouseClick += p2Monster1;
            p1MonsterBox[2].MouseClick += p1Monster2;
            p2MonsterBox[2].MouseClick += p2Monster2;
            p1MonsterBox[3].MouseClick += p1Monster3;
            p2MonsterBox[3].MouseClick += p2Monster3;
            p1MonsterBox[4].MouseClick += p1Monster4;
            p2MonsterBox[4].MouseClick += p2Monster4;
            #endregion
            #region Spell Cards Sellect Monster
            SpellMode = false;
            RefMonster = null;
            p1MonsterBox[0].MouseClick += p1MonsterSpell0;
            p2MonsterBox[0].MouseClick += p2MonsterSpell0;
            p1MonsterBox[1].MouseClick += p1MonsterSpell1;
            p2MonsterBox[1].MouseClick += p2MonsterSpell1;
            p1MonsterBox[2].MouseClick += p1MonsterSpell2;
            p2MonsterBox[2].MouseClick += p2MonsterSpell2;
            p1MonsterBox[3].MouseClick += p1MonsterSpell3;
            p2MonsterBox[3].MouseClick += p2MonsterSpell3;
            p1MonsterBox[4].MouseClick += p1MonsterSpell4;
            p2MonsterBox[4].MouseClick += p2MonsterSpell4;
            #endregion
            for (int i = 0; i < 6; i++)
            {
                HCards[i].MouseEnter += showCard;
                HCards[i].MouseClick += cardClick;
                HCards[i].MouseEnter += Rise;
                HCards[i].MouseLeave += Fall;
              //HCards[i].MouseDoubleClick += cardDouble;
            }
            if (MyTurn)
                FirstTurn = true;
            else
                FirstTurn = false;
            
        }

        void tDraw_Tick(object sender, EventArgs e)
        {
            if (CanDraw)
            {
                if (Counter != 18)
                { DrawOneCard(); }
                else {
                    CanDraw = false;
                    /*Program.Game.p1.LifePoints = 0;
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    tDraw.Stop();
                    this.CheckWinner();*/
                }
                CanDraw = false;
            }
        }
        //Attack phase image
        public void showMSG1()
        {
            sp5.Play();
            Msgs.msg1.SetBounds(400, 320, 857, 181);
            Msgs.msg1.Show();
            t1.Interval = 1000;
            t1.Start();
            ATkphLbl.Image = yo_gi.Properties.Resources.atkph;
            ATkphLbl.BackColor = Color.Transparent;
            ATkphLbl.BringToFront();
            ATkphLbl.Dock = DockStyle.Fill;
            Msgs.msg1.Controls.Add(ATkphLbl);
            t1.Tick += TerminateMsg1;

        }

        //You've lost! image
        public void showMSG2()
        {
            sp6.Play();
            Msgs.msg2.SetBounds(400, 320, 857, 181);
            Msgs.msg2.Show();
            t2.Interval = 2000;
            t2.Start();
            SurrphLbl.Image = yo_gi.Properties.Resources.lost;
            SurrphLbl.BackColor = Color.Transparent;
            SurrphLbl.BringToFront();
            SurrphLbl.Dock = DockStyle.Fill;
            Msgs.msg2.Controls.Add(SurrphLbl);
            t2.Tick += TerminateMsg2;

        }

        //Turn change image
        public void showMSG3()
        {

            Msgs.msg3.SetBounds(400, 320, 857, 181);
            Msgs.msg3.Show();
            t3.Interval = 1000;
            t3.Start();
            TurnphLbl.Image = yo_gi.Properties.Resources.turnch;
            TurnphLbl.BackColor = Color.Transparent;
            TurnphLbl.BringToFront();
            TurnphLbl.Dock = DockStyle.Fill;
            Msgs.msg3.Controls.Add(TurnphLbl);
            t3.Tick += TerminateMsg3;

        }

        //Draw phase image
        public void showMSG4()
        {
            Msgs.msg4.SetBounds(400, 320, 857, 181);
            Msgs.msg4.Show();
            t4.Interval = 1000;
            t4.Start();
            DrawphLbl.Image = yo_gi.Properties.Resources.drawph;
            DrawphLbl.BackColor = Color.Transparent;
            DrawphLbl.BringToFront();
            DrawphLbl.Dock = DockStyle.Fill;
            Msgs.msg4.Controls.Add(DrawphLbl);
            t4.Tick += TerminateMsg4;

        }

        //You've won! image
        public void showMSG5()
        {
            Msgs.msg5.SetBounds(400, 320, 857, 181);
            Msgs.msg5.Show();
            t5.Interval = 2000;
            t5.Start();
            WonLbl.Image = yo_gi.Properties.Resources.won;
            WonLbl.BackColor = Color.Transparent;
            WonLbl.BringToFront();
            WonLbl.Dock = DockStyle.Fill;
            Msgs.msg5.Controls.Add(WonLbl);
            t5.Tick += TerminateMsg5;

        }

        private void TerminateMsg1(object sender, EventArgs e)
        {
            Msgs.msg1.Hide();
            t1.Stop();
        }
        private void TerminateMsg2(object sender, EventArgs e)
        {
            
            Application.Exit();
            //GameNetwork.Terminate();
            t2.Stop();
        }
        private void TerminateMsg3(object sender, EventArgs e)
        {
            Msgs.msg3.Hide();
            t3.Stop();
        }

        private void TerminateMsg4(object sender, EventArgs e)
        {
            Msgs.msg4.Hide();
            t4.Stop();
        }

        private void TerminateMsg5(object sender, EventArgs e)
        {
            
            Application.Exit();
            //GameNetwork.Terminate();
            t5.Stop();
        }
        private void BorderColour(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((PictureBox)sender).ClientRectangle, Color.Red, ButtonBorderStyle.Solid);

        }

        private void BorderSwiping(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((PictureBox)sender).ClientRectangle, Color.DimGray, ButtonBorderStyle.Solid);

        }

        private void Fall(object sender, EventArgs e)
        {
            if (!(((PictureBox)sender).Image == InitImg || ((PictureBox)sender).Image == null))
            {
                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 730);
                ((PictureBox)sender).Paint += BorderSwiping;
            }

        }


        private void Rise(object sender, EventArgs e)
        {
            if (!(((PictureBox)sender).Image == InitImg || ((PictureBox)sender).Image == null))
            {
                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 715);
                ((PictureBox)sender).Paint += BorderColour;
            }


        }
        private void Attack(object sender, MouseEventArgs e)
        {
            //I don't know what to write here :D
            //bs yla
            if (!FirstTurn)
            {
                if (!SpellMode)
                {
                    if (MyTurn)
                    {
                        if (AttMode == false)
                        {
                            AttMode = true;
                            showMSG1();
                        }
                        
                    }
                    else
                        MessageBox.Show("Not your Turn");
                }
                else
                    MessageBox.Show("you Should choose Monster to your Spell");
                }
                else
                    MessageBox.Show("you Cant Attack in this Turn");
        }

        private void p1Monster(NormalMonster m)
        {
            if (!SpellMode)
            {

                if (AttMode && m.state == TheState.Att)
                {
                    System.Console.WriteLine(((m.DefPoint )));
                    if (!m.Attacked)
                    {
                        if (!this.IsP2FieldEmpty())
                            MTA = m;
                        else
                        {
                            Program.Game.p2.DecreaseBy(m.AttPoint + m.BounsAtt);
                            this.Invalidate();
                            GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));

                            CheckWinner();
                        }
                        m.Attacked = true;
                    }
                    else
                        MessageBox.Show("you Can't Attack with this Monster");
                }
                else
                    MessageBox.Show("monster should be in Att State");
            }
        }
        private void p2Monster(NormalMonster m)
        {
            if (!SpellMode)
            {
                if (AttMode && m != null)
                {
                    if (MTA != null)
                    {
                        MAttacked = m;
                        if (MAttacked.state == TheState.Att)
                        {
                            if (MTA.AttPoint + MTA.BounsAtt > MAttacked.AttPoint + MAttacked.BounsAtt)
                            {
                                this.removeMonsterP2(MAttacked);
                                Program.Game.p2.DecreaseBy(MTA.AttPoint + MTA.BounsAtt - MAttacked.AttPoint);

                            }
                            else if (MTA.AttPoint + MTA.BounsAtt < MAttacked.AttPoint + MAttacked.BounsAtt)
                            {
                                this.removeMonsterP1(MTA);
                                Program.Game.p1.DecreaseBy(MAttacked.AttPoint + MAttacked.BounsAtt - MTA.AttPoint);

                            }
                            else
                            {
                                this.removeMonsterP1(MTA);
                                this.removeMonsterP2(MAttacked);

                            }
                        }
                        else
                        {
                            if (MTA.AttPoint + MTA.BounsAtt > MAttacked.DefPoint + MAttacked.BounsDef)
                            {
                                this.removeMonsterP2(MAttacked);

                            }
                            else if (MTA.AttPoint + MTA.BounsAtt < MAttacked.DefPoint + MAttacked.BounsDef)
                            {
                                this.removeMonsterP1(MTA);
                                Program.Game.p1.DecreaseBy(MAttacked.DefPoint - MTA.AttPoint);
                            }
                            else
                            {
                                this.removeMonsterP1(MTA);
                                this.removeMonsterP2(MAttacked);


                            }
                            
                        }
                        this.Invalidate();
                        GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                        this.CheckWinner();
                        MTA = null;
                        MAttacked = null;
                    }
                    else
                        MessageBox.Show("you should choose your Monster to Attack");
                }
            }
        }
        private void EndTurn(object sender, MouseEventArgs e)
        {
            //"sp5.Play();" hatet7at 3and el player ely das 3al zorar 3shan msh hays7ab war2a la2n msh haynfa3 ash3'al 2 sounds fe nafs el wa2t
            //**El 5olasa** law player 1 das endturn --> sp5.Play(); and player 2 will call the function DrawOneCard
            if (!SpellMode)
            {
                if (MyTurn)
                {
                    sp5.Play();
                    showMSG3();
                    
                    isSummon = false;
                    MyTurn = false;
                    this.MonsterReload();
                    this.AttMode = false;
                    FirstTurn = false;
                    this.Invalidate();
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    
                    
                }
                else
                    MessageBox.Show("Not Your Turn");
            }
            else
                MessageBox.Show("you Should choose Monster to your Spell");
        }
        private void Surrender(object sender, MouseEventArgs e)
        {
            if (MyTurn)
            {
                sp6.Play();
                Program.Game.p1.LifePoints = 0;
                GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, "W", Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                showMSG2();
                Application.Exit();
                //Terminate the connection between player 1 and 2
                GameNetwork.Terminate();
            }
            else
                MessageBox.Show("Surrender In Your Turn");
        }

        private void cardClick(object sender, MouseEventArgs e)
        {
            
                if (MyTurn)
                {
                    try
                    {
                        thread.Join();
                        CardBox p = (CardBox)sender;
                        Card c = p.c;
                        if (e.Button == MouseButtons.Right)
                        {
                            if (c.Name.Equals("Shurima"))
                            {
                                this.addField(c, p);
                                Fieldbg = new Bitmap(yo_gi.Properties.Resources.Shurima);
                                BackGround = Fieldbg;
                                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 880);
                                this.Invalidate();
                                ((PictureBox)sender).Paint += BorderSwiping;
                            }
                            else if (c.type.Equals("Monster"))
                            {
                                if (!isSummon)
                                {
                                    this.addMonster(c, p, "defence");
                                    p.c = null;
                                    isSummon = true;
                                    ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 880);
                                    ((PictureBox)sender).Paint += BorderSwiping;
                                }
                            }

                            else
                            {
                                this.addMagic(c, p);
                                p.c = null;
                                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 880);
                                ((PictureBox)sender).Paint += BorderSwiping;
                            }
                        }
                        else if (e.Button == MouseButtons.Left)
                        {
                            if (c.Name.Equals("Shurima"))
                            {
                                this.addField(c, p);
                                Fieldbg = new Bitmap(yo_gi.Properties.Resources.Shurima);
                                BackGround = Fieldbg;
                                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 880);
                                this.Invalidate();
                                ((PictureBox)sender).Paint += BorderSwiping;
                            }
                            else if (c.type.Equals("Monster"))
                            {
                                if (!isSummon)
                                {
                                    this.addMonster(c, p, "Attack");
                                    p.c = null;
                                    isSummon = true;
                                    ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 880);
                                    ((PictureBox)sender).Paint += BorderSwiping;
                                }
                            }

                            else
                            {
                                this.addMagic(c, p);
                                p.c = null;
                                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X, 880);
                                ((PictureBox)sender).Paint += BorderSwiping;
                            }
                        }

                    }
                    catch (Exception ex)
                    { }
                }
                else
                    MessageBox.Show("Not your Turn");
           
        }
        private void showCard(object sender, System.EventArgs e)
        {
            if(((PictureBox)sender).Image!=null || ((CardBox)sender).c!=null)
                Form1.CardShow.Image = ((PictureBox)sender).Image;
        }
        /// <summary>
        /// Initialize the cards in the players' hand to a transparent image
        /// </summary>
        public void InitHCards()
        {
            int v = 0;
            for (int i = 0; i < 6; i++)
            {
                HCards[i] = new CardBox();
                HCards[i].BackColor = System.Drawing.Color.Transparent;
                HCards[i].Cursor = Cursors.Hand;
                HCards[i].SetBounds(400 + v, 730, 140, 140);
                //HCards[i].SetBounds(600 + v, 870, 190, 190);
                HCards[i].Image = InitImg;
                HCards[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

                v += 150;

                this.Controls.Add(HCards[i]);

            }

        }
        /// <summary>
        /// Initialize all the cards in the deck
        /// </summary>

        public void InitDeck()
        {
            for (int i = 0; i < 10; i++)
            {
                DeckCards[i] = new PictureBox();
                DeckCards[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.Controls.Add(DeckCards[i]);

            }
            for (int i = 0; i < 2; i++)
            {
                //DeckCards[i].Image = Program.Game.p1.deck.ElementAt(i).image;
            }
            /*DeckCards[0].Image = ShurimaCard;
            DeckCards[1].Image = c1;
            DeckCards[2].Image = c3;
            DeckCards[3].Image = c4;
            DeckCards[4].Image = c5;
            DeckCards[5].Image = c6;
            DeckCards[6].Image = c7;
            DeckCards[7].Image = c8;
            DeckCards[8].Image = c9;
            DeckCards[9].Image = c10;*/

        }

        /// <summary>
        /// Each player will have 6 cards in his hands after calling this function
        /// </summary>

        public void StartDrawing()
        {
                //showMSG4();
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    if (HCards[i].Image == InitImg)
                    { 
                        HCards[i].Image = DeckCards[i].Image;
                        sp1.Play();
                    }
                }
                Program.Game.p1.start();
                //Program.Game.p2.start();
            

        }
        /// <summary>
        /// At the end of each turn the next player will be able only to draw one card
        /// </summary>

        public void DrawOneCard()
        {
                for (int i = 0; i < 6; i++)
                {
                    if (HCards[i].Image == null)
                    {
                        sp1.Play();
                        //showMSG4();
                        break;
                    }
                    //else { continue; }
                }
                Program.Game.p1.drawCard();
            }
            

        #region Player 1

        /// <summary>
        /// To destroy or remove any monster card for player 1
        /// </summary>
        /// <param name="m">Monster Card</param>
        public void removeAllMonsterP1()
        {
            for (int i = 0; i < 5; i++)
            {
                
                    p1MonsterCard[i] = null;
                    p1MonsterBox[i].Image = fieldFrame;
                    this.Invalidate();
            }
            GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
        }

        /// <summary>
        /// To destroy or remove any monster card for player 1
        /// </summary>
        /// <param name="m">Monster Card</param>
        public void removeMonsterP1(Card m)
        {
            for (int i = 0; i < 5; i++)
            {
                if (m.Equals(p1MonsterCard[i]))
                {
                    p1MonsterCard[i] = null;
                    p1MonsterBox[i].Image = fieldFrame;
                    this.Invalidate();
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    break;
                }
            }
        }
        /// <summary>
        /// To destroy or remove any magic card for player 1
        /// </summary>
        /// <param name="m">Magic Card</param>
        public void removeMagicP1(Card m)
        {
            for (int i = 0; i < 5; i++)
            {
                if (m.Equals(p1MagicCard[i]))
                {
                    p1MagicCard[i] = null;
                    p1MagicBox[i].Image = fieldFrame;
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    break;
                }
            }
        }
        #endregion

        #region Player 2

        /// <summary>
        /// To destroy or remove any monster card for player 1
        /// </summary>
        /// <param name="m">Monster Card</param>
        public void removeAllMonsterP2()
        {
            for (int i = 0; i < 5; i++)
            {

                p2MonsterCard[i] = null;
                p2MonsterBox[i].Image = fieldFrame;
                this.Invalidate();

            }
            GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
        }

        /// <summary>
        /// To destroy or remove any monster card for player 2
        /// </summary>
        /// <param name="m">Monster Card</param>
        public void removeMonsterP2(Card m)
        {
            for (int i = 0; i < 5; i++)
            {
                if (m.Equals(p2MonsterCard[i]))
                {
                    p2MonsterCard[i] = null;
                    p2MonsterBox[i].Image = fieldFrame;
                    this.Invalidate();
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    break;
                }
            }
        }
        /// <summary>
        /// To destroy or remove any magic card for player 2
        /// </summary>
        /// <param name="m">Magic Card</param>
        public void removeMagicP2(Card m)
        {
            for (int i = 0; i < 5; i++)
            {
                if (m.Equals(p1MagicCard[i]))
                {
                    p2MagicCard[i] = null;
                    p2MagicBox[i].Image = fieldFrame;
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    break;
                }
            }
        }

#endregion

        public void addField(Card c , CardBox p)
        {
            sp4.Play();
            exist = true;
            FieldBox.c = c;
            FieldBox.Image = c.image;
            p.Image = null;
            p.c = null;
            GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard,FieldBox.c, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
        }
        /// <summary>
        /// add magic card to field
        /// </summary>
        /// <param name="m">Magic card</param>
        public void addMagic(Card m, PictureBox p)
        {
            for (int i = 0; i < 5; i++)
            {
                if (p1MagicCard[i] == null)
                {
                    sp3.Play();
                    p1MagicCard[i] = m;
                    p1MagicBox[i].Image = m.image;
                    p.Image = null;
                    Spell.SpellFunction(m.Name);
                    SpellActive = (Spell)m;
                    if (!SpellMode)
                        this.removeMagicP1(SpellActive);
                    GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                    break;
                }

            }
        }
        /// <summary>
        /// summon any monster to field for player 1
        /// </summary>
        /// <param name="m">Monster card</param>
        public void addMonster(Card m, PictureBox p, string Choice)
        {
            NormalMonster c = (NormalMonster)m;
            for (int i = 0; i < 5; i++)
            {
                if (p1MonsterCard[i] == null)
                {
                    sp3.Play();
                    if (Choice.Equals("Attack"))
                    {
                        c.state = TheState.Att;
                        p1MonsterCard[i] = c;
                        p1MonsterBox[i].Image = c.image;
                        p.Image = null;
                        Program.Game.p1.putCard(c);
                        this.Invalidate();
                        GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                        break;
                    }
                    else if (Choice.Equals("defence"))
                    {
                        c.state = TheState.def;
                        // p1MonsterBox[i].c = m;
                        p1MonsterCard[i] = c;
                        Bitmap im = (Bitmap)p.Image;
                        im.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        p1MonsterBox[i].Image = im;
                        p.Image = null;
                        Program.Game.p1.putCard(c);
                        this.Invalidate();
                        GameNetwork.SendUpdates(new Actionn(MyTurn, p1MonsterCard, p2MonsterCard, p1MagicCard, p2MagicCard, Program.Game.p1.LifePoints, Program.Game.p2.LifePoints));
                        break;
                    }
                }
            }
        }

        public void UpdateField()
        {
            if (GameNetwork.CommonAction != null)
            {
                if (GameNetwork.CommonAction.Win !=null && GameNetwork.CommonAction.Win.Equals("W"))
                {
                    MessageBox.Show("You've Win the duel!");
                    Application.Exit();
                    GameNetwork.Terminate();
                }
                else if (this.MyTurn == false)
                {
                
                    System.Console.WriteLine(" update");
                    if (GameNetwork.CommonAction.MyOppTurn)
                        MyTurn = false;
                    else
                    {
                        MyTurn = true;
                        CanDraw = true;
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        
                            //card data
                            this.p1MagicCard[i] = GameNetwork.CommonAction.p2MagicCard[i];
                            this.p1MonsterCard[i] = GameNetwork.CommonAction.p2MonsterCard[i];
                            this.p2MagicCard[i] = GameNetwork.CommonAction.p1MagicCard[i];
                            this.p2MonsterCard[i] = GameNetwork.CommonAction.p1MonsterCard[i];
                            //sowar
                            if (this.p1MagicCard[i] != null)
                                this.p1MagicBox[i].Image = this.p1MagicCard[i].image;
                            else
                                this.p1MagicBox[i].Image = yo_gi.Properties.Resources.frame;
                            if (this.p1MonsterCard[i] != null)
                                this.p1MonsterBox[i].Image = this.p1MonsterCard[i].image;
                            else
                                this.p1MonsterBox[i].Image = yo_gi.Properties.Resources.frame;    
                            if (this.p2MagicCard[i] != null)
                                this.p2MagicBox[i].Image = this.p2MagicCard[i].image;
                            else
                                this.p2MagicBox[i].Image = yo_gi.Properties.Resources.frame;
                            if (this.p2MonsterCard[i] != null)
                                this.p2MonsterBox[i].Image = this.p2MonsterCard[i].image;
                            else
                                this.p2MonsterBox[i].Image = yo_gi.Properties.Resources.frame;
                        
                    }
                    if (GameNetwork.CommonAction.FieldCard != null)
                    {
                        FieldBox.c = GameNetwork.CommonAction.FieldCard;
                        FieldBox.Image = GameNetwork.CommonAction.FieldCard.image;
                        BackGround = new Bitmap(yo_gi.Properties.Resources.Shurima);
                    }
                        
                    Program.Game.p1.LifePoints = GameNetwork.CommonAction.p2Life;
                    Program.Game.p2.LifePoints = GameNetwork.CommonAction.p1Life;

                    this.Invalidate();
                    this.CheckWinner();
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = e.Graphics;
            g.DrawImage(BackGround, 0, 0, this.Width, this.Height);
            Brush b = new SolidBrush(Color.Red);
            g.DrawString(Program.Game.p1.LifePoints.ToString(), new Font("Arial", 24), b, 510, 22);
            g.DrawString(Program.Game.p2.LifePoints.ToString(), new Font("Arial", 24), b, 1180, 22);
            for (int i = 0; i < 5; i++)
            {
                if (this.p1MonsterCard[i] != null)
                    ShowCardPoint((NormalMonster)this.p1MonsterCard[i], this.p1MonsterBox[i]);
                if (this.p2MonsterCard[i] != null)
                    ShowCardPoint((NormalMonster)this.p2MonsterCard[i], this.p2MonsterBox[i]);
            }
            if (MyTurn)
            {
                Brush B = new SolidBrush(Color.Green);
                g.DrawString("Your Turn", new Font("Arial", 24), B, 750, 22);
            }

        }
        private void ShowCardPoint(NormalMonster m, PictureBox p)
        {
            Graphics g1 = p.CreateGraphics();
            float y = p.Height - 24;
            Brush b;
            System.Drawing.Font f = new Font("Arial", 18);
            if (m.state == TheState.Att)
            {
                if (m.BounsAtt < 0)
                {
                    b = new SolidBrush(Color.Red);
                    g1.DrawString("" + (m.AttPoint + m.BounsAtt), f, b, 0, y);
                }
                else if (m.BounsAtt > 0)
                {
                    b = new SolidBrush(Color.Green);
                    g1.DrawString("" + (m.AttPoint + m.BounsAtt), f, b, 0, y);
                }
                else
                {
                    b = new SolidBrush(Color.Black);
                    g1.DrawString("" + (m.AttPoint + m.BounsAtt), f, b, 0, y);
                }

            }
            else
            {
                if (m.BounsDef < 0)
                {
                    b = new SolidBrush(Color.Red);
                    g1.DrawString("" + (m.DefPoint + m.BounsDef), f, b, 0, y);
                }
                else if (m.BounsDef > 0)
                {
                    b = new SolidBrush(Color.Green);
                    g1.DrawString("" + (m.DefPoint + m.BounsDef), f, b, 0, y);
                }
                else
                {
                    b = new SolidBrush(Color.Black);
                    g1.DrawString("" + (m.DefPoint + m.BounsDef), f, b, 0, y);
                }
            }

        }



        public Boolean IsFieldEmpty()
        {
            for (int i = 0; i < 5; i++)
            {
                if (p1MonsterCard[i] != null)
                    return false;
                if (p2MonsterCard[i] != null)
                    return false;
            }
            return true;
        }
        public Boolean IsP2FieldEmpty()
        {
            for (int i = 0; i < 5; i++)
            {
                if (p2MonsterCard[i] != null)
                    return false;
            }
            return true;
        }
 
        public void CheckWinner()
        {
            if (Program.Game.p1.LifePoints <= 0)
            {
                //showMSG2();
                MessageBox.Show("You've lost the duel!");
                Application.Exit();
                GameNetwork.Terminate();
            }
            else if (Program.Game.p2.LifePoints <= 0)
            {
                //7ot yla el showMSG hna
                //showMSG5();
                MessageBox.Show("You've Win the duel!");
                Application.Exit();
                GameNetwork.Terminate();
            }
        }
        public void MonsterReload()//to make monster can attack again
        {
            for (int i = 0; i < 5; i++)
                if (this.p1MonsterCard[i]!=null)
                    ((NormalMonster)(this.p1MonsterCard[i])).Attacked = false;
        }
        #region MonsterCardShow
        private void MonsterShow(NormalMonster m)
        {
            if (m != null)
            {
                if (m.state == TheState.Att)
                    Form1.CardShow.Image = m.image;
                else
                {
                    Bitmap b = (Bitmap)m.image.Clone();
                    b.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    Form1.CardShow.Image = b;
                }

            }
        }
        private void p2Monster0Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p2MonsterCard[0]);
        }
        private void p2Monster1Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p2MonsterCard[1]);
        }
        private void p2Monster2Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p2MonsterCard[2]);
        }
        private void p2Monster3Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p2MonsterCard[3]);
        }
        private void p2Monster4Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p2MonsterCard[4]);
        }
        private void p1Monster0Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p1MonsterCard[0]);
        }
        private void p1Monster1Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p1MonsterCard[1]);
        }
        private void p1Monster2Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p1MonsterCard[2]);
        }
        private void p1Monster3Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p1MonsterCard[3]);
        }
        private void p1Monster4Show(object sender, System.EventArgs e)
        {
            MonsterShow((NormalMonster)p1MonsterCard[4]);
        }
        #endregion
        #region MonsterAttack
        private void p2Monster0(object sender, MouseEventArgs e)
        {
            
            p2Monster((NormalMonster)p2MonsterCard[0]);
        }
        private void p2Monster1(object sender, MouseEventArgs e)
        {
            p2Monster((NormalMonster)p2MonsterCard[1]);
        }
        private void p2Monster2(object sender, MouseEventArgs e)
        {
            p2Monster((NormalMonster)p2MonsterCard[2]);
        }
        private void p2Monster3(object sender, MouseEventArgs e)
        {
            p2Monster((NormalMonster)p2MonsterCard[3]);
        }
        private void p2Monster4(object sender, MouseEventArgs e)
        {
            p2Monster((NormalMonster)p2MonsterCard[4]);
        }
        private void p1Monster0(object sender, MouseEventArgs e)
        {
            p1Monster((NormalMonster)p1MonsterCard[0]);
        }
        private void p1Monster1(object sender, MouseEventArgs e)
        {
            p1Monster((NormalMonster)p1MonsterCard[1]);
        }
        private void p1Monster2(object sender, MouseEventArgs e)
        {
            p1Monster((NormalMonster)p1MonsterCard[2]);
        }
        private void p1Monster3(object sender, MouseEventArgs e)
        {
            p1Monster((NormalMonster)p1MonsterCard[3]);
        }
        private void p1Monster4(object sender, MouseEventArgs e)
        {
            p1Monster((NormalMonster)p1MonsterCard[4]);
        }
        #endregion
        #region Spell Cards and monester
        private void p1MonsterSpell0(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p1MonsterCard[0];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p1MonsterSpell1(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p1MonsterCard[1];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p1MonsterSpell2(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p1MonsterCard[2];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p1MonsterSpell3(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p1MonsterCard[3];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p1MonsterSpell4(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p1MonsterCard[4];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p2MonsterSpell0(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p2MonsterCard[0];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p2MonsterSpell1(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p2MonsterCard[1];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p2MonsterSpell2(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p2MonsterCard[2];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p2MonsterSpell3(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p2MonsterCard[3];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        private void p2MonsterSpell4(object sender, MouseEventArgs e)
        {
            if (SpellMode)
            {
                RefMonster = p2MonsterCard[4];
                Spell.SpellFunction(SpellActive.Name);
                SpellMode = false;
                this.removeMagicP1(SpellActive);
                this.Invalidate();
            }
            else RefMonster = null;
        }
        #endregion
    }

}
