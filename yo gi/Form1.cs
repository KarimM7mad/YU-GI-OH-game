using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace yo_gi
{
    public partial class Form1 : Form
    {
        Thread thread0, thread1, thread2;
        public Image[] Card = new Image[10];
        public Player p1;
        public Player p2;
        public static PictureBox CardShow;
        public Form1()
        {
            this.Icon = yo_gi.Properties.Resources.images;
            InitializeComponent();
            //int wForm = Screen.PrimaryScreen.Bounds.Width;
            //int hForm = Screen.PrimaryScreen.Bounds.Height;
            //this.Size = new Size(wForm + 50, hForm + 50);
            int wForm = this.Width;
            int hForm = this.Height;
            int xField = (wForm / 4);
            int wField = this.Width - xField;
            int hField = this.Height;
            CenterToScreen();
            //Card[0]= Image.FromFile(@"C:\Users\Rogue Scarlet\Desktop\Azir.jpg");
            //  this.FieldPanel1.Size = new Size(wField, hField);
            //CardShow.Size = new Size(xField-50, 550);
            CardShow.Size = new Size(xField - 150, 500);
            if (FieldPanel1.MyTurn)
            {
                p1 = new Player("Hassan", true);
                p2 = new Player("Wagih", false);
            }
            else
            {
                p1 = new Player("Wagih", false);
                p2 = new Player("Hassan", true);
            }
            for (int i = 0; i < 10; i++)
            {
                
                FieldPanel.DeckCards[i].Image = p1.deck.ElementAt(i).image;
            }

            thread0 = new Thread(this.RecieveAndUpdate);
            thread0.Start();
        }
        private void RecieveAndUpdate()
        {
            while (true)
            {
                //Thread.Sleep(1000);
                GameNetwork.RecieveUpdates();
                //Thread.Sleep(2000);
                FieldPanel1.UpdateField();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void FieldPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }
    }
}
