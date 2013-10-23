using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WakSharp.Modules.Developper
{
    public class DevelopperModule
    {
        public static ScriptingForm ScriptWindow { get; set; }

        public static void Initialize()
        {
            ScriptWindow = new ScriptingForm();
            Application.Run(ScriptWindow);
        }
    }
}
