using Mono.Cecil;
using Mono.Cecil.Rocks;
using MstatReader.Lib.Models;

namespace MstatReader.Lib;

public class Reader
{
    private readonly string? _filePath;
    private readonly AssemblyDefinition _assemblyDefinition;
    private TypeDefinition _assemblyTypeDefinition;
    private MethodDefinition _types;
    private MethodDefinition _methods;

    private int _typeBodySize;
    private int _methodBodySize;

    public Reader(string mstatFilePath)
    {
        ArgumentNullException.ThrowIfNull(mstatFilePath);
        File.Exists(mstatFilePath);

        _filePath = mstatFilePath;
        _assemblyDefinition = AssemblyDefinition.ReadAssembly(_filePath);

        CommonProcessing();
    }

    public Reader(Stream fileStream)
    {
        ArgumentNullException.ThrowIfNull(fileStream);

        fileStream.Position = 0;
        _assemblyDefinition = AssemblyDefinition.ReadAssembly(fileStream);

        CommonProcessing();
    }

    private void CommonProcessing()
    {
        _assemblyTypeDefinition = (TypeDefinition)_assemblyDefinition.MainModule.LookupToken(0x02000001);

        _types = _assemblyTypeDefinition.Methods.First(x => x.Name == "Types");
        _methods = _assemblyTypeDefinition.Methods.First(x => x.Name == "Methods");

        _typeBodySize = _types.Body.CodeSize;
        _methodBodySize = _methods.Body.CodeSize;
    }

    public IEnumerable<TypeInformation> GetAllTypes()
    {
        _types.Body.SimplifyMacros();

        var il = _types.Body.Instructions;

        for (int i = 0; i + 2 < il.Count; i += 2)
        {
            var type = (TypeReference)il[i + 0].Operand;
            var size = (int)il[i + 1].Operand;

            yield return new TypeInformation
            {
                TypeReference = type,
                Size = new() { SelfSize = size, ContributionToOverallSize = ((decimal)size / (decimal)_typeBodySize) * 100 },
            };
        }
    }

    public IEnumerable<MethodInformation> GetAllMethods()
    {
        _methods.Body.SimplifyMacros();

        var il = _methods.Body.Instructions;
        for (int i = 0; i + 4 < il.Count; i += 4)
        {
            var method = (MethodReference)il[i + 0].Operand;
            var size = (int)il[i + 1].Operand;
            //var gcInfoSize = (int)il[i + 2].Operand;
            //var ehInfoSize = (int)il[i + 3].Operand;
            yield return new MethodInformation
            {
                MethodReference = method,
                Size = new SizeInformation { SelfSize = size, ContributionToOverallSize =  ((decimal)size / (decimal)_methodBodySize) * 100},
            };
        }
    }
}
