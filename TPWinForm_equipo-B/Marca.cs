using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    public class Marca : IValidable
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            // Si la descripción es nula, devuelve "Sin asignar"
            return Descripcion ?? "Sin asignar";
        }

        public bool EsValido()
        {
            return Validaciones.TextoValido(Descripcion) && Validaciones.LongitudValida(Descripcion, 50);
        }
    }
}
