using Microsoft.Extensions.Options;

namespace Veda.Api.Configurations;

public class JwtConfigurationSetup(IConfiguration configuration) : IConfigureOptions<JwtConfiguration>
{
    private const string SectionName = "Jwt";

    public void Configure(JwtConfiguration options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}