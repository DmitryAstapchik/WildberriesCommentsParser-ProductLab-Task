using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WildberriesCommentsParser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button3.Text = "Parsing...";
            button3.Enabled = false;
            try
            {
                Parser.Parse(openFileDialog1.FileName, folderBrowserDialog1.SelectedPath);
                MessageBox.Show("Parsed.", "Success");
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Error");
            }
            button3.Text = "Parse";
            button3.Enabled = true;
        }
    }
}
