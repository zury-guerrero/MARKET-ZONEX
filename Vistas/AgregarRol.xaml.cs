using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MARKET_PLACE.Vistas
{
    /// <summary>
    /// Lógica de interacción para AgregarRol.xaml
    /// </summary>
    public partial class AgregarRol : Window
    {
        public AgregarRol()
        {
            InitializeComponent();
            GetRolTable();

        }
        RolServices services = new RolServices();
        private bool ValidarTexto(string texto)
        {
            // Patrón regex que acepta solo letras y no espacios
            string patron = @"^[A-Za-z]+$";

            return !string.IsNullOrWhiteSpace(texto) && Regex.IsMatch(texto, patron);
        }
        private void BtnAddRol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    Rol rol = new Rol();
                    rol.Nombre = txtRol.Text;
                   
                    if(txtPkRol.Text == "")
                    {
                        if (!ValidarTexto(rol.Nombre))
                        {
                        MessageBox.Show("Agregue datos al apratado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("¿Desea guardar los datos?", "Confirmación", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                services.AddRol(rol);

                                txtRol.Clear();

                                MessageBox.Show("Se agrego correctamente");
                                GetRolTable();
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtRol.Text))
                        {
                            MessageBox.Show("El apartado no puede estar vacíos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); ;
                        }
                        else
                        {
                            rol.PkRol = int.Parse(txtPkRol.Text);
                            rol.Nombre = txtRol.Text;
                            services.UpdateRol(rol);
                            MessageBox.Show("Se editó el Rol correctamente");
                            GetRolTable();
                        }
                    }
                    

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }
        private void DeletItem(object sender, RoutedEventArgs e)
        {
            Rol RolSeleccionado = UserTableR.SelectedItem as Rol;

            if (RolSeleccionado != null)
            {
                services.deleteRol(RolSeleccionado.PkRol);
                //MessageBox.Show("Se eliminó el usuario correctamente");
                GetRolTable();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún Rol para eliminar");
            }
        }
        public void GetRolTable()
        {
            UserTableR.ItemsSource = services.GetRols();
        }

        public void EditItem(object sender, RoutedEventArgs e)
        {
            Rol rol = new Rol();

            rol = (sender as FrameworkElement).DataContext as Rol;

            txtPkRol.Text = rol.PkRol.ToString();
            txtRol.Text = rol.Nombre.ToString();
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administrador administrador = new Administrador();
            administrador.Show();
            Close();
        }

        private void UserTableR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
