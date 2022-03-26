using System.Windows.Forms;

namespace VISAInstrument.Utility.Extension.UI
{
    static class TextBoxEx
    {
        public static void SetSelect(this TextBoxBase textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
        }
    }
}
