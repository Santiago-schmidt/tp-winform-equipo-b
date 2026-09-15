using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal class ArticuloBuscador
    {
        public List<Articulo> Buscar(
            List<Articulo> articulos,
            string texto,
            int idMarca,
            int idCategoria,
            decimal? precioMinimo,
            decimal? precioMaximo,
            bool ordenarMayorMenor)
        { 
            if (!string.IsNullOrWhiteSpace(texto))
                {
                string textoBusqueda = texto.ToLower();
                    articulos = articulos.Where(a =>
                        (a.Codigo ?? "").ToLower().Contains(textoBusqueda) || 
                        (a.Nombre ?? "").ToLower().Contains(textoBusqueda) ||
                        (a.Descripcion ?? "").Contains(textoBusqueda)
                        ).ToList();
                }
            if (idMarca != 0)
            {
                articulos = articulos.Where(a => a.Marca.Id == idMarca).ToList();
            }
            if (idCategoria != 0) 
            {
                articulos = articulos.Where(a => a.Categoria.Id == idCategoria).ToList();            
            }

            if (precioMinimo.HasValue)
            {
                articulos = articulos.Where(a => a.Precio >= precioMinimo.Value).ToList();
            }

            if (precioMaximo.HasValue)
            { 
                articulos = articulos.Where(a => a.Precio <= precioMaximo.Value).ToList();
            }

            if (ordenarMayorMenor)
            {
                articulos = articulos.OrderByDescending(a => a.Precio).ToList();

            }
            else
            {
                articulos = articulos.OrderBy(a => a.Precio).ToList();
            }

            return articulos;
        }
    }
}
