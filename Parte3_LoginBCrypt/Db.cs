using System.Data;
using MySqlConnector;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Parte3_LoginBCrypt;

// Acceso a datos + lógica de BCrypt
static class Db
{
    private const string ConnStr =
       "Server=localhost;Port=3306;Database=login_db;User ID=root;Password=Shupy2006;";

    // Registra un usuario guardando el HASH de BCrypt (incluye salt). Devuelve el hash generado.
    // Lanza MySqlException con Number 1062 si el usuario ya existe.
    public static string Registrar(string nombreCompleto, string usuario, string password)
    {
        string hash = BCryptNet.HashPassword(password);

        using var conn = new MySqlConnection(ConnStr);
        conn.Open();
        using var cmd = new MySqlCommand(
            "INSERT INTO usuarios (nombre_usuario, nombre_completo, password_hash) " +
            "VALUES (@u, @n, @h)", conn);
        cmd.Parameters.AddWithValue("@u", usuario);
        cmd.Parameters.AddWithValue("@n", nombreCompleto);
        cmd.Parameters.AddWithValue("@h", hash);
        cmd.ExecuteNonQuery();
        return hash;
    }

    // Verifica con BCrypt.Verify() y guarda el intento (exitoso o fallido)
    public static bool Login(string usuario, string password, out string nombreCompleto)
    {
        string hash = null;
        nombreCompleto = null;

        using var conn = new MySqlConnection(ConnStr);
        conn.Open();

        using (var cmd = new MySqlCommand(
            "SELECT password_hash, nombre_completo FROM usuarios WHERE nombre_usuario = @u", conn))
        {
            cmd.Parameters.AddWithValue("@u", usuario);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                hash = reader.GetString("password_hash");
                nombreCompleto = reader.GetString("nombre_completo");
            }
        }

        bool exitoso = hash != null && BCryptNet.Verify(password, hash);

        using (var log = new MySqlCommand(
            "INSERT INTO intentos_login (usuario, exitoso, fecha) VALUES (@u, @e, @f)", conn))
        {
            log.Parameters.AddWithValue("@u", usuario);
            log.Parameters.AddWithValue("@e", exitoso ? 1 : 0);
            log.Parameters.AddWithValue("@f", DateTime.Now);
            log.ExecuteNonQuery();
        }

        return exitoso;
    }

    // Últimos intentos para mostrarlos en la pantalla de bienvenida
    public static DataTable ObtenerIntentos()
    {
        using var conn = new MySqlConnection(ConnStr);
        conn.Open();
        using var da = new MySqlDataAdapter(
            "SELECT usuario AS Usuario, IF(exitoso = 1, 'Exitoso', 'Fallido') AS Resultado, " +
            "fecha AS Fecha FROM intentos_login ORDER BY fecha DESC LIMIT 50", conn);
        var dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}
