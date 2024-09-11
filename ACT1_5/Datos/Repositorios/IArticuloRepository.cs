using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT1_5.Dominio;

namespace ACT1_5.Datos.Repositorios
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo? GetById(int id);
        bool Save(Articulo articulo);
        bool Delete(int id);

    }
}
