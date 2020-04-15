using Acumatica.RESTClient.Model;
using System.Runtime.Serialization;

namespace Acumatica.POS_17_200_001.Model
{
	[DataContract]
	public class MarkContactAsValidated : EntityAction<Contact>
	{
		public MarkContactAsValidated(Contact entity) : base(entity)
		{ }
		public MarkContactAsValidated() : base()
		{ }
	}
}
