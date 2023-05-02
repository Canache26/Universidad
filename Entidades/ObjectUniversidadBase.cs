using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUniversidad.Entidades
{
    public abstract class ObjectoUniversidadBase
    {
        public string UniqueId { get; private set; }

        public string Nombre { get; set; }

        public ObjectoUniversidadBase()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        //Implementacion por defecto para la sobreescritura con ToString
        public override string ToString()
        {
            return $"{Nombre},{UniqueId}";
        }
    }

}