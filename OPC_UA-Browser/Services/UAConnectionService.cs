using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Opc.Ua.Client;
using OPCUaClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Opc.Ua;
using OPC_UA_Browser.Models;
using System.Data.Common;

namespace OPC_UA_Browser.Services;


public interface IUAConnectionService
{

}


public class UAConnectionService
{

    private UaClient _uaClient;
    private Session _session;

    public static NodeId RootNode => ObjectIds.ObjectsFolder;


    public UAConnectionService(string serverUrl)
    {
        var uaClient = new UaClient("TZIDC_Test", serverUrl, true, true);

        uaClient.Connect();

        // I was to lazy to figure out how UaClient works but it creates all the certificate stuff and loads it easily
        var sessionField = typeof(UaClient).GetField("Session", BindingFlags.NonPublic | BindingFlags.Instance);
        _session = (Session)sessionField.GetValue(uaClient);
        
    }


    public ReferenceDescriptionCollection ReadChildNodes(NodeId node)
    {
        var result = _session.Browse(
            null,
            null,
            node,
            0u,
            BrowseDirection.Forward,
            ReferenceTypeIds.HierarchicalReferences,
            true,
            (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
            out var cp,
            out var refs);
        
        return refs;
    }
    
    public Task<IEnumerable<UaNodeModel>> BrowseNodeAsync(UaNodeModel node, CancellationToken cancellationToken = default)
    {
        var nodeId = ExpandedNodeId.ToNodeId(node.NodeId, null);
        return BrowseNodeAsync(nodeId, cancellationToken);
    }

    public async Task<IEnumerable<UaNodeModel>> BrowseNodeAsync(NodeId node, CancellationToken cancellationToken = default)
    {

        var description = new BrowseDescriptionCollection()
        {
            GetBrowseDescription(node)
        };

        try
        {

            var result = await _session.BrowseAsync(null, null, 10_000u, description, cancellationToken);

            if (!result.Results.Any())
                return new List<UaNodeModel>();

            if (result.Results[0].StatusCode != StatusCodes.Good)
                throw new Exception($"Result status code was {result.Results[0].StatusCode}");

            return result.Results[0].References.Select(x => new UaNodeModel(x));
        }
        catch (Exception ex)
        {

        }

        return new List<UaNodeModel>();
    }


    public async Task<IEnumerable<ReferenceDescriptionCollection>> BrowseNodesAsync(IEnumerable<NodeId> nodes, CancellationToken cancellationToken = default)
    {

        var description = new BrowseDescriptionCollection(
            nodes.Select(x => GetBrowseDescription(x))
            );
        
        var result = await _session.BrowseAsync(null, null, 0u, description, cancellationToken);

        if (!result.Results.Any())
            return null;

        var failedStatus = result.Results.FirstOrDefault(x => !StatusCode.IsGood(x.StatusCode));
        if (failedStatus != null)
            throw new Exception($"Result status code was {failedStatus.StatusCode}");
        

        return result.Results.Select(x => x.References);
    }


    public async Task<UaNodeValueModel> ReadNodeValueAsync(UaNodeModel node, CancellationToken cancellationToken = default)
    {

        var nodeId = ExpandedNodeId.ToNodeId(node.NodeId, null);
        var result = await _session.ReadValueAsync(nodeId, cancellationToken);
        return new UaNodeValueModel(result);
    }


    private BrowseDescription GetBrowseDescription(
        NodeId nodeToBrowse,
        BrowseDirection direction = BrowseDirection.Forward,
        NodeId refTypeId = null, 
        bool includeSubTypes = true, 
        uint nodeClassMask = (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method, 
        uint resultMask = 63u)
    {

        refTypeId = refTypeId ?? ReferenceTypeIds.HierarchicalReferences;
        return new BrowseDescription()
        {
            NodeId = nodeToBrowse,
            BrowseDirection = BrowseDirection.Forward,
            ReferenceTypeId = refTypeId,
            IncludeSubtypes = includeSubTypes,
            NodeClassMask = nodeClassMask,
            ResultMask = resultMask,
        };
    }


}