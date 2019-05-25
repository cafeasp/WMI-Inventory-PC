using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace Inventory
{
    class WMI
    {
        public WMI()
        {
            ComputerSystem = new List<ComputerSystem>();
            ComputerSystemProduct = new List<ComputerSystemProduct>();

            ComputerDrives = new DataTable();
            ComputerDrives.Columns.Add("Caption", typeof(string));
            ComputerDrives.Columns.Add("DeviceID", typeof(string));
            ComputerDrives.Columns.Add("MediaType", typeof(string));
            ComputerDrives.Columns.Add("Model", typeof(string));
            ComputerDrives.Columns.Add("SerialNumber", typeof(string));
            ComputerDrives.Columns.Add("Size", typeof(string));
            ComputerDrives.Columns.Add("Status", typeof(string));
            ComputerDrives.Columns.Add("Manufacturer", typeof(string));
            ComputerDrives.Columns.Add("InterfaceType", typeof(string));
            ComputerDrives.Columns.Add("Capabilities", typeof(string));

            ComputerNetworkAdapterConfig = new DataTable();
            ComputerNetworkAdapterConfig.Columns.Add("Index", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("IP", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("Subnet", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("Gateway", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("DNS", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("MAC", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("Desc", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("DHCP Server", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("Host_Name", typeof(string));
            ComputerNetworkAdapterConfig.Columns.Add("DHCP Enabled", typeof(string));

            ComputerPrinters = new DataTable();
            ComputerPrinters.Columns.Add("PrinterName", typeof(string));
            ComputerPrinters.Columns.Add("Port", typeof(string));

        }

        public string UserName { get; set; }
        public List<ComputerSystem> ComputerSystem { get; set; }
        public List<ComputerSystemProduct> ComputerSystemProduct { get; set; }
        public DataTable ComputerDrives { get; set; }
        public DataTable ComputerNetworkAdapterConfig { get; set; }
        public DataTable ComputerPrinters { get; set; }


        public void getComputerSystemValues()
        {

            try
            {
                
            
            ComputerSystem myComputer = new ComputerSystem();//computer system
            ComputerSystemProduct myComputerProduct = new ComputerSystemProduct();//computer serial
            DiskDrive myDrives = new DiskDrive();//drives object
            //NetworkAdapter myNetAdapters = new NetworkAdapter(); won't use it for now

            NetworkAdapterConfiguration adapterConfig = new NetworkAdapterConfiguration();//this is has better info

            Printer myPrinters = new Printer();

            #region ComputerSystem
            Console.WriteLine("Computer");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\root\\CIMV2", "select * from Win32_ComputerSystem");
            
            
            foreach (ManagementObject queryObj in searcher.Get())
            {
                Console.WriteLine("userName: {0}", queryObj["UserName"]);
                myComputer.UserName = queryObj["UserName"].ToString();
                myComputer.NumberOfProcessors = queryObj["NumberOfProcessors"].ToString();
                myComputer.NumberOfLogicalProcessors = queryObj["NumberOfLogicalProcessors"].ToString();
                myComputer.Description = queryObj["Description"].ToString();
                myComputer.DNSHostName = queryObj["DNSHostName"].ToString();
                myComputer.Manufacturer = queryObj["Manufacturer"].ToString();
                myComputer.Model = queryObj["Model"].ToString();
                myComputer.Name = queryObj["Name"].ToString();
                myComputer.TotalPhysicalMemory = queryObj["TotalPhysicalMemory"].ToString();
                ComputerSystem.Add(myComputer);
            }
            #endregion

            #region ComputerSystemProduct
            
            searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_ComputerSystemProduct");
            foreach (ManagementObject sysProduct in searcher.Get())
            {
                myComputerProduct.IndentifyingNumber = sysProduct["IdentifyingNumber"].ToString();
                ComputerSystemProduct.Add(myComputerProduct);
            }
            #endregion

            #region DiskDrive
            
            searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_DiskDrive");
            foreach (ManagementObject myDisk in searcher.Get())
            {
                Console.WriteLine(myDisk["Caption"].ToString());
                myDrives.Caption = myDisk["Caption"].ToString();
                Console.WriteLine(myDisk["DeviceID"].ToString());
                myDrives.DeviceID = myDisk["DeviceID"].ToString();
                Console.WriteLine(myDisk["MediaType"].ToString());
                myDrives.MediaType = myDisk["MediaType"].ToString();
                Console.WriteLine(myDisk["Model"].ToString());
                myDrives.Model = myDisk["Model"].ToString();
                if (myDisk["SerialNumber"] != null)
                {
                    Console.WriteLine(myDisk["SerialNumber"].ToString());
                    myDrives.SerialNumber = myDisk["SerialNumber"].ToString();
                }
                else
                    myDrives.SerialNumber = "n/a";

                if (myDisk["Size"] != null)
                {
                    Console.WriteLine(myDisk["Size"].ToString());
                    myDrives.Size = myDisk["Size"].ToString();
                }
                else
                    myDrives.Size = "n/a";

                if (myDisk["Status"] != null)
                {
                    Console.WriteLine(myDisk["Status"].ToString());
                    myDrives.Status = myDisk["Status"].ToString();
                }
                else
                    myDrives.Status = "n/a";

                if (myDisk["Manufacturer"] != null)
                {
                    Console.WriteLine(myDisk["Manufacturer"].ToString());
                    myDrives.Manufacturer = myDisk["Manufacturer"].ToString();
                }

                if (myDisk["InterfaceType"] != null)
                {
                    Console.WriteLine(myDisk["InterfaceType"].ToString());
                    myDrives.InterfaceType = myDisk["InterfaceType"].ToString();
                }

                if (myDisk["Capabilities"] != null)
                {
                    Console.WriteLine(myDisk["Capabilities"].ToString());
                    myDrives.Capabilities = myDisk["Capabilities"].ToString();
                }
                else
                    myDrives.Capabilities = "n/a";


                ComputerDrives.Rows.Add(myDrives.Caption, myDrives.DeviceID, myDrives.MediaType, myDrives.Model, myDrives.SerialNumber, myDrives.Size,myDrives.Status,myDrives.Manufacturer,myDrives.InterfaceType);

            }
            #endregion

            #region NetworkAdapters
            //for now I won't be using this class
            //searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_NetworkAdapter");

            //foreach (ManagementObject myNet in searcher.Get())
            //{
            //    if (myNet["DeviceID"] != null)
            //    {
            //        Console.WriteLine(myNet["DeviceID"].ToString());
            //        myNetAdapters.DeviceID = myNet["DeviceID"].ToString();
            //    }
            //    else
            //        myNetAdapters.DeviceID = "n/a";

            //    if (myNet["Name"] != null)
            //    {
            //        Console.WriteLine(myNet["Name"].ToString());
            //        myNetAdapters.Name = myNet["Name"].ToString();
            //    }
            //    else
            //        myNetAdapters.Name = "n/a";

            //    if (myNet["Description"] != null)
            //    {
            //        Console.WriteLine(myNet["Description"].ToString());
            //        myNetAdapters.Description = myNet["Description"].ToString();
            //    }
            //    else
            //        myNetAdapters.Description = "n/a";

            //    if (myNet["Manufacturer"] != null)
            //    {
            //        Console.WriteLine(myNet["Manufacturer"].ToString());
            //        myNetAdapters.Manufacturer = myNet["Manufacturer"].ToString();
            //    }
            //    else
            //        myNetAdapters.Manufacturer = "n/a";

            //    if (myNet["MACAddress"] != null)
            //    {
            //        Console.WriteLine(myNet["MACAddress"].ToString());
            //        myNetAdapters.MACAddress = myNet["MACAddress"].ToString();
            //    }
            //    myNetAdapters.MACAddress = "n/a";

            //    if (myNet["ProductName"] != null)
            //    {
            //        Console.WriteLine(myNet["ProductName"].ToString());
            //        myNetAdapters.ProductName = myNet["ProductName"].ToString();
            //    }
            //    else
            //        myNetAdapters.ProductName = "n/a";

            //    if (myNet["NetConnectionStatus"] != null)
            //    {
            //        Console.WriteLine(myNet["NetConnectionStatus"].ToString());
            //        myNetAdapters.NetConnectionStatus = myNet["NetConnectionStatus"].ToString();
            //    }
            //    else
            //        myNetAdapters.NetConnectionStatus = "n/a";

            //    ComputerNetworkAdapters.Rows.Add(myNetAdapters.DeviceID, myNetAdapters.Name, myNetAdapters.Description, myNetAdapters.Manufacturer, myNetAdapters.MACAddress, myNetAdapters.ProductName, myNetAdapters.NetConnectionStatus);



            //}

            #endregion

                #region NetAdapterConfiguration
                searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_NetworkAdapterConfiguration where IPEnabled = True");
                Console.WriteLine("----------------IP config---------------------");
                foreach (ManagementObject myNetConfig in searcher.Get())
                {

                    if (myNetConfig["Index"] != null)
                    {
                        Console.WriteLine("Index: " + myNetConfig["Index"].ToString());
                        adapterConfig._Index = myNetConfig["Index"].ToString();
                    }
                    else
                        adapterConfig._Index = "n/a";
                                       
                    if (myNetConfig["IPAddress"] != null)
                    {
                        String[] arrIPAddress = (String[])(myNetConfig["IPAddress"]);
                        Console.WriteLine("IP: "  + arrIPAddress[0]);
                        adapterConfig.IPAddress = arrIPAddress[0];
                        //foreach (String ip in arrIPAddress)
                        //{
                        //    Console.WriteLine(ip);
                        //}

                    }
                    else
                    {
                        adapterConfig.IPAddress = "n/a";
                    }

                    if (myNetConfig["IPSubnet"] != null)
                    {
                        String[] arrSubnets = (String[])(myNetConfig["IPSubnet"]);
                        Console.WriteLine("Subnet: " + arrSubnets[0]);
                        adapterConfig.IPSubnet = arrSubnets[0];
                    }
                    else
                        adapterConfig.IPSubnet = "n/a";

                    if (myNetConfig["DefaultIPGateway"] != null)
                    {
                        String[] arrGatewayIPAddress = (String[])(myNetConfig["DefaultIPGateway"]);
                        Console.WriteLine("Gateway: " + arrGatewayIPAddress[0]);
                        adapterConfig.DefaultIPGateway = arrGatewayIPAddress[0];
                        //foreach (String ipGateway in arrGatewayIPAddress)
                        //{
                        //    Console.WriteLine(ipGateway);
                        //}

                    }
                    else
                    {
                        adapterConfig.DefaultIPGateway = "n/a";
                    }

                    if (myNetConfig["DNSServerSearchOrder"] != null)
                    {
                        String[] arrDNSIPAddress = (String[])(myNetConfig["DNSServerSearchOrder"]);
                        Console.WriteLine("Number of dns: " + arrDNSIPAddress.Length.ToString());
                        //int numberOfDNS = 0;

                        foreach (String d in arrDNSIPAddress)
                        {
                            Console.WriteLine("DNS: " + d);
                            adapterConfig.DNSServerSearchOrder.Add(d);
                            
                        }

                       

                    }
                    else
                    {

                    }

                    if (myNetConfig["MACAddress"] != null)
                    {
                        Console.WriteLine("MAC: " + myNetConfig["MACAddress"].ToString());
                        adapterConfig.MACAddress = myNetConfig["MACAddress"].ToString();
                    }
                    else
                        adapterConfig.MACAddress = "n/a";

                    if (myNetConfig["Description"] != null)
                    {
                        Console.WriteLine("Description: " + myNetConfig["Description"].ToString());
                        adapterConfig.Description = myNetConfig["Description"].ToString();
                    }
                    else
                        adapterConfig.Description = "n/a";


                    if (myNetConfig["DHCPServer"] != null)
                    {
                        Console.WriteLine("DHCP Server: " + myNetConfig["DHCPServer"].ToString());
                        adapterConfig.DHCPServer = myNetConfig["DHCPServer"].ToString();
                    }
                    else
                        adapterConfig.DHCPServer = "n/a";

                    if (myNetConfig["DNSHostName"] != null)
                    {
                        Console.WriteLine("DNS Host Name: " + myNetConfig["DNSHostName"].ToString());
                        adapterConfig.DNSHostName = myNetConfig["DNSHostName"].ToString();
                    }
                    else
                        adapterConfig.DNSHostName = "n/a";


                    if (myNetConfig["DHCPEnabled"] != null)
                    {
                        Console.WriteLine("DHCP enabled: " + myNetConfig["DHCPEnabled"].ToString());
                        adapterConfig.DHCPEnabled = myNetConfig["DHCPEnabled"].ToString();
                    }
                    else
                        adapterConfig.DHCPEnabled = "n/a";

                    StringBuilder sb = new StringBuilder();

                    foreach (string dnsValue in adapterConfig.DNSServerSearchOrder)
                    {
                        sb.Append(dnsValue + " " );
                    }
                    ComputerNetworkAdapterConfig.Rows.Add(adapterConfig._Index, adapterConfig.IPAddress, adapterConfig.IPSubnet, adapterConfig.DefaultIPGateway, sb.ToString(),
                        adapterConfig.MACAddress, adapterConfig.Description, adapterConfig.DHCPServer, adapterConfig.DNSHostName,
                        adapterConfig.DHCPEnabled);

                    adapterConfig.DNSServerSearchOrder.Clear();
                   

                }

                #endregion

                 searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_Printer");
                Console.WriteLine("----------------Printers---------------------");
                foreach (ManagementObject myPrinter in searcher.Get())
                {
                    if (myPrinter["Name"] != null)
                    {
                        Console.WriteLine(myPrinter["Name"].ToString());
                        myPrinters.Name = myPrinter["Name"].ToString();
                    }
                    else
                        myPrinters.Name = "n/a";

                    if (myPrinter["PortName"] != null)
                    {
                        Console.WriteLine(myPrinter["PortName"].ToString());
                        myPrinters.PortName = myPrinter["PortName"].ToString();
                    }
                    else
                        myPrinters.PortName = "n/a";

                    ComputerPrinters.Rows.Add(myPrinters.Name, myPrinters.PortName);
                }


            }
            catch (ManagementException ex)
            {

                MessageBox.Show(ex.Message);
            }



        }

        
    }
}
