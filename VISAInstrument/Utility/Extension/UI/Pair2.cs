namespace VISAInstrument.Utility.Extension.UI
{
    internal class Pair<TValue, TDisplay> : Pair
    {
        public Pair() { }

        public Pair(TValue value, TDisplay display)
        {
            this.Value = value;
            this.Display = display;
        }

        public TValue Value { set; get; }
        public TDisplay Display { set; get; }
    }
}
