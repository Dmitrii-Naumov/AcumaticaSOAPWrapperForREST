using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Acumatica.RESTClient.Model;

namespace SOAPLikeWrapperForREST
{
    public class DetailEntity
    {
        public IEnumerable<Entity> Details;
        public string Name;
        public Type Type;
        public DetailEntity(Type type, IEnumerable details, string name)
        {
            Details = details.Cast<Entity>();
            Name = name;
            Type = type;
        }
    }
}
