using SavegameToolkit;
using SavegameToolkit.Arrays;
using SavegameToolkit.Propertys;
using SavegameToolkit.Structs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentTribe
    {
        [DataMember] public long TribeId { get; set; } = 0;
        [DataMember] public string TribeName { get; set; } = "";
        [DataMember] public bool IsSolo { get; set; } = false;


        [DataMember] public ConcurrentBag<ContentPlayer> Players { get; set; } = new ConcurrentBag<ContentPlayer>();
        [DataMember] public ConcurrentBag<ContentStructure> Structures { get; set; } = new ConcurrentBag<ContentStructure>();
        [DataMember] public ConcurrentBag<ContentTamedCreature> Tames { get; set; } = new ConcurrentBag<ContentTamedCreature>();
        [DataMember] public string[] Logs { get; set; } = new string[0];

        public DateTime TribeFileDate { get; set; } = DateTime.MinValue;

        public DateTime? LastActive
        {
            get
            {
                var maxPlayer = Players.Max(p => p.LastActiveDateTime);
                return maxPlayer.GetValueOrDefault(DateTime.MinValue) > TribeFileDate? maxPlayer.Value : TribeFileDate;
            }
        }
        public bool HasGameFile {get;set;} = false;

        public ContentTribe(GameObject tribeObject)
        {
            PropertyStruct properties = (PropertyStruct)tribeObject.Properties[0];
            StructPropertyList propertyList = (StructPropertyList)properties.Value;
            
            TribeId = propertyList.GetPropertyValue<int>("TribeId");
            if (TribeId == 0) TribeId = propertyList.GetPropertyValue<int>("TribeID");
            TribeName = propertyList.GetPropertyValue<string>("TribeName");

            //logs
            if (propertyList.HasAnyProperty("TribeLog"))
            {
                IArkArray<string> tribeLogProp = (IArkArray<string>)propertyList.GetTypedProperty<PropertyArray>("TribeLog").Value;
                Logs = tribeLogProp.ToArray<string>();
            }
        }
        public ContentTribe()
        {

        }

        public override bool Equals(object obj)
        {
            return ((ContentTribe)obj).TribeId == TribeId;
        }
        public override int GetHashCode()
        {
            return TribeId.GetHashCode();
        }
    }
}
