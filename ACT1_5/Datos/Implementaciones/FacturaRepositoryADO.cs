using ACT1_5.Datos.Repositorios;
using ACT1_5.Datos.Utiles;
using ACT1_5.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Datos.Implementaciones
{
    public class FacturaRepositoryADO : IFacturaRepository
    {
        DataHelper helper = DataHelper.GetInstance();
        public List<Factura>? GetAll()
        {
            List<Factura>? facturas = null;
            var t = helper.ExecuteSPQuery("SP_GET_ALL_FACTURAS", null);
            if (t != null)
            {
                facturas = new List<Factura>();
                for (int i = 1; i <= t.Rows.Count; i++)
                {
                    var factura = GetById(i);
                    if(factura != null)
                    {
                        facturas.Add(factura);
                    }
                    else
                    {
                        facturas = null;
                        break;
                    }
                    
                }
            }
            return facturas;


        }
        public Factura? GetById(int id) //CONFIRMAR QUE FUNCIONE, ANALIZALO.
        {
            Factura? oFactura = null;
            SqlConnection? conn = null;
            //SqlTransaction? t = null;
            try
            {
                conn = helper.GetConnection();
                conn.Open();
                //t = conn.BeginTransaction();
                List<DetalleFactura> detallesFacturas = new List<DetalleFactura>();

                var cmd = new SqlCommand("SP_GET_DETALLE_FACTURA", conn/*, t*/);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                
                dt.Load(cmd.ExecuteReader());



                int idDetalle = 0;
                int idArticulo = 0;
                string descArticulo = string.Empty;
                Articulo oArticulo;
                int cantidad = 0;
                float precioUnidad = 0;

                foreach (DataRow row in dt.Rows) 
                {
                    idDetalle = Convert.ToInt32(row["id_detalle_factura"]);
                    idArticulo = Convert.ToInt32(row["id_articulo"]);
                    descArticulo = Convert.ToString(row["descripcion"]);
                    oArticulo = new Articulo(idArticulo, descArticulo);
                    cantidad = Convert.ToInt32(row["cantidad"]);
                    precioUnidad = (float) Convert.ToDouble(row["precio_unidad"]);
                    detallesFacturas.Add(new DetalleFactura(idDetalle, oArticulo, cantidad, precioUnidad));
                }
                dt.Reset();
                

                var cmdFactura = new SqlCommand("SP_GET_FACTURA", conn/*, t*/);
                cmdFactura.CommandType = System.Data.CommandType.StoredProcedure;
                cmdFactura.Parameters.AddWithValue("@id", id);
                
                dt.Load(cmdFactura.ExecuteReader());
                
                
                DateTime fecha = Convert.ToDateTime(dt.Rows[0][0]);
                
                int idTipoPago = Convert.ToInt32(dt.Rows[0][1]);
                string descTipoPago = Convert.ToString(dt.Rows[0][2]);
                TipoPago tipoPago = new TipoPago(idTipoPago, descTipoPago);
                string cliente = Convert.ToString(dt.Rows[0][3]);

                oFactura = new Factura(id, fecha, tipoPago, detallesFacturas, cliente);

                
                //t.Commit();

            }
            catch (SqlException)
            {
                /*
                if(t != null)
                {
                    Console.Write("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
                    t.Rollback();
                    t.Dispose();
                    oFactura = null;
                }
                */
                
                oFactura = null;
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open) 
                {
                    conn.Close();
                }
            }

            return oFactura;

        }
        public bool Save(Factura oFactura) // TRANSACCIONES CLASE 03-09-2024 sin Unit Of Work
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? conn = null;
            try
            {
                conn = helper.GetConnection();
                conn.Open();
                t = conn.BeginTransaction();

                var cmd = new SqlCommand("SP_INSERT_FACTURA", conn, t);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_tipo_pago", oFactura.TipoPago.Id);
                cmd.Parameters.AddWithValue("@cliente", oFactura.Cliente);
                //parametro de salida del SP
                SqlParameter param = new SqlParameter("@id_factura", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(param);
                
                cmd.ExecuteNonQuery();

                
                int idFactura = (int)param.Value;
                foreach (var detalle in oFactura.GetDetallesFacturas())
                {
                    var cmdDetalle = new SqlCommand("SP_INSERT_DETALLE_FACTURA", conn, t);
                    cmdDetalle.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@id_factura", idFactura);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", detalle.Articulo.Id);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@precio_unidad", detalle.PrecioUnidad);

                    cmdDetalle.ExecuteNonQuery();

                }

                
                t.Commit();

            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if(conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return result;
        }
    }
}
