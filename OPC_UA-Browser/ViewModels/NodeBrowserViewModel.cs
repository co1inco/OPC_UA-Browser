using System;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Opc.Ua;
using OPC_UA_Browser.Models;
using OPC_UA_Browser.Services;
using Exception = System.Exception;

namespace OPC_UA_Browser.ViewModels;


[ObservableObject]
public partial class NodeBrowserViewModel
{

    private readonly UAConnectionService _uaConnection;



    public NodeBrowserViewModel()
    {
        _uaConnection = ((App)Application.Current).UaConnectionService;
    }

    public NodeBrowserViewModel(UAConnectionService uaConnection = null)
    {
        _uaConnection = uaConnection ?? ((App)Application.Current).UaConnectionService;
    }



    [ObservableProperty] private ObservableCollection<UaNodeModel>? _nodes;

    [ObservableProperty] private UaNodeModel? _selectedNode;

    [ObservableProperty] private bool _isLoading = false;

    public event EventHandler<UaNodeModel> SelectedNodeChangedEvent;



    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task Loaded()
    {

        var response = await _uaConnection.BrowseNodeAsync(UAConnectionService.RootNode);
        Nodes = new ObservableCollection<UaNodeModel>(response);

    }

    [RelayCommand]
    private void SelectionChanged(UaNodeModel uaNode)
    {
        SelectedNode = uaNode;
        SelectedNodeChangedEvent?.Invoke(this, uaNode);
    }


}

