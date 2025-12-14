using Microsoft.Extensions.Configuration;

namespace yaml_config_provider;

public sealed class YamlConfigurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);

        return new YamlConfigurationProvider(this);
    }
}
