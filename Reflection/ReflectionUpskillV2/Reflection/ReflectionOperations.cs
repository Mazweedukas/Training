using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: CLSCompliant(true)]

namespace Reflection;

/// <summary>
/// Provides operations for working with reflection.
/// </summary>
public static class ReflectionOperations
{
    /// <summary>
    /// Gets the type name of the specified object.
    /// </summary>
    /// <param name="obj">The object to get type name from.</param>
    /// <returns>The type name of the object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string GetTypeName(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        Type type = obj.GetType();
        return type.Name;
    }

    /// <summary>
    /// Gets the full type name of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to get full name from.</typeparam>
    /// <returns>The full type name.</returns>
    public static string GetFullTypeName<T>()
    {
        Type type = typeof(T);
        return type.FullName!;
    }

    /// <summary>
    /// Gets the assembly-qualified name of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to get assembly-qualified name from.</typeparam>
    /// <returns>The assembly-qualified name.</returns>
    public static string GetAssemblyQualifiedName<T>()
    {
        Type type = typeof(T);
        return type.AssemblyQualifiedName!;
    }

    /// <summary>
    /// Gets all private instance fields of the specified object.
    /// </summary>
    /// <param name="obj">The object to get fields from.</param>
    /// <returns>An array of strings containing field names.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string[] GetPrivateInstanceFields(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(f => f.Name)
            .ToArray();
    }

    /// <summary>
    /// Gets all public static fields of the specified object.
    /// </summary>
    /// <param name="obj">The object to get fields from.</param>
    /// <returns>An array of strings containing field names.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string[] GetPublicStaticFields(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.Name)
            .ToArray();
    }

    /// <summary>
    /// Gets all full names of the interfaces implemented by the specified object.
    /// </summary>
    /// <param name="obj">The object to get interface details from.</param>
    /// <returns>An array of strings containing the full names of the interfaces implemented by the specified object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string?[] GetInterfaceDataDetails(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetInterfaces()
            .Select(f => f.FullName)
            .ToArray();
    }

    /// <summary>
    /// Gets all constructor details of the specified object.
    /// </summary>
    /// <param name="obj">The object to get constructor details from.</param>
    /// <returns>An array of strings containing constructor details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string?[] GetConstructorsDataDetails(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetConstructors()
            .Select(f => f.ToString())
            .ToArray();
    }

    /// <summary>
    /// Gets all type member details of the specified object.
    /// </summary>
    /// <param name="obj">The object to get member details from.</param>
    /// <returns>An array of strings containing member details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string?[] GetTypeMembersDataDetails(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetMembers()
            .Select(f => f.ToString())
            .ToArray();
    }

    /// <summary>
    /// Gets all method details of the specified object.
    /// </summary>
    /// <param name="obj">The object to get method details from.</param>
    /// <returns>An array of strings containing method details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string?[] GetMethodDataDetails(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Select(f => f.ToString())
            .ToArray();
    }

    /// <summary>
    /// Gets all property details of the specified object.
    /// </summary>
    /// <param name="obj">The object to get property details from.</param>
    /// <returns>An array of strings containing property details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    public static string?[] GetPropertiesDataDetails(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        return obj.GetType()
            .GetProperties()
            .Select(f => f.ToString())
            .ToArray();
    }
}
