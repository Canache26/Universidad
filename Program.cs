using System;
using System.Collections.Generic;
using CoreUniversidad.Util;
using CoreUniversidad.Entidades;
using static System.Console;

namespace CoreUniversidad
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new UniversidadEngine();
            engine.Inicializar();

            Printer.WriteTitle("BIENVENIDOS A LA UNIVERSIDAD ALEJANDRO DE HUMBOLDT");
            //Printer.Beep(10000, cantidad:1);
            ImpimirCursosUniversidad(engine.Universidad);
            var listaObjetos = engine.GetObjectosUniversidad();

            //engine.Universidad.LimpiarLugar();

            var listaILugar = from obj in listaObjetos
                              where obj is ILugar
                              select obj (ILugar)obj;

        }

        private static void ImpimirCursosUniversidad(Universidad universidad)
        {
            
            Printer.WriteTitle("CURSOS DE LA CARRERA INGENIERIA EN INFORMATICA");
            
            
            if (universidad?.Cursos != null) //manera corta de hacer un desicional
            {
                foreach (var curso in universidad.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}