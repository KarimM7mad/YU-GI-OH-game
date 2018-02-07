using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace yo_gi
{
    [Serializable]
    public class Spell : Card

    {
        
        public Spell(string name, string type, string description, Bitmap image) : base(name, type, description, image)
        {
        }
        /// <summary>
        /// Destroy all cards in the field
        /// </summary>
        public static void  DarkHoleFunction()
        {
            Form1.FieldPanel1.removeAllMonsterP1();
            Form1.FieldPanel1.removeAllMonsterP2();
        }
        /*-------------------------------------------------------------*/

        /// <summary>
        /// Increase attPoint and defPoint of monster 300 point 
        /// </summary>
        /// <param name="m">the Monster that you will increase</param>
        public static void LegendrySwordFunction(Monster m)
        {
            if (m != null)
            {
                m.BounsAtt += 300;
                m.BounsDef += 300;
            }
            else if (Form1.FieldPanel1.IsFieldEmpty())
                Form1.FieldPanel1.SpellMode = false;
        }
        /*-------------------------------------------------------------*/

        /// <summary>
        /// increase player lb with 600 
        /// </summary>
        public static void GoblonsFunction()
        {
            Program.Game.p1.LifePoints += 600;

        }
        /*-------------------------------------------------------------*/

        /// <summary>
        /// destroy lowes monster of the vs player
        /// </summary>
        /// <param name="player"></param>
        public static void FissureFnction()
        {
           
                int temp = 20000;
                Monster m = null;
                for (int i = 0; i < 5; i++)
                {
                    if (Form1.FieldPanel1.p2MonsterCard[i] == null)
                    {
                        continue;
                    }
                    else if (temp > ((Monster)(Form1.FieldPanel1.p2MonsterCard[i])).AttPoint)
                    {
                        m = (Monster)Form1.FieldPanel1.p2MonsterCard[i];
                        temp = m.AttPoint;
                        
                    }

                }
                Form1.FieldPanel1.removeMonsterP2(m);
            }
        /*-------------------------------------------------------------*/

        /// <summary>
        /// Change the monster state
        /// </summary>
        /// <param name="m"></param>
        public static void ShieldAndSwordFunction(Monster m)
        {
            if (m != null)
            {
                if (m.State == TheState.Att)
                {
                    m.State = TheState.def;
                }
                if (m.State == TheState.def)
                {
                    m.State = TheState.Att;
                }
            }
            else if (Form1.FieldPanel1.IsFieldEmpty())
                Form1.FieldPanel1.SpellMode = false;
        }
        /*-------------------------------------------------------------*/

        /// <summary>
        /// Draw 2 Cards
        /// </summary>
        public static void BotOfGreedFnction()
        {



            Form1.FieldPanel1.DrawOneCard();
            Form1.FieldPanel1.DrawOneCard();

        }
        /*-------------------------------------------------------------*/

        /// <summary>
        /// decrease vs player 600 lb
        /// </summary>
        public void SparksFunction()
        {

            Program.Game.p2.LifePoints -= 600;
            Form1.FieldPanel1.CheckWinner();
        }




        public static void SpellFunction(string name)
        {
            Form1.FieldPanel1.SpellMode = true;
            switch (name)
            {
                case "Dark Hole":
                    Spell.DarkHoleFunction();
                    Form1.FieldPanel1.SpellMode = false;
                    break;
                case "Bot Of Greed":
                    Spell.BotOfGreedFnction();
                    Form1.FieldPanel1.SpellMode = false;
                    break;
                case "Fissure":
                    Spell.FissureFnction();
                    Form1.FieldPanel1.SpellMode = false;
                    break;
                case "Goblons":
                    Spell.GoblonsFunction();
                    Form1.FieldPanel1.SpellMode = false;
                    break;
                case "Legendry Sword":
                    Spell.LegendrySwordFunction((NormalMonster)Form1.FieldPanel1.RefMonster);
                    
                    break;
                case "Shield And Sword":
                    Spell.ShieldAndSwordFunction((NormalMonster)Form1.FieldPanel1.RefMonster);
                    break;
                default:
                    break;
            }
            
        }

    }
}
