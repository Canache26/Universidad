using System;
using System.Collections.Generic;
using CoreUniversidad.Entidades;
using CoreUniversidad.Util;

namespace CoreUniversidad.Entidades
{
    public class Curso: ObjectoUniversidadBase, ILugar
    {
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }

        public string Dirección { get; set; }

        public void LimpiarLugar()
        {
            Printer.DrawLine();
            Console.WriteLine("Limpiando Curso....");
            Console.WriteLine($"Curso {Nombre} Limpiado");
        }
    }
}