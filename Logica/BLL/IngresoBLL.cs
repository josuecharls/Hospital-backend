using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class IngresoBLL
    {
        public static ListadoPaginadoVMR<IngresoVMR> LeerTodo(int cantidadPag, int numPagina, string textoBusqueda)
        {
            return IngresoDAL.LeerTodo(cantidadPag, numPagina, textoBusqueda);
        }
        public static IngresoVMR LeerUno(long id)
        {
            return IngresoDAL.LeerUno(id);
        }
        //Aquí si usamos la Entidad (Medico)
        public static long Crear(Ingreso item)
        {
            return IngresoDAL.Crear(item);
        }
        public static void Actualizar(IngresoVMR item)
        {
            IngresoDAL.Actualizar(item);
        }
        public static void Eliminar(List<long> ids)
        {
            IngresoDAL.Eliminar(ids);
        }
    }
}
