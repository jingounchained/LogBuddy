using System.Windows.Forms;
using GeniePlugin.Interfaces;
using System.IO;

namespace LogBuddy
{
    public class Program : IPlugin
    {
        #region Interface Properties
        public string Name => "Log Buddy";

        public string Version => "1.0";

        public string Description => "Automates Path Management of Log Files";

        public string Author => "Djordje";

        public bool Enabled { get; set; }
        #endregion

        private Genie _host => Genie.Instance;

#if DEBUG
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(LogBuddyDialog.Instance);
        }
#endif

        public void Initialize(IHost Host)
        {
            _host.SetHost(ref Host);
            LogBuddyDialog.Instance.MdiParent = _host.ParentForm;
            if (LogBuddyDialog.Instance.LoadPath())
            {
                LogBuddyDialog.Instance.Apply();
            }
        }

        public void Show()
        {
            LogBuddyDialog.Instance.Show();
            LogBuddyDialog.Instance.BringToFront();
        }

        #region Unused Interface Functions
        public void ParentClosing()
        {
            //Do nothing. 
        }

        public string ParseInput(string Text)
        {
            return Text;
        }

        public string ParseText(string Text, string Window)
        {
            return Text;
        }

        public void ParseXML(string XML)
        {
            //Do nothing. 
        }

        public void VariableChanged(string Variable)
        {
            //do nothing
        }
        #endregion
    }
}
