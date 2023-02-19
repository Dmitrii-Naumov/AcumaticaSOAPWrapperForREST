using Acumatica.RESTClient.Model;

using SOAPLikeWrapperForREST;
using SOAPLikeWrapperForREST.Helpers;

namespace SOAPWrapperTests
{
    public class FiltersHelperTests
    {
        [Fact]
        public void ComposeFilters_ComposesFilterForIntSearch()
        {
            var testEntity = new TestEntityWithIntField()
            {
                TestField1 = new IntSearch() { Value = 123 }
            };
            FiltersHelper.ComposeFilters(testEntity)
                .Should().Be($"{nameof(TestEntityWithIntField.TestField1)} eq 123");
        }
        [Fact]
        public void ComposeFilters_ComposesFilterForIntSearchInLinkedEntity()
        {
            var testEntity = new TestEntityWithIntFieldAndLinkedEntity()
            {
                LinkedEntity = new TestEntityWithIntField()
                {
                    TestField1 = new IntSearch() { Value = 123 }
                }
            };
            FiltersHelper.ComposeFilters(testEntity)
                .Should().Be($"{nameof(TestEntityWithIntFieldAndLinkedEntity.LinkedEntity)}/{nameof(TestEntityWithIntField.TestField1)} eq 123");
        }
        [Fact]
        public void ComposeFiltersForKeyFields_IgnoresSearchTypes()
        {
            var testEntity = new TestEntityWithIntField()
            {
                TestField1 = new IntSearch() { Value = 123 }
            };
            FiltersHelper.ComposeFiltersForPossibleKeyFieldsInternal(testEntity)
                .Should().BeEmpty();
        }
        [Fact]
        public void ComposeFiltersForKeyFields_FiltersByKeys()
        {
            var testEntity = new TestEntityWithIntField()
            {
                TestField1 = new IntValue() { Value = 123 }
            };
            FiltersHelper.ComposeFiltersForPossibleKeyFieldsInternal(testEntity)
                .Should().ContainSingle();
        }

        [Fact]
        public void GetSearchFieldsWithValues_FindsIntSearch()
        {
            var testEntity = new TestEntityWithIntField()
            {
                TestField1 = new IntSearch() { Value = 123 }
            };
            FiltersHelper.GetSearchFieldsWithValues<IntSearch>(testEntity)
                .Should().ContainSingle();
        }

        [Fact]
        public void GetSearchFieldsWithValues_FindsIntSearchInLinkedEntity()
        {
            var testEntity = new TestEntityWithIntFieldAndLinkedEntity()
            {
                LinkedEntity = new TestEntityWithIntField()
                {
                    TestField1 = new IntSearch() { Value = 123 }
                }
            };
            FiltersHelper.GetSearchFieldsWithValues<IntSearch>(testEntity)
                .Should().ContainSingle();

        }
    }
}
