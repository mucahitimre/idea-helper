using Type = IdeaHelper.TreeMaker.Models.Type;

namespace IdeaHelper.TreeMaker;

public class BuildTreeByPath
{
    public static List<TModel> Build<TModel, TFlat>(List<TFlat> flatList)
        where TModel : class, ITreeModel, INode<TModel>, new()
        where TFlat : class, IPathable, new()
    {
        var list = new List<TModel>();
        if (flatList == null || !flatList.Any())
        {
            return list;
        }

        foreach (var flat in flatList)
        {
            var parent = default(TModel);
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
                        parent = CreateModel<TModel>(key, flat.Path);
                        list.Add(parent);
                    }
                    else
                    {
                        var data = CreateModel<TModel>(key, flat.Path);
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
        model.Childs = new List<TModel>();
        model.Key = key;
        model.Path = path;
        model.Type = GetType(key);

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