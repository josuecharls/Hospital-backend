using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    //Aplico generesidad, para no crear un listadopaginado para cada entidad, ya que no es recomendable
    //Es por esto que yo agrego "<T>", YA QUE ASI SE DEVOLVERA LISTADO DE TODAS LAS ENTIDADES
    // <T> <- SE AGREGA UN TIPO T POR GENERESIDAD (recurso de POO)
    public class ListadoPaginadoVMR<T>
    {
        public int cantidadTotal { get; set; }
        // IEnumerable es una interface de entity
        public IEnumerable<T> elemento { get; set; }

        public ListadoPaginadoVMR()
        {
            elemento = new List<T>();
            cantidadTotal = 0;
        }
    }
}
