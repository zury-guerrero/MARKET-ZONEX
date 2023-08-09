using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Registro()
        {
            InitializeComponent();

        }
        UsuarioServices Servicios = new UsuarioServices();

        private bool ValidarTexto(string texto)
        {
            // Patrón regex que acepta solo letras y no espacios
            string patron = @"^[A-Za-z]+$";

            return !string.IsNullOrWhiteSpace(texto) && Regex.IsMatch(texto, patron);
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                using (var _Context = new ApplicationDbContext())
                {
                    Usuario usuario = new Usuario();

                    usuario.Nombre = TxtName.Text;
                    usuario.UserName = TxtUserName.Text;
                    usuario.Password = TxtPassword.Password;
                    //usuario.FkRol = int.Parse(SelectRol.SelectedValue.ToString());
                    if (!ValidarTexto(usuario.Nombre) || !ValidarTexto(usuario.UserName) || string.IsNullOrWhiteSpace(usuario.Password))
                    {
                        {
                            MessageBox.Show("Los apartados no pueden estar vacíos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); ;
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("¿Desea guardar los datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Servicios.AddUsuario(usuario);

                            TxtName.Clear();
                            TxtUserName.Clear();
                            TxtPassword.Clear();

                            MessageBox.Show("Se agrego correctamente", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);

                            Close();

                            MainWindow mw = new MainWindow();
                            mw.Show();
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }

        private void BtCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}