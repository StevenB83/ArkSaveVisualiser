﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SavegameToolkit.Structs {

    public abstract class StructBase : IStruct {

        public bool IsNative => true;

        public virtual void CollectNames(NameCollector collector) { }

        public abstract void WriteJson(JsonTextWriter generator, WritingOptions writingOptions);
        //public virtual void WriteJson(JsonTextWriter generator) {
        //    JsonSerializer.CreateDefault().Serialize(generator, this);
        //}

        public abstract void WriteBinary(ArkArchive archive);
        public abstract int Size(NameSizeCalculator nameSizer);

        public abstract void Init(ArkArchive archive);
        public abstract void Init(JObject node);

    }

}
