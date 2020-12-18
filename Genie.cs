using System.Collections.Generic;
using GeniePlugin.Interfaces;
using System.Windows.Forms;

namespace LogBuddy
{
    sealed class Genie
    {
        private static IHost _host;
        private static Genie _instance;
        public static Genie Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Genie();
                }
                return _instance;
            }
        }

        public Form ParentForm
        {
            get
            {
                return _host.ParentForm;
            }
        }

        private Genie()
        {

        }
        public string PluginPath
        {
            get
            {
                string pluginPath = _host.get_Variable("PluginPath");
                if (!pluginPath.EndsWith("\\")) pluginPath += "\\";
                return pluginPath;
            }
        }
        public void SetHost(ref IHost host)
        {
            _host = host;
        }

        public string GetVariable(string variableName)
        {

            return _host.get_Variable(variableName);

        }

        public void SetVariable(string variableName, string variableValue)
        {
            _host.SendText("#var " + variableName + " " + variableValue);
        }

        public void Echo(string echo)
        {
            _host.EchoText(echo);
        }

        public void SendText(string text)
        {

            _host.SendText(text);

        }
    }
}
