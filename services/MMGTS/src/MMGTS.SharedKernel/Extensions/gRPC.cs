using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Text.Json;

namespace MMGTS.SharedKernel.Utils
{
    public static class gRPC
    {
        public static Any ConvertToAnyTypeAsync<T>(this T data, JsonSerializerOptions options = null)
        {
            options ??= new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var any = new Any();
            if (data == null)
                return any;

            var bytes = JsonSerializer.SerializeToUtf8Bytes(data, options);
            any.Value = ByteString.CopyFrom(bytes);

            return any;
        }

        public static T ConvertFromAnyTypeAsync<T>(this Any any, JsonSerializerOptions options = null)
        {
            options ??= new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var utf8String = any.Value.ToStringUtf8();
            return JsonSerializer.Deserialize<T>(utf8String, options);
        }
    }
}
