using ACT1_5.Datos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT1_5.Dominio;
namespace ACT1_5.Servicios
{
    public class ArticuloManager
    {
        private IArticuloRepository articuloRepository;

        public ArticuloManager(IArticuloRepository articuloRepository)
        {
            this.articuloRepository = articuloRepository;
        }

        public Articulo? GetArticuloById(int id)
        {
            return articuloRepository.GetById(id);
        }
        public List<Articulo> GetArticulos()
        {
            return articuloRepository.GetAll();
        }

        public bool SaveArticulo(Articulo oArticulo)
        {
            return articuloRepository.Save(oArticulo);
        }
        public bool DeleteArticulo(int id)
        {
            return articuloRepository.Delete(id);
        }
    }
}
