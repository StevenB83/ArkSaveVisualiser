using SavegameToolkit;
using SavegameToolkit.Propertys;
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
        [DataMember] public string Quality { get; set; } = "";
        [DataMember] public float? Rating { get; set; } = null;

        public ContentItem(GameObject itemObject)
        {
            ClassName = itemObject.ClassString;
            CustomName = itemObject.GetPropertyValue<string>("CustomName");
            IsBlueprint = itemObject.GetPropertyValue<bool>("bIsBlueprint");
            IsEngram = itemObject.GetPropertyValue<bool>("bIsEngram");
            Quantity = itemObject.GetPropertyValue<int>("ItemQuantity",0,1);
            CraftedByTribe = itemObject.GetPropertyValue<string>("CrafterTribeName");
            CraftedByPlayer = itemObject.GetPropertyValue<string>("CrafterCharacterName");

            if (itemObject.HasAnyProperty("ItemRating"))
            {
                Rating = itemObject.GetTypedProperty<PropertyFloat>("ItemRating").Value;
                if (Rating.HasValue) Rating = (float)Math.Round(Rating.Value, 2);
            }


            if (itemObject.HasAnyProperty("ItemQualityIndex"))
            {
                var itemQual = (byte)itemObject.GetTypedProperty<PropertyByte>("ItemQualityIndex").Value.ByteValue;
                if (itemQual <= 1)
                {
                    Quality = "Primitive";
                }
                else if (itemQual > 1 && itemQual <= 1.25)
                {
                    Quality = "Ramshackle";
                }
                else if (itemQual > 1.25 && itemQual <= 2.5)
                {
                    Quality = "Apprentice";
                }
                else if (itemQual > 2.5 && itemQual <= 4.5)
                {
                    Quality = "Journeyman";
                }
                else if (itemQual > 4.5 && itemQual <= 7)
                {
                    Quality = "Mastercraft";
                }
                else if (itemQual > 7)
                {
                    Quality = "Ascendant";
                }
            }


        }

        public ContentItem()
        {

        }

    }
}
