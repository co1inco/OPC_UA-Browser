using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using OPC_UA_Browser.Models;
using OPC_UA_Browser.Services;

namespace OPC_UA_Browser.ViewModels;


[ObservableObject]
public partial class NodeSelectorViewModel
{

    private UAConnectionService _uaConnection;

    public NodeSelectorViewModel()
    {
        _uaConnection = ((App)Application.Current).UaConnectionService;
    }


    public NodeSelectorViewModel(UAConnectionService uaConnection)
    {
        _uaConnection = uaConnection;
    }



    public event EventHandler<UaNodeModel> SelectedNodeChanged;

    #region Properties

    private UaNodeModel? _node;
    public UaNodeModel? Node
    {
        get => _node;
        set
        {
            SetProperty(ref _node, value);
            ApplyFilter();
        }
    }


    private ObservableCollection<UaNodeModel> _visibleNodes;
    public ObservableCollection<UaNodeModel> VisibleNodes
    {
        get => _visibleNodes;
        private set => SetProperty(ref _visibleNodes, value);
    }


    private UaNodeModel? _selectedNode;
    public UaNodeModel? SelectedNode
    {
        get => _selectedNode;
        set
        {
            SetProperty(ref _selectedNode, value);
            SelectedNodeChanged?.Invoke(this, value);
        }
    }

    private string _filter = "";
    public string Filter
    {
        get => _filter;
        set
        {
            SetProperty(ref _filter, value);
            ApplyFilter();
        }
    }

    #endregion



    private void ApplyFilter()
    {
        if (Node == null)
            return;

        VisibleNodes = new ObservableCollection<UaNodeModel>(Node.ChildNodes.Where(x => x.DisplayName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase)));
    }




}