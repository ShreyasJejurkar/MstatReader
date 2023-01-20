using Mono.Cecil;
using MstatReader.Lib.Models;

namespace MstatReader.Lib.Extensions;

public static class TypeInformationExtensions
{
    public static IEnumerable<TypeInformation> ExcludeSystemTypes(this IEnumerable<TypeInformation> typeList)
    {
        return typeList.Where(x => !x.TypeReference.Scope.Name.StartsWith("System")
        && !x.TypeReference.Namespace.StartsWith("System")
        && !x.TypeReference.Namespace.StartsWith("Microsoft")
        && x.TypeReference.FullName != "<Module>");
    }

    public static IEnumerable<MethodInformation> ExcludeSystemTypes(this IEnumerable<MethodInformation> methodList)
    {
        return methodList.Where(x => !x.MethodReference.DeclaringType.Scope.Name.StartsWith("System")
        && !x.MethodReference.DeclaringType.Namespace.StartsWith("System")
        && !x.MethodReference.DeclaringType.Namespace.StartsWith("Microsoft")
        && x.MethodReference.DeclaringType.FullName != "<Module>");
    }

    public static IEnumerable<IGrouping<string, TypeInformation>> GroupByNamespace(this IEnumerable<TypeInformation> types)
    {
        return types.GroupBy(x => FindNamespace(x.TypeReference!));
    }

    public static IEnumerable<IGrouping<string, MethodInformation>> GroupByNamespace(this IEnumerable<MethodInformation> types)
    {
        return types.GroupBy(x => FindNamespace(x.MethodReference.DeclaringType!));
    }

    private static string FindNamespace(TypeReference type)
    {
        var current = type;
        while (true)
        {
            if (!string.IsNullOrEmpty(current.Namespace))
            {
                return current.Namespace;
            }

            if (current.DeclaringType == null)
            {
                return current.Name;
            }

            current = current.DeclaringType;
        }
    }
}
