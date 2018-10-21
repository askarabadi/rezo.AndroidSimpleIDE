using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ARA.AndroidReZo
{
    public partial class FrmOptions : Form
    {
        public FrmOptions(string path)
        {
            InitializeComponent();

            string PROJNAME = path.Split('\\')[path.Split('\\').Length - 2];
            string json = System.IO.File.ReadAllText(path + PROJNAME + ".json");
            var config = (ProjectConfig)Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectConfig>(json);
            textBox4.Text = config.BUILD_TOOLS;
            txtLIBs.Text = String.Join("\r\n ", config.LIBS);
            txtPackage.Text = config.PACKAGE;
            txtPlatform.Text = config.PLATFORM;
            txtJAVAC.Text = config.JAVAC;
            txtKEYTOOL.Text = config.KEYTOOL;
        }
    }
}
