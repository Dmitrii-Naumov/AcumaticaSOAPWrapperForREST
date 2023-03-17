using Acumatica.RESTClient.ContractBasedApi.Model;

namespace SOAPWrapperTests
{
    class TestEntityWitBoolField : Entity
    {
        public BooleanValue TestField8 { get; set; }
    }
    class TestEntityWithoutDetails : Entity
    {
        public StringValue TestField1 { get; set; }
    }
    class TestEntityWithDetails : Entity
    {
        public StringValue TestField2 { get; set; }
        public List<TestEntityWithoutDetails> DetailEntity1 { get; set; }
        public TestEntityWithoutDetails[] DetailEntity2 { get; set; }
    }
    class TestEntityWithMultiLevelDetails : Entity
    {
        public StringValue TestField3 { get; set; }
        public List<TestEntityWithDetails> DetailEntity4 { get; set; }
    }
    class TestEntityWithLinkedEntity : Entity
    {
        public TestEntityWithoutDetails TestLinkedEntity1 { get; set; }
    }

    class TestEntityWithLinkedEntityAndMultiLevelDetails : Entity
    {
        public StringValue TestField4 { get; set; }
        public TestEntityWithLinkedEntity TestLinkedEntity2 { get; set; }
        public TestEntityWithDetails TestLinkedEntity3 { get; set; }
    }


    class TestEntityWith3LevelsOfLinkedEntities : Entity
    {
        public StringValue TestField5 { get; set; }
        public StringValue TestField6 { get; set; }
        public TestEntityWithLinkedEntityAndMultiLevelDetails TestLinkedEntity4 { get; set; }
    }
    internal class TestEntityWithIntField : Entity
    {
        public IntValue TestField1 { get; set; }
    }
    internal class TestEntityWithIntFieldAndLinkedEntity : Entity
    {
        public IntValue TestField2 { get; set; }
        public TestEntityWithIntField LinkedEntity { get; set; }
    }
}
