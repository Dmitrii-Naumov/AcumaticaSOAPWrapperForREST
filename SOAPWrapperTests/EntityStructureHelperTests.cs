using Acumatica.RESTClient.Model;

using SOAPLikeWrapperForREST;
using SOAPLikeWrapperForREST.Helpers;

namespace SOAPWrapperTests
{
    public class EntityStructureHelperTests
    {

        [InlineData(typeof(TestEntityWithoutDetails), 0)]
        [InlineData(typeof(TestEntityWithDetails), 2)]
        [InlineData(typeof(TestEntityWithMultiLevelDetails), 1)]
        [Theory]
        public void GetDetails_ReturnsTopLevelDetails(Type entityType, int expectedCount)
        {
            EntityStructureHelper.GetDetails(entityType)
                .Count().Should().Be(expectedCount);
        }
        [Fact]
        public void GetAllSubentitiesRecursive_ReturnsMultiLevelDetails()
        {
            EntityStructureHelper.GetAllSubEntitiesRecursive(typeof(TestEntityWithMultiLevelDetails))
                .Count().Should().Be(3);
        }

        [Fact]
        public void GetAllSubentitiesRecursive_ReturnsMultiLevelLinkedEntities()
        {
            EntityStructureHelper.GetAllSubEntitiesRecursive(typeof(TestEntityWithLinkedEntityAndMultiLevelDetails))
                .Count().Should().Be(5);
        }

        [Fact]
        public void GetAllSubentitiesRecursive_ProperlyComposesPath()
        {
            EntityStructureHelper.GetAllSubEntitiesRecursive(typeof(TestEntityWith3LevelsOfLinkedEntities))
                .Should().Contain(new[]
                {$"{nameof(TestEntityWith3LevelsOfLinkedEntities.TestLinkedEntity4)}/{nameof(TestEntityWithLinkedEntityAndMultiLevelDetails.TestLinkedEntity2)}/{nameof(TestEntityWithLinkedEntity.TestLinkedEntity1)}"});
        }

        [InlineData(typeof(TestEntityWith3LevelsOfLinkedEntities), 2)]
        [InlineData(typeof(TestEntityWithoutDetails), 1)]
        [Theory]
        public void GetFields_ReturnsTopLevleFields(Type entityType, int expectedCount)
        {
            EntityStructureHelper.GetFields(entityType)
                .Count().Should().Be(expectedCount);
        }

        [InlineData(typeof(TestEntityWith3LevelsOfLinkedEntities), 1)]
        [InlineData(typeof(TestEntityWithoutDetails), 0)]
        [Theory]
        public void GetLinkedEntities_ReturnsTopLevelEntities(Type entityType, int expectedCount)
        {
            EntityStructureHelper.GetLinkedEntities(entityType)
                .Count().Should().Be(expectedCount);
        }

        [Fact]
        public void GetFieldWithValues_ReturnsOnlyTopLevelFields()
        {
            var testEntity = new TestEntityWithLinkedEntityAndMultiLevelDetails()
            {
                TestField4 = new StringValue() { Value = "TestValue" },
                TestLinkedEntity3 = new TestEntityWithDetails()
                {
                    TestField2 = new StringValue("TestVal2")
                }

            };
            EntityStructureHelper.GetFieldsWithValues(testEntity)
                .Count().Should().Be(1);
        }

        [Fact]
        public void GetFieldWithValues_IgnoresSystemFields()
        {
            var testEntity = new TestEntityWithLinkedEntityAndMultiLevelDetails()
            {
                ID = new Guid(),
                Note = new StringValue() { Value = "TestNote" },
                TestField4 = new StringValue() { Value = "TestValue" }
            };
            EntityStructureHelper.GetFieldsWithValues(testEntity)
                .Should().ContainSingle();
        }

        [Fact]
        public void GetFieldWithValues_PreservesSearchType()
        {
            var testEntity = new TestEntityWithLinkedEntityAndMultiLevelDetails()
            {
                TestField4 = new StringSearch() { Value = "TestValue" }
            };
            EntityStructureHelper.GetFieldsWithValues(testEntity)
                .Should().ContainSingle(_=>_.Value is StringSearch);
        }
    }
}
