using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// deben estar en el mismo namspace donde estan las clases del modelo (Model.Modelos) a todas las clases
namespace Modelo.Modelos
{
    //La notacion definida siguiente es para indicar que la clase Metadato se aplica sobre la clase extension del modelo
    [MetadataType(typeof(MedicoMetadato))]
    // tambien publica y parcial
    public partial class Medico
    {
    }

    public class MedicoMetadato
    {
        // el id no es necesario ya que no cambia, y el borrador tampoco ya que el usuario no entrará ahi
        // lo demas si se copia de la clase Medico originada en Model
        [Required] // campo requerido
        [StringLength(10)] //longitud de 10 <- ambos para cedula
        public string cedula { get; set; }
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string apellidoPaterno { get; set; }
        [StringLength(50)]
        public string apellidoMaterno { get; set; }
        [Required]
        public bool esEspecialista { get; set; }
        [Required]
        public bool habilitado { get; set; }
    }
    }
