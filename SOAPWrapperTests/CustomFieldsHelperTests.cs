using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.Helpers;

namespace SOAPWrapperTests
{
    public class CustomFieldsHelperTests
    {
        [Fact]
        public void GetCustomFields_FindsTopLevelCustomFields()
        {
            var testEntity = new TestEntityWithoutDetails()
            {
                CustomFields = new CustomField[]
                 {
                    new CustomDecimalField {viewName = "TestView", fieldName = "TestField"},
                 }
            };
            CustomFieldsHelper.ComposeCustomParameter(testEntity)
                .Should().Be("TestView.TestField");
        }
        [Fact]
        public void GetCustomFields_FindsDetailLevelCustomFields()
        {
            var testEntity = new TestEntityWithDetails()
            {
                DetailEntity1 = new List<TestEntityWithoutDetails>()
                {
                    new TestEntityWithoutDetails()
                    {
                         CustomFields = new CustomField[]
                         {
                            new CustomDecimalField {viewName = "TestView", fieldName = "TestField"},
                         }
                    }
                }
            };
            CustomFieldsHelper.ComposeCustomParameter(testEntity)
                .Should().Be($"{nameof(TestEntityWithDetails.DetailEntity1)}/TestView.TestField");
        }
        [Fact]
        public void GetCustomFields_MergesCustomFieldsFromDifferentLevels()
        {
            var testEntity = new TestEntityWithDetails()
            {
                CustomFields = new CustomField[]
                {
                    new CustomDecimalField {viewName = "TestView0", fieldName = "TestField0"},
                },
                DetailEntity1 = new List<TestEntityWithoutDetails>()
                {
                    new TestEntityWithoutDetails()
                    {
                         CustomFields = new CustomField[]
                         {
                            new CustomDecimalField {viewName = "TestView1", fieldName = "TestField1"},
                         }
                    },

                    new TestEntityWithoutDetails()
                    {
                         CustomFields = new CustomField[]
                         {
                            new CustomStringField {viewName = "TestView2", fieldName = "TestField2"},
                            new CustomIntField {viewName = "TestView3", fieldName = "TestField3"},
                         }
                    }
                },
                DetailEntity2 = new TestEntityWithoutDetails[]
                {
                    new TestEntityWithoutDetails()
                    {
                         CustomFields = new CustomField[]
                         {
                            new CustomDecimalField {viewName = "TestView4", fieldName = "TestField4"},
                         }
                    }
                }
            };
            CustomFieldsHelper.ComposeCustomParameter(testEntity)
                .Should().ContainAll(new[]
                {
                    $"TestView0.TestField0",
                    $"{nameof(TestEntityWithDetails.DetailEntity1)}/TestView1.TestField1",
                    $"{nameof(TestEntityWithDetails.DetailEntity1)}/TestView2.TestField2",
                    $"{nameof(TestEntityWithDetails.DetailEntity1)}/TestView3.TestField3",
                    $"{nameof(TestEntityWithDetails.DetailEntity2)}/TestView4.TestField4",
                });
        }
    }
}
