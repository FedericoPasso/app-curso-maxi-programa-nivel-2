using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Categorias
    {
        public int Id{ get; set; }

        public string Descripcion{ get; set; }

        public override string ToString()//sobreescribir el metodo ToString para que devuelva el contenido de la variable con el objeto Descripcion
        {
            return Descripcion;
        }
    }
}
