using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal interface IRepositorioMarca
    {
        List<Marca> Listar();
        Marca ObtenerPorId(int id);
        void Agregar(Marca marca);
        void Modificar(Marca marca);
        void EliminarLogico(int id);
    }
}
