using SavegameToolkit;
using SavegameToolkit.Propertys;
using SavegameToolkit.Structs;
using SavegameToolkit.Types;
using SavegameToolkitAdditions;
using System;
using System.Runtime.Serialization;

namespace ASVPack.Models
{
    [DataContract]
    public class ContentPlayer
    {
        [DataMember] public long Id { get; set; } = 0;
        [DataMember] public string CharacterName { get; set; } = "";
        [DataMember] public string Name { get; set; } = "";
        [DataMember] public string Gender { get; set; } = "Male";
        [DataMember] public string SteamId { get; set; } = "";
        [DataMember] public float? Latitude { get; set; } = null;
        [DataMember] public float? Longitude { get; set; } = null;
        [DataMember] public float? X { get; set; } = null;
        [DataMember] public float? Y { get; set; } = null;
        [DataMember] public float? Z { get; set; } = null;
        [DataMember] public ContentInventory Inventory { get; set; } = new ContentInventory();
        [DataMember] public int Level { get; set; } = 0;
        [DataMember] public byte[] Stats { get; set; } = new byte[0];
        [DataMember] public double LastTimeInGame { get; set; } = 0;
        [DataMember] public DateTime? LastActiveDateTime { get; set; } = null;
        [DataMember] public int TargetingTeam { get; set; } = int.MinValue; //abandoned

        public bool HasGameFile { get; set; } = false;

        public bool IsSpawned()
        {
            return X.HasValue;
        }
        public ContentPlayer()
        {

        }
        public ContentPlayer(GameObject playerComponent, GameObject statusComponent)
        {


            //get data
            Id = playerComponent.HasAnyProperty("PlayerDataID")?playerComponent.GetPropertyValue<long>("PlayerDataID"):playerComponent.GetPropertyValue<long>("LinkedPlayerDataID");
            TargetingTeam = playerComponent.GetPropertyValue<int>("TargetingTeam");
            SteamId = ((StructUniqueNetIdRepl)playerComponent.GetTypedProperty<PropertyStruct>("PlatformProfileID").Value).NetId;
            Stats = new byte[12];
            if(statusComponent!=null)
                for (var i = 0; i < Stats.Length; i++) Stats[i] = statusComponent.GetPropertyValue<ArkByteValue>("NumberOfLevelUpPointsApplied", i)?.ByteValue ?? 0;

            LastTimeInGame = playerComponent.GetPropertyValue<double>("SavedLastTimeHadController");
            Name = playerComponent.GetPropertyValue<string>("PlatformProfileName");
            CharacterName = playerComponent.GetPropertyValue<string>("PlayerName");
            Level = getFullLevel(statusComponent);
            if (playerComponent.Location != null)
            {
                X = playerComponent.Location?.X;
                Y = playerComponent.Location?.Y;
                Z = playerComponent.Location?.Z;
            }
            Gender = playerComponent.GetPropertyValue<bool>("bIsFemale", 0,false)?"Female": "Male";



        }

        public override bool Equals(object obj)
        {
            return ((ContentPlayer)obj).Id == Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private int getFullLevel(GameObject statusComponent)
        {
            if (statusComponent == null)
            {
                return 1;
            }

            int baseLevel = statusComponent.GetPropertyValue<int>("BaseCharacterLevel", defaultValue: 1);
            short extraLevel = statusComponent.GetPropertyValue<short>("ExtraCharacterLevel");
            return baseLevel + extraLevel;
        }

    }
}
