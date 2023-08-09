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
    /// Lógica de interacción para AgregarGenero.xaml
    /// </summary>
    public partial class AgregarGenero : Window //ventana genero
    {
        public AgregarGenero()
        {
            InitializeComponent();
            GetGeneroTable();
            
        }
        GeneroServices services = new GeneroServices();
        private bool ValidarTexto(string texto)
        {
            // Patrón regex que acepta solo letras y no espacios
            string patron = @"^[A-Za-z]+$";

            return !string.IsNullOrWhiteSpace(texto) && Regex.IsMatch(texto, patron);
        }

        private void BtnAddGenero_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var _Context = new ApplicationDbContext ()) //uso BD
                { 
                        Genero genero = new Genero (); //instancia de la clase genero
                        genero.NombreG = txtGenero.Text; //obtener nombre del G del cuadro de texxto
                        if (txtPkGenero.Text == "")
                        {
                            if (string.IsNullOrWhiteSpace(txtGenero.Text)) //si el campo de llave primaria esta vacio agregar genero
                            {
                                MessageBox.Show("Agregue datos al apratado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (!ValidarTexto(genero.NombreG))
                            {
                                MessageBox.Show("Dato invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBoxResult result = MessageBox.Show("¿Desea guardar los datos?", "Confirmación", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    services.AgregarGenero(genero); //llama funcion agregar genero
                                    txtGenero.Clear();

                                    MessageBox.Show("Se agrego correctamente");
                                    GetGeneroTable(); //act table G
                                }

                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(txtGenero.Text))
                            {
                                MessageBox.Show("El apartado no puede estar vacíos", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error); ;
                            }
                            else
                            {
                                genero.PkGenero = int.Parse(txtPkGenero.Text);
                                genero.NombreG = txtGenero.Text;
                                services.ActualizarGenero(genero); //llama funcion actualizar genero
                                MessageBox.Show("Se editó el Genero correctamente");
                                GetGeneroTable(); //actualizar la tabla de genero
                            }
                        }


                    }

                    
                
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }
        private void DeletItem(object sender, RoutedEventArgs e) //al hacer click eliminar un elemento
        {
            Genero GeneroSeleccionado = GeneroTable.SelectedItem as Genero; //obtener genero seleccionado de la tabla

            if (GeneroSeleccionado != null)
            {
                services.EliminarGenero(GeneroSeleccionado.PkGenero); 
                //MessageBox.Show("Se eliminó el Genero correctamente");
                GetGeneroTable(); //actualiza t g
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún genero para eliminar");
            }
        }
        public void GetGeneroTable()
        {
            GeneroTable.ItemsSource = services.ObtenerGeneros();
        }

        public void EditItem(object sender, RoutedEventArgs e) //editar un elemento
        {
            Genero genero = new Genero();

            genero = (sender as FrameworkElement).DataContext as Genero;

            txtPkGenero.Text = genero.PkGenero.ToString();
            txtGenero.Text = genero.NombreG.ToString();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Vendedor(); //crear instancia clase vendedor
            Close();
        }

        
        
    }
}
