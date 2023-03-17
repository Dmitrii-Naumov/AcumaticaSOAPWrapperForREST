using Acumatica.RESTClient.ContractBasedApi.Model;

using SOAPLikeWrapperForREST.Helpers;

namespace SOAPWrapperTests
{
    public class ExpandssHelperTests
    {
        [Fact]
        public void ComposeExpands_ComposesExpandsFor3Levels()
        {
            ExpandsHelper.ComposeExpands(new TestEntityWith3LevelsOfLinkedEntities() { ReturnBehavior = ReturnBehavior.All }).
                Should().ContainAll(new[]
                {
                    "TestLinkedEntity4/TestLinkedEntity3/DetailEntity2"
                });
        }
        [Fact]
        public void ComposeExpands_SkipsEmptyEntities()
        {
            ExpandsHelper.ComposeExpands(
                new TestEntityWith3LevelsOfLinkedEntities()
                {

                }).
                Should().BeEmpty();
        }
    }
}
