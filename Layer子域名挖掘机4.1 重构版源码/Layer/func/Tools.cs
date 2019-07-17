using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Layer.func
{
    class Tools
    {
        /// <summary>
        /// 判断是否泛解析
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool checkAlwaysResolution(info.DnsInfo RD)
        {
            info.DnsInfo ReqDns = RD as info.DnsInfo;
            bool IsAlways = false;
            for (int i = 0; i < 4; i++)
            {
                DnsResolver checkdns = null;
                if (i < 1)
                {
                    checkdns = new DnsResolver(i + "seaydomaincheck." + ReqDns.Domain, ReqDns.DnsServer, ReqDns.TimeOut);
                }
                else if (i < 2)
                {
                    checkdns = new DnsResolver(i + "seaydomain.check." + ReqDns.Domain, ReqDns.DnsServer, ReqDns.TimeOut);
                }
                else
                {
                    checkdns = new DnsResolver(i + "seay.domain.check." + ReqDns.Domain, ReqDns.DnsServer, ReqDns.TimeOut);
                }
                if (checkdns.IsSuccess)
                {
                    IsAlways = true;
                    foreach (string item in checkdns.Record)
                    {
                        F_main.blackIp.Add(item);
                    }
                }
            }
            F_main.blackIp = F_main.blackIp.Distinct().ToList();
            return IsAlways;
        }

        //端口缓存
        public static Dictionary<string, string> PortCache = null;
        /// <summary>
        /// 扫描端口
        /// </summary>
        /// <param name="host">主机IP</param>
        /// <param name="ports">要扫描的端口，以逗号分隔</param>
        /// <returns></returns>
        public static string ScanPort(string host, string ports)
        {
            //先判断这个IP有没有扫描过端口，如果有扫描过就直接返回
            if (PortCache.ContainsKey(host))
            {
                return PortCache[host];
            }
            string result = "";
            TcpClient objTCP = null;
            
            foreach (string port in ports.Split(','))
            {
                try
                {
                    //用TcpClient对象扫描端口
                    objTCP = new TcpClient();
                    IAsyncResult oAsyncResult = objTCP.BeginConnect(host, Convert.ToInt32(port), null, null);
                    oAsyncResult.AsyncWaitHandle.WaitOne(2000, true);//1000为超时时间 
                    
                    if (objTCP.Connected)
                    {
                        objTCP.Close();
                        result += port + ",";
                    }
                }
                catch
                {
                }
            }
            if (result != "")
            {
                result = result.Remove(result.Length - 1);

                //加入缓存
                if (!PortCache.ContainsKey(host))
                {
                    PortCache.Add(host, result);
                }
            }

            return result;

        }
    }
}
