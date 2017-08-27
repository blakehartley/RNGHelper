using System.Windows.Forms;

namespace FF12RNGHelper.Forms
{
    public partial class MasterForm : Form
    {
        private Form _currentForm;

        public MasterForm()
        {
            InitializeComponent();

            OpenFormChest();
        }

        private void OpenFormChest()
        {
            OpenForm(new FormChest2());
        }

        private void OpenFormSpawn()
        {
            OpenForm(new FormSpawn2());
        }

        private void OpenFormSteal()
        {
            OpenForm(new FormSteal2());
        }

        private void OpenForm(Form newForm)
        {
            if (_currentForm != null && _currentForm.Visible)
            {
                _currentForm.Hide();
                MainPanel.Controls.Remove(_currentForm);
            }
            _currentForm = newForm;
            _currentForm.TopLevel = false;
            MainPanel.Controls.Add(newForm);
            _currentForm.FormBorderStyle = FormBorderStyle.None;
            _currentForm.Dock = DockStyle.Fill;
            _currentForm.Show();
        }

        private void stealToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenFormSteal();
        }

        private void rareGameToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenFormChest();
        }

        private void spawnToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenFormSpawn();
        }

        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(FormConstants.AboutMsg);
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormUtils.CloseApplication();
        }
    }
}
