using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.SOAPTypeWrappers;

using System;

namespace SOAPLikeWrapperForREST
{
    public enum GuidCondition
    {
        Equal,
        NotEqual,
        IsNull,
        IsNotNull,
    }

    public partial class GuidSearch : GuidValue
    {
        public GuidSearch(Guid? value = null) : base(value)
        { }

        public GuidCondition Condition { get; set; }

        public Guid Value2
        {
            get;
            set;
        }
    }
    public partial class GuidReturn : GuidValue
    {
    }
    public partial class GuidSkip : GuidValue, ISkipValueMarker
    {
    }
}
