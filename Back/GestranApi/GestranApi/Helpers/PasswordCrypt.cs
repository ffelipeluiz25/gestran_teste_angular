using System.Security.Cryptography;
using System.Text;
namespace GestranApi.Helpers
{
    public class PasswordCrypt
    {
        public static string GerarHashMd5(string textoParaCriptografar)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(textoParaCriptografar));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool VerificaPassword(string passwordRequest, string passwordBanco)
        {
            passwordRequest = GerarHashMd5(passwordRequest);
            return passwordRequest.Equals(passwordBanco);
        }

    }
}