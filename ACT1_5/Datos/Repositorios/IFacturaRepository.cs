using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Datos.Repositorios
{
    public interface IFacturaRepository
    {
        List<Factura?>? GetAll();
        Factura? GetById(int id);
        bool Save(Factura oFactura);
    }
}
