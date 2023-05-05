using System;
using CoreUniversidad.Entidades;

namespace CoreUniversidad.Entidades
{
    public class Evaluación: ObjectoUniversidadBase
    {
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura  { get; set; }

        public float Nota { get; set; }
        public object promedio { get; internal set; }

        public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
        
    }
}