using MySqlConnector;

namespace Parte3_LoginBCrypt;

public class FormRegistro : Form
{
    private readonly TextBox txtNombre = new();
    private readonly TextBox txtUsuario = new();
    private readonly TextBox txtPassword = new();
    private readonly TextBox txtConfirmar = new();

    public FormRegistro()
    {
        Text = "Registrar usuario";
        ClientSize = new Size(360, 360);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        var titulo = new Label
        {
            Text = "Nuevo usuario",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 15)
        };

        Controls.Add(titulo);
        AgregarCampo("Nombre completo:", txtNombre, 65, false);
        AgregarCampo("Usuario:", txtUsuario, 125, false);
        AgregarCampo("Contraseña:", txtPassword, 185, true);
        AgregarCampo("Confirmar contraseña:", txtConfirmar, 245, true);

        var btnGuardar = new Button { Text = "Guardar", Location = new Point(20, 310), Size = new Size(150, 32) };
        btnGuardar.Click += BtnGuardar_Click;

        var btnCancelar = new Button { Text = "Cancelar", Location = new Point(190, 310), Size = new Size(150, 32) };
        btnCancelar.Click += (s, e) => Close();

        Controls.Add(btnGuardar);
        Controls.Add(btnCancelar);
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
    }

    private void AgregarCampo(string etiqueta, TextBox caja, int y, bool oculto)
    {
        Controls.Add(new Label { Text = etiqueta, AutoSize = true, Location = new Point(20, y) });
        caja.SetBounds(20, y + 20, 320, 25);
        caja.UseSystemPasswordChar = oculto;
        Controls.Add(caja);
    }

    private void BtnGuardar_Click(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text.Trim();
        string usuario = txtUsuario.Text.Trim();
        string password = txtPassword.Text;

        if (nombre == "" || usuario == "" || password == "")
        {
            MessageBox.Show("Todos los campos son obligatorios.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (password != txtConfirmar.Text)
        {
            MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            string hash = Db.Registrar(nombre, usuario, password);
            MessageBox.Show("Usuario registrado.\n\nHash BCrypt guardado en la BD:\n" + hash,
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        catch (MySqlException ex) when (ex.Number == 1062) // usuario duplicado
        {
            MessageBox.Show("Ese nombre de usuario ya existe.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error de base de datos:\n" + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
