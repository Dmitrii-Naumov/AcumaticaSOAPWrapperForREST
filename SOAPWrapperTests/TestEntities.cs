using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acumatica.RESTClient.Model;

namespace SOAPWrapperTests
{
    class TestEntityWithoutDetails : Entity_v4
    {
        public StringValue TestField1 { get; set; }
    }
    class TestEntityWithDetails : Entity_v4
    {
        public StringValue TestField2 { get; set; }
        public List<TestEntityWithoutDetails> DetailEntity1 { get; set; }
        public TestEntityWithoutDetails[] DetailEntity2 { get; set; }
    }
    class TestEntityWithMultiLevelDetails : Entity_v4
    {
        public StringValue TestField3 { get; set; }
        public List<TestEntityWithDetails> DetailEntity4 { get; set; }
    }
    class TestEntityWithLinkedEntity : Entity_v4
    {
        public TestEntityWithoutDetails TestLinkedEntity1 { get; set; }
    }

    class TestEntityWithLinkedEntityAndMultiLevelDetails : Entity_v4
    {
        public StringValue TestField4 { get; set; }
        public TestEntityWithLinkedEntity TestLinkedEntity2 { get; set; }
        public TestEntityWithDetails TestLinkedEntity3 { get; set; }
    }


    class TestEntityWith3LevelsOfLinkedEntities : Entity_v4
    {
        public StringValue TestField5 { get; set; }
        public StringValue TestField6 { get; set; }
        public TestEntityWithLinkedEntityAndMultiLevelDetails TestLinkedEntity4 { get; set; }
    }
    internal class TestEntityWithIntField : Entity_v4
    {
        public IntValue TestField1 { get; set; }
    }
    internal class TestEntityWithIntFieldAndLinkedEntity : Entity_v4
    {
        public IntValue TestField2 { get; set; }
        public TestEntityWithIntField LinkedEntity { get; set; }
    }
}
