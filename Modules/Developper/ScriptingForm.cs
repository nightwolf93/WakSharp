using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Noesis.Javascript;

namespace WakSharp.Modules.Developper
{
    public partial class ScriptingForm : Form
    {
        public ScriptingForm()
        {
            InitializeComponent();
        }

        private void lancerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (JavascriptContext context = new JavascriptContext())
            {
                try
                {
                    context.SetParameter("API", new APIScriptModule(this));
                    context.SetParameter("Settings", Utilities.Settings.ConfigurationManager.Server);
                    richTextBox2.Text = "";
                    context.Run(richTextBox1.Text);
                }
                catch (Exception ex)
                {
                    richTextBox2.AppendText(ex.ToString() + "\n");
                }
            }
        }

        public class APIScriptModule
        {
            private ScriptingForm scriptingForm;

            public APIScriptModule(ScriptingForm scriptingForm)
            {
                this.scriptingForm = scriptingForm;
            }

            public Network.Realm.RealmSession GetSession(string username)
            {
                return Network.Realm.RealmServer.Clients.FindAll(x => x.Account != null).FirstOrDefault(x => x.Account.Username == username);
            }

            public void Log(string message)
            {
                scriptingForm.richTextBox2.AppendText("[Log] : " + message + "\n");
            }

            public Network.WakfuWorld GetWorld(int id)
            {
                return Utilities.Settings.ConfigurationManager.Server.Worlds.FirstOrDefault(x => x.ID == id);
            }
        }
    }
}
