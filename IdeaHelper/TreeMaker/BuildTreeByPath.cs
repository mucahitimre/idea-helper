namespace IdeaHelper.TreeMaker
{
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
                    var node = parent != null ? parent.Childs.FirstOrDefault(w => w.Key == key) : list.FirstOrDefault(w => w.Key == key);
                    if (node != null)
                    {
                        parent = node;
                    }
                    else
                    {
                        if (parent == null)
                        {
                            parent = CreateModel<TModel>();
                            parent.Key = key;
                            parent.Path = flat.Path;
                            list.Add(parent);
                        }
                        else
                        {
                            var data = CreateModel<TModel>();
                            data.Key = key;
                            data.Path = flat.Path;
                            parent.Childs.Add(data);
                            parent = data;
                        }
                    }
                }
            }

            return list;
        }

        private static TModel CreateModel<TModel>()
            where TModel : class, ITreeModel, INode<TModel>, new()
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            return (TModel)Activator.CreateInstance(typeof(TModel));
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        private static string[] GetPathSplited<TFlat>(TFlat code)
            where TFlat : class, IPathable, new()
        {
            return code.Path.Replace("~/", "").Split('/');
        }
    }
}
