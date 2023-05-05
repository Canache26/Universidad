using System;
using System.Collections.Generic;
using CoreUniversidad.Entidades;
using CoreUniversidad.Util;
using CoreUniversidad.App;
using static System.Console;
using System.Linq;

namespace CoreUniversidad
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new UniversidadEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDO A LA UNIVERSIDAD ALEJANDRO DE HUMBOLDT");
            
            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evList = reporteador.GetListaEvaluaciones();
            var listaAsig = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDiccionarioEvaluacionesPorAsignatura();
            var listaPromXAsig = reporteador.GetPromedioAlumnosXAsignatura();
            var topEstudiantes = reporteador.GetTopEstudiantesXAsignatura(5);
            
            
            /* Reto final.
            El último reto del curso colocará a prueba todos los conocimientos que has aprendido a lo largo del curso y te permitirá afianzar todos los conocimientos.
            Debes crear distintos métodos para mostrar por consola cada uno de los reportes, debe mantener una vista agradable y es más importante una buena experiencia del usuario al leer la información que la cantidad mostrada. */
            
            int selección = 1;
            string _selección;
            string _selector;
            int selector = 0;
            while (selección == 1)
            {
                Printer.WriteTitle("SELECCIONE EL REPORTE QUE DESEA IMPRIMIR.");
                WriteLine("(1) NOMBRE DE LA ESCUELA.");
                WriteLine("(2) LISTA DE LOS CURSOS.");
                WriteLine("(3) REPORTE DE EVALUACIONES.");
                WriteLine("(4) REPORTE DE ASIGNATURAS.");
                WriteLine("(5) REPORTE DE EVALUACIONES POR ASIGNATURA.");
                WriteLine("(6) REPORTE DEL PROMEDIOS DE ALUMNOS POR ASIGNATURA.");
                WriteLine("(7) REPORTE POR TOP MEJORES ALUMNOS POR ASIGNATURA.");
                WriteLine("PRESONE 0 PARA SALIR.");
                Printer.PressEnterToContinue();
                _selector = ReadLine();
                selector = int.Parse(_selector);

                switch (selector)
                {
                    case 0:
                        WriteLine("SALIENDO DEL PROGRAMA...");
                        selección = 0;
                        break;
                    case 1:
                        WriteLine(engine.Universidad);
                        break;
                    case 2:
                        ImpimirCursosEscuela(engine.Universidad);
                        break;
                    case 3:
                        reporteador.ImprimirEvaluaciones();
                        break;
                    case 4:
                        reporteador.ImprimirAsignaturas();
                        break;
                    case 5:
                        reporteador.ImprimirEvaluacionesXAsignatura();
                        break;
                    case 6:
                        reporteador.ImprimirPromedioAlumnosXAsignatura();
                        break;
                    case 7:
                        int cantidad;
                        string _cantidad;
                        WriteLine("INGRESE LA CANTIDAD DE ESTUDIANTES");
                        _cantidad = ReadLine();
                        cantidad = int.Parse(_cantidad);
                        reporteador.ImprimirTopPromediosXAsignatura(cantidad);
                        break;
                    default:
                        WriteLine("OPCION NO VALIDA");
                        WriteLine("SALIENDO DEL PROGRAMA....");
                        break;
                }
                WriteLine("PRESIONE 0 PARA SALIR, PRESIONE 1 PARA OTRO REPORTE.");
                _selección = ReadLine();
                selección = int.Parse(_selección);
            }
        }

 
        private static void ImpimirCursosEscuela(Universidad universidad)
        {

            Printer.WriteTitle("CURSOS DE LA UNIVERSIDAD ALEJANDRO DE HUMBOLDT");


            if (universidad?.Cursos != null)
            {
                foreach (var curso in universidad.Cursos)
                {
                    WriteLine($"Nombre del curso: {curso.Nombre  }, Id:  {curso.UniqueId}");
                }
            }
        }
    }
}