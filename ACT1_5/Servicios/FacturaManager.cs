using ACT1_5.Datos.Repositorios;
using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Servicios
{
    public class FacturaManager
    {
        private IFacturaRepository facturaRepository;

        public FacturaManager(IFacturaRepository facturaRepository)
        {
            this.facturaRepository = facturaRepository;
        }
        public Factura? GetFacturaById(int id)
        {
            return facturaRepository.GetById(id);
        }

        public List<Factura?>? GetFacturas() 
        {
            return facturaRepository.GetAll();
        }

        public bool SaveFactura(Factura factura)
        {
            return facturaRepository.Save(factura);
        }
    }
}
