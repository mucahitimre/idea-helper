using Type = IdeaHelper.TreeMaker.Models.Type;

namespace IdeaHelper.TreeMaker;

public class BuildTreeByPath
{
    public static List<TTree> Build<TTree, TFlat>(List<TFlat> flatList)
        where TTree : class, ITreeModel, INode<TTree>, new()
        where TFlat : class, IPathable, new()
    {
        var list = new List<TTree>();
        if (flatList == null || !flatList.Any())
        {
            return list;
        }

        foreach (var flat in flatList)
        {
            var parent = default(TTree);
            var pathSplited = GetPathSplited(flat);
            foreach (var key in pathSplited)
            {
                var node = GetNode(list, parent, key);
                if (node != null)
                {
                    parent = node;
                }
                else
                {
                    if (parent == null)
                    {
                        parent = CreateModel<TTree>(key, flat.Path);
                        list.Add(parent);
                    }
                    else
                    {
                        var data = CreateModel<TTree>(key, flat.Path);
                        parent.Childs.Add(data);
                        parent = data;
                    }
                }
            }
        }

        return list;
    }

    private static TModel GetNode<TModel>(List<TModel> list, TModel parent, string key) where TModel : class, ITreeModel, INode<TModel>, new()
    {
        return parent != null ? parent.Childs.FirstOrDefault(w => w.Key == key) : list.FirstOrDefault(w => w.Key == key);
    }

    private static TModel CreateModel<TModel>(string key, string path)
        where TModel : class, ITreeModel, INode<TModel>, new()
    {
        var model = (TModel)Activator.CreateInstance(typeof(TModel));
        model.Key = key;
        model.Path = path;
        model.Type = GetType(key);
        model.Childs = new List<TModel>();

        return model;
    }

    private static Type GetType(string key)
    {
        return key.Contains('.') ? Type.File : Type.Folder;
    }

    private static string[] GetPathSplited<TFlat>(TFlat code)
        where TFlat : class, IPathable, new()
    {
        return code.Path.Replace("~/", "").Split('/');
    }
}