using System.Reflection;

namespace IdeaHelper;

public static class Extensions
{
    public static PropertyInfo[] GetAllDeclaredProperties(this Type type)
    {
        var propertyInfos = new List<PropertyInfo>();
        var considered = new List<Type>();
        var queue = new Queue<Type>();

        considered.Add(type);
        queue.Enqueue(type);
        while (queue.Count > 0)
        {
            var subType = queue.Dequeue();

            if (subType.BaseType != null)
            {
                if (considered.Contains(subType.BaseType))
                {
                    continue;
                }

                considered.Add(subType.BaseType);
                queue.Enqueue(subType.BaseType);
            }

            var typeProperties = subType.GetProperties(
                BindingFlags.FlattenHierarchy |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.CreateInstance |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly);

            var newPropertyInfos = typeProperties
                .Where(x => !propertyInfos.Contains(x));

            propertyInfos.InsertRange(0, newPropertyInfos);
        }

        return propertyInfos.ToArray();
    }
}
