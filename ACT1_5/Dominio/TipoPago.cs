using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Dominio
{
    public class TipoPago
    {
        private int id;
        private string descripcion;
        public int Id { get { return id; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }

        public TipoPago()
        {
            id = 0;
            descripcion = string.Empty;
        }
        public TipoPago(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
