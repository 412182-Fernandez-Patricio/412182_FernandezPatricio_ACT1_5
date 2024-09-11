using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Datos.Repositorios
{
    public interface ITipoPagoRepository
    {
        List<TipoPago> GetAll();
        TipoPago? GetById(int id);
        bool Save(TipoPago tipoPago);

    }
}
