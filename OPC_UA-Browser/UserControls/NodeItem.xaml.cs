using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OPC_UA_Browser.Models;

namespace OPC_UA_Browser.UserControls
{
    /// <summary>
    /// Interaction logic for NodeItem.xaml
    /// </summary>
    public partial class NodeItem : UserControl
    {
        public NodeItem()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty NodeProperty = DependencyProperty.Register(
            nameof(Node), typeof(UaNodeModel), typeof(NodeItem), new PropertyMetadata(default(UaNodeModel)));

        public UaNodeModel Node
        {
            get { return (UaNodeModel)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }


    }
}
