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
        string path1;
        public FrmOptions(string path)
        {
            InitializeComponent();

            path1 = path;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProjectConfig c = new ProjectConfig();
            c.BUILD_TOOLS = textBox4.Text;
            c.LIBS = txtLIBs.Text.Split(';');
            c.PACKAGE = txtPackage.Text;
            c.PLATFORM = txtPlatform.Text;
            c.JAVAC = txtJAVAC.Text;
            c.KEYTOOL = txtKEYTOOL.Text;
            c.KEYTOOL_STOREPASSWD = "android";
            c.KEYTOOL_PASSWD = "android";
            c.KEYTOOL_DNAME = "CN=reza, OU=askarabadi, O=neyshabour, ST=kh-razavi, C=iran";
            c.KEYTOOL_ALIAS = "androidkey";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(c);

            string PROJNAME = path1.Split('\\')[path1.Split('\\').Length - 2];
            System.IO.File.WriteAllText(path1 + PROJNAME + ".json", json);
            this.Close();
        }
    }
}
