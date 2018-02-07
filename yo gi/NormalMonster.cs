using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace yo_gi
{
    [Serializable]
    public class NormalMonster : Monster
    {
        public NormalMonster(string name, string tybe, string description, Bitmap image, int level, int attPoint, int defPoint, TheState state) : base(name, tybe, description, image, level, attPoint, defPoint, state)
        {


        }

        public override void attack(Monster m)
        {
            int M1Att = this.AttPoint + this.BounsAtt;
            int M2Att = m.AttPoint + m.BounsAtt;
            int M1Def = this.DefPoint + this.BounsDef;
            int M2Def = m.AttPoint + m.BounsAtt;

            if (m.State == TheState.Att)
            {
                if (M1Att == M2Att)
                {
                    // Destroy 2 Monsters
                    Form1.FieldPanel1.removeMonsterP1(this);
                    Form1.FieldPanel1.removeMonsterP2(m);
                }
                else if (M1Att > M2Att)
                {
                    // destroy the m Monster
                    Form1.FieldPanel1.removeMonsterP2(m);
                    Program.Game.p2.LifePoints -= M1Att - M2Att;
                }
                else if (M1Att <M2Att)
                {
                    // destroy this monster
                    Form1.FieldPanel1.removeMonsterP1(this);
                         Program.Game.p1.LifePoints -=M2Att - M1Att;
                }
            }
            if (m.State == TheState.def || m.State == TheState.defFlip)
            {
                if (M1Att == M2Def)
                {
                    // do no think
                    return;
                }
                else if (M1Att> M2Def)
                {
                    // destroy the m Monster
                    Form1.FieldPanel1.removeMonsterP2(m);

                }
                else if (M1Att < M2Def)
                {
                    // - the def of att and def from player 1
                    Program.Game.p1.LifePoints -= M2Def - M1Att;
                }
            }
        }
    }
}
