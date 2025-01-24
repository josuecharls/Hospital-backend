using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class IngresoDAL
    {
        public static ListadoPaginadoVMR<IngresoVMR> LeerTodo(int cantidadPag, int numPagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<IngresoVMR> resultado = new ListadoPaginadoVMR<IngresoVMR>();

            using (var db = DbConexion.Create())
            {
                //query es la conexion a la bd, la condicion where me devuelve los pacientes mientras que no esten borrados
                //me muestra los que no son borrados
                //select para crear nuevos objetos de tipo medico
                //esa x es un objeto presente en la tabla Medico
                var query = db.Ingreso.Where(x => !x.borrado).Select(x => new IngresoVMR
                {
                    //lo que necesito visualizar
                    id = x.id,
                    fecha = x.fecha,
                    numeroSala = x.numeroSala,
                    numeroCama = x.numeroCama,
                    diagnostico = x.diagnostico,
                    observacion = x.observacion,
                    medicoId = x.medicoId,
                    pacienteId = x.pacienteId,
                    medicoNombre = x.Medico.nombre + " " + x.Medico.apellidoPaterno + " " + x.Medico.apellidoMaterno,
                    pacienteNombre = x.Paciente.nombre + " " + x.Paciente.apellidoPaterno + " " + x.Paciente.apellidoMaterno,
                });
                //aplicamos el criterio de busqueda
                // si el texto de busquena no es null o no esta vacio
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    //estamos usando expresiones lambda (lo que me interesa filtrar)
                    query = query.Where(x => x.medicoNombre.Contains(textoBusqueda) || x.pacienteNombre.Contains(textoBusqueda));
                }

                //Cuenta todo
                resultado.cantidadTotal = query.Count();

                //despues del query enter porque se usarán varias funciones
                resultado.elemento = query
                    //ordenar sobre el id
                    .OrderBy(x => x.id)
                    //saltar un numero "x" de paginas
                    .Skip(numPagina * cantidadPag)
                    //limite de elementos que deseo
                    .Take(cantidadPag)
                    //toda la consulta lo convierto en una lista
                    .ToList();

            }

            return resultado;
        }

        public static IngresoVMR LeerUno(long id)
        {
            IngresoVMR item = null;

            using (var db = DbConexion.Create())
            {

                item = db.Ingreso.Where(x => !x.borrado && x.id == id).Select(x => new IngresoVMR
                {
                    //lo que deseo devolver
                    id = x.id,
                    fecha = x.fecha,
                    numeroSala = x.numeroSala,
                    numeroCama = x.numeroCama,
                    diagnostico = x.diagnostico,
                    observacion = x.observacion,
                    medicoId = x.medicoId,
                    pacienteId = x.pacienteId,
                    //el primer elemento si lo encuentra
                }).FirstOrDefault();
            }

            return item;
        }

        //Aquí si usamos la Entidad (Paciente)
        public static long Crear(Ingreso item)
        {
            using (var db = DbConexion.Create())
            {
                item.borrado = false;
                db.Ingreso.Add(item);
                db.SaveChanges(); //salvar los datos
            }

            return item.id;
        }

        public static void Actualizar(IngresoVMR item)
        {
            using (var db = DbConexion.Create())
            {
                //va a recibir el objeto - funcion FIND para buscar por su id
                var itemUpdate = db.Ingreso.Find(item.id);

                itemUpdate.fecha = item.fecha;
                itemUpdate.numeroSala = item.numeroSala;
                itemUpdate.numeroCama = item.numeroCama;
                itemUpdate.diagnostico = item.diagnostico;
                itemUpdate.observacion = item.observacion;
                itemUpdate.medicoId = item.medicoId;
                itemUpdate.pacienteId = item.pacienteId;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Ingreso.Where(x => ids.Contains(x.id));

                foreach (var item in items)
                {
                    item.borrado = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

    }
}
