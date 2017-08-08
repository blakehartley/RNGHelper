using System.Windows.Forms;

namespace FF12RNGHelper.Forms
{
    public static class FormUtils
    {
        public static void CloseApplication(object o, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public static int ParseNumRows(string text)
        {
            int numRows = int.Parse(text);
            if (numRows < 30)
                numRows = 30;
            if (numRows > 10000)
                numRows = 10000;
            return numRows;
        }
    }
}