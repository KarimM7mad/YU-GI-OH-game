using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace yo_gi
{
    class GameNetwork
    {
        //fields
        public static Player P1;   // player 1 , lazem yb2a howa howa ele fl Form**
        public static Player P2;   // player 2 , lazem yb2a howa howa ele fl Form**
        public static Actionn CommonAction; // w da wa7ed lel etnen ACTION YA HASSAN ELE BT3MELHA
        public static TcpListener server; // server for recieve data 
        public static TcpClient client;   // client to send data
        public static int PortNumber { get; set; }
        //methods
        public static void CreateGame(int portnum)
        {
            //P1.hisTurn = true;
            GameNetwork.PortNumber = portnum;
            GameNetwork.server = TcpListener.Create(GameNetwork.PortNumber);
            GameNetwork.server.Start();
            MessageBox.Show("Watitng For Guest ,Game Starts Automatically on Joining");
            client = GameNetwork.server.AcceptTcpClient();
            Program.Game = new Form1();
            Form1.FieldPanel1.MyTurn = true;
            Program.Game.ShowDialog();
        }
        public static void JoinGame(int prtnum, String Ip)
        {
            //P2.hisTurn = false;
            GameNetwork.PortNumber = prtnum;
            GameNetwork.client = new TcpClient(Ip, prtnum);
            MessageBox.Show("finding Host , Please Wait");
            while (true)
            {
                if (GameNetwork.client.Connected)
                {
                    Program.Game = new Form1();
                    Form1.FieldPanel1.MyTurn = false;
                    Program.Game.ShowDialog();
                    break;
                }
            }
        }
        public static void SendUpdates(Actionn A)
        {
            try
            {
                NetworkStream ns = client.GetStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ns, A);
                ns.Flush();
                //ns.Close();
                System.Console.WriteLine(" send");
            }
            catch (Exception e) { }
        }
        public static void RecieveUpdates()
        {
            try
            {
            NetworkStream ns = client.GetStream();
            BinaryFormatter bf = new BinaryFormatter();
            CommonAction = (Actionn)bf.Deserialize(ns);
            //ns.Close();
            System.Console.WriteLine(" recieved");
            }
            catch (Exception e) {
                //Form1.FieldPanel1.CheckWinner();
                //Terminate();
            }
            
        }
        public static void Terminate()//for surrender
        {
            GameNetwork.CommonAction = null;
            GameNetwork.client.Close();
            if(server!=null)
                GameNetwork.server.Stop();
            
            
        }
    }//Class end
}//nameSpace end