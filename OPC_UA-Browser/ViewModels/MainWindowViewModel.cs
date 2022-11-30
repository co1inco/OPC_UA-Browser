using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Opc.Ua.Export;
using OPC_UA_Browser.Models;
using OPC_UA_Browser.Services;

namespace OPC_UA_Browser.ViewModels;

[ObservableObject]
public partial class MainWindowViewModel
{
    private readonly UAConnectionService _uaConnection;

    public MainWindowViewModel()
    {
        _uaConnection = ((App)Application.Current).UaConnectionService;
        NodeBrowser = new NodeBrowserViewModel(_uaConnection);
        NodeSelector = new NodeSelectorViewModel(_uaConnection);


        NodeBrowser.SelectedNodeChangedEvent += NodeBrowserOnSelectedNodeChangedEvent;
        NodeSelector.SelectedNodeChanged += NodeSelectorOnSelectedNodeChanged;
    }
    

    public MainWindowViewModel(UAConnectionService uaConnection)
    {
        _uaConnection = uaConnection;
        NodeBrowser = new NodeBrowserViewModel(_uaConnection);
        NodeSelector = new NodeSelectorViewModel(_uaConnection);
    }



    public NodeBrowserViewModel NodeBrowser { get; }

    public NodeSelectorViewModel NodeSelector { get; }
    

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private NodeDetailsViewModel? _nodeDetails;


    private UaNodeModel? _selectedNode;
    public UaNodeModel? SelectedNode
    {
        get => _selectedNode;
        set
        {
            SetProperty(ref _selectedNode, value);

            if (value != null)
            {
                NodeDetails = new NodeDetailsViewModel(value, _uaConnection);
                NodeDetails.LoadValue();
            }
            else
            {
                NodeDetails = null;
            }
        }
    }


    private async void NodeBrowserOnSelectedNodeChangedEvent(object? sender, UaNodeModel uaNode)
    {
        if (!uaNode.IsBrowsed)
        {
            try
            {
                IsLoading = true;

                var result = await _uaConnection.BrowseNodeAsync(uaNode);

                uaNode.ChildNodes.Clear();
                foreach (var uaNodeModel in result)
                    uaNode.ChildNodes.Add(uaNodeModel);
                
                SelectedNode = uaNode;
            }
            finally
            {
                IsLoading = false;
            }
        }


        SelectedNode = uaNode;
        NodeSelector.Node = SelectedNode;
    }

    private async void NodeSelectorOnSelectedNodeChanged(object? sender, UaNodeModel e)
    {
        SelectedNode = e;
    }


}

