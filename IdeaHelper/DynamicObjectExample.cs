using System.Dynamic;

namespace IdeaHelper
{
    // Reference: https://www.codeguru.com/csharp/using-dynamicobject-and-expandoobject/
    public class DynamicObjectExample : DynamicObject
    {
        private readonly Dictionary<string, object> _properties = new();

        // If Property is called, 'TryGetMember' is triggered.
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            var name = GetName(binder.Name);
            if (_properties.ContainsKey(name))
            {
                result = _properties[name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        // Common set method.
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[GetName(binder.Name)] = value;
            return true;
        }

        // If Method is called, 'TryInvokeMember' is triggered.
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object? result)
        {
            try
            {
                dynamic member = _properties[GetName(binder.Name)];
                result = member();
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        private static string GetName(string name)
        {
            return name?.ToLower();
        }
    }
}
