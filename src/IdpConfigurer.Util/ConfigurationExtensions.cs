using Microsoft.Extensions.Configuration;

namespace IdpConfigurer.Util;

public static class ConfigurationExtensions
{
    public static T? GetSectionAs<T>(this IConfiguration configuration, string? section = null) where T : class
    {
        section = ResolveSection<T>(section);
        var sectionValue = configuration.GetSection(section);
        if (sectionValue == null) return null;
        return sectionValue.Get<T>();
    }

    public static T GetRequiredSectionAs<T>(this IConfiguration configuration, string? section = null) where T : class
    {
        var retVal = configuration.GetSectionAs<T>(section);
        if (retVal == null)
        {
            throw new ArgumentException($"section '{ResolveSection<T>(section)}' not found or can not be parsed to '{typeof(T).Name}'");
        }

        return retVal;
    }

    private static string ResolveSection<T>(string? section) where T : class
    {
        return string.IsNullOrWhiteSpace(section) ? typeof(T).Name : section.Trim();
    }

}
