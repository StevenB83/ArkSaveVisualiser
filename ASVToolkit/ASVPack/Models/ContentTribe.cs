﻿using SavegameToolkit;
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
                List<DateTime> possibleDates = new List<DateTime>();

                
                var maxPlayer = Players.Max(p => p.LastActiveDateTime);
                if (maxPlayer != null && maxPlayer.HasValue) possibleDates.Add(maxPlayer.Value);


                var lastTameRange = Tames.Max(t => t.LastAllyInRangeTime);
                if (lastTameRange != null && lastTameRange.HasValue) possibleDates.Add(lastTameRange.Value);

                var lastStructureRange = Structures.Max(s => s.LastAllyInRangeTime);
                if (lastStructureRange != null && lastStructureRange.HasValue) possibleDates.Add(lastStructureRange.Value);

                if(possibleDates.Count > 0)
                {
                    //activity
                    return possibleDates.Max();
                }
                else
                {
                    //non player related last activity - dino deaths, structure decay etc.
                    return TribeFileDate;
                }


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
            var tribeLogs = propertyList.GetTypedProperty<PropertyArray>("TribeLog");
            if (tribeLogs!=null)
            {
                IArkArray<string> tribeLogProp = (IArkArray<string>)tribeLogs.Value;
                Logs = tribeLogProp.ToArray<string>();
            }
        }

        public ContentTribe()
        {

        }

        public override bool Equals(object obj)
        {
            if(obj is ContentTribe) return ((ContentTribe)obj).TribeId == TribeId;
            return false;
        }
        public override int GetHashCode()
        {
            return TribeId.GetHashCode();
        }
    }
}
