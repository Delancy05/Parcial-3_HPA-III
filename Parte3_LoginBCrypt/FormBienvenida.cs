using MySqlConnector;

namespace Parte3_LoginBCrypt;

// Pantalla que se muestra tras un acceso exitoso: indica el nombre del usuario
public class FormBienvenida : Form
{
    public FormBienvenida(string nombreCompleto, string usuario)
    {
        Text = "Acceso exitoso";
        ClientSize = new Size(520, 360);
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;

        var lblBienvenida = new Label
        {
            Text = $"✅ Acceso exitoso. Bienvenido(a), {nombreCompleto} ({usuario})",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Color.DarkGreen,
            AutoSize = false,
            Location = new Point(20, 15),
            Size = new Size(480, 50)
        };

        var lblHistorial = new Label
        {
            Text = "Historial de intentos de login (últimos 50):",
            AutoSize = true,
            Location = new Point(20, 75)
        };

        var grid = new DataGridView
        {
            Location = new Point(20, 100),
            Size = new Size(480, 200),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };

        try { grid.DataSource = Db.ObtenerIntentos(); }
        catch (MySqlException ex) { lblHistorial.Text = "No se pudo cargar el historial: " + ex.Message; }

        var btnCerrar = new Button
        {
            Text = "Cerrar sesión",
            Location = new Point(190, 312),
            Size = new Size(140, 32),
            DialogResult = DialogResult.OK
        };

        Controls.AddRange(new Control[] { lblBienvenida, lblHistorial, grid, btnCerrar });
        AcceptButton = btnCerrar;
    }
}
