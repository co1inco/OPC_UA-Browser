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

            
            //var nodes = await uaService.BrowseNodeAsync(UAConnectionService.RootNode);


            //var uaClient = new UaClient("TZIDC_Test", "opc.tcp://172.18.1.45:48030", true, true);

            //uaClient.Connect();



            //var sessionField = typeof(UaClient).GetField("Session", BindingFlags.NonPublic | BindingFlags.Instance);
            //var session = (Session)sessionField.GetValue(uaClient);

            //var result = session.Browse(
            //    null,
            //    null,
            //    ObjectIds.ObjectsFolder,
            //    0u,
            //    BrowseDirection.Forward,
            //    ReferenceTypeIds.HierarchicalReferences,
            //    true,
            //    (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
            //    out var cp,
            //    out var refs);


            //var result = uaClient.Tags("hart_fsk.22.5697.16410D0C2B.Online.ParameterSet.tag");
            //var result = uaClient.Tags("hart_fsk");

            //var client = uaClient.Devices(true);
            //var group = uaClient.Groups("Device");


        }
    }
}
