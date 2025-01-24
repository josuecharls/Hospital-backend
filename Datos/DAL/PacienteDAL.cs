using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class PacienteDAL
    {
        public static ListadoPaginadoVMR<PacienteVMR> LeerTodo(int cantidadPag, int numPagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<PacienteVMR> resultado = new ListadoPaginadoVMR<PacienteVMR>();

            using (var db = DbConexion.Create())
            {
                //query es la conexion a la bd, la condicion where me devuelve los pacientes mientras que no esten borrados
                //me muestra los que no son borrados
                //select para crear nuevos objetos de tipo medico
                //esa x es un objeto presente en la tabla Medico
                var query = db.Paciente.Where(x => !x.borrado).Select(x => new PacienteVMR
                {
                    //lo que necesito visualizar
                    id = x.id,
                    cedula = x.cedula,
                    //nombre y apellido en una misma columna - la condicion para el apellido materno, ya que puede que sea null o no
                    nombre = x.nombre,
                    apellidoPaterno = x.apellidoPaterno,
                    apellidoMaterno = x.apellidoMaterno != null ? (" " + x.apellidoMaterno) : " ",
                    direccion = x.direccion,
                    celular = x.celular,
                    correoElectronico = x.correoElectronico,
                });
                //aplicamos el criterio de busqueda
                // si el texto de busquena no es null o no esta vacio
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    //estamos usando expresiones lambda (lo que me interesa filtrar)
                    query = query.Where(x => x.cedula.Contains(textoBusqueda) || x.nombre.Contains(textoBusqueda));
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

        public static PacienteVMR LeerUno(long id)
        {
            PacienteVMR item = null;

            using (var db = DbConexion.Create())
            {

                item = db.Paciente.Where(x => !x.borrado && x.id == id).Select(x => new PacienteVMR
                {
                    //lo que deseo devolver
                    id = x.id,
                    cedula = x.cedula,
                    nombre = x.nombre,
                    apellidoMaterno = x.apellidoMaterno,
                    apellidoPaterno = x.apellidoPaterno,
                    direccion = x.direccion,
                    celular = x.celular,
                    correoElectronico = x.correoElectronico,
                    //el primer elemento si lo encuentra
                }).FirstOrDefault();
            }

            return item;
        }

        //Aquí si usamos la Entidad (Paciente)
        public static long Crear(Paciente item)
        {
            using (var db = DbConexion.Create())
            {
                item.borrado = false;
                db.Paciente.Add(item);
                db.SaveChanges(); //salvar los datos
            }

            return item.id;
        }

        public static void Actualizar(PacienteVMR item)
        {
            using (var db = DbConexion.Create())
            {
                //va a recibir el objeto - funcion FIND para buscar por su id
                var itemUpdate = db.Paciente.Find(item.id);

                itemUpdate.cedula = item.cedula;
                itemUpdate.nombre = item.nombre;
                itemUpdate.apellidoMaterno = item.apellidoMaterno;
                itemUpdate.apellidoPaterno = item.apellidoPaterno;
                itemUpdate.direccion = item.direccion;
                itemUpdate.celular = item.celular;
                itemUpdate.correoElectronico= item.correoElectronico;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Paciente.Where(x => ids.Contains(x.id));

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
