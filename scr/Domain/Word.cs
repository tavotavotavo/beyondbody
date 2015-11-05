namespace Domain
{
    public class Word
    {
        public Word(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}