using System;
using System.Collections.Generic;
using CoreUniversidad.Util;

namespace CoreUniversidad.Entidades
{
    public class Universidad: ObjectoUniversidadBase, ILugar
    {
        public int AñoDeCreación { get; set; }

        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Dirección { get; set; }
        public TiposUniversidad TipoUniversidad { get; set; }
        public List<Curso> Cursos { get; set; }

        public Universidad(string nombre, int año) => (Nombre, AñoDeCreación) = (nombre, año);

        public Universidad(string nombre, int año, 
                       TiposUniversidad tipo, 
                       string pais = "", string ciudad = "") : base()
        {
            (Nombre, AñoDeCreación) = (nombre, año);
            Pais = pais;
            Ciudad = ciudad;
        }

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoUniversidad} {System.Environment.NewLine} Pais: {Pais}, Ciudad:{Ciudad}";
        }

        public void LimpiarLugar(){
            
            Printer.DrawLine();
            Console.WriteLine("Limpiando la Universidad....");

            foreach (var curso in Cursos)
            {
                curso.LimpiarLugar();
            }

            Printer.WriteTitle($"Universidad {Nombre} Limpia");
        }
    }
}