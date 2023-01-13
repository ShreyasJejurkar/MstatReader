using MstatReader.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
