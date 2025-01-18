using System.Text.RegularExpressions;

namespace LinkLobby.Helper
{
    public static class IdentifierHelper
    {
        public static bool Check(string identifier)
        {
            // 判断输入是否为 null 或空字符串
            if (string.IsNullOrEmpty(identifier)) return false;

            // 长度检查：应为 19 个字符
            if (identifier.Length != 19) return false;

            // 检查第 5, 11, 17 个字符是否为 '-'
            if (identifier[4] != '-' || identifier[9] != '-' || identifier[14] != '-') return false;

            // 检查其他部分是否由 4 个字母或数字组成
            for (int i = 0; i < identifier.Length; i++)
            {
                if ((i == 4 || i == 9 || i == 14)) continue;  // 跳过连字符
                if (!char.IsLetterOrDigit(identifier[i])) return false;
            }

            return true;
        }
    }
}
