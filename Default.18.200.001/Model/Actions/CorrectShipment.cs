using Acumatica.RESTClient.Model;
using System.Runtime.Serialization;

namespace Acumatica.DefaultEndpoint_18_200_001.Model
{
    [DataContract]
    public partial class CorrectShipment : EntityAction<Shipment>
    {
        public CorrectShipment(Shipment entity) : base(entity)
        { }

        [DataMember(Name = "entity", EmitDefaultValue = false)]
        public Shipment Entity
        {
            get
            {
                return _Entity;
            }
            set
            {
                _Entity = value;
            }
        }
    }
}