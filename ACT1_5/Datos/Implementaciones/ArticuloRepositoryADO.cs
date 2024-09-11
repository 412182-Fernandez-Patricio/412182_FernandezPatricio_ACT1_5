using ACT1_5.Datos.Repositorios;
using ACT1_5.Datos.Utiles;
using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Datos.Implementaciones
{
    public class ArticuloRepositoryADO : IArticuloRepository
    {
        //private SqlConnection _conn;
        private List<ParameterSQL> parameters;
        private DataHelper helper = DataHelper.GetInstance();
        public ArticuloRepositoryADO()
        {
            //_conn = helper.GetConnection();
            parameters = new List<ParameterSQL>();
        }
        private void clearParameters()
        {
            parameters.Clear();
        }
        public bool Delete(int id)
        {
            bool deleted = false;
            parameters.Add(new ParameterSQL("@id", id));
            if (helper.ExecuteSPDML("SP_DELETE", parameters) > 0)
            {
                deleted = true;
            }
            clearParameters();
            return deleted;
        }

        public List<Articulo> GetAll() //VERIFICAR QUE FUNCIONE
        {
            List<Articulo> list = new List<Articulo>();
            var t = helper.ExecuteSPQuery("SP_GET_ALL", parameters);
            int id;
            string descripcion = string.Empty;
            if (t != null)
            {
                foreach (DataRow row in t.Rows)
                {
                    id = Convert.ToInt32(row["id_articulo"]);
                    descripcion = Convert.ToString(row["descripcion"]);
                    list.Add(new Articulo(id, descripcion));
                }
            }
            clearParameters();
            return list;
        }

        public Articulo? GetById(int id) //VERIFICAR QUE FUNCIONE
        {
            Articulo? a = null;
            int _id;
            string descripcion = string.Empty;
            parameters.Add(new ParameterSQL("@id", id));
            var t = helper.ExecuteSPQuery("SP_GET_BY_ID", parameters);
            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                _id = Convert.ToInt32(row["id_articulo"]);
                descripcion = Convert.ToString(row["descripcion"]);
                a = new Articulo(_id, descripcion);
            }
            clearParameters();
            return a;
        }

        public bool Save(Articulo articulo)
        {
            bool guardado = false;
            if (GetById(articulo.Id) == null)
            {
                parameters.Add(new ParameterSQL("@descripcion", articulo.Descripcion));

                if (helper.ExecuteSPDML("SP_SAVE", parameters) == 1)
                {
                    guardado = true;
                }
            }
            else
            {
                Console.WriteLine("\nYa existe un articulo con esa ID");
            }
            
            clearParameters();
            return guardado;
        }


    }
}
