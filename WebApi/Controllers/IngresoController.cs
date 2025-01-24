using Comun.ViewModels;
using Logica.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class IngresoController : ApiController
    {
        // debe recibir los parametros necesarios para el paginado y busqueda
        [HttpGet]
        public IHttpActionResult LeerTodo(int cantidadPag = 10, int numPagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<IngresoVMR>>();

            try
            {
                respuesta.datos = IngresoBLL.LeerTodo(cantidadPag, numPagina, textoBusqueda);
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<IngresoVMR>();

            try
            {
                respuesta.datos = IngresoBLL.LeerUno(id);
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            if (respuesta.datos == null && respuesta.mensajes.Count() == 0)
            {
                {
                    respuesta.codigo = HttpStatusCode.NotFound;
                    respuesta.mensajes.Add("Elemento no encontrado");
                }
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpPost]
        public IHttpActionResult Crear(Ingreso item)
        {
            // long? <- permite valores nulos (respuesta.datos=null)
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = IngresoBLL.Crear(item);
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpPut]// PÉRMITE MANEJAR ACTUALIZACIONES DE LA INFO
        public IHttpActionResult Actualizar(long id, IngresoVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;//En mi PacienteDAL se necesita que el item tenga el pk del elemento para utilizar la funcion find
                IngresoBLL.Actualizar(item);
                respuesta.datos = true;
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                PacienteBLL.Eliminar(ids);
                respuesta.datos = true;
            }
            catch (Exception ex)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(ex.Message);
                respuesta.mensajes.Add(ex.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }
    }
}
