using ACT1_5.Datos.Repositorios;
using ACT1_5.Datos.Utiles;
using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Datos.Implementaciones
{
    public class TipoPagoRepositoryADO : ITipoPagoRepository
    {
        private List<ParameterSQL> parameters;
        private DataHelper helper = DataHelper.GetInstance();

        public TipoPagoRepositoryADO()
        {
            parameters = new List<ParameterSQL>();
        }
        private void clearParameters()
        {
            parameters.Clear();
        }
        public List<TipoPago> GetAll()
        {
            List<TipoPago> list = new List<TipoPago>();
            var t = helper.ExecuteSPQuery("SP_GET_ALL_TP", parameters);
            int id;
            string descripcion = string.Empty;
            if (t != null)
            {
                foreach (DataRow row in t.Rows)
                {
                    id = Convert.ToInt32(row["id_tipo_pago"]);
                    descripcion = Convert.ToString(row["descripcion"]);
                    list.Add(new TipoPago(id, descripcion));
                }
            }
            clearParameters();
            return list;

        }

        public TipoPago? GetById(int id)
        {
            TipoPago? tp = null;
            int _id;
            string descripcion = string.Empty;
            parameters.Add(new ParameterSQL("@id", id));
            var t = helper.ExecuteSPQuery("SP_GET_BY_ID_TP", parameters);
            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                _id = Convert.ToInt32(row["id"]);
                descripcion = Convert.ToString(row["descripcion"]);
                tp = new TipoPago(_id, descripcion);
            }
            clearParameters();
            return tp;
        }

        public bool Save(TipoPago tipoPago)
        {

            bool guardado = false;
            if (GetById(tipoPago.Id) != null)
            {
                parameters.Add(new ParameterSQL("@descripcion", tipoPago.Descripcion));

                if (helper.ExecuteSPDML("SP_SAVE_TP", parameters) == 1)
                {
                    guardado = true;
                }
            }
            else
            {
                Console.WriteLine("\nYa existe un tipo de pago con esa ID");
            }

            clearParameters();
            return guardado;
        }
    }
}
