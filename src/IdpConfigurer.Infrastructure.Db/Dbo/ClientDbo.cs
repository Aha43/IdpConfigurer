using IdpConfigurer.Domain;
using IdpConfigurer.Domain.Param.Client;
using Newtonsoft.Json;

namespace IdpConfigurer.Infrastructure.Db.Dbo;

public class ClientDbo
{
    public required string ClientId { get; set; }
    public required string ClientName { get; set; }
    public required string IdpName { get; set;}
    public required string Json { get; set; }
}


public static class ClientDboExtensions
{
    public static ClientDbo ToDbo(this Client dto, string idpName)
    {
        return new ClientDbo
        {
            ClientId = dto.ClientId,
            ClientName = dto.ClientName,
            IdpName = idpName,
            Json = JsonConvert.SerializeObject(dto)
        };
    }

    public static ClientDbo ToDbo(this CreateClientParam param)
    {
        var client = new Client
        {
            ClientId = param.ClientId,
            ClientName = param.ClientName
        };

        return client.ToDbo(param.IdpName);
    }

    public static Client ToDto(this ClientDbo dbo)
    {
        var retVal = JsonConvert.DeserializeObject<Client>(dbo.Json);
        if (retVal == null) 
        {
            throw new ArgumentException("Parsing of json produced null");
        }

        if (!retVal.ClientId.Equals(dbo.ClientId))
        {
            throw new ArgumentException($"Expected client id : '{dbo.ClientId}' (from dbo) but found '{retVal.ClientId}' in json");
        }
        if (!retVal.ClientName.Equals(dbo.ClientName))
        {
            throw new ArgumentException($"Expected clientName : '{dbo.ClientName}' (from dbo) but found '{retVal.ClientName}' in json");
        }

        return retVal;
    }

}
