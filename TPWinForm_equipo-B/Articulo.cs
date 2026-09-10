using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal class Articulo
    {
        public Articulo() 
        {
            Imagenes = new List<Imagen>();
        }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public decimal Precio { get; set; }

    }
}
