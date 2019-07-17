using Layer.func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Layer.mode
{
    class mode_serverapi
    {
        info.DnsInfo ReqDns = null;
        
        public mode_serverapi(info.DnsInfo dnsinfo)
        {
            ReqDns = dnsinfo;
        }

        public void start()
        {
            //暂时只有一个接口
            get_formlinkscn(ReqDns.Domain);

        }

        public void AnalysisDomain(string domain) {
            
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (F_main.isrun)
                {
                    List<string> openPort = new List<string>();

                    F_main.FM.lbl_state.BeginInvoke(new Action(() =>
                    {
                        F_main.FM.lbl_state.Text = "状态：接口模式，" + "已发现" + F_main.FM.LV_result.Items.Count + "个域名";
                    }));

                    DnsResolver Dns = new DnsResolver(domain, ReqDns.DnsServer, ReqDns.TimeOut);
                    if (Dns.IsSuccess && F_main.blackIp.Contains(Dns.Record[0]) == false)
                    {
                        //避免接口查询的和枚举的重复
                        if (!F_main.DomainList.Contains(domain))
                        {
                            F_main.DomainList.Add(domain);

                            //加入到ListView
                            System.Windows.Forms.ListViewItem list = new System.Windows.Forms.ListViewItem(domain);

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
                                        string[] WebState = Tools.CheckWeb("http://" + domain);
                                        list.SubItems.Add(WebState[0]);
                                        list.SubItems.Add("80:" + WebState[1]);
                                    }
                                    else if (openPort.Contains("443"))
                                    {
                                        string[] WebState = Tools.CheckWeb("https://" + domain);
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

                            list.SubItems.Add("服务接口");

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

        public void get_formlinkscn(string domain)
        {
            try
            {
                string ret = func.HttpHelper.request("http://i.links.cn/subdomain/", "POST", "GBK", new string[] { "Content-Type: application/x-www-form-urlencoded", "Referer: http://i.links.cn/subdomain/" }, "domain=" + domain + "&b2=1&b3=1&b4=1", true, false);
                if (ret.Contains("亲，系统崩溃了，稍后再试吧"))
                {
                    ret = func.HttpHelper.request("http://i.links.cn/subdomain/", "POST", "GBK", new string[] { "Content-Type: application/x-www-form-urlencoded", "Referer: http://i.links.cn/subdomain/" }, "domain=" + domain + "&b2=1&b3=1&b4=1", true, false);
                }

                MatchCollection Matches = Regex.Matches(ret, "id=domain\\d+\\s+value=\"http://(.*)\"><input");

                if (Matches.Count > 0)
                {
                    
                    //遍历分析所有匹配结果
                    for (int i = 0; i < Matches.Count; i++)
                    {
                        if (Matches[i].Success)
                        {
                            try
                            {
                                if (Matches[i].Groups[1].Value.Contains("."+domain))
                                {
                                    //增加计数器
                                    F_main.handler = new CountdownEvent(F_main.handler.CurrentCount + 1);
                                    AnalysisDomain(Matches[i].Groups[1].Value);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            
        }
    }
}
