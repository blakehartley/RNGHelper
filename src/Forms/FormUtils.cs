using System.Windows.Forms;

namespace FF12RNGHelper
{
    public static class FormUtils
    {
        public static void CloseApplication(object o, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}