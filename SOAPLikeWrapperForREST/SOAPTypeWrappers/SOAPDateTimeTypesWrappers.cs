using Acumatica.RESTClient.Model;
using SOAPLikeWrapperForREST.SOAPTypeWrappers;

using System;

namespace SOAPLikeWrapperForREST
{
    public partial class DateTimeSearch : DateTimeValue
    {
        public DateTimeSearch(DateTime? value = null) : base(value)
        { }
        public DateTimeCondition Condition { get; set; }

        public DateTime? Value2 { get; set; }
    }
    public partial class DateTimeReturn : DateTimeValue
    { }
    public partial class DateTimeSkip : DateTimeValue, ISkipValueMarker
    { }

    public enum DateTimeCondition
    {
        Equal,
        NotEqual,
        IsBetween,
        IsGreaterThan,
        IsLessThan,
        IsGreaterThanOrEqualsTo,
        IsLessThanOrEqualsTo,
        Today,
        Overdue,
        Tomorrow,
        ThisWeek,
        ThisMonth,
        IsNull,
        IsNotNull,
    }

}
