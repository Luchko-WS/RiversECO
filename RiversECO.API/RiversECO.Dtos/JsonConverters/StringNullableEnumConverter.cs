using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RiversECO.Dtos.JsonConverters
{
    /// <summary>
    /// JSON converter converts the string representation of enum to an enum value. Supports nullable enum type.
    /// </summary>
    /// <typeparam name="T">Enum type.</typeparam>
    public class StringNullableEnumConverter<T> : JsonConverter<T>
    {
        private readonly Type _underlyingType;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringNullableEnumConverter{T}"/> class.
        /// </summary>
        public StringNullableEnumConverter()
        {
            _underlyingType = Nullable.GetUnderlyingType(typeof(T));
        }

        /// <inheritdoc/>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        /// <inheritdoc/>
        public override T Read(ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string value = reader.GetString();
            if (string.IsNullOrEmpty(value)) return default;

            try
            {
                return (T)Enum.Parse(_underlyingType, value, true);
            }
            catch (ArgumentException ex)
            {
                throw new JsonException(ex.Message);
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer,
            T value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
