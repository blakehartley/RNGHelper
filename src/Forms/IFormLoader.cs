using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace FF12RNGHelper.Forms
{
    public interface IFormLoader
    {
        void SaveForm(Form form, string filePath);

        void LoadForm(Form form, string filePath);
    }
}