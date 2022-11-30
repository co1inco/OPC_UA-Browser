using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OPC_UA_Browser.Models;
using OPC_UA_Browser.Services;

namespace OPC_UA_Browser.ViewModels;


[ObservableObject]
public partial class NodeDetailsViewModel
{

    private readonly UAConnectionService _uaConnection;

    public NodeDetailsViewModel(UaNodeModel node, UAConnectionService? uaConnection = null)
    {
        _uaConnection = uaConnection ?? ((App)Application.Current).UaConnectionService;
        Node = node;
    }


    public UaNodeModel Node { get; }


    [ObservableProperty] private bool _isLoading = false;
    
    [ObservableProperty] private UaNodeValueModel _nodeValue;

    [ObservableProperty] private bool _isError = false;

    [ObservableProperty] private string _errorMessage = "";


    [RelayCommand]
    public async Task LoadValue()
    {
        try
        {
            IsLoading = true;

            var result = await _uaConnection.ReadNodeValueAsync(Node);
            NodeValue = result;

            IsError = false;
        }
        catch (Exception ex)
        {
            if (Debugger.IsAttached)
            {

            }

            ErrorMessage = ex.Message;
            IsError = true;
        }
        finally
        {
            IsLoading = false;
        }
    }


}