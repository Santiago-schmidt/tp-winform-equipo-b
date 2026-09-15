using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal interface IRepositorioCategoria
    {
        List<Categoria> Listar();
        Categoria ObtenerPorId(int id);
        void Agregar(Categoria categoria);
        void Modificar(Categoria categoria);
        void EliminarLogico(int id);
    }
}
