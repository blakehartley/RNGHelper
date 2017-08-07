using System.Windows.Forms;

namespace FF12RNGHelper.Forms
{
    public static class FormUtils
    {
        public static void CloseApplication(object o, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}