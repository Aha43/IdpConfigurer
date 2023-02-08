using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Idp;
using Newtonsoft.Json;

namespace IdpConfigurer.Infrastructure.Db.Dbo;

public class IdpDbo
{
    public required string Name { get; init; }
    public required string Uri { get; init; }
    public required string Json { get; init; }
}

public static class IdpDboExtensions
{
    public static IdpDbo ToDbo(this Idp dto)
    {
        return new IdpDbo
        {
            Name = dto.Name,
            Uri = dto.Uri,
            Json = JsonConvert.SerializeObject(dto.CustomData)
        };
    }

    public static IdpDbo ToDbo(this CreateIdpParam param)
    {
        var idp = new Idp
        {
            Name = param.Name,
            Uri = param.Uri
        };

        return idp.ToDbo();
    }

    public static Idp ToDto(this IdpDbo dbo)
    {
        var cd = JsonConvert.DeserializeObject<IdpCustomData>(dbo.Json);
        if (cd == null) 
        {
            throw new ArgumentException("Deseralizing Idp custom data yield null");
        }

        return new Idp
        {
            Name = dbo.Name,
            Uri = dbo.Uri,
            CustomData = cd
        };
    }

}
