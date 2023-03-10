using Mono.Cecil;
using Mono.Cecil.Rocks;
using MstatReader.Lib;
using MstatReader.Lib.Extensions;

internal class Program
{
    public static void Main()
    {
        Reader reader = new(@"C:\repos\NativeAOTTests\obj\Release\net7.0\win-x64\native\NativeAOTTests.mstat");

        var types1 = reader.GetAllMethods().ToList();

        var d1 = types1.ExcludeSystemTypes().ToList();

        var d = types1.FirstOrDefault(x => x.MethodReference.FullName.Contains("AddNumbers"));

        Console.ReadLine();

        /*var asm = AssemblyDefinition.ReadAssembly(@"C:\repos\NativeAOTTests\obj\Release\net7.0\win-x64\native\NativeAOTTests.mstat");
        var globalType = (TypeDefinition)asm.MainModule.LookupToken(0x02000001);



        var types = globalType.Methods.First(x => x.Name == "Types");

        var s = types.Body.CodeSize;


        var typeStats = GetTypes(types).ToList();
        var typeSize = typeStats.Sum(x => x.Size);
        var typesByModules = typeStats.GroupBy(x => x.Type.Scope).Select(x => new { x.Key.Name, Sum = x.Sum(x => x.Size) }).ToList();
        var typesByModules1 = typeStats.GroupBy(x => x.Type.Scope).ToList();
        Console.WriteLine($"// ********** Types Total Size {typeSize:n0}");
        foreach (var m in typesByModules.OrderByDescending(x => x.Sum))
        {
            Console.WriteLine($"{m.Name,-40} {m.Sum,7:n0}");
        }
        Console.WriteLine($"// **********");

        Console.WriteLine();

        var methods = globalType.Methods.First(x => x.Name == "Methods");
        var methodStats = GetMethods(methods).ToList();
        var methodSize = methodStats.Sum(x => x.Size + x.GcInfoSize + x.EhInfoSize);
        var methodsByModules = methodStats.GroupBy(x => x.Method.DeclaringType.Scope).Select(x => new { x.Key.Name, Sum = x.Sum(x => x.Size + x.GcInfoSize + x.EhInfoSize) }).ToList();
        Console.WriteLine($"// ********** Methods Total Size {methodSize:n0}");
        foreach (var m in methodsByModules.OrderByDescending(x => x.Sum))
        {
            Console.WriteLine($"{m.Name,-40} {m.Sum,7:n0}");
        }
        Console.WriteLine($"// **********");

        Console.WriteLine();

        string FindNamespace(TypeReference type)
        {
            var current = type;
            while (true)
            {
                if (!String.IsNullOrEmpty(current.Namespace))
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

        var methodsByNamespace = methodStats.Select(x => new TypeStats { Type = x.Method.DeclaringType, Size = x.Size + x.GcInfoSize + x.EhInfoSize }).Concat(typeStats).GroupBy(x => FindNamespace(x.Type)).Select(x => new { x.Key, Sum = x.Sum(x => x.Size) }).ToList();
        Console.WriteLine($"// ********** Size By Namespace");
        foreach (var m in methodsByNamespace.OrderByDescending(x => x.Sum))
        {
            Console.WriteLine($"{m.Key,-40} {m.Sum,7:n0}");
        }
        Console.WriteLine($"// **********");

        Console.WriteLine();

        var blobs = globalType.Methods.First(x => x.Name == "Blobs");
        var blobStats = GetBlobs(blobs).ToList();
        var blobSize = blobStats.Sum(x => x.Size);
        Console.WriteLine($"// ********** Blobs Total Size {blobSize:n0}");
        foreach (var m in blobStats.OrderByDescending(x => x.Size))
        {
            Console.WriteLine($"{m.Name,-40} {m.Size,7:n0}");
        }
        Console.WriteLine($"// **********");


    }

    public static IEnumerable<TypeStats> GetTypes(MethodDefinition types)
    {
        types.Body.SimplifyMacros();
        var il = types.Body.Instructions;
        for (int i = 0; i + 2 < il.Count; i += 2)
        {
            var type = (TypeReference)il[i + 0].Operand;
            var size = (int)il[i + 1].Operand;
            yield return new TypeStats
            {
                Type = type,
                Size = size
            };
        }
    }

    public static IEnumerable<MethodStats> GetMethods(MethodDefinition methods)
    {
        methods.Body.SimplifyMacros();
        var il = methods.Body.Instructions;
        for (int i = 0; i + 4 < il.Count; i += 4)
        {
            var method = (MethodReference)il[i + 0].Operand;
            var size = (int)il[i + 1].Operand;
            var gcInfoSize = (int)il[i + 2].Operand;
            var ehInfoSize = (int)il[i + 3].Operand;
            yield return new MethodStats
            {
                Method = method,
                Size = size,
                GcInfoSize = gcInfoSize,
                EhInfoSize = ehInfoSize
            };
        }
    }

    public static IEnumerable<BlobStats> GetBlobs(MethodDefinition blobs)
    {
        blobs.Body.SimplifyMacros();
        var il = blobs.Body.Instructions;
        for (int i = 0; i + 2 < il.Count; i += 2)
        {
            var name = (string)il[i + 0].Operand;
            var size = (int)il[i + 1].Operand;
            yield return new BlobStats
            {
                Name = name,
                Size = size
            };
        }
    }*/
    }

    /*public class TypeStats
    {
        public TypeReference Type { get; set; }
        public int Size { get; set; }
    }

    public class MethodStats
    {
        public MethodReference Method { get; set; }
        public int Size { get; set; }
        public int GcInfoSize { get; set; }
        public int EhInfoSize { get; set; }
    }

    public class BlobStats
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }*/
}