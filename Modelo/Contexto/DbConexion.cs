using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Cambio el namespace a Modelo.Modelos porque debe tener el mismo nombre donde está el modelo
namespace Modelo.Modelos
{
    // La clase se hace publica y parcial, ademas extiende de DbContext, aqui EntityFrm va a entender que nuestra clase va a extender algunas caracteristicas de la clase original del contexto(DbContext)
    public partial class DbConexion : DbContext
    {
        private DbConexion(string stringConexion)
            : base(stringConexion)
        {
            //se pone en falso porque evita que los objetos que estan anidados dentro de otros objetos, automaticamente se carguen haciendo uso de get y set
            this.Configuration.LazyLoadingEnabled = false; // se desactiva porque aveces da errores de relacion ciclica (un objeto que está relacionado con otro objeto y ese esta relacionado con el objeto anterior)
            this.Configuration.ProxyCreationEnabled = false; // tambien porque no queremos que sea automatico la carga por la relacion ciclica
            this.Database.CommandTimeout = 900; // para que el tiempo de consulta en la BD no se exceda y evitar errores (significa 900 segundos)
        }
        //metodo estatico para no crear un objeto y se pueda usar directamente la clase
        public static DbConexion Create()
        {
            return new DbConexion("name=DbConexion");//En app.config de modelos está el nombre
        }
    }
}
