using System;
using System.Windows.Forms;
using System.IO;

namespace testTask
{
    public partial class Main : Form
    {
        Directory dir = new Directory();
        public Main()
        {
            InitializeComponent();
        }

        private void ChooseFolderButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(folderBrowserDialog.SelectedPath);
            dir.bypassDirectory(dirInfo);
        }
    }
}
