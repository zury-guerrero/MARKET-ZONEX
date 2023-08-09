using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MARKET_PLACE.Vistas
{
    /// <summary>
    /// Lógica de interacción para Carrito.xaml
    /// </summary>
    public partial class Carrito : UserControl
    {
        private readonly ProductoServices productoServices = new ProductoServices();
        private List<Producto> carrito = new List<Producto>();
        private List<Genero> generito = new List<Genero>();
        private double totalCompra = 0;


        public Carrito()
        {
            InitializeComponent();
            renderCarrito();
           /* calcularTotal()*/;
        }

        public static class CarritoController
        {
            public static List<Producto> carrito = new List<Producto>();

            public static void AgregarAlCarrito(Producto producto)
            {
                carrito.Add(producto);
            }

            public static List<Producto> GetCarrito()
            {
                return carrito;
            }

          
        }

        private void renderCarrito()
        {

            TablaCarrito.ItemsSource = null;
            List<Producto> carrito = CarritoController.GetCarrito();

            if (carrito != null && carrito.Count > 0)
            {
                TablaCarrito.ItemsSource = carrito;
                TablaCarrito.Items.Refresh(); // Actualizar la interfaz
            }

            calcularTotal();

        }
       
       
        private void BTNborrar(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            Producto? producto = button!.DataContext! as Producto;

            if (producto == null) return;

            if (producto != null && CarritoController.carrito.Contains(producto))
            {

                CarritoController.carrito.Remove(producto);

                renderCarrito();

            }
        }


        private void calcularTotal()
        {
            
            totalCompra = CarritoController.carrito.Sum(producto => producto.Precio);
            totalcarrito.Content = totalCompra.ToString("C");
            
        }


        private void Pagar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea Pagar el carrito?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                HashSet<Producto> hashSetProductos = new HashSet<Producto>(CarritoController.carrito);


                foreach (Producto producto in hashSetProductos)
                {
                    int cantidadApariciones = CarritoController.carrito.Count(p => p.PkProducto == producto.PkProducto);
                    Producto productoEnBaseDeDatos = productoServices.GetProductoPorID(producto.PkProducto);

                    if (productoEnBaseDeDatos.Cantidad < cantidadApariciones)
                    {
                        MessageBox.Show($"No hay suficiente cantidad disponible de {producto.Nombre}. Algunos productos ya no están disponibles.");
                        return; // Detener la compra
                    }

                    productoServices.DescontarCantidadProducto(producto.PkProducto, cantidadApariciones);

                }

                MessageBox.Show("Se pagó correctamente");

                CarritoController.carrito.Clear();


                renderCarrito();

                calcularTotal();

            }
        }
      


    }
}

