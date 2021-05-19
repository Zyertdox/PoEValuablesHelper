using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PoEValuablesHelper.Poe.Model
{
    public class TabsResponse
    {
        [JsonPropertyName("numTabs")]
        public int NumTabs { get; set; }
        [JsonPropertyName("tabs")]
        public IList<Tab> Tabs { get; set; }
        [JsonPropertyName("items")]
        public IList<Item> Items { get; set; }
    }

    public class Tab
    {
        [JsonPropertyName("i")]
        public int Index { get; set; }
        [JsonPropertyName("n")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("baseType")]
        public string BaseType { get; set; }
        [JsonPropertyName("typeLine")]
        public string FullTypeName { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("identified")]
        public bool Identified { get; set; }
        [JsonPropertyName("ilvl")]
        public int ItemLevel { get; set; }
        [JsonPropertyName("frameType")]
        public int Rarity { get; set; }
        [JsonPropertyName("synthesised")]
        public bool Synthesised { get; set; }
        [JsonPropertyName("influences")]
        public IDictionary<string, bool> Influences { get; set; }

        [JsonPropertyName("h")]
        public int Height { get; set; }
        [JsonPropertyName("w")]
        public int Width { get; set; }
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
