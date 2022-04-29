using System;
using System.Runtime.Serialization;

namespace NetCore3WithReact.BusinessLogic.DataContracts
{
    [DataContract]
    public class TagData
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

    }
}
