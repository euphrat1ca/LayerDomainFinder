using Layer.func;
using Layer.mode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Layer
{
    public partial class F_main : Form
    {
        public F_main()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.LV_result.ListViewItemSorter = lvwColumnSorter;
        }

        #region
        // ----------------配置区----------------
        //泛解析的黑名单IP
        public static List<string> blackIp = new List<string>();

        //注册线程计数器
        public static CountdownEvent handler = null;

        //枚举字典
        public static string[] diclist = null;

        //存在的子域名
        public static List<string> DomainList = null;

        //是否停止
        public static bool isrun = true;

        //是否获取端口
        public static bool isGetPorts = true;

        //是否获取web服务器信息
        public static bool isGetWebState = true;

        //本实例对象
        public static F_main FM = null;

        //是否开启枚举模式
        bool isEnumerate = true;

        //是否开启接口模式
        bool isServerapi = true;

        //更新线程
        Thread update = null;
        
        //当前版本
        const string version = "4.2";

        //核心线程
        Thread Th = null;
        private ListViewColumnSorter lvwColumnSorter;

        #endregion

        public void AllStart(object RD)
        {
            info.DnsInfo ReqDns = RD as info.DnsInfo;

            //接口模式
            if (isServerapi)
            {
                lbl_state.BeginInvoke(new Action(() =>
                {
                    lbl_state.Text = "状态：正在运行服务接口模式获取子域名，请等待...";
                }));

                mode_serverapi Serverapi = new mode_serverapi(ReqDns);
                Serverapi.start();
            }

            //枚举模式
            if (isEnumerate)
            {
                F_main.handler = new CountdownEvent(F_main.handler.CurrentCount + diclist.Length);
                mode_enumerate enumerate = new mode_enumerate(ReqDns);
                enumerate.start();
            }
            if (handler!=null)
            {
                handler.Wait();
            }
            
            lbl_state.BeginInvoke(new Action(() =>
            {
                bt_control.Text = "启 动";
                lbl_state.Text = "状态：完成，共发现" + DomainList.Count + "个子域名";
            }));

        }
        public void updatever()
        {
            try
            {
                string newver = HttpHelper.request("http://www.cnseay.com/tools/layerver.txt", "GET", "utf-8", new string[] { }, "", false, false).Trim();
                if (newver != version && newver != "请求异常"&& newver!="")
                {
                    MessageBox.Show("最新版本为" + newver + "，请访问www.cnseay.com下载新版");
                }
            }
            catch (Exception)
            {
                //避免检测新版失败老提示，就不提醒了
            }
            Thread.CurrentThread.Abort();
        }

        private void F_main_Load(object sender, EventArgs e)
        {
            LV_result.Columns[1].Width = LV_result.Width - LV_result.Columns[0].Width - LV_result.Columns[2].Width - LV_result.Columns[3].Width - LV_result.Columns[4].Width - LV_result.Columns[5].Width - 30;
            //LV_result.Width = this.Width - 100;
            //this.Size = new Size(1542, 1041);

            FM = this;
            cmb_dns.SelectedIndex = 2;
            cmb_thread.SelectedIndex = 0;
            //获取更新
            update = new Thread(updatever);
            update.Start();
        }

        private void F_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Th!=null&&Th.ThreadState!=ThreadState.Aborted)
            {
                Th.Abort();
            }

            if (update != null && update.ThreadState != ThreadState.Aborted)
            {
                update.Abort();
            }
        }

        private void bt_control_Click(object sender, EventArgs e)
        {
            //Tools.CheckWeb("http://3g.wandoujia.com/");
            //return;
            if (txt_domain.Text.Trim()=="")
            {
                MessageBox.Show("请输入域名");
                return;
            }

            if (bt_control.Text != "启 动")
            {
                isrun = false;
                bt_control.Text = "停止中";
                lbl_state.Text = "状态：停止中";
                return;
            }

            bt_control.Text = "启动中";
            
            //初始化参数
            #region
            info.DnsInfo ReqDns = new info.DnsInfo();
            ReqDns.Domain = txt_domain.Text.Trim();
            ReqDns.TimeOut = 2000;
            ReqDns.Ports = txt_ports.Text.Trim();

            if (cmb_thread.SelectedIndex!=0)
            {
                int ThreadCount = Convert.ToInt32(cmb_thread.SelectedItem.ToString());
                ThreadPool.SetMaxThreads(ThreadCount, ThreadCount);
            }
            else
            {
                ThreadPool.SetMaxThreads(100000,100000);
            }
            LV_result.Items.Clear();

            if (cmb_dns.SelectedItem!=null&&cmb_dns.SelectedItem.ToString().Contains("-"))
            {
                ReqDns.DnsServer = cmb_dns.SelectedItem.ToString().Trim().Split('-')[1];
            }
            else
            {
                if (Regex.IsMatch(cmb_dns.Text.Trim(), @"^\d+\.\d+\.\d+\.\d+$"))
                {
                    ReqDns.DnsServer = cmb_dns.Text.Trim();
                }
                else
                {
                    MessageBox.Show("请输入正确的DNS服务器地址");
                    bt_control.Text = "启 动";
                    return;
                }
                
            }

            if (chb_scanports.Checked)
            {
                isGetPorts = true;
            }
            else
            {
                isGetPorts = false;
            }

            if (chb_webstate.Checked)
            {
                isGetWebState = true;
            }
            else
            {
                isGetWebState = false;
            }

            if (chb_Enumerate.Checked)
            {
                isEnumerate = true;
            }
            else
            {
                isEnumerate = false;
            }

            if (chb_Serverapi.Checked)
            {
                isServerapi = true;
            }
            else
            {
                isServerapi = false;
            }
            
            //清空端口扫描缓存
            Tools.PortCache = new Dictionary<string, List<string>>();
            DomainList = new List<string>();


            #endregion
            
            if (isEnumerate)
            {
                if (File.Exists("dic.txt"))
                {
                    diclist = File.ReadAllLines("dic.txt");
                }
                else
                {
                    bt_control.Text = "启 动";
                    MessageBox.Show("字典文件dic.txt不存在,请放置在本程序同目录下");
                    return;
                }
                bool isgo = true;
                //重置线程池计数器
                handler = new CountdownEvent(1);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        if (Tools.checkAlwaysResolution(ReqDns))
                        {
                            string tmpblackip = string.Join(",", blackIp.ToArray());
                            if (System.Windows.Forms.DialogResult.No == MessageBox.Show("此域名泛解析到：" + tmpblackip + ",程序将自动略过解析到此IP的域名\n是否继续爆破？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                            {
                                bt_control.Text = "启 动";
                                isgo = false;
                                
                            }
                        }
                        handler.Signal();

                    }
                    catch (Exception)
                    {
                    }
                });
                //等待线程池结束
                handler.Wait();
                if (!isgo)
                {
                    return;
                }
            }

            //这里代表要真正开始启动了
            isrun = true;
            bt_control.Text = "停 止";

            Th = new Thread(new ParameterizedThreadStart(AllStart));
            Th.Start(ReqDns);
        }

        private void LV_result_DoubleClick(object sender, EventArgs e)
        {
            if (LV_result.SelectedItems.Count > 0)
            {
                if (LV_result.SelectedItems[0].Tag != null && LV_result.SelectedItems[0].Tag.ToString() == "https")
                {
                    System.Diagnostics.Process.Start("https://" + LV_result.SelectedItems[0].SubItems[0].Text);
                }
                else
                {
                    System.Diagnostics.Process.Start("http://" + LV_result.SelectedItems[0].SubItems[0].Text);
                }

            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cnseay.com/");
        }

        private void LV_result_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // 重新设置此列的排序方法.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // 设置排序列，默认为正向排序
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // 用新的排序方法对ListView排序
            this.LV_result.Sort();
        }

        private void 打开网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_result.SelectedItems.Count > 0)
            {
                if (LV_result.SelectedItems[0].Tag !=null&& LV_result.SelectedItems[0].Tag.ToString()=="https")
                {
                    System.Diagnostics.Process.Start("https://" + LV_result.SelectedItems[0].SubItems[0].Text);
                }
                else
                {
                    System.Diagnostics.Process.Start("http://" + LV_result.SelectedItems[0].SubItems[0].Text);
                }

            }
        }

        private void 复制域名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_result.SelectedItems.Count > 0)
            {
                Clipboard.SetDataObject(LV_result.SelectedItems[0].SubItems[0].Text);
            }
        }

        private void 复制IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_result.SelectedItems.Count > 0)
            {
                Clipboard.SetDataObject(LV_result.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void 仅导域名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_result.Items.Count > 0)
            {
                save_path.FileName = txt_domain.Text.Trim() + "子域名列表_Layer.txt";
                if (save_path.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string list = "";
                        foreach (ListViewItem item in LV_result.Items)
                        {
                            list += item.SubItems[0].Text + "\r\n";
                        }

                        File.WriteAllText(save_path.FileName, list);

                        MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("导出失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("列表为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 导出列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_result.Items.Count > 0)
            {
                save_path.FileName = txt_domain.Text.Trim() + "子域名列表_Layer.txt";
                if (save_path.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string list = "域名" + "\t" + "解析IP" + "\t" + "开放端口" + "\t" + "WEB服务器" + "\t" + "网站状态\r\n";
                        foreach (ListViewItem item in LV_result.Items)
                        {
                            list += item.SubItems[0].Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text + "\t" + item.SubItems[3].Text + "\t" + item.SubItems[4].Text + "\r\n";
                        }
                        File.WriteAllText(save_path.FileName, list);

                        MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("导出失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("列表为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void F_main_SizeChanged(object sender, EventArgs e)
        {
            LV_result.Columns[1].Width = LV_result.Width - LV_result.Columns[0].Width - LV_result.Columns[2].Width - LV_result.Columns[3].Width - LV_result.Columns[4].Width - LV_result.Columns[5].Width - 30;
        }

        private void 复制所选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LV_result.Items.Count > 0)
            {
                string list = "域名" + "\t" + "解析IP" + "\t" + "开放端口" + "\t" + "WEB服务器" + "\t" + "网站状态\r\n";

                foreach (ListViewItem item in LV_result.SelectedItems)
                {
                    list += item.SubItems[0].Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text + "\t" + item.SubItems[3].Text + "\t" + item.SubItems[4].Text + "\r\n";
                }
                Clipboard.SetDataObject(list);
            }
        }
    }
}
