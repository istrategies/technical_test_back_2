using System;
using System.Text;

namespace SampleApp.Application.Contracts.Mappers
{
    // NOTA: Si se quisiese hacer inaccesible se podría sustituir el BASE64 por algún tipo de encriptacion
    internal static class MapperExtensions
    {
        public static string IdEncode(this Guid guid)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(guid.ToString());
            return Convert.ToBase64String(plainTextBytes);
        }

        public static Guid IdDecode(this string id)
        {
            var base64EncodedBytes = Convert.FromBase64String(id);
            return new Guid(Encoding.UTF8.GetString(base64EncodedBytes));
        }
    }
}
