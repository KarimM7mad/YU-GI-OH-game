using System;
using System.Windows.Forms;

namespace yo_gi
{
    public partial class Menu : Form
    {
        SetupMenu pm = new SetupMenu();
        public Menu()
        {
            InitializeComponent();
            CenterToScreen();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            pm.Show();
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void closef()
        {
            this.Close();
            pm.CloseF();
            
        }
    }
}
