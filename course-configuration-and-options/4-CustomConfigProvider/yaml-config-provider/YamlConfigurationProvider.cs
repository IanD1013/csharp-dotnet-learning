using Microsoft.Extensions.Configuration;

namespace yaml_config_provider;

public sealed class YamlConfigurationProvider(
    YamlConfigurationSource source) : FileConfigurationProvider(source)
{
    public override void Load(Stream stream)
    {
        try
        {
            Data = YamlConfigurationFileParser.Parse(stream);
        }
        catch (Exception ex)
        {
            throw new FormatException("Unable to parse YAML.", ex);
        }
    }
}
