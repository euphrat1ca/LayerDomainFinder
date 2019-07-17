using Layer.func;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Layer.mode
{
    class mode_enumerate
    {
        info.DnsInfo ReqDns = null;

        //计数
        int count = 0;
        public mode_enumerate(info.DnsInfo dnsinfo)
        {
            ReqDns = dnsinfo;
        }
        
        public void start()
        {
            foreach (string item in F_main.diclist)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (F_main.isrun)
                    {
                        List<string> openPort = new List<string>();
                        count += 1;
                        F_main.FM.lbl_state.BeginInvoke(new Action(() =>
                        {
                            F_main.FM.lbl_state.Text = "状态：枚举模式，" + "已发现"+F_main.FM.LV_result.Items.Count +"个域名，进度" + count + "/" + F_main.diclist.Length + "  当前" + item + "." + ReqDns.Domain;
                        }));

                        DnsResolver Dns = new DnsResolver(item + "." + ReqDns.Domain, ReqDns.DnsServer, ReqDns.TimeOut);
                        if (Dns.IsSuccess && F_main.blackIp.Contains(Dns.Record[0]) == false)
                        {
                            //避免接口查询的和枚举的重复
                            if (!F_main.DomainList.Contains(item + "." + ReqDns.Domain))
                            {
                                F_main.DomainList.Add(item + "." + ReqDns.Domain);

                                //加入到ListView
                                ListViewItem list = new ListViewItem(item + "." + ReqDns.Domain);

                                if (Dns.Record.Count > 1)
                                {
                                    list.SubItems.Add(string.Join(",", Dns.Record.ToArray()));
                                }
                                else
                                {
                                    list.SubItems.Add(string.Join(",", Dns.Record[0]));
                                }

                                if (F_main.isGetPorts)
                                {
                                    //扫描开放端口
                                    openPort = Tools.ScanPort(Dns.Record[0], ReqDns.Ports);
                                }

                                if (openPort.Count > 0)
                                {
                                    list.SubItems.Add(string.Join(",", openPort.ToArray()));

                                    if (F_main.isGetWebState)
                                    {
                                        if (openPort.Contains("80"))
                                        {
                                            string[] WebState = Tools.CheckWeb("http://" + item + "." + ReqDns.Domain);
                                            list.SubItems.Add(WebState[0]);
                                            list.SubItems.Add("80:" + WebState[1]);
                                        }
                                        else if (openPort.Contains("443"))
                                        {
                                            string[] WebState = Tools.CheckWeb("https://" + item + "." + ReqDns.Domain);
                                            list.SubItems.Add(WebState[0]);
                                            list.SubItems.Add("443:" + WebState[1]);
                                            list.Tag = "https";
                                        }
                                    }
                                    else
                                    {
                                        list.SubItems.Add("-");
                                        list.SubItems.Add("-");
                                    }

                                }
                                else
                                {
                                    list.SubItems.Add("-");
                                    if (F_main.isGetWebState)
                                    {
                                        list.SubItems.Add("端口未开放");
                                        list.SubItems.Add("端口未开放");
                                    }
                                    else
                                    {
                                        list.SubItems.Add("-");
                                        list.SubItems.Add("-");
                                    }
                                    
                                }
                                list.SubItems.Add("暴力枚举");

                                F_main.FM.LV_result.BeginInvoke(new Action(() =>
                                {
                                    F_main.FM.LV_result.Items.Add(list);
                                }));
                            }
                        }
                    }
                    F_main.handler.Signal();

                });
            }

        }

    }
}
