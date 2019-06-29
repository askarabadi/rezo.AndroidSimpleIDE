namespace ARA.AndroidReZo
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("java");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("layout");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("values");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("menu");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("drawable");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("res", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCompile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAddJava = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChangeFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRun1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnShowLayout = new System.Windows.Forms.Button();
            this.btnMakeKey = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(248, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 540);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCompile
            // 
            this.btnCompile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCompile.Location = new System.Drawing.Point(12, 346);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(47, 23);
            this.btnCompile.TabIndex = 1;
            this.btnCompile.Text = "Build";
            this.toolTip1.SetToolTip(this.btnCompile, "کامپایل و ساخت فایل نتیجه");
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // txtPath
            // 
            this.txtPath.AutoCompleteCustomSource.AddRange(new string[] {
            "D:\\Android\\Projects\\MyGheble\\",
            "D:\\Android\\Projects\\MyGpsLogger\\"});
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPath.Location = new System.Drawing.Point(12, 17);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(166, 21);
            this.txtPath.TabIndex = 3;
            this.txtPath.Text = "D:\\Android\\Projects\\MyToolbar";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Location = new System.Drawing.Point(12, 41);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "ndSrc";
            treeNode1.Text = "java";
            treeNode2.Name = "ndLayout";
            treeNode2.Text = "layout";
            treeNode3.Name = "ndValues";
            treeNode3.Text = "values";
            treeNode4.Name = "ndMenu";
            treeNode4.Text = "menu";
            treeNode5.Name = "ndDrawable";
            treeNode5.Text = "drawable";
            treeNode6.Name = "ndRes";
            treeNode6.Text = "res";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6});
            this.treeView1.Size = new System.Drawing.Size(224, 299);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddJava,
            this.btnAddLayout,
            this.btnChangeFileName,
            this.toolStripSeparator1,
            this.btnDeleteFile,
            this.btnBuild,
            this.btnRun1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 142);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // btnAddJava
            // 
            this.btnAddJava.Name = "btnAddJava";
            this.btnAddJava.Size = new System.Drawing.Size(161, 22);
            this.btnAddJava.Text = "فایل جدید جاوا";
            this.btnAddJava.Click += new System.EventHandler(this.btnAddJava_Click);
            // 
            // btnAddLayout
            // 
            this.btnAddLayout.Name = "btnAddLayout";
            this.btnAddLayout.Size = new System.Drawing.Size(161, 22);
            this.btnAddLayout.Text = "فایل جدید لی اوت";
            this.btnAddLayout.Click += new System.EventHandler(this.btnAddLayout_Click);
            // 
            // btnChangeFileName
            // 
            this.btnChangeFileName.Name = "btnChangeFileName";
            this.btnChangeFileName.Size = new System.Drawing.Size(161, 22);
            this.btnChangeFileName.Text = "تغییر نام";
            this.btnChangeFileName.Click += new System.EventHandler(this.btnChangeFileName_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteFile.Image")));
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(161, 22);
            this.btnDeleteFile.Text = "حذف فایل انتخابی";
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.btnBuild.Size = new System.Drawing.Size(161, 22);
            this.btnBuild.Text = "کامپایل";
            this.btnBuild.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // btnRun1
            // 
            this.btnRun1.Name = "btnRun1";
            this.btnRun1.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.btnRun1.Size = new System.Drawing.Size(161, 22);
            this.btnRun1.Text = "اجرا";
            this.btnRun1.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(209, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(27, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnShowLayout);
            this.panel1.Controls.Add(this.btnMakeKey);
            this.panel1.Controls.Add(this.btnConfig);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnSaveAll);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.btnCompile);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 540);
            this.panel1.TabIndex = 6;
            // 
            // btnRun
            // 
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRun.Location = new System.Drawing.Point(60, 346);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(26, 23);
            this.btnRun.TabIndex = 13;
            this.toolTip1.SetToolTip(this.btnRun, "کامپایل و ساخت فایل نتیجه");
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.Location = new System.Drawing.Point(181, 15);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(27, 23);
            this.btnLoad.TabIndex = 12;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnShowLayout
            // 
            this.btnShowLayout.Image = ((System.Drawing.Image)(resources.GetObject("btnShowLayout.Image")));
            this.btnShowLayout.Location = new System.Drawing.Point(88, 346);
            this.btnShowLayout.Name = "btnShowLayout";
            this.btnShowLayout.Size = new System.Drawing.Size(28, 23);
            this.btnShowLayout.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btnShowLayout, "مشاهده لی اوت");
            this.btnShowLayout.UseVisualStyleBackColor = true;
            this.btnShowLayout.Click += new System.EventHandler(this.btnShowLayout_Click);
            // 
            // btnMakeKey
            // 
            this.btnMakeKey.Image = ((System.Drawing.Image)(resources.GetObject("btnMakeKey.Image")));
            this.btnMakeKey.Location = new System.Drawing.Point(204, 346);
            this.btnMakeKey.Name = "btnMakeKey";
            this.btnMakeKey.Size = new System.Drawing.Size(28, 23);
            this.btnMakeKey.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnMakeKey, "ساخت کلید");
            this.btnMakeKey.UseVisualStyleBackColor = true;
            this.btnMakeKey.Click += new System.EventHandler(this.btnMakeKey_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConfig.Image")));
            this.btnConfig.Location = new System.Drawing.Point(175, 346);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(28, 23);
            this.btnConfig.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnConfig, "تنظیمات");
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(10, 375);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(226, 153);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(218, 127);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Output";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(212, 121);
            this.textBox1.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(145, 346);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(28, 23);
            this.btnSave.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnSave, "ذخیره فایل");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAll.Image")));
            this.btnSaveAll.Location = new System.Drawing.Point(116, 346);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(28, 23);
            this.btnSaveAll.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnSaveAll, "ذخیره همه");
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(245, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 540);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 540);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "Android Simple IDE";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnMakeKey;
        private System.Windows.Forms.Button btnShowLayout;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnAddJava;
        private System.Windows.Forms.ToolStripMenuItem btnAddLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteFile;
        private System.Windows.Forms.ToolStripMenuItem btnChangeFileName;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem btnBuild;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ToolStripMenuItem btnRun1;
    }
}

