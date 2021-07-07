﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    public class ASVFoundItem
    {
        public long TribeId { get; set; } = int.MinValue;
        public string TribeName { get; set; } = "[Abandoned]";
        public string ContainerName { get; set; } = "Structure";
        public string ClassName { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public bool IsBlueprint { get; set; } = false;
        public int Quantity { get; set; } = 0;
        public decimal Latitude { get; set; } = 0;
        public decimal Longitude { get; set; } = 0;
        public decimal X { get; set; } = 0;
        public decimal Y { get; set; } = 0;
        public decimal Z { get; set; } = 0;
        public string Quality { get; set; } = "";


    }
}
