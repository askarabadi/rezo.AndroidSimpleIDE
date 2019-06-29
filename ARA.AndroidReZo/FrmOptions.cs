using System;
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
            string PROJNAME = System.IO.Path.GetFileNameWithoutExtension(path);
            string json = System.IO.File.ReadAllText(System.IO.Path.Combine(path, PROJNAME + ".json"));
            var config = (ProjectConfig)Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectConfig>(json);
            textBox4.Text = config.BUILD_TOOLS;
            txtLIBs.Text = String.Join("\r\n", config.LIBS ?? new string[] { });
            txtRESs.Text = String.Join("\r\n", config.RESS ?? new string[] { });
            txtPackage.Text = config.PACKAGE;
            txtPlatform.Text = config.PLATFORM;
            txtJAVAC.Text = config.JAVAC;
            txtKEYTOOL.Text = config.KEYTOOL;
            txtALIAS.Text = config.KEYTOOL_ALIAS;
            txtDNAME.Text = config.KEYTOOL_DNAME;
            txtPASSWD.Text = config.KEYTOOL_PASSWD;
            txtSTOREPASSWD.Text = config.KEYTOOL_STOREPASSWD;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProjectConfig c = new ProjectConfig();
            c.BUILD_TOOLS = textBox4.Text;
            c.LIBS = txtLIBs.Text.Replace("\r\n", "\n").Split('\n');
            c.RESS = txtRESs.Text.Replace("\r\n", "\n").Split('\n');
            c.PACKAGE = txtPackage.Text;
            c.PLATFORM = txtPlatform.Text;
            c.JAVAC = txtJAVAC.Text;
            c.KEYTOOL = txtKEYTOOL.Text;
            c.KEYTOOL_STOREPASSWD = txtSTOREPASSWD.Text;
            c.KEYTOOL_PASSWD = txtPASSWD.Text;
            c.KEYTOOL_DNAME = txtDNAME.Text;
            c.KEYTOOL_ALIAS = txtALIAS.Text;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(c);

            string PROJNAME = System.IO.Path.GetFileNameWithoutExtension(path1);
            System.IO.File.WriteAllText(System.IO.Path.Combine(path1, PROJNAME + ".json"), json);
            this.Close();
        }
    }
}
