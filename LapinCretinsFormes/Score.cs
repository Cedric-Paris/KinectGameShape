using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    public class Score
    {

        public int Value
        {
            get;
            private set;
        }
        public string Nom
        {
            get;
            private set;
        } 
        

        public Score(int value, string nom = "Anonyme")
        {
            this.Value = value;
            this.Nom = nom;
        }
    }
}
