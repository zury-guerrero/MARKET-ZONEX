using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using MARKET_PLACE.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MARKET_PLACE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState=WindowState.Minimized;
        }
        UsuarioServices Services = new UsuarioServices();

        private bool ValidarTexto(string texto)
        {
            // Patrón regex que acepta solo letras y no espacios
            string patron = @"^[A-Za-z]+$";

            return !string.IsNullOrWhiteSpace(texto) && Regex.IsMatch(texto, patron);
        }


        private void BtnLogin_Click_1(object sender, RoutedEventArgs e)
        {
                try
                {
                using (var _Context = new ApplicationDbContext())
                {
                    String UserName = TxtUserName.Text;
                    String Password = TxtPassword.Password;

                    var res = Services.Login(UserName, Password);
                    if (res != null)
                    {
                        if (res.Roles.Nombre == "Administrador")
                        {
                            Administrador Admin = new Administrador();
                            Close();
                            Admin.Show();
                        }
                        else
                        {
                            Productos pr = new Productos();
                            Close();
                            pr.Show();
                        }
                          
                    }
                    else if (string.IsNullOrWhiteSpace(TxtUserName.Text) || string.IsNullOrWhiteSpace(TxtPassword.Password))
                    {
                        MessageBox.Show("Los apartados no pueden estar vacíos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning); 
                    }
                    else
                    {
                        MessageBox.Show("No se encontró usuario con esos datos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); 
                    }
                }
                }

                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
        }

        private void BtnCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            Registro reg = new Registro();
            reg.Show();
        }
    }
    }
