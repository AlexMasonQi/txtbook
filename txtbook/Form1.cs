using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace txtbook
{
    public partial class Form1 : Form
    {
        bool txtchanged = false;
        string txtfileName = "";
        string linenumber, txtlength;
        public Form1()
        {
            InitializeComponent();
        }

       

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 新建NToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtBox.Clear();
            txtchanged = false;
            txtfileName = "";
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openfiles();
        }
        protected void openfiles()//读取文件
        {
            OpenFileDialog opfiles = new OpenFileDialog();
            opfiles.Title = "打开文件";
            opfiles.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if(opfiles.ShowDialog()==DialogResult.OK)
            {
                FileStream fs = new FileStream(opfiles.FileName, FileMode.Open, FileAccess.Read,FileShare.None);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                txtBox.Text = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                txtfileName = opfiles.FileName;
                this.Text = txtfileName.Substring(txtfileName.LastIndexOf("\\") + 1);
            }
            txtchanged = false;
        }
        protected void savefiles()//写入、保存文件
        {
            if (txtfileName == "")
            {
                SaveFileDialog sffiles = new SaveFileDialog();
                sffiles.Title = "另存为...";
                sffiles.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (sffiles.ShowDialog() == DialogResult.OK)
                {
                    FileStream wr = new FileStream(sffiles.FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(wr, Encoding.Default);
                    sw.Write(txtBox.Text);
                    sw.Close();
                    wr.Close();
                    txtchanged = false;
                    txtfileName = sffiles.FileName;
                    this.Text = txtfileName.Substring(txtfileName.LastIndexOf("\\") + 1);
                }
            }
            else
            {
                FileStream wr = new FileStream(txtfileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(wr, Encoding.Default);
                sw.Write(txtBox.Text);
                sw.Close();
                wr.Close();
                txtchanged = false;
            }

        }

        private void 保存SToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savefiles();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            txtchanged = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(txtchanged)
            {
                if(this.Text!="无标题 - 记事本")
                {
                    DialogResult cue = MessageBox.Show("是否将更改保存到 " + this.Text + "?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if(cue==DialogResult.Yes)
                    {
                        savefiles();
                        e.Cancel = false;
                    }
                    else if(cue==DialogResult.No)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (txtBox.Text != "")
                    {
                        DialogResult cue1 = MessageBox.Show("是否将更改保存到 无标题?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (cue1 == DialogResult.Yes)
                        {
                            savefiles();
                            e.Cancel = false;
                        }
                        else if (cue1 == DialogResult.No)
                        {
                            e.Cancel = false;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            /***软件界面几何数据***/
            int x = this.Location.X;
            int y = this.Location.Y;
            int w = this.Size.Width;
            int h = this.Size.Height;

            /*******配置文件txtbook.suk,用于存放软件界面几何数据*******/
            StreamWriter swtb = new StreamWriter(Application.StartupPath + "\\txtbook.suk", false);
            swtb.WriteLine(x.ToString());
            swtb.WriteLine(y.ToString());
            swtb.WriteLine(w.ToString());
            swtb.WriteLine(h.ToString());
            swtb.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FontDialog font = new FontDialog();
            //font.Font = txtBox.Font;
            string readfile = Application.StartupPath + "\\txtbook.suk";
            if(File.Exists(readfile))
            {
                StreamReader read = new StreamReader(readfile);
                int x = Convert.ToInt32(read.ReadLine());
                int y = Convert.ToInt32(read.ReadLine());
                int w = Convert.ToInt32(read.ReadLine());
                int h = Convert.ToInt32(read.ReadLine());
                this.Location = new Point(x, y);
                this.Size = new Size(w, h);
                read.Close();
            }
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtfileName == "")
            {
                SaveFileDialog sffiles = new SaveFileDialog();
                sffiles.Title = "另存为...";
                sffiles.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (sffiles.ShowDialog() == DialogResult.OK)
                {
                    FileStream wr = new FileStream(sffiles.FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(wr, Encoding.Default);
                    sw.Write(txtBox.Text);
                    sw.Close();
                    wr.Close();
                    txtchanged = false;
                    txtfileName = sffiles.FileName;
                    this.Text = txtfileName.Substring(txtfileName.LastIndexOf("\\") + 1);
                }
            }
            else
            {
                SaveFileDialog sffiles = new SaveFileDialog();
                sffiles.Title = "另存为...";
                sffiles.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (sffiles.ShowDialog() == DialogResult.OK)
                {
                    FileStream wr = new FileStream(sffiles.FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(wr, Encoding.Default);
                    sw.Write(txtBox.Text);
                    sw.Close();
                    wr.Close();
                    txtfileName = sffiles.FileName;
                    txtchanged = false;
                }
            }
        }

        private void 撤消UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBox.Undo();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBox.Cut();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBox.Copy();
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBox.Paste();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//删除选中的文本
        {
            txtBox.SelectedText = null;
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBox.Focus();
            txtBox.SelectAll();
        }

        private void 时间日期DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtBox.Text = DateTime.Now.ToString();
        }

       

        private void 字体FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = txtBox.Font;
            if(font.ShowDialog()==DialogResult.OK)
            {
                txtBox.Font = font.Font;
            }
        }

       

        private void 查看帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://windows.microsoft.com/zh-cn/windows-10/support");
        }

        private void 自动换行ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if(自动换行ToolStripMenuItem.Checked==true)
            {
                txtBox.WordWrap = true;
                statusStrip1.Visible = false;
                状态栏SToolStripMenuItem.Enabled = false;
                //状态栏SToolStripMenuItem.Checked = false;
            }
            else
            {
                txtBox.WordWrap = false;
                状态栏SToolStripMenuItem.Enabled = true;
                CheckState ck = 状态栏SToolStripMenuItem.CheckState;
                if(ck==CheckState.Checked)
                {
                    //状态栏SToolStripMenuItem.Checked = true;
                    statusStrip1.Visible = true;
                }
                else if(ck==CheckState.Unchecked)
                {
                    statusStrip1.Visible = false;
                }
            }
        }

        private void 状态栏SToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            GetCurrentNum();
            if (状态栏SToolStripMenuItem.Checked)
            {
                statusStrip1.Visible = true;
                tssl.Text = "第" + linenumber + "行" + "," + "第" + txtlength + "列";
                statusStrip1.Update();
            }
            else
            {
                statusStrip1.Visible = false;
            }
        }
        private void GetCurrentNum()
        {
            int currentCount = 0;//每行的个数
            int totalCount = txtBox.SelectionStart;//当前光标以前的字数
            int index = txtBox.SelectionStart;
            currentCount = index + 1;//SelectionStart是从0开始 默认+1
            int last = 0;
            string[] currentStr = txtBox.Text.Substring(0, index).Split('\n');//当前光标以前每行的数组
            last = currentStr.Length;//当前光标前面有多少行
            linenumber = last.ToString();//显示光标所在的行数
            //循环减去前面几行的文本字数  
            for (int i = 1; i < last; i++)
            {
                currentCount = currentCount - currentStr[i - 1].Length - 1;//多一个字符  一个换行2个字符
            }
            if (last == 1)
                currentCount = index + 1;//如果为一行 等于当前光标所在个数
            txtlength = currentCount.ToString();
        }
    }
}
