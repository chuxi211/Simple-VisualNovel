using Assets.Scripts.Story;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Assets.Scripts.Story
{
    [System.Serializable]
    public class StoryNode
    {
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Type Type;
        public bool HasCharacter;
        [JsonIgnore]
        public Node Node;
        [JsonProperty("Node")]
        public Newtonsoft.Json.Linq.JObject NodeRaw;
    }
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum Type
    {
        Dialogue,
        Choice,
        ChapterTitle,
        Tip
    }
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum CommandType
    {
        ShowText,
        ShowChoices,
        ShowBG,
        PlayMusic,
        PlayVideo,
        ShowCharacter,
        HideCharacter,
    }
    [System.Serializable]
    public class StoryNodeList
    {
        public string Chapter;
        public string Title;
        public List<StoryNode> storyNodes;
    }
}
