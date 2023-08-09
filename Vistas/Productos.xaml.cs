using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MARKET_PLACE.Vistas.Carrito;

namespace MARKET_PLACE.Vistas
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        private readonly ProductoServices productoServices = new ProductoServices();


        private List<Producto> carrito = new List<Producto>();
        Carrito carro = new Carrito();


        public Productos()
        {


            InitializeComponent();

            renderCarrito();


        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void PreviewMouseLefthButtonDownBG(object sender, MouseButtonEventArgs e)
        {
            BtnShowHide.IsChecked = false;
        }



        private void BtnVender_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Vendedor();
            ProductoTable.Visibility = Visibility.Hidden;
        }

        private void BtnMicuenta_Click(object sender, RoutedEventArgs e)
        {


            MainWindow m = new MainWindow();
            m.Show();

            Close();
            renderCarrito();
        }



        private void MenuP(object sender, RoutedEventArgs e)
        {
            DataContext = new();
            ProductoTable.Visibility = Visibility.Visible;
            renderCarrito();
        }

        private void BtnAyuda_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = new AyudaC();
            ProductoTable.Visibility = Visibility.Hidden;
        }


        private void BtnCarrito_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Carrito();
            ProductoTable.Visibility = Visibility.Hidden;
        }


        public void AgregarClick(object sender, RoutedEventArgs e)//funcion agregar
        {
            // Obtener el botón que disparó el evento
            Button button = sender as Button;
            Producto productoSeleccionado = button.DataContext as Producto;

            if (productoSeleccionado != null)
            {
                CarritoController.AgregarAlCarrito(productoSeleccionado);
                MessageBox.Show("Producto agregado al carrito");
            }
            renderCarrito();
        }

        public void agregarACarrito(Producto producto)
        {
            if (producto.Cantidad > 0)
            {

                // Configurar la propiedad RutaImagen antes de agregar el producto al carrito

                Producto productoEnBaseDeDatos = productoServices.GetProductoPorID(producto.PkProducto);

                producto.ImagenPath = productoEnBaseDeDatos.ImagenPath;


                carrito.Add(producto);

                renderCarrito();




            }
            else
            {
                MessageBox.Show("No hay suficiente cantidad disponible de este producto.");
            }
        }
        public void renderCarrito()
        {

            ProductoTable.ItemsSource = null;

            ProductoTable.ItemsSource = productoServices.GetProducto();

        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            renderCarrito();
            ProductoTable.Visibility = Visibility.Visible;

        }

        private void ProductoTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}