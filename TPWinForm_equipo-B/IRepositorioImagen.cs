using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal interface IRepositorioImagen
    {
        List<Imagen> Listar();
        Imagen ObtenerPorId(int id);
        void Agregar(Imagen imagen);
        void Modificar(Imagen imagen);
        void EliminarLogico(int id);
    }
}
