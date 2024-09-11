using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT1_5.Dominio
{
    public class Articulo
    {
        private int id;
        private string? descripcion;
        public int Id 
        {
            get { return id; }
            set { id = value; } 
        }
        public string? Descripcion
        {
            get { return descripcion; }
            set {  descripcion = value; } 
        }

        public Articulo() 
        {
            id = 0;
            descripcion = string.Empty;
        }
        public Articulo(int id, string descripcion)
        {
            this.id = id;   
            this.descripcion = descripcion;
        }
    }
}
