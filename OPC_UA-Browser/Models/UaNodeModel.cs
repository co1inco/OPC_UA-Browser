using System;
using System.Collections.Generic;
using Opc.Ua;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Opc.Ua.Export;
using OPC_UA_Browser.Services;

namespace OPC_UA_Browser.Models;


[ObservableObject]
public partial class UaNodeModel
{
    private readonly ReferenceDescription _referenceDescription;

    public UaNodeModel(ReferenceDescription referenceDescription)
    {
        _referenceDescription = referenceDescription;
        ChildNodes = new ObservableCollection<UaNodeModel>();
    }



    public ReferenceDescription ReferenceDescription => _referenceDescription;

    public string DisplayName => _referenceDescription.DisplayName.ToString();

    public ExpandedNodeId NodeId => _referenceDescription.NodeId;


    private ObservableCollection<UaNodeModel> _childNodes;
    public ObservableCollection<UaNodeModel> ChildNodes
    {
        get => _childNodes;
        private set => SetProperty(ref _childNodes, value);
    }


    private bool _isBrowsed = false;
    public bool IsBrowsed
    {
        get => _isBrowsed;
        set => SetProperty(ref _isBrowsed, value);
    }

}