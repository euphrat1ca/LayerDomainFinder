using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.info
{
    public class DnsInfo
    {
        public string Domain { get; set; }
        public string DnsServer { get; set; }
        public int TimeOut { get; set; }

        public string Ports { get; set; }
    }
}
