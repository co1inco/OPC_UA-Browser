using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using OPC_UA_Browser.Services;
using OPCUaClient;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;

namespace OPC_UA_Browser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        public UAConnectionService UaConnectionService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            UaConnectionService = new UAConnectionService("opc.tcp://172.18.1.45:48030");

            base.OnStartup(e);
            
        }
    }
}
