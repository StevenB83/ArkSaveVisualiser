using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ASVBreedingSearch
    {
        [DataMember] public string ClassName { get; set; } = "";
        [DataMember] public Tuple<int, int> RangeHp { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeStamina { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeMelee { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeWeight { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeSpeed { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeFood { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeOxygen { get; set; } = new Tuple<int, int>(0, 10);
        [DataMember] public Tuple<int, int> RangeCrafting { get; set; } = new Tuple<int, int>(0, 10);

        [DataMember] public List<int> ColoursRegion0 { get; set; } = new List<int>();
        [DataMember] public List<int> ColoursRegion1 { get; set; } = new List<int>();
        [DataMember] public List<int> ColoursRegion2 { get; set; } = new List<int>();
        [DataMember] public List<int> ColoursRegion3 { get; set; } = new List<int>();
        [DataMember] public List<int> ColoursRegion4 { get; set; } = new List<int>();
        [DataMember] public List<int> ColoursRegion5 { get; set; } = new List<int>();

    }
}
