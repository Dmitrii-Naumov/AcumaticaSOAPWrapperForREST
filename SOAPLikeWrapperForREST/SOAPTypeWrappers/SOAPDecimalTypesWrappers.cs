using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.SOAPTypeWrappers;

namespace SOAPLikeWrapperForREST
{
    public partial class DecimalReturn : DecimalValue
    {
    }
    public partial class DecimalSkip : DecimalValue, ISkipValueMarker
    {
    }
    public partial class DecimalSearch : DecimalValue
    {
        public DecimalCondition Condition { get; set; }

        public decimal? Value2 { get; set; }
    }
    public enum DecimalCondition
    {
        Equal,
        NotEqual,
        IsBetween,
        IsGreaterThan,
        IsLessThan,
        IsGreaterThanOrEqualsTo,
        IsLessThanOrEqualsTo,
        IsNull,
        IsNotNull,
    }
}
