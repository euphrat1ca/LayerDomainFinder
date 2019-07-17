using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
        public static Dictionary<string, List<string>> PortCache = null;

        //缓存锁
        public static object locker = new object();

        /// <summary>
        /// 扫描端口
        /// </summary>
        /// <param name="host">主机IP</param>
        /// <param name="ports">要扫描的端口，以逗号分隔</param>
        /// <returns></returns>
        public static List<string> ScanPort(string host, string ports)
        {
            //先判断这个IP有没有扫描过端口，如果有扫描过就直接返回
            if (PortCache.ContainsKey(host))
            {
                return PortCache[host];
            }
            List<string> result = new List<string>();
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
                        result.Add(port);
                    }
                }
                catch
                {
                }
            }
            if (result.Count>0)
            {
                //加入缓存
                if (!PortCache.ContainsKey(host))
                {
                    lock (locker) {
                        PortCache.Add(host, result);
                    }
                    
                }
            }

            return result;

        }


        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }

        /// <summary>
        /// 获取web服务器信息
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static string[] CheckWeb(string url)
        {
            string[] result = new string[2] {"-", "连接超时" };
            HttpWebRequest httpWebRequest = null;

            try
            {
                //如果是https就直接忽略证书
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.Method = "HEAD";
                //10秒超时
                httpWebRequest.Timeout = 5000;
                result[0] = httpWebRequest.GetResponse().Headers["server"];
                result[1] = "正常访问";

            }
            catch (WebException ex)
            {
                try
                {
                    if (ex.Response.Headers["server"]!=null)
                    {
                        result[0] = ex.Response.Headers["server"];
                    }
                    else
                    {
                        result[0] = "-";
                    }
                    
                    result[1] = ex.Message.Replace("远程服务器返回错误: ", "").Replace("。", "");
                }
                catch (Exception)
                {
                    result[0] = "获取失败";
                    result[1] = "-";
                }
            }
            catch (Exception)
            {
                result[0] = "-";
                result[1] = "连接超时";
            }
            
            return result;

        }
    }
}
