using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IdeaHelper.TreeMaker.Models;

public class DefaultTreeModel : ITreeModel, INode<DefaultTreeModel>
{
    public string Key { get; set; }

    public string Path { get; set; }

    public Type Type { get; set; }

    public List<DefaultTreeModel> Childs { get; set; }
}
