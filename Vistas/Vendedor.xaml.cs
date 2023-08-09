using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using MARKET_PLACE.Services;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MARKET_PLACE.Vistas
{
    /// <summary>
    /// Lógica de interacción para Vendedor.xaml
    /// </summary>
    public partial class Vendedor : UserControl
    {
        public Vendedor()
        {
            InitializeComponent();
            GetProductoTable();
            GetGeneros();
        }

        ProductoServices Services = new ProductoServices();
        private bool ValidarTexto(string texto)
        {
            // Patrón regex que acepta solo letras y no espacios
            string patron = @"^[A-Za-z]+$";

            return !string.IsNullOrWhiteSpace(texto) && Regex.IsMatch(texto, patron);
        }

        #region Agregar Producto
        private void BtnAddProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    if (string.IsNullOrWhiteSpace(txtProducto.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text) || string.IsNullOrWhiteSpace(txtCantidad.Text))
                    {
                        MessageBox.Show("Los apartados no pueden estar vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Producto producto;

                    if (string.IsNullOrEmpty(txtId.Text))
                    {
                        // Agregar un nuevo producto
                        producto = new Producto();
                    }
                    else
                    {
                        // Editar producto existente
                        int productoId = int.Parse(txtId.Text);
                        producto = _Context.Productos.FirstOrDefault(p => p.PkProducto == productoId);

                        if (producto == null)
                        {
                            MessageBox.Show("Producto no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    producto.Nombre = txtProducto.Text;
                    producto.Precio = double.Parse(txtPrecio.Text);
                    producto.Cantidad = int.Parse(txtCantidad.Text);

                    if (SelectGenero.SelectedValue != null)
                    {
                        producto.FkGenero = int.Parse(SelectGenero.SelectedValue.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un género", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Agregar código para guardar la imagen aquí
                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        producto.ImagenPath = selectedImagePath;
                    }

                    if (string.IsNullOrEmpty(txtId.Text))
                    {
                        // Agregar el nuevo producto a la base de datos
                        Services.AddProducto(producto);
                        MessageBox.Show("Se agregó el producto correctamente");
                    }
                    else
                    {
                        // Actualizar el producto existente en la base de datos
                        _Context.SaveChanges(); // Guardar cambios en la base de datos
                        MessageBox.Show("Se editó el producto correctamente");
                    }

                    // Limpiar campos y reiniciar ruta de la imagen
                    txtId.Clear();
                    txtProducto.Clear();
                    txtPrecio.Clear();
                    txtCantidad.Clear();
                    selectedImagePath = null;

                    GetProductoTable(); // Actualizar la tabla
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private string selectedImagePath = null;






        #endregion

        #region Eliminar Producto
        private void DeletItem(object sender, RoutedEventArgs e)
        {
            Producto ProductoSeleccionado = ProductoTable.SelectedItem as Producto;

            if (ProductoSeleccionado != null)
            {

                Services.deleteProducto(ProductoSeleccionado.PkProducto);
                //MessageBox.Show("Se eliminó el usuario correctamente");
                GetProductoTable();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún producto para eliminar");
            }
        }
        #endregion


        public void GetProductoTable()
        {
            ProductoTable.ItemsSource = Services.GetProducto();

        }

        public void GetGeneros()
        {
            SelectGenero.ItemsSource = Services.GetGeneros();
            SelectGenero.DisplayMemberPath = "NombreG";
            SelectGenero.SelectedValuePath = "PkGenero";
        }

        #region Editar Producto
        public void EditItem(object sender, RoutedEventArgs e)
        {
            Producto producto = (sender as FrameworkElement).DataContext as Producto;

            txtId.Text = producto.PkProducto.ToString();
            txtProducto.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString();
            txtCantidad.Text = producto.Cantidad.ToString();

            // También puedes cargar la imagen del producto en el Image control si existe una imagen guardada
            if (!string.IsNullOrEmpty(producto.ImagenPath))
            {
                imageControl.Source = new BitmapImage(new Uri(producto.ImagenPath));
            }
        }

        #endregion

        private void BtnAddGenero_Click(object sender, RoutedEventArgs e)
        {
            AgregarGenero genero = new AgregarGenero();
            genero.Show();

        }

        private void BtnAgregarImagen_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                imageControl.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }
    }
}