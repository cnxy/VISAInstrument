using System.Linq;
using System.Windows.Forms;

namespace VISAInstrument.Utility.Extension.UI
{
    internal static class ComboBoxEx
    {
        public static void SetItems(this ComboBox comboBox, params object[] content)
        {
            comboBox.Items.AddRange(content);
        }

        public static void SetItems<TValue, TDisplay>(this ComboBox comboBox, TValue[] values, TDisplay[] displays)
        {
            PairCollection<TValue, TDisplay> pc = new PairCollection<TValue, TDisplay>();
            pc.AddRange(values, displays);
            comboBox.DataSource = pc.DataList;
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
        public static void SetFirstItem<TValue, TDisplay>(this ComboBox comboBox)
        {
            if (comboBox.Items.Count == 0) return;
            string item = ((Pair<TValue, TDisplay>)comboBox.Items[0]).Display.ToString();
            comboBox.Text = !string.IsNullOrEmpty(item) ? item : ((Pair<TValue, TDisplay>)comboBox.Items[comboBox.Items.Count - 1]).Display.ToString();

        }

        public static void ShowAndDisplay(this ComboBox comboBox, params string[] content)
        {
            comboBox.ClearItems();
            comboBox.SetItems(content.Select(x=>(object)x).ToArray());
            comboBox.SetFirstItem();
        }

        public static void ShowAndDisplay<TValue, TDisplay>(this ComboBox comboBox, TValue[] values, TDisplay[] displays)
        {
            comboBox.ClearItems();
            comboBox.SetItems(values, displays);
            comboBox.SetFirstItem<TValue, TDisplay>();
        }

        public static void AddItem(this ComboBox comboBox, string content)
        {
            if (comboBox.Items.Cast<object>().Any(item => (string)item == content))
            {
                return;
            }
            string[] commands = (string[])comboBox.DataSource;
            commands = commands.Concat(new[] { content }).ToArray();
            comboBox.DataSource = commands;
            comboBox.Text = content;
        }
    }
}
