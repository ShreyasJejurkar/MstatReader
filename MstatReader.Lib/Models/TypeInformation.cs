using Mono.Cecil;
using System.ComponentModel.DataAnnotations;

namespace MstatReader.Lib.Models;

public class TypeInformation
{
    public TypeReference? TypeReference { get; set; }
    public SizeInformation? Size { get; set; }
}

public class SizeInformation
{
    public Decimal SelfSize { get; set; }
    public Decimal ContributionToOverallSize { get; set; }
}

public class MethodInformation
{
    public MethodReference MethodReference { get; set; }
    public SizeInformation Size { get; set; }
}

