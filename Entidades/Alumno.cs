using System;
using System.Collections.Generic;

namespace CoreUniversidad.Entidades
{
    public class Alumno: ObjectoUniversidadBase
    {
        public List<Evaluación> Evaluaciones { get; set; } = new List<Evaluación>();   

    }
}