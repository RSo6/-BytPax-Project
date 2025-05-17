using System.Text.Json;
using System.Text.Json.Serialization;
using BytPax.Models;
using BytPax.Models.core;

namespace BytPax.Services;

public class UserJsonConverter : JsonConverter<User>
{
    public override User Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;
        
        if (!root.TryGetProperty("Role", out var roleProp))
        {
            throw new JsonException("Відсутня роль користувача 'Role'");
        }

        var role = roleProp.GetInt32(); 

        // Десеріалізація в залежності від ролі
        if (role == 1)
        {
            return JsonSerializer.Deserialize<AdminUser>(root.GetRawText(), options);  
        }
        else if (role == 0)
        {
            return JsonSerializer.Deserialize<RegularUser>(root.GetRawText(), options);  
        }
        else
        {
            throw new JsonException($"Невідома роль: {role}");
        }
    }

    public override void Write(Utf8JsonWriter writer, User value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }  
}