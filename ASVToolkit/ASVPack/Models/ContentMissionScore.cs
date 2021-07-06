using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentMissionScore
    {
        [DataMember] public string FullTag { get; set; } = "";
        [DataMember] public string MissionTag { get; set; } = "";
        [DataMember] public decimal HighScore { get; set; } = 0;

        
    }
}
