using System;
using System.Collections.Generic;

namespace VISAInstrument.Utility.Extension.UI
{
    internal class PairCollection<TValue, TDisplay>
    {
        public PairCollection()
        {
            DataList = new List<Pair<TValue, TDisplay>>();
        }

        public Pair<TValue, TDisplay> this[int index] => DataList[index];

        public void Add(TValue value, TDisplay display)
        {
            DataList.Add(new Pair<TValue, TDisplay>() { Value = value, Display = display });
        }

        public void AddRange(TValue[] values, TDisplay[] displays)
        {
            if (values.Length != displays.Length) throw new ArgumentException("参数长度必须相等");
            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i], displays[i]);
            }
        }

        public List<Pair<TValue, TDisplay>> DataList { get; }
    }
}
