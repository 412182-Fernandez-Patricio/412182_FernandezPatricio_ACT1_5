using ACT1_5.Datos.Repositorios;
using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT1_5.Datos.Repositorios;

namespace ACT1_5.Servicios
{
    public class TipoPagoManager
    {
        private ITipoPagoRepository tipoPagoRepository;
        public TipoPagoManager(ITipoPagoRepository tipoPagoRepository)
        {
            this.tipoPagoRepository = tipoPagoRepository;
        }
        public TipoPago? GetTipoPagoById(int id)
        {
            return tipoPagoRepository.GetById(id);
        }

        public List<TipoPago> GetTiposPagos()
        {
            return tipoPagoRepository.GetAll();
        }

        public bool SaveTipoPago(TipoPago tipoPago)
        {
            return tipoPagoRepository.Save(tipoPago);
        }
    }
}
