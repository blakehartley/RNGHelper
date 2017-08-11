using System.IO;
using System.Windows.Forms;

namespace FF12RNGHelper.Forms
{
    public class FormManager
    {
        private const string XmlExt = ".xml";
        private const string XmlFilter = "XML Files (*.xml)|*.xml";

        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        public static void SaveForm(Form form, IFormLoader loader)
        {
           SaveFileDialog save = new SaveFileDialog
            {
                InitialDirectory = CurrentDirectory,
                DefaultExt = XmlExt,
                Filter = XmlFilter
            };
            if (save.ShowDialog().Equals(DialogResult.OK))
            {
                loader.SaveForm(form, save.FileName);
            }
        }

        public static void LoadForm(Form form, IFormLoader loader)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                InitialDirectory = CurrentDirectory,
                DefaultExt = XmlExt,
                Filter = XmlFilter
            };
            if (open.ShowDialog().Equals(DialogResult.OK))
            {
                loader.LoadForm(form, open.FileName);
            }
        }
    }
}