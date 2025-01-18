using System.Text;

namespace LinkLobby.Helper
{
    public static class UuidHelper
    {
        public static string GenerateUuidV7()
        {
            // 获取当前时间戳（毫秒）
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // 生成 128 位随机数（16字节） 使用 Random
            byte[] randomBytes = new byte[16]; // 128 位 = 16 字节
            Random random = new Random();
            random.NextBytes(randomBytes);  // 填充 randomBytes 数组

            // 拼接时间戳和随机字节生成 UUIDv7（这里是一个简化的方案）
            byte[] uuidBytes = new byte[16];

            // 填充时间戳（8字节，64位）
            var timestampBytes = BitConverter.GetBytes(timestamp);
            Array.Copy(timestampBytes, 0, uuidBytes, 0, 8);

            // 填充随机字节（8字节）
            Array.Copy(randomBytes, 0, uuidBytes, 8, 8);

            // 转为十六进制字符串
            StringBuilder sb = new StringBuilder();
            foreach (var b in uuidBytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }
    }
}
