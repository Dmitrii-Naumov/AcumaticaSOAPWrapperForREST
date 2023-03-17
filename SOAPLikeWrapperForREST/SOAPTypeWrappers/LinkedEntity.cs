using System;

using Acumatica.RESTClient.ContractBasedApi.Model;

namespace SOAPLikeWrapperForREST
{
    // TODO: Move to the SOAPWrapper project
    public class LinkedEntity
    {
        public Entity Value;
        public Type Type;
        public string Name;
        public LinkedEntity(Type type, Entity value, string name)
        {
            Value = value;
            Type = type;
            Name = name;
        }
    }
}
