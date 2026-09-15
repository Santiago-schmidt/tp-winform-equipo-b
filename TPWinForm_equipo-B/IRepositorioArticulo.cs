using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal interface IRepositorioArticulo
    {
        List<Articulo> Listar();
        Articulo ObtenerPorId(int id);
        void Agregar(Articulo articulo);
        void Modificar(Articulo articulo);
        void EliminarLogico(int id);
    }
}
