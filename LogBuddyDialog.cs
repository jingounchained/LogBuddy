using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace LogBuddy
{
    public partial class LogBuddyDialog : Form
    {
        private static LogBuddyDialog _instance;
        public static LogBuddyDialog Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new LogBuddyDialog();
                }
                return _instance;
            }
        }

        public string LogPath
        {
            get
            {
                return txtPath.Text;
            }
            set
            {
                txtPath.Text = value;
            }
        }

        private LogBuddyDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Apply();
            Save();
        }

        public void Save()
        {
            if (!Directory.Exists(Genie.Instance.PluginPath + "\\LogBuddy")) Directory.CreateDirectory(Genie.Instance.PluginPath + "\\LogBuddy");
            string settingPath = Genie.Instance.PluginPath + "\\LogBuddy\\path.djo";
            using (StreamWriter writer = new StreamWriter(settingPath, false))
            {
                writer.WriteLine(txtPath.Text);
            }
        }

        public bool LoadPath()
        {
            string settingPath = Genie.Instance.PluginPath + "\\LogBuddy\\path.djo";
            if (File.Exists(settingPath))
            {
                using(StreamReader reader = new StreamReader(settingPath))
                {
                    txtPath.Text = reader.ReadLine();
                }
                return true;
            }
            else
            {
                Genie.Instance.Echo("[Log Buddy] Log Path File was not found. Default Path of \"Logs\" has been saved.");
                Genie.Instance.Echo("[Log Buddy] Please open Log Buddy through the Plugin Menu to set your Log Path.");
                txtPath.Text = "Logs";
                Save();
                return false;
            }
        }

        public void Apply()
        {
            string path = TranslateTokens(txtPath.Text);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Genie.Instance.SendText("#config logdir " + path);
            Genie.Instance.SendText("#save config");
        }

        private string TranslateTokens(string tokenizedPath)
        {
            string resolvedPath = tokenizedPath;
            resolvedPath = Regex.Replace(resolvedPath, "{DATE}", DateTime.Today.ToString("MM-dd-yyyy"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{YEAR}", DateTime.Today.ToString("yyyy"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{YR}", DateTime.Today.ToString("yy"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{MONTH}", DateTime.Today.ToString("MMMM"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{MO}", DateTime.Today.ToString("MM"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{M}", DateTime.Today.ToString("M"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{DAY}", DateTime.Today.ToString("dddd"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{DY}", DateTime.Today.ToString("dd"), RegexOptions.IgnoreCase);
            resolvedPath = Regex.Replace(resolvedPath, "{D}", DateTime.Today.ToString("d"), RegexOptions.IgnoreCase);
            return resolvedPath;
        }

        private void LogBuddyDialog_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
