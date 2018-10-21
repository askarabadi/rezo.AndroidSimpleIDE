using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace ARA.AndroidReZo
{
    public partial class FrmLayout : Form
    {
        string _layout;
        public FrmLayout(string layout)
        {
            InitializeComponent();
            _layout = layout;
        }

        private void FrmLayout_Load(object sender, EventArgs e)
        {
            ShowLayout(_layout);
        }

        private void ShowLayout(string layout)
        {
            StringReader sr = new StringReader(layout);
            XDocument doc = XDocument.Load(sr);
            tableLayoutPanel1.Tag = true;
            CreateLayout(tableLayoutPanel1, doc.Root);
            if (tableLayoutPanel1.RowCount > 1) tableLayoutPanel1.RowCount--;
            if (tableLayoutPanel1.ColumnCount > 1) tableLayoutPanel1.ColumnCount--;
        }

        private void CreateLayout(Control parent, XElement el)
        {
            Control c;
            switch (el.Name.LocalName)
            {
                case "LinearLayout":
                    bool vertical = false;
                    foreach (XAttribute attr in el.Attributes())
                    {
                        if (attr.Name.LocalName == "orientation" && attr.Value == "vertical")
                            vertical = true;
                    }
                    c = new TableLayoutPanel();
                    (c as TableLayoutPanel).BorderStyle = BorderStyle.FixedSingle;
                    (c as TableLayoutPanel).AutoSize = true;
                    (c as TableLayoutPanel).AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                    (c as TableLayoutPanel).ColumnCount = 1;
                    (c as TableLayoutPanel).RowCount = 1;
                    (c as TableLayoutPanel).Tag = vertical;

                    foreach (XAttribute attr in el.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "id": c.Name = attr.Value; break;
                            case "layout_width": SetWidth(c, attr.Value); break;
                            case "layout_height":
                                c.Height = 25;
                                break;
                            case "layout_marginTop":
                                //(c as FlowLayoutPanel).Height = 100;
                                break;
                            case "weightSum":
                                //(c as FlowLayoutPanel).Height = 100;
                                break;
                            case "gravity":
                                //(c as FlowLayoutPanel).Height = 100;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "TextView":
                    c = new Label();
                    (c as Label).BorderStyle = BorderStyle.FixedSingle;
                    foreach (XAttribute attr in el.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "id":
                                (c as Label).Name = attr.Value;
                                break;
                            case "layout_width": SetWidth(c, attr.Value); break;
                            case "layout_height":
                                (c as Label).Height = 25;
                                break;
                            case "layout_marginTop":
                                (c as Label).Height = 25;
                                break;
                            case "gravity":
                                (c as Label).TextAlign = ContentAlignment.MiddleCenter;
                                break;
                            case "text":
                                (c as Label).Text = attr.Value;
                                break;
                            case "textSize":
                                //(c as Label).Text = attr.Value;
                                break;
                            case "background":
                                (c as Label).BackColor = Color.Gray;//attr.Value;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "EditText":
                    c = new TextBox();
                    (c as TextBox).BorderStyle = BorderStyle.FixedSingle;
                    foreach (XAttribute attr in el.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "id":
                                (c as TextBox).Name = attr.Value;
                                break;
                            case "layout_width": SetWidth(c, attr.Value); break;
                            case "layout_height":
                                (c as TextBox).Height = 25;
                                break;
                            case "layout_marginTop":
                                (c as TextBox).Height = 25;
                                break;
                            case "gravity":
                                (c as TextBox).TextAlign = HorizontalAlignment.Center;
                                break;
                            case "text":
                                (c as TextBox).Text = attr.Value;
                                break;
                            case "textSize":
                                (c as TextBox).MaxLength = int.Parse(attr.Value);
                                break;
                            case "singleLine":
                                (c as TextBox).Multiline = attr.Value == "true";
                                break;
                            case "numeric":
                                //(c as TextBox).Multiline = attr.Value == "true";
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "Button":
                    c = new Button();
                    foreach (XAttribute attr in el.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "id":
                                (c as Button).Name = attr.Value;
                                break;
                            case "layout_width": SetWidth(c, attr.Value); break;
                            case "layout_height":
                                (c as Button).Height = 25;
                                break;
                            case "layout_marginTop":
                                (c as Button).Margin = new Padding(0, int.Parse(attr.Value), 0, 0);
                                break;
                            case "layout_margin":
                                (c as Button).Margin = new Padding(int.Parse(attr.Value));
                                break;
                            case "layout_weight":
                                //(c as Button).TextAlign = ContentAlignment.MiddleCenter;
                                break;
                            case "layout_gravity":
                                (c as Button).TextAlign = ContentAlignment.MiddleCenter;
                                break;
                            case "gravity":
                                (c as Button).TextAlign = ContentAlignment.MiddleCenter;
                                break;
                            case "text":
                                (c as Button).Text = attr.Value;
                                break;
                            case "background":
                                (c as Label).BackColor = Color.Gray;//attr.Value;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "TableLayout":
                    c = null;
                    break;
                default:
                    c = null;
                    break;
            }

            foreach (XElement ele in el.Elements())
            {
                CreateLayout(c, ele);
            }
            //--------------------------------------
            if (c is TableLayoutPanel)
            {
                if ((parent as TableLayoutPanel).RowCount > 1) (parent as TableLayoutPanel).RowCount--;
                if ((parent as TableLayoutPanel).ColumnCount > 1) (parent as TableLayoutPanel).ColumnCount--;
            }

            if ((bool)(parent as TableLayoutPanel).Tag)
            {
                (parent as TableLayoutPanel).Controls.Add(c, 0, (parent as TableLayoutPanel).RowCount - 1);
                (parent as TableLayoutPanel).RowCount++;
            }
            else
            {
                (parent as TableLayoutPanel).Controls.Add(c, (parent as TableLayoutPanel).ColumnCount - 1, 0);
                (parent as TableLayoutPanel).ColumnCount++;
            }
        }

        private void SetWidth(Control c, string p)
        {
            switch (p)
            {
                case "wrap_content":
                    c.AutoSize = true;
                    break;
                case "fill_parent":
                    c.Dock = DockStyle.Fill;
                    break;
                case "match_parent":
                    c.AutoSize = true;
                    break;
                default:
                    c.Width = int.Parse(p);
                    break;
            }
        }
    }
}
