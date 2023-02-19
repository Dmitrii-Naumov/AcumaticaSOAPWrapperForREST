﻿using System;

namespace SOAPLikeWrapperForREST
{
    
    public class EntityField
    {
        public object Value;
        public Type Type;
        public string Name;
        public EntityField(Type type, object value, string name)
        {
            Value = value;
            Type = type;
            Name = name;
        }
    }
}
