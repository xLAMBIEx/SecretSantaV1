using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SecretSantaV1.Models
{
    [Serializable]
    public class SantaGroup
    {
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<Santa> Santas { get; set; }
    }
}
