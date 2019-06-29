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
        string mainPath { get { return System.IO.Path.Combine(txtPath.Text, "src\\main"); } }
        string srcPath { get { return System.IO.Path.Combine(mainPath, "java"); } }
        string resPath { get { return System.IO.Path.Combine(mainPath, "res"); } }
        string keyPath { get { return System.IO.Path.Combine(txtPath.Text, "keystore.jks"); } }
        string PROJNAME { get { return System.IO.Path.GetFileNameWithoutExtension(txtPath.Text); } }
        public FrmMain()
        {
            InitializeComponent();
            if (txtPath.Text != "" && System.IO.Directory.Exists(txtPath.Text))
                btnRefresh_Click(null, null);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (new FrmOptions(txtPath.Text).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                btnRefresh_Click(sender, e);
        }

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textboxes.Clear();
            treeView1.Nodes["ndSrc"].Nodes.Clear();
            AddFilesToTree(srcPath, treeView1.Nodes["ndSrc"]);
            //--------------------------------------------
            UpdateDir(treeView1.Nodes["ndRes"], "ndValues", resPath + "\\values");
            UpdateDir(treeView1.Nodes["ndRes"], "ndLayout", resPath + "\\layout");
            UpdateDir(treeView1.Nodes["ndRes"], "ndMenu", resPath + "\\menu");
            UpdateDir(treeView1.Nodes["ndRes"], "ndDrawable", resPath + "\\drawable");

            if (!treeView1.Nodes.ContainsKey("AndroidManifest.xml"))
                treeView1.Nodes.Add(GetNodeAndTextBox("AndroidManifest.xml"));

            treeView1.ExpandAll();
            //----------------------------------------
            string json = System.IO.File.ReadAllText(System.IO.Path.Combine(txtPath.Text, PROJNAME + ".json"));
            config = (ProjectConfig)Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectConfig>(json);
            config.LIBS = config.LIBS ?? new string[] { };
            config.RESS = config.RESS ?? new string[] { };
        }

        private void UpdateDir(TreeNode treeNode, string nodeName, string path)
        {
            treeNode.Nodes[nodeName].Nodes.Clear();
            if (System.IO.Directory.Exists(path))
            {
                string[] ff = System.IO.Directory.GetFiles(path);
                foreach (string f in ff)
                {
                    treeNode.Nodes[nodeName].Nodes.Add(GetNodeAndTextBox(f));
                }
            }
        }

        private void AddFilesToTree(string path, TreeNode tnc)
        {
            string[] dd = System.IO.Directory.GetDirectories(path);
            foreach (string d in dd)
            {
                TreeNode tn = tnc.Nodes.Add(System.IO.Path.GetFileNameWithoutExtension(d));
                AddFilesToTree(d, tn);
            }
            string[] ff = System.IO.Directory.GetFiles(path);
            foreach (string f in ff)
            {
                tnc.Nodes.Add(GetNodeAndTextBox(f));
            }
        }

        private TreeNode GetNodeAndTextBox(string f)
        {
            TreeNode tn = new TreeNode();
            TextBox tb = new TextBox();
            groupBox1.Controls.Add(tb);
            tb.Visible = false;
            tb.Multiline = true;
            tb.WordWrap = false;
            tb.AcceptsTab = true;
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
                    if (System.IO.File.Exists(System.IO.Path.Combine(mainPath, e.Node.FullPath)))
                    {
                        string src = System.IO.File.ReadAllText(System.IO.Path.Combine(mainPath, e.Node.FullPath));
                        tb.Text = src;
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
            string myBuild = System.IO.Path.Combine(txtPath.Text, @"mybuild\");
            string apk = myBuild + PROJNAME;
            string manifest = System.IO.Path.Combine(mainPath, "AndroidManifest.xml");

            if (System.IO.Directory.Exists(myBuild))
                System.IO.Directory.Delete(myBuild, true);
            System.IO.Directory.CreateDirectory(myBuild);
            System.IO.Directory.CreateDirectory(myBuild + "gen");
            System.IO.Directory.CreateDirectory(myBuild + "obj");
            System.IO.Directory.CreateDirectory(myBuild + "apk");

            foreach (string l in LIBS)
                if (l != "")
                {
                    Run("cmd.exe", @"/C xcopy """ + l.Trim() + @""" " + myBuild + @"obj /Q");
                }
            echo("build directory recreated ...");
            Run(BUILD_TOOLS + @"\aapt.exe", @" package -f -m --auto-add-overlay " +
                @"-J """ + myBuild + @"gen"" " +
                @"-S """ + resPath + @""" " + GetResourcePath(config.RESS) +
                @"-M """ + manifest + @""" " +
                @"-I """ + PLATFORM + @"\android.jar""");

            if (!System.IO.File.Exists(myBuild + @"gen\" + PACKAGE + @"\R.java"))
            {
                echo("error: R.java not created ...");
                return;
            }
            echo("file R.java created ...");

            bool hasNoError = Run(config.JAVAC, @" -source 1.7 -target 1.7 " +
                @"-bootclasspath ""C:\Java\jre1.8.0_102\lib\rt.jar"" " +
                @"-classpath """ + PLATFORM + @"/android.jar""" + GetClassPath(LIBS) + @" -d " +
                myBuild + @"obj " + myBuild + @"gen\" + PACKAGE + @"\R.java " + GetJavaFiles(treeView1.Nodes["ndSrc"]));
            if (!hasNoError)
                echo("java files compiled with error...");
            else
            {
                echo("all java files compiled ...");

                Run(BUILD_TOOLS + @"\dx.bat", @" --dex --output=" + myBuild + @"apk\classes.dex " + myBuild + @"obj ");
                if (!System.IO.File.Exists(myBuild + @"apk\classes.dex"))
                {
                    echo("error: classes.dex not created ...");
                    return;
                }
                echo("classes.dex created ...");

                Run(BUILD_TOOLS + @"\aapt.exe", @" package -f --auto-add-overlay " +
                    @"-M """ + manifest + @""" " +
                    @"-S """ + resPath + @""" " + GetResourcePath(config.RESS) +
                    @"-I """ + PLATFORM + @"\android.jar"" " +
                    @"-F " + apk + @".unsigned.apk " + myBuild + @"apk");
                if (!System.IO.File.Exists(apk + @".unsigned.apk"))
                {
                    echo("error: .unsigned.apk not created ...");
                    return;
                }
                echo(".unsigned.apk created ...");

                Run(BUILD_TOOLS + @"\zipalign.exe", @" -f -p 4 " + apk + @".unsigned.apk " + apk + @".aligned.apk");
                echo(".aligned.apk created ...");

                Run(BUILD_TOOLS + @"\apksigner.bat",
                    " sign --ks " + keyPath + @" " +
                    "--ks-key-alias " + config.KEYTOOL_ALIAS + " --ks-pass pass:" + config.KEYTOOL_STOREPASSWD +
                    " --key-pass pass:" + config.KEYTOOL_PASSWD + " " +
                    "--out " + apk + @".apk " + apk + @".aligned.apk");
                echo(PROJNAME + ".apk created ...");
            }
        }

        private string GetClassPath(string[] LIBS)
        {
            string res = "";
            foreach (string l in LIBS)
                if (l.Trim() != "")
                {
                    res += @";""" + l.Trim() + @"""";
                }
            return res;
        }

        private string GetResourcePath(string[] RESS)
        {
            string res = "";
            foreach (string l in RESS)
                if (l.Trim() != "")
                {
                    res += @" -S """ + l.Trim() + @""" ";
                }
            return res;
        }

        private string GetJavaFiles(TreeNode tn)
        {
            string java = "";
            foreach (TreeNode nd in tn.Nodes)
            {
                if (nd.Text.Contains(".java"))
                    java += System.IO.Path.Combine(mainPath, nd.FullPath) + " ";
                else
                    java += GetJavaFiles(nd);
            }
            return java;
        }
//ADDED  from com.android.support:appcompat-v7:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-v4:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-compat:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-media-compat:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-core-utils:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-core-ui:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-fragment:26.0.0-alpha1:25:5
//MERGED from com.android.support:support-vector-drawable:26.0.0-alpha1:22:5
//MERGED from com.android.support:animated-vector-drawable:26.0.0-alpha1:22:5
        private bool Run(string fileName, string p)
        {
            System.IO.File.AppendAllText(System.IO.Path.Combine(txtPath.Text, "run.bat"), fileName + " " + p + "\r\n");
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
                        System.IO.File.WriteAllText(System.IO.Path.Combine(mainPath, tn.FullPath.Replace("*", "")), tb.Text);
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
                    string s = currentTextBox.Text;
                    s = s.Replace("\r\n", "\r");
                    s = s.Replace("\r", "\r\n");
                    System.IO.File.WriteAllText(System.IO.Path.Combine(mainPath, tn.FullPath.Replace("*", "")), s);
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
                System.IO.File.Delete(keyPath);
                echo("keystore.jks deleted ...");
                echo(config.KEYTOOL_ALIAS);
                echo(config.KEYTOOL_DNAME);
                Run(@config.KEYTOOL,
                    "-genkeypair " +
                    "-keystore " + keyPath + " " +
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
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.FullPath.Contains(@"res\layout\"))
                new FrmLayout(System.IO.File.ReadAllText(System.IO.Path.Combine(mainPath, treeView1.SelectedNode.FullPath))).ShowDialog();
            else
                new FrmLayout(System.IO.File.ReadAllText(System.IO.Path.Combine(mainPath, @"res\layout\activity_main.xml"))).ShowDialog();
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
            btnDeleteFile.Enabled = btnChangeFileName.Enabled = (treeView1.SelectedNode.Tag != null
                && treeView1.SelectedNode.Text.Contains(".java"));
        }

        private void btnAddJava_Click(object sender, EventArgs e)
        {
            var a = InputBox.Show("File Name", "Add java file");
            if (a.ReturnCode == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = "";
                if (treeView1.SelectedNode.FullPath.Contains("java\\"))
                    fileName = System.IO.Path.Combine(mainPath, treeView1.SelectedNode.FullPath + "\\" + a.Text + ".java");
                else
                    fileName = System.IO.Path.Combine(srcPath, a.Text + ".java");

                var f = System.IO.File.Create(fileName);
                f.Close();
                btnRefresh_Click(sender, e);
            }
        }

        private void btnChangeFileName_Click(object sender, EventArgs e)
        {
            var a = InputBox.Show("File Name", "Rename", treeView1.SelectedNode.Text);
            if (a.ReturnCode == System.Windows.Forms.DialogResult.OK)
            {
                string fileName1 = System.IO.Path.Combine(mainPath, treeView1.SelectedNode.FullPath);
                string fileName2 = fileName1.Replace(treeView1.SelectedNode.Text, a.Text);
                System.IO.File.Move(fileName1, fileName2);
                btnRefresh_Click(sender, e);
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete file?", "delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                System.IO.File.Delete(System.IO.Path.Combine(mainPath, treeView1.SelectedNode.FullPath));
                btnRefresh_Click(sender, e);
            }
        }

        private void btnAddLayout_Click(object sender, EventArgs e)
        {
            var a = InputBox.Show("File Name", "Add Layout file");
            if (a.ReturnCode == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = "";
                if (treeView1.SelectedNode.FullPath.Contains("layout\\"))
                    fileName = System.IO.Path.Combine(mainPath, treeView1.SelectedNode.FullPath + "\\" + a.Text + ".xml");
                else
                    fileName = System.IO.Path.Combine(resPath, "layout\\" + a.Text + ".xml");

                var f = System.IO.File.CreateText(fileName);
                f.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>

<LinearLayout xmlns:android=""http://schemas.android.com/apk/res/android""
    android:orientation=""vertical"" android:layout_height=""match_parent""
    android:paddingLeft=""10dp"" android:paddingRight=""10dp""
    android:background=""#fff"" android:layout_width=""300dp""
    android:paddingTop=""10dp"" android:layoutDirection=""rtl"">
</LinearLayout>");
                f.Close();
                btnRefresh_Click(sender, e);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string PACKAGE = config.PACKAGE;

            Run(@"C:\Program Files (x86)\BlueStacks\HD-ApkHandler.exe", System.IO.Path.Combine(txtPath.Text, @"mybuild\" + PROJNAME + @".apk"));
            echo(".apk installed ...");

            Run(@"C:\Program Files (x86)\BlueStacks\HD-RunApp.exe", "Android " + PACKAGE.Replace(@"\", ".") +
                " " + PACKAGE.Replace(@"\", ".") + ".MainActivity");
            echo(".apk run ...");
        }
    }
}
