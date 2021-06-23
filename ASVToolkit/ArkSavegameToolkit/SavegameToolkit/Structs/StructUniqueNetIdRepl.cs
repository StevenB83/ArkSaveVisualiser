﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SavegameToolkit.Structs {

    [JsonObject(MemberSerialization.OptIn)]
    public class StructUniqueNetIdRepl : StructBase {

        [JsonProperty(Order = 0)]
        public int Unk { get; private set; }

        [JsonProperty(Order = 1)]
        public string NetId { get; private set; }

        public override void Init(ArkArchive archive) {
            Unk = archive.ReadInt();
            NetId = archive.ReadString();
        }

        public override void Init(JObject node) {
            Unk = node.Value<int>("unk");
            NetId = node.Value<string>("netId");
        }

        public override void WriteJson(JsonTextWriter generator, WritingOptions writingOptions) {
            generator.WriteStartObject();

            generator.WriteField("unk", Unk);
            generator.WriteField("netId", NetId);

            generator.WriteEndObject();
        }

        public override void WriteBinary(ArkArchive archive) {
            archive.WriteInt(Unk);
            archive.WriteString(NetId);
        }

        public override int Size(NameSizeCalculator nameSizer) => sizeof(int) + ArkArchive.GetStringLength(NetId);
    }

}
