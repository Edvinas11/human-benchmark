using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AimReactionAPI.Models;
using Microsoft.Extensions.Logging;

namespace AimReactionAPI.Services
{
    public class FileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<GameConfig> ParseJsonFileAsync(string filePath)
        {
            GameConfig gameConfig = null;

            try
            {
                using var reader = new StreamReader(filePath);
                string jsonContent = await reader.ReadToEndAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverterIgnoreCase<GameType>());

                gameConfig = JsonSerializer.Deserialize<GameConfig>(jsonContent, options);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while parsing JSON file at {FilePath}", filePath);
            }

            return gameConfig;
        }
    }

    // helps convert enum to string correctly
    public class JsonStringEnumConverterIgnoreCase<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var enumValue = reader.GetString();

            if (Enum.TryParse(enumValue, true, out T result))
            {
                return result;
            }

            throw new JsonException($"Unable to convert \"{enumValue}\" to Enum \"{typeof(T)}\".");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
