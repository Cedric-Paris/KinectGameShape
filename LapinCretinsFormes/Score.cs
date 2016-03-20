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
            Value = value;
            Nom = nom;
        }
    }
}
