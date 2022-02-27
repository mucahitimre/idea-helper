namespace IdeaHelper.TreeMaker;

public interface INode<TModel>
        where TModel : class, ITreeModel, new()
{
    public List<TModel> Childs { get; set; }
}