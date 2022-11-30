using System;
using Opc.Ua;

namespace OPC_UA_Browser.Models;

public class UaNodeValueModel
{

    public UaNodeValueModel(DataValue dataValue)
    {
        RawDataValue = dataValue;
    }

    public DataValue RawDataValue { get; private set; }

    public StatusCode Status => RawDataValue.StatusCode;

    public object Value => RawDataValue.Value;

    public Variant WrappedValue => RawDataValue.WrappedValue;

    public TypeInfo ValueType => RawDataValue.WrappedValue.TypeInfo;



}