using Microsoft.Win32;
using System;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Text;

namespace Auth_
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        public static string Decoder(string e)
        {
            byte[] data = Convert.FromBase64String(e);
            string a = Encoding.UTF8.GetString(data);
            byte[] _data = Convert.FromBase64String(a);
            return Encoding.UTF8.GetString(_data);
        }

        public string enc = "TjJGd1pIVT0=";
        public string _rentry = $"https://rentry.co/";
        public bool _status = false;

        [STAThread]
        public void Loader()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Test());
        }

        public void LoadMain()
        {
            this.Hide();
        }

        public void Authentiator(string hwid)
        {
            WebClient wc = new WebClient();
            string list = wc.DownloadString(_rentry + Decoder(enc) + "/raw");

            if (list.Contains(hwid))
            {
                _status = true;
            }
        }

        private static string GetHWID()
        {
            bool is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
            bool flag = is64BitOperatingSystem;
            RegistryKey registryKey;
            if (flag)
            {
                registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }
            return registryKey.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion").GetValue("ProductId").ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Icon = null;
            this.ShowIcon = false;
            this.Text = "";
            label5.Text = GetHWID();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetHWID());
        }

        private void login_Click(object sender, EventArgs e)
        {
            Authentiator(hwid: GetHWID());

            if (_status)
            {
                label5.Text = "Successfully authenticated..";
                LoadMain();
                Thread t = new Thread(() => Loader());
                t.Start();
                this.Close();
            }
            else { label5.Text = "Error occurred while authenticating.."; }
        }
    }
}
