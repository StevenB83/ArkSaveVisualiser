﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SavegameToolkit.Structs {
    [JsonObject(MemberSerialization.OptIn)]
    public class StructQuat : StructBase {

        [JsonProperty(Order = 0)]
        public float X { get; private set; }
        [JsonProperty(Order = 1)]
        public float Y { get; private set; }
        [JsonProperty(Order = 2)]
        public float Z { get; private set; }
        [JsonProperty(Order = 3)]
        public float W { get; private set; }

        public override void Init(ArkArchive archive) {
            X = archive.ReadFloat();
            Y = archive.ReadFloat();
            Z = archive.ReadFloat();
            W = archive.ReadFloat();
        }

        public override void Init(JObject node) {
            X = node.Value<float>("x");
            Y = node.Value<float>("y");
            Z = node.Value<float>("z");
            W = node.Value<float>("w");
        }

        public override void WriteJson(JsonTextWriter generator, WritingOptions writingOptions) {
            generator.WriteStartObject();

            if (Math.Abs(X) > 0f) {
                generator.WriteField("x", X);
            }
            if (Math.Abs(Y) > 0f) {
                generator.WriteField("y", Y);
            }
            if (Math.Abs(Z) > 0f) {
                generator.WriteField("z", Z);
            }
            if (Math.Abs(W) > 0f) {
                generator.WriteField("w", W);
            }

            generator.WriteEndObject();
        }

        public bool ShouldSerializeX() => Math.Abs(X) > 0f;
        public bool ShouldSerializeY() => Math.Abs(Y) > 0f;
        public bool ShouldSerializeZ() => Math.Abs(Z) > 0f;
        public bool ShouldSerializeW() => Math.Abs(W) > 0f;

        public override void WriteBinary(ArkArchive archive) {
            archive.WriteFloat(X);
            archive.WriteFloat(Y);
            archive.WriteFloat(Z);
            archive.WriteFloat(W);
        }

        public override int Size(NameSizeCalculator nameSizer) => sizeof(float) * 4;
    }
}
