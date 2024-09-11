using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Dominio
{
    public class Factura
    {
        private int nroFactura;
        private DateTime fecha;
        private TipoPago tipoPago;
        private List<DetalleFactura> detallesFacturas;
        private string cliente;

        public int NroFactura { get { return nroFactura; } }
        public DateTime Fecha { get { return fecha; } }
        public TipoPago TipoPago { get { return tipoPago; } set { tipoPago = value; } }
        //public List<DetalleFactura> DetallesFacturas { get; set; }
        public string Cliente { get { return cliente; } set { cliente = value; } }

        public List<DetalleFactura> GetDetallesFacturas()
        {
            return detallesFacturas;
        }
        public Factura() // OJO CON EL NULL
        {
            nroFactura = 0;
            fecha = DateTime.Now;
            tipoPago = new TipoPago();
            List<DetalleFactura> detalleFacturas = new List<DetalleFactura>();
            cliente = string.Empty;
        }
        public Factura(int nroFactura, DateTime fecha, TipoPago tipoPago, List<DetalleFactura> detallesFacturas, string cliente) // OJO CON EL NULL
        {
            this.nroFactura = nroFactura;
            this.fecha = fecha;
            this.tipoPago = tipoPago;
            this.detallesFacturas = detallesFacturas;
            this.cliente = cliente;
        }
        public Factura(TipoPago tipoPago, List<DetalleFactura> detallesFacturas, string cliente) // OJO CON EL NULL
        {
            nroFactura = 0;
            this.fecha = DateTime.Now;
            this.tipoPago = tipoPago;
            this.detallesFacturas = detallesFacturas;
            this.cliente = cliente;
        }
    }
}
