using System.Linq;
using System.Windows.Forms;

namespace VISAInstrument.Extension
{
    static class ComboBoxEx
    {
        public static void SetItems(this ComboBox comboBox,params string[] content)
        {
            comboBox.Items.AddRange(content);
        }

        public static void ClearItems(this ComboBox comboBox)
        {
            comboBox.Items.Clear();
        }

        public static void SetFirstItem(this ComboBox comboBox)
        {
            if(comboBox.Items.Count != 0 )
                comboBox.Text = comboBox.Items[0].ToString();
        }

        public static void ShowAndDisplay(this ComboBox comboBox, params string[] content)
        {
            comboBox.ClearItems();
            comboBox.SetItems(content);
            comboBox.SetFirstItem();
        }

        public static void AddItem(this ComboBox comboBox, string content)
        {
            foreach (var item in comboBox.Items)
            {
                if ((string)item == content)
                {
                    return;
                }
            }
            string[] commands = (string[])comboBox.DataSource;
            commands = commands.Concat(new string[] { content }).ToArray();
            comboBox.DataSource = commands;
            comboBox.Text = content;
        }
    }
}
