using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace yo_gi

{
    [Serializable]
    public class Card
    {

        protected string name;
        public string type;
        protected string description;
        public Bitmap image;

        public string Description
        {
            get { return this.description; }
        }
        public string Name
        {
            get { return this.name; }
        }
        public string Type
        {
            get { return this.type; }
        }

        public Card(string name, string type, string description ,Bitmap image)
        {

            this.name = name;
            this.type = type;
            this.description = description;
            this.image = image;
        }
    }
}
