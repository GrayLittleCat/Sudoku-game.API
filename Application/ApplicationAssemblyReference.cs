using System.Reflection;

namespace Application;

public sealed class ApplicationAssemblyReference
{
    private ApplicationAssemblyReference()
    {
    }

    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
