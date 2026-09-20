using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    public class Articulo : IValidable
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

        public string MarcaDescripcion
        {
            get { return Marca != null ? Marca.Descripcion : string.Empty; }
        }

        public string CategoriaDescripcion
        {
            get { return Categoria != null ? Categoria.Descripcion : string.Empty; }
        }

        public override string ToString()
        {
            return Descripcion;
        }

        public bool EsValido()
        {
            if (!Validaciones.TextoValido(Codigo))
                return false;

            if (!Validaciones.TextoValido(Nombre))
                return false;

            if (!Validaciones.PrecioValido(Precio))
                return false;

            if (Marca == null || Categoria == null)
                return false;

            return true;
        }
    }
}
