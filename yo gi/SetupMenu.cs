using System;
using System.Windows.Forms;

namespace yo_gi
{
    public partial class SetupMenu : Form
    {
        private Boolean HostIsChecked = false;
        private Boolean GuestIsChecked = false;
        public SetupMenu()
        {
            InitializeComponent();
            CenterToScreen();
        }
        private void HostBtn_CheckedChanged(object sender, EventArgs e)
        {
            HostIsChecked = true;
        }
        private void GBtn_CheckedChanged(object sender, EventArgs e)
        {
            GuestIsChecked = true;
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (HostIsChecked)
            {
                this.Hide();
                //Host logic
                GameNetwork.CreateGame(Convert.ToInt32(this.PortBar.Text));
            }
            else if (GuestIsChecked)
            {
                this.Hide();
                //Guest logic
                GameNetwork.JoinGame(Convert.ToInt32(this.PortBar.Text),this.textBox1.Text);
                

            }
            else
            {
                MessageBox.Show("You must choose either to be host or guest to start the game!");
            }
        }
        private void PortBar_TextChanged(object sender, EventArgs e)
        {
            //Port Number logic
        }
                //Allowing only numbers to be written (8 for backspace)
        private void PortNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;

            }

        }

        private void maskedIPBar_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        public void CloseF()
        {
            this.Close();
        }
    }
}
/* 
    field w7da lel etnen
    b3d kol turn update local GUI w b3den update local field w b3den send 
    el tany recieve update field w update GUI
 */