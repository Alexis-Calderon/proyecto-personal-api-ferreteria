using System.Security.Cryptography;
using System.Text;

namespace ferreteriaJuanito;

// Clase encargada de cifrar las contraseñas con SHA256.
public class Encrypt
{
    public static string GetSHA256(string input)
    {
        if (input != null)
        {
            // Crear un hash de SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Retorna un arreglo de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Comvierte el arreglo de bytes en un string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        return null;
    }
}