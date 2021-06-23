﻿using SavegameToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentStructure
    {
        [DataMember] public string ClassName { get; set; } = "";
        [DataMember] public float? Latitude { get; set; } = 0;
        [DataMember] public float? Longitude { get; set; } = 0;
        [DataMember] public float X { get; set; } = 0;
        [DataMember] public float Y { get; set; } = 0;
        [DataMember] public float Z { get; set; } = 0;
        [DataMember] public ContentInventory Inventory { get; set; } = new ContentInventory();
        [DataMember] public long TargetingTeam { get; set; } = 0;

        [DataMember] public double CreatedTimeInGame { get; set; } = 0;
        public DateTime? CreatedDateTime { get; internal set; }

        public ContentStructure(GameObject structureObject)
        {
            ClassName = structureObject.ClassString;
            if (structureObject.Location != null)
            {
                X = structureObject.Location.X;
                Y = structureObject.Location.Y;
                Z = structureObject.Location.Z;
            }

            TargetingTeam = structureObject.GetPropertyValue<int>("TargetingTeam", 0, 0);
            CreatedTimeInGame = structureObject.GetPropertyValue<double>("OriginalCreationTime", 0, 0);

        }

        public ContentStructure()
        {

        }
    }
}
