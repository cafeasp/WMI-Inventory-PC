using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Inventory
{
    class NetworkAdapterConfiguration
    {
        public NetworkAdapterConfiguration()
        {
            DNSServerSearchOrder = new ArrayList();
        }

        public string _Index { get; set; }
        public string IPAddress { get; set; }
        public string IPSubnet { get; set; }
        public string DefaultIPGateway { get; set; }
        public ArrayList DNSServerSearchOrder { get; set; }
        public string MACAddress { get; set; }
        public string Description { get; set; }
        public string DHCPServer { get; set; }
        public string DNSHostName { get; set; }
        public string DHCPEnabled { get; set; }

    }
}
