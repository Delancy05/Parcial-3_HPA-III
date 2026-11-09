using MySqlConnector;

namespace Parte3_LoginBCrypt;

public class FormLogin : Form
{
    private readonly TextBox txtUsuario = new();
    private readonly TextBox txtPassword = new();
    private readonly Label lblMensaje = new();

    public FormLogin()
    {
        Text = "Login con BCrypt";
        ClientSize = new Size(360, 270);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var titulo = new Label
        {
            Text = "Iniciar sesión",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 15)
        };

        var lblUsuario = new Label { Text = "Usuario:", AutoSize = true, Location = new Point(20, 65) };
        txtUsuario.SetBounds(20, 85, 320, 25);

        var lblPassword = new Label { Text = "Contraseña:", AutoSize = true, Location = new Point(20, 120) };
        txtPassword.SetBounds(20, 140, 320, 25);
        txtPassword.UseSystemPasswordChar = true;

        var btnIngresar = new Button { Text = "Ingresar", Location = new Point(20, 180), Size = new Size(150, 32) };
        btnIngresar.Click += BtnIngresar_Click;

        var btnRegistrar = new Button { Text = "Registrar usuario", Location = new Point(190, 180), Size = new Size(150, 32) };
        btnRegistrar.Click += BtnRegistrar_Click;

        lblMensaje.SetBounds(20, 225, 320, 30);
        lblMensaje.ForeColor = Color.Firebrick;

        Controls.AddRange(new Control[]
        {
            titulo, lblUsuario, txtUsuario, lblPassword, txtPassword, btnIngresar, btnRegistrar, lblMensaje
        });

        AcceptButton = btnIngresar; // Enter = Ingresar
    }

    private void BtnIngresar_Click(object sender, EventArgs e)
    {
        string usuario = txtUsuario.Text.Trim();
        string password = txtPassword.Text;

        if (usuario == "" || password == "")
        {
            lblMensaje.Text = "Escribe usuario y contraseña.";
            return;
        }

        try
        {
            if (Db.Login(usuario, password, out string nombreCompleto))
            {
                lblMensaje.Text = "";
                txtPassword.Clear();
                Hide();
                using (var bienvenida = new FormBienvenida(nombreCompleto, usuario))
                    bienvenida.ShowDialog();
                Show();
                txtUsuario.Focus();
            }
            else
            {
                lblMensaje.Text = "❌ Usuario o contraseña incorrectos.";
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Error de base de datos:\n" + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnRegistrar_Click(object sender, EventArgs e)
    {
        using var registro = new FormRegistro();
        registro.ShowDialog(this);
    }
}
