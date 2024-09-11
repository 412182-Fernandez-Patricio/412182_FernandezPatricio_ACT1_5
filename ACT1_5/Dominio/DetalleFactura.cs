using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Dominio
{
    public class DetalleFactura
    {
        private int id;
        private Articulo articulo;
        private int cantidad;
        private float precioUnidad;

        public int Id { get{ return id; } }
        public Articulo Articulo { get { return articulo; } set { articulo = value; } }
        public int Cantidad { get { return cantidad; } set { cantidad = value; } }
        public float PrecioUnidad { get { return precioUnidad; } set { precioUnidad = value; } }

        public DetalleFactura()
        {
            id = 0;
            articulo = new Articulo();
            cantidad = 0;
            precioUnidad = 0;
        }
        public DetalleFactura(int id, Articulo articulo, int cantidad, float precioUnidad)
        {
            this.id = id;
            this.articulo = articulo;
            this.cantidad = cantidad;
            this.precioUnidad = precioUnidad;
        }
        public DetalleFactura(Articulo articulo, int cantidad, float precioUnidad)
        {
            id = 0;
            this.articulo = articulo;
            this.cantidad = cantidad;
            this.precioUnidad = precioUnidad;
        }
    }
}
