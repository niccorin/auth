using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace Auth_
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private string _github = "https://github.com/subvee";

        private void Test_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Icon = null;
            this.ShowIcon = false;
            this.Text = "";
        }

        private void github_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => Process.Start(_github));
            t.Start();
        }
    }
}
