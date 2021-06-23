using SavegameToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentItem
    {
        [DataMember] public string ClassName { get; set; } = "";
        [DataMember] public string CustomName { get; set; } = "";
        [DataMember] public string CraftedByPlayer { get; set; } = "";
        [DataMember] public string CraftedByTribe { get; set; } = "";
        [DataMember] public int Quantity { get; set; } = 1;
        [DataMember] public bool IsBlueprint { get; set; } = false;
        [DataMember] public bool IsEngram { get; set; } = false;

        public ContentItem(GameObject itemObject)
        {
            ClassName = itemObject.ClassString;
            CustomName = itemObject.GetPropertyValue<string>("CustomName");
            IsBlueprint = itemObject.GetPropertyValue<bool>("bIsBlueprint");
            IsEngram = itemObject.GetPropertyValue<bool>("bIsEngram");
            Quantity = itemObject.GetPropertyValue<int>("ItemQuantity",0,1);
            CraftedByTribe = itemObject.GetPropertyValue<string>("CrafterTribeName");
            CraftedByPlayer = itemObject.GetPropertyValue<string>("CrafterCharacterName");

        }

        public ContentItem()
        {

        }

    }
}
