using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using CoreUniversidad.Entidades;
using CoreUniversidad.Util;
using static System.Console;

namespace CoreUniversidad.App
{
    public class Reporteador
    {
        Dictionary<LlavesDiccionario, IEnumerable<ObjectoUniversidadBase>> _diccionario;
        public Reporteador(Dictionary<LlavesDiccionario, IEnumerable<ObjectoUniversidadBase>> dicObjEsc)
        {
            if (dicObjEsc == null)
            {
                throw new ArgumentNullException(nameof(dicObjEsc));
            }
            _diccionario = dicObjEsc;
        }

        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {
            // var lista = _diccionario.GetValueOrDefault(LlaveDiccionario.Escuela);
            // IEnumerable<Escuela> rta;
            if (_diccionario.TryGetValue(
                LlavesDiccionario.Evaluacion,
                out IEnumerable<ObjectoUniversidadBase> lista
            ))
            {
                return lista.Cast<Evaluación>();
            }
            {
                return new List<Evaluación>();
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dumy);
        }
        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluación ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDiccionarioEvaluacionesPorAsignatura()
        {
            var diccionarioRta = new Dictionary<string, IEnumerable<Evaluación>>();

            var listaAsignaturas = GetListaAsignaturas(out var listaEvaluaciones);

            foreach (var asig in listaAsignaturas)
            {
                var evalAsig = from eval in listaEvaluaciones
                               where eval.Asignatura.Nombre == asig
                               select eval;
                diccionarioRta.Add(asig, evalAsig);
            }
            return diccionarioRta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromedioAlumnosXAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var diccionarioEvXAsig = GetDiccionarioEvaluacionesPorAsignatura();
            foreach (var asigConEval in diccionarioEvXAsig)
            {
                var promsAlumnos = from eval in asigConEval.Value
                           group eval by new{
                               eval.Alumno.UniqueId,
                               eval.Alumno.Nombre
                           }
                           into grupoEvalAlumno
                           select new AlumnoPromedio
                           {
                               alumnoID = grupoEvalAlumno.Key.UniqueId,
                               alumnoNombre = grupoEvalAlumno.Key.Nombre,
                               promedio = grupoEvalAlumno.Average(evaluacion => evaluacion.Nota)
                           };                
                rta.Add(asigConEval.Key, promsAlumnos);
            }

            return rta;
        }

        /* Reto 2.
        Realizar un reporteador que tome el top 5, top 10 o top X de los mejores estudiantes por asignatura. */

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetTopEstudiantesXAsignatura (int cantidad)
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var listaPromedioXAsignatura = GetPromedioAlumnosXAsignatura();
            foreach (var listaPromedio in listaPromedioXAsignatura)
            {
                var lista = listaPromedio.Value.OrderByDescending(eva => eva.promedio).Take(cantidad);
                rta.Add(listaPromedio.Key, lista);
            }

            return rta;
        }
        public void ImprimirEvaluaciones()
        {
            var eval = GetListaEvaluaciones();
            Printer.WriteTitle("Lista de Evaluaciones por alumno.");
            foreach (var lista in eval)
            {
                WriteLine($"Nombre de la evaluación: {lista.Nombre}{System.Environment.NewLine}Asignatura: {lista.Asignatura.Nombre} {System.Environment.NewLine}Nombre del estudiante: {lista.Alumno.Nombre} Nota: {lista.Nota}{System.Environment.NewLine}=====");
            }
        }
        public void ImprimirAsignaturas()
        {
            var asig = GetListaAsignaturas();
            Printer.WriteTitle("Lista de Asignaturas.");
            foreach (var lista in asig)
            {
                WriteLine($"Nombre de la asignatura: {lista}");
            }
        }

        public void ImprimirEvaluacionesXAsignatura()
        {
            var evXAsig = GetDiccionarioEvaluacionesPorAsignatura();
            Printer.WriteTitle("Lista de Evaluaciones por Asignatura.");
            foreach (var lista in evXAsig)
            {
                Printer.WriteTitle($"Asignatura: {lista.Key}");
                foreach (var eval in lista.Value)
                {
                    WriteLine($"Nombre de la evaluación: {eval.Nombre}{System.Environment.NewLine}Nombre del estudiante: {eval.Alumno.Nombre}{System.Environment.NewLine}Nota: {eval.Nota}");
                }
            }
        }
        public void ImprimirPromedioAlumnosXAsignatura()
        {
            var proms = GetPromedioAlumnosXAsignatura();
            Printer.WriteTitle("Promedio de Alumnos por Asignatura");
            foreach (var lista in proms)
            {
                Printer.WriteTitle($"Asignatura: {lista.Key}");
                foreach (var prom in lista.Value)
                {
                    WriteLine($"Nombre del estudiante: {prom.alumnoNombre} Promedio: {prom.promedio}");
                }
            }
        }
        public void ImprimirTopPromediosXAsignatura(int cantidad)
        {
            var top = GetTopEstudiantesXAsignatura(cantidad);
            Printer.WriteTitle($"Top {cantidad} estudiantes por asignatura.");
            foreach (var lista in top)
            {
                Printer.WriteTitle($"Asignatura: {lista.Key}");
                foreach (var est in lista.Value)
                {
                    WriteLine($"Nombre del estudiante: {est.alumnoNombre} - Promedio {est.promedio}");
                }
            }
        }
    }
}