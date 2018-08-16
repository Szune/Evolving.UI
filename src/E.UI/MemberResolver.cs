using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using E.UI.Attributes;
using E.UI.Utilities;

namespace E.UI
{
    public class MemberResolver : IMemberResolver
    {
        private readonly Dictionary<Type, List<EMemberInfo>> _typeCache =
            new Dictionary<Type, List<EMemberInfo>>();

        private readonly Dictionary<EMemberInfo, bool> _isReadOnlyCache = new Dictionary<EMemberInfo, bool>();

        private static readonly BindingFlags _onlyPublicMembers =
            BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        private static readonly BindingFlags _includePrivateMembers =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        

        public IEnumerable<EMemberInfo> GetMembers(object objectToEdit)
        {
            var type = objectToEdit.GetType();
            if (_typeCache.ContainsKey(type))
                return _typeCache[type];

            var entireClass = type.GetCustomAttribute<UIClassAttribute>();
            if (entireClass != null)
            {
                var ignoredMembers = objectToEdit.GetMembersWithAttribute<UIIgnoreAttribute>();
                var collection = type
                    .GetProperties(entireClass.IncludePrivateMembers ? _includePrivateMembers : _onlyPublicMembers)
                    .Select(f => new EMemberInfo(f, type))
                    .Concat(type
                        .GetFields(entireClass.IncludePrivateMembers ? _includePrivateMembers : _onlyPublicMembers)
                        .Select(p => new EMemberInfo(p, type)))
                    .Except(ignoredMembers).ToList();
                _typeCache[type] = collection;
                return collection;
            }

            var editableMembers = objectToEdit.GetMembersWithAttribute<ShowInEditorAttribute>().ToList();
            if (!editableMembers.Any())
                throw new InvalidOperationException($"Type '{objectToEdit.GetType()}' does not apply '{nameof(ShowInEditorAttribute)}' to any member or '{nameof(UIClassAttribute)}' to entire class.");
            _typeCache[type] = editableMembers;
            return editableMembers;
        }

        public bool IsReadOnly(EMemberInfo member)
        {
            if (_isReadOnlyCache.ContainsKey(member))
                return _isReadOnlyCache[member];
            var isReadOnly = member.ParentType.GetCustomAttribute<ReadOnlyAttribute>() != null ||
                             member.GetCustomAttribute<ReadOnlyAttribute>() != null;
            _isReadOnlyCache[member] = isReadOnly;
            return isReadOnly;
        }

        public bool HasLabel(EMemberInfo member)
        {
            var isReadOnly = member.GetCustomAttribute<NoLabelAttribute>() == null;
            return isReadOnly;
        }
    }
}
