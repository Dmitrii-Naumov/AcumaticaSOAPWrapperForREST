using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.SOAPTypeWrappers;

namespace SOAPLikeWrapperForREST
{
    public partial class BooleanReturn : BooleanValue
    {
    }
    public partial class BooleanSkip : BooleanValue, ISkipValueMarker
    {
    }

    public enum BooleanCondition
    {
        Equal,
        NotEqual,
        IsNull,
        IsNotNull,
    }
    public partial class BooleanSearch : BooleanValue
    {
		public BooleanCondition Condition { get; set; }

		public bool? Value2 { get; set; }
	}

}
