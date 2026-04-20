using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GnomeGame.Data
{
    public static class ProfileJsonSerializer
    {
        public static string ToJson(PlayerProfileData profile)
        {
            var serializer = new DataContractJsonSerializer(typeof(PlayerProfileData));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, profile);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static PlayerProfileData FromJson(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(PlayerProfileData));
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var stream = new MemoryStream(bytes))
            {
                return serializer.ReadObject(stream) as PlayerProfileData;
            }
        }
    }
}
