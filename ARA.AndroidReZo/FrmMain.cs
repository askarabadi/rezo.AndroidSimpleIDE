using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ARA.AndroidReZo
{
    public partial class FrmMain : Form
    {
        TextBox currentTextBox;
        List<TextBox> textboxes = new List<TextBox>();
        ProjectConfig config;
        public FrmMain()
        {
            InitializeComponent();
            btnRefresh_Click(null, null);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            new FrmOptions(txtPath.Text).ShowDialog();
        }

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textboxes.Clear();
            string[] ff = System.IO.Directory.GetFiles(System.IO.Path.Combine(txtPath.Text, "src"));
            treeView1.Nodes["ndSrc"].Nodes.Clear();
            foreach (string f in ff)
            {
                treeView1.Nodes["ndSrc"].Nodes.Add(GetNodeAndTextBox(f));
            }

            treeView1.Nodes["ndRes"].Nodes["ndValues"].Nodes.Clear();
            ff = System.IO.Directory.GetFiles(System.IO.Path.Combine(txtPath.Text, "res\\values"));
            foreach (string f in ff)
            {
                treeView1.Nodes["ndRes"].Nodes["ndValues"].Nodes.Add(GetNodeAndTextBox(f));
            }

            treeView1.Nodes["ndRes"].Nodes["ndLayout"].Nodes.Clear();
            ff = System.IO.Directory.GetFiles(System.IO.Path.Combine(txtPath.Text, "res\\layout"));
            foreach (string f in ff)
            {
                treeView1.Nodes["ndRes"].Nodes["ndLayout"].Nodes.Add(GetNodeAndTextBox(f));
            }
            if (!treeView1.Nodes.ContainsKey("AndroidManifest.xml"))
                treeView1.Nodes.Add(GetNodeAndTextBox(System.IO.Path.Combine(txtPath.Text, "AndroidManifest.xml")));

            treeView1.ExpandAll();
            //----------------------------------------
            string PROJNAME = txtPath.Text.Split('\\')[txtPath.Text.Split('\\').Length - 2];
            string json = System.IO.File.ReadAllText(System.IO.Path.Combine(txtPath.Text, PROJNAME + ".json"));
            config = (ProjectConfig)Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectConfig>(json);
        }

        private TreeNode GetNodeAndTextBox(string f)
        {
            TreeNode tn = new TreeNode();
            TextBox tb = new TextBox();
            groupBox1.Controls.Add(tb);
            tb.Visible = false;
            tb.Multiline = true;
            tb.ScrollBars = ScrollBars.Both;
            tb.Dock = DockStyle.Fill;
            tb.Tag = tn;
            tn.Tag = tb;
            tn.Name = System.IO.Path.GetFileName(f);
            tn.Text = System.IO.Path.GetFileName(f);
            textboxes.Add(tb);
            return tn;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null)
            {
                foreach (Control c in groupBox1.Controls)
                {
                    c.Visible = false;
                }
                TextBox tb = treeView1.SelectedNode.Tag as TextBox;
                if (tb.Text == "")
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(txtPath.Text, e.Node.FullPath)))
                    {
                        string src = System.IO.File.ReadAllText(System.IO.Path.Combine(txtPath.Text, e.Node.FullPath));
                        tb.Text = src.Replace('\n', '~').Replace("~", "\r\n");
                        tb.TextChanged += txtSrc_TextChanged;
                    }
                }
                tb.Visible = true;
                groupBox1.Text = e.Node.Text;
                currentTextBox = tb;
            }
        }

        #endregion
        #region Compile

        private void btnCompile_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            string[] LIBS = config.LIBS;
            string BUILD_TOOLS = config.BUILD_TOOLS;
            string PLATFORM = config.PLATFORM;
            string PACKAGE = config.PACKAGE;
            string myBuild = txtPath.Text + @"build\";
            string PROJNAME = txtPath.Text.Split('\\')[txtPath.Text.Split('\\').Length - 2];

            System.IO.File.Delete(txtPath.Text + PROJNAME + ".apk");
            if (System.IO.Directory.Exists(myBuild))
                System.IO.Directory.Delete(myBuild, true);
            System.IO.Directory.CreateDirectory(myBuild);
            System.IO.Directory.CreateDirectory(myBuild + "gen");
            System.IO.Directory.CreateDirectory(myBuild + "obj");
            System.IO.Directory.CreateDirectory(myBuild + "apk");

            foreach (string l in LIBS)
            {
                Run("cmd.exe", @"/C xcopy """ + l + @""" " + myBuild + @"obj /Q");
            }
            echo("build directory recreated ...");
            Run(BUILD_TOOLS + @"\aapt.exe", @" package -f -m -J """ + myBuild + @"gen"" -S """ +
                txtPath.Text + @"res"" -M """ + txtPath.Text + @"AndroidManifest.xml"" -I """ +
                PLATFORM + @"\android.jar""");
            echo("file R.java created ...");

            bool hasNoError = Run(config.JAVAC, @" -source 1.7 -target 1.7 " +
                @"-bootclasspath ""C:\Java\jre1.8.0_102\lib\rt.jar"" " +
                @"-classpath """ + PLATFORM + @"/android.jar""" + GetClassPath(LIBS) + @" -d " +
                myBuild + @"obj " + myBuild + @"gen\" + PACKAGE + @"\R.java " + GetJavaFiles());
            if (!hasNoError)
                echo("java files compiled with error...");
            else
            {
                echo("all java files compiled ...");

                Run(BUILD_TOOLS + @"\dx.bat", @" --dex --output=" + myBuild + @"apk\classes.dex " + myBuild + @"obj ");
                echo("classes.dex created ...");

                Run(BUILD_TOOLS + @"\aapt.exe", @" package -f -M """ + txtPath.Text + @"AndroidManifest.xml"" -S " +
                    txtPath.Text + @"res -I """ +
                    PLATFORM + @"\android.jar"" -F " + myBuild + @"" + PROJNAME + @".unsigned.apk " + myBuild + @"apk");
                echo(PROJNAME + ".unsigned.apk created ...");

                Run(BUILD_TOOLS + @"\zipalign.exe", @" -f -p 4 " + myBuild + @"" + PROJNAME + @".unsigned.apk " +
                    myBuild + @"" + PROJNAME + @".aligned.apk");
                echo(PROJNAME + ".aligned.apk created ...");

                Run(BUILD_TOOLS + @"\apksigner.bat",
                    " sign --ks " + txtPath.Text + @"keystore.jks " +
                    "--ks-key-alias " + config.KEYTOOL_ALIAS + " --ks-pass pass:" + config.KEYTOOL_STOREPASSWD +
                    " --key-pass pass:" + config.KEYTOOL_PASSWD + " " +
                    "--out " + txtPath.Text + PROJNAME + @".apk " + myBuild + @"" + PROJNAME + @".aligned.apk");
                echo(PROJNAME + ".apk created ...");
            }
        }

        private string GetClassPath(string[] LIBS)
        {
            string res = "";
            foreach (string l in LIBS)
            {
                res += @";""" + l + @"""";
            }
            return res;
        }

        private string GetJavaFiles()
        {
            string java = "";
            foreach (TreeNode nd in treeView1.Nodes["ndSrc"].Nodes)
            {
                java += txtPath.Text + @"src\" + nd.Text + " ";
            }
            return java;
        }

        private bool Run(string fileName, string p)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.Arguments = p.Replace(@"\r\n", "");
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;

            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            cmd.StartInfo = startInfo;
            cmd.Start();

            cmd.WaitForExit();
            string res = "";
            if (cmd.HasExited)
                res = cmd.StandardError.ReadToEnd();

            echo(res);
            return !res.Contains("error");
        }
        private void echo(string str)
        {
            if (str != null && str != "")
                textBox1.Text += str + "\r\n";
        }

        #endregion
        #region Save
        private void txtSrc_TextChanged(object sender, EventArgs e)
        {
            string t = ((sender as TextBox).Tag as TreeNode).Text;
            if (!t.Contains("*"))
                ((sender as TextBox).Tag as TreeNode).Text = t + "*";
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to save all changed files?", "Save All",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                foreach (TextBox tb in textboxes)
                {
                    TreeNode tn = (tb.Tag as TreeNode);
                    if (tn.Text.Contains("*"))
                    {
                        System.IO.File.WriteAllText(txtPath.Text + tn.FullPath.Replace("*", ""), tb.Text);
                        tn.Text = tn.Text.Replace("*", "");
                    }
                }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentTextBox != null)
            {
                TreeNode tn = (currentTextBox.Tag as TreeNode);
                if (tn.Text.Contains("*"))
                {
                    System.IO.File.WriteAllText(txtPath.Text + tn.FullPath.Replace("*", ""), currentTextBox.Text);
                    tn.Text = tn.Text.Replace("*", "");
                }
            }
        }

        #endregion
        #region MakeKey
        private void btnMakeKey_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete last jsk and make new?", "Recreate JSK",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                textBox1.Text = "";
                System.IO.File.Delete(txtPath.Text + "keystore.jks");
                echo("keystore.jks deleted ...");
                echo(config.KEYTOOL_ALIAS);
                echo(config.KEYTOOL_DNAME);
                Run(@config.KEYTOOL,
                    "-genkeypair " +
                    "-keystore " + txtPath.Text + "keystore.jks " +
                    "-alias " + config.KEYTOOL_ALIAS + " " +
                    "-validity 10000 " +
                    "-keyalg RSA " +
                    "-keysize 2048 " +
                    "-dname \"" + config.KEYTOOL_DNAME + "\" " +
                    "-storepass " + config.KEYTOOL_STOREPASSWD + " " +
                    "-keypass " + config.KEYTOOL_PASSWD
                    );
                echo("keystore.jks created ...");
            }
        }
        #endregion

        private void btnShowLayout_Click(object sender, EventArgs e)
        {
            new FrmLayout(System.IO.File.ReadAllText(System.IO.Path.Combine(txtPath.Text, @"res\layout\activity_main.xml"))).ShowDialog();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, null);
                e.Handled = true;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var f = new FolderBrowserDialog();
            try
            {
                f.SelectedPath = txtPath.Text;
            }
            catch { }
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = f.SelectedPath + "\\";
                btnRefresh_Click(sender, e);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnDeleteFile.Enabled = (treeView1.SelectedNode.Tag != null);
        }

        private void btnAddJava_Click(object sender, EventArgs e)
        {
            var a = InputBox.Show("نام فایل");
            if (a.ReturnCode == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.Create(System.IO.Path.Combine(txtPath.Text, "src\\" + a.Text + ".java"));
                btnRefresh_Click(sender, e);
            }
        }
    }
}
