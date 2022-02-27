using Type = IdeaHelper.TreeMaker.Models.Type;

namespace IdeaHelper.TreeMaker;

public interface ITreeModel
{
    public string Key { get; set; }

    public string Path { get; set; }

    public Type Type { get; set; }
}
