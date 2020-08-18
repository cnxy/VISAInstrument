using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VISAInstrument.Extension
{
    static class ComboBoxEx
    {
        public static void SetItems(this ComboBox comboBox, params string[] content)
        {
            comboBox.Items.AddRange(content);
        }

        public static void SetItems<Tvalue,Tdisplay>(this ComboBox comboBox, Tvalue[] values,Tdisplay[] displays)
        {
            PairCollection<Tvalue, Tdisplay> pc = new PairCollection<Tvalue, Tdisplay>();
            pc.AddRange(values, displays);
            comboBox.DataSource = pc.Datas;
            comboBox.ValueMember = Pair.ValueName;
            comboBox.DisplayMember = Pair.DisplayName;
        }

        public static void ClearItems(this ComboBox comboBox)
        {
            comboBox.DataSource = null;
            comboBox.Items.Clear();
        }

        public static void SetFirstItem(this ComboBox comboBox)
        {
            if (comboBox.Items.Count != 0)
                comboBox.Text = comboBox.Items[0].ToString();
        }
        public static void SetFirstItem<Tvalue, Tdisplay>(this ComboBox comboBox)
        {
            if (comboBox.Items.Count != 0)
            {
                string item = ((Pair<Tvalue, Tdisplay>)comboBox.Items[0]).Display.ToString();
                if(!string.IsNullOrEmpty(item))
                {
                    comboBox.Text = item;
                }
                else
                {
                    comboBox.Text = ((Pair<Tvalue, Tdisplay>)comboBox.Items[comboBox.Items.Count-1]).Display.ToString();
                }
            }
                
        }

        public static void ShowAndDisplay(this ComboBox comboBox, params string[] content)
        {
            comboBox.ClearItems();
            comboBox.SetItems(content);
            comboBox.SetFirstItem();
        }

        public static void ShowAndDisplay<Tvalue, Tdisplay>(this ComboBox comboBox, Tvalue[] values, Tdisplay[] displays)
        {
            comboBox.ClearItems();
            comboBox.SetItems(values, displays);
            comboBox.SetFirstItem<Tvalue, Tdisplay>();
        }

        public static void AddItem(this ComboBox comboBox, string content)
        {
            if (comboBox.Items.Cast<object>().Any(item => (string)item == content))
            {
                return;
            }
            string[] commands = (string[])comboBox.DataSource;
            commands = commands.Concat(new string[] { content }).ToArray();
            comboBox.DataSource = commands;
            comboBox.Text = content;
        }
    }

    static class TextBoxEx
    {
        public static void SetSelect(this TextBoxBase textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
        }
    }
    class Pair
    {
        public static string ValueName => "Value";
        public static string DisplayName => "Display";
    }

    class Pair<Tvalue, Tdisplay> : Pair
    {
        public Pair() { }

        public Pair(Tvalue value, Tdisplay display)
        {
            this.Value = value;
            this.Display = display;
        }

        public Tvalue Value { set; get; } = default(Tvalue);
        public Tdisplay Display { set; get; } = default(Tdisplay);

    }
    class PairCollection<Tvalue, Tdisplay>
    {
        public PairCollection()
        {
            list = new List<Pair<Tvalue, Tdisplay>>();
        }

        public Pair<Tvalue, Tdisplay> this[int index] => list[index];

        List<Pair<Tvalue, Tdisplay>> list;

        public void Add(Tvalue value, Tdisplay display)
        {
            list.Add(new Pair<Tvalue, Tdisplay>() { Value = value, Display = display });
        }

        public void AddRange(Tvalue[] values, Tdisplay[] displays)
        {
            if (values.Length != displays.Length) throw new ArgumentException("参数长度必须相等");
            for (int i = 0; i < values.Length; i++)
            {
                Add(values[i], displays[i]);
            }
        }

        public List<Pair<Tvalue, Tdisplay>> Datas => list;
    }
}
