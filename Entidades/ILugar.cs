using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUniversidad.Entidades
{
    public interface ILugar
    {
        public string Dirección { get; set; }

       public void LimpiarLugar();
    }
}