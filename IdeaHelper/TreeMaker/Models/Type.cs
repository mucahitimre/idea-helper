using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IdeaHelper.TreeMaker.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum Type
{
    Folder = 1,

    File = 2
}
