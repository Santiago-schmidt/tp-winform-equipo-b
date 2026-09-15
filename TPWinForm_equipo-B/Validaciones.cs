using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal static class Validaciones
    {
        public static bool TextoValido(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        public static bool LongitudValida(string texto, int longitudMaxima)
        {
            return texto != null && texto.Length <= longitudMaxima;
        }

        public static bool PrecioValido(decimal precio)
        {
            return precio > 0;
        }
    }
}
