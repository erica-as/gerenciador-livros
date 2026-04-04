using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GerenciadorLivros.API.Configuration;

public static class MongoSerializationConfig
{
    private static readonly Lazy<bool> _bootstrap = new(() =>
    {
        try
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
        catch (BsonSerializationException)
        {
            // Ja registrado por outro bootstrap no mesmo processo.
        }

        return true;
    });

    public static void ConfigureGuidRepresentation() => _ = _bootstrap.Value;
}