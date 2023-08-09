// Importaciones necesarias
using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MARKET_PLACE.Vistas
{
    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Window
    {
        // Inicialización de la ventana
        public Administrador()
        {
            InitializeComponent();
            GetUserTable();
            GetRoles();
        }

        // Crear una instancia del servicio de usuarios
        UsuarioServices services = new UsuarioServices();

        // Manejar clic en el botón "Agregar" o "Editar"
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();

            // Verificar si se está agregando un nuevo usuario o editando uno existente
            if (txtPkUser.Text == "")
            {
                // Verificar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(SelectRol.Text))
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); ;
                }
                else
                {
                    // Asignar valores al objeto usuario y agregarlo a la base de datos
                    usuario.Nombre = txtNombre.Text;
                    usuario.UserName = txtUserName.Text;
                    usuario.Password = txtPassword.Text;
                    usuario.FkRol = int.Parse(SelectRol.SelectedValue.ToString());

                    services.AddUsuario(usuario);

                    // Limpiar campos y mostrar mensaje de éxito
                    txtNombre.Clear();
                    txtUserName.Clear();
                    txtPassword.Clear();
                    MessageBox.Show("Usuario agregado correctamente");
                    GetUserTable();
                }
            }
            else
            {
                // Verificar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(SelectRol.Text))
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); ;
                }
                else
                {
                    // Asignar valores al objeto usuario y actualizar en la base de datos
                    usuario.PkUsuario = int.Parse(txtPkUser.Text);
                    usuario.Nombre = txtNombre.Text;
                    usuario.UserName = txtUserName.Text;
                    usuario.Password = txtPassword.Text;
                    usuario.FkRol = int.Parse(SelectRol.SelectedValue.ToString());

                    services.UpdateUser(usuario);

                    MessageBox.Show("Usuario editado correctamente");
                    GetUserTable();
                }
            }
        }

        // Manejar clic en el botón "Eliminar"
        private void DeletItem(object sender, RoutedEventArgs e)
        {
            Usuario usuarioSeleccionado = UserTable.SelectedItem as Usuario;

            if (usuarioSeleccionado != null)
            {
                // Eliminar usuario seleccionado y actualizar la tabla
                services.deleteUser(usuarioSeleccionado.PkUsuario);
                GetUserTable();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún usuario para eliminar");
            }
        }

        // Obtener y mostrar la lista de usuarios en la tabla
        public void GetUserTable()
        {
            UserTable.ItemsSource = services.GetUsuarios();
        }

        // Obtener y mostrar la lista de roles en el ComboBox
        public void GetRoles()
        {
            SelectRol.ItemsSource = services.GetRols();
            SelectRol.DisplayMemberPath = "Nombre";
            SelectRol.SelectedValuePath = "PkRol";
        }

        // Manejar clic en el botón "Editar"
        public void EditItem(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();

            // Obtener el usuario seleccionado y mostrar sus datos en los campos de edición
            usuario = (sender as FrameworkElement).DataContext as Usuario;
            txtPkUser.Text = usuario.PkUsuario.ToString();
            txtNombre.Text = usuario.Nombre.ToString();
            txtUserName.Text = usuario.UserName.ToString();
            txtPassword.Text = usuario.Password.ToString();
        }

        // Manejar clic en el botón "Volver al Inicio"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        // Manejar clic en el botón "Cerrar"
        private void BtCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Manejar clic en el botón "Agregar Rol"
        private void BtnAddRol_Click(object sender, RoutedEventArgs e)
        {
            AgregarRol agregarRol = new AgregarRol();
            agregarRol.Show();
            Close();
        }
    }
}
