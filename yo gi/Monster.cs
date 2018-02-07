using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace yo_gi
{
    public enum TheState { Att, def, defFlip }
    [Serializable]
    public abstract class Monster : Card
    {
        protected int level;
        protected int attPoint;
        protected int defPoint;
        protected int bounsAtt = 0;
        protected int bounsDef = 0;
        public TheState state;
        public bool Attacked = false;
       

        public int Level
        {
            get { return this.level; }
        }
        public int AttPoint
        {
            get { return this.attPoint; }
            set { this.attPoint = value; }
        }
        public int DefPoint
        {
            get { return this.defPoint; }
            set { this.defPoint = value; }
        }
        public TheState State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public int BounsAtt {
            set { this.bounsAtt = value; }
            get { return this.bounsAtt; }
        }

        public int BounsDef
        {
            set { this.bounsDef = value; }
            get { return this.bounsDef; }
        }



        public Monster(string name, string tybe, string description, Bitmap image, int level, int attPoint, int defPoint, TheState state) : base(name, tybe, description, image)
        {

            this.level = level;
            this.attPoint = attPoint;
            this.defPoint = defPoint;
            this.state = state;
            this.BounsAtt = 0;
            this.BounsDef = 0;

        }

        // the function that change the state of the card
        public void ChangeState()
        {
            if (this.state == TheState.Att)
            {
                this.state = TheState.def;
            }
            else if (this.state == TheState.def)
            {
                this.state = TheState.Att;
            }
            else if (this.state == TheState.defFlip)
            {
                this.state = TheState.Att;
            }

        }
        public abstract void attack(Monster m);

    }
}
