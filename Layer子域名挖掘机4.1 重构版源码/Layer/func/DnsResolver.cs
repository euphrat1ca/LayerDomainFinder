using System;
using System.Collections.Generic;
using System.Diagnostics;
using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using System.Net;

namespace Layer.func
{
    class DnsResolver
    {
        public string Domain { get; set; }
        public List<string> Record { get; set; }
        public string DnsServer { get; set; }
        public int TimeOut { get; set; }
        public bool IsSuccess { get; private set; }
        public DnsResolver(string hostName, string dnsServer = "114.114.114.114", int timeOut = 2000)
        {
            Domain = hostName;
            DnsServer = dnsServer;
            TimeOut = timeOut;
            Record = new List<string>();
            
            Dig();
        }

        public void Dig()
        {
            try
            {
                //初始化DnsClient，第一个参数为DNS服务器的IP，第二个参数为超时时间
                DnsClient dnsClient = new DnsClient(IPAddress.Parse(DnsServer), TimeOut);

                //解析域名。将域名请求发送至DNS服务器解析，参数为需要解析的域名
                DnsMessage dnsMessage = dnsClient.Resolve(DomainName.Parse(Domain));
                //TimeSpan = s.Elapsed;
                //若返回结果为空，或者存在错误，则该请求失败。
                if (dnsMessage == null || (dnsMessage.ReturnCode != ReturnCode.NoError && dnsMessage.ReturnCode != ReturnCode.NxDomain))
                {
                    IsSuccess = false;
                    return;
                }
                //循环遍历返回结果，将返回的IPV4记录添加到结果集List中。
                if (dnsMessage != null)
                {
                    foreach (DnsRecordBase dnsRecord in dnsMessage.AnswerRecords)
                    {
                        ARecord AR = dnsRecord as ARecord;
                        if (AR != null)
                        {
                            IsSuccess = true;
                            Record.Add(AR.Address.ToString());
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
