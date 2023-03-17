using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.SOAPTypeWrappers;

namespace SOAPLikeWrapperForREST
{
    public enum LongCondition
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

    public partial class LongSearch : LongValue
    {
        public LongCondition Condition { get; set; }

        public long? Value2 { get; set; }
    }
    public partial class LongReturn : LongValue
    {
    }
    public partial class LongSkip : LongValue, ISkipValueMarker
    {
    }
}
