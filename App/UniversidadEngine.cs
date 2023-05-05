using System;
using System.Collections.Generic;
using System.Linq;
using CoreUniversidad.Entidades;
using CoreUniversidad.Util;

namespace CoreUniversidad.Entidades
{
    public sealed class UniversidadEngine
    {

        public Universidad Universidad { get; set; }

        public UniversidadEngine()
        {

        }

        public void Inicializar()
        {
            Universidad = new Universidad("Alejandro de Humboldt", 1995, TiposUniversidad.Intermedio,
            ciudad: "Caracas", pais: "Venezuela");

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }
        //Imprimir Diccionario
        public void ImprimirDiccionario(Dictionary<LlavesDiccionario, IEnumerable<ObjectoUniversidadBase>> dic)
        {

            foreach (var obj in dic)
            {

                Printer.WriteTitle(obj.Key.ToString());

                foreach (var val in obj.Value)
                {
                    switch (obj.Key)
                    {
                        case LlavesDiccionario.Evaluacion:
                            bool ImprEval = true;
                            if (ImprEval)
                                Console.WriteLine(val);
                            break;
                        case LlavesDiccionario.Universidad:
                            Console.WriteLine("Universidad: " + val);
                            break;
                        case LlavesDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        case LlavesDiccionario.Curso:
                            var curtmp = val as Curso;
                            if (curtmp != null)
                            {
                                int Count = curtmp.Alumnos.Count;
                                Console.WriteLine("Cursos: " + val.Nombre + "Cantidad Alumno" + Count);
                            }

                            break;
                        default:
                            Console.WriteLine(val);
                            break;

                    }

                }

            }

        }
        //Diccionario de Datos
        public Dictionary<LlavesDiccionario, IEnumerable<ObjectoUniversidadBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlavesDiccionario, IEnumerable<ObjectoUniversidadBase>>();

            diccionario.Add(LlavesDiccionario.Universidad, new[] { Universidad });
            diccionario.Add(LlavesDiccionario.Curso, Universidad.Cursos.Cast<ObjectoUniversidadBase>());

            var listatmp = new List<Evaluación>();
            var listatmpas = new List<Asignatura>();
            var listatmpal = new List<Alumno>();
            foreach (var cur in Universidad.Cursos)
            {

                listatmpas.AddRange(cur.Asignaturas);
                listatmpal.AddRange(cur.Alumnos);


                foreach (var alum in cur.Alumnos)
                {
                    listatmp.AddRange(alum.Evaluaciones);
                }
            }
            diccionario.Add(LlavesDiccionario.Evaluacion, listatmp.Cast<ObjectoUniversidadBase>());

            diccionario.Add(LlavesDiccionario.Asignatura, listatmpas.Cast<ObjectoUniversidadBase>());
            diccionario.Add(LlavesDiccionario.Alumno, listatmpal.Cast<ObjectoUniversidadBase>());

            return diccionario;
        }

        //Lista de Objectos de Polimorfica para establecer parametros de salida
        public IReadOnlyList<ObjectoUniversidadBase> GetObjectosUniversidad(
                            out int conteoEvaluaciones,

                            bool traeEvaluaciones = true,
                            bool traeAlumnos = true,
                            bool traeAsignaturas = true,
                            bool traeCursos = true
            )
        {

            return GetObjectosUniversidad(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjectoUniversidadBase> GetObjectosUniversidad(
                            out int conteoEvaluaciones,
                            out int conteoCursos,

                            bool traeEvaluaciones = true,
                            bool traeAlumnos = true,
                            bool traeAsignaturas = true,
                            bool traeCursos = true
            )
        {

            return GetObjectosUniversidad(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
        }
        public IReadOnlyList<ObjectoUniversidadBase> GetObjectosUniversidad(
                            out int conteoEvaluaciones,
                            out int conteoCursos,
                            out int conteoAsignaturas,

                            bool traeEvaluaciones = true,
                            bool traeAlumnos = true,
                            bool traeAsignaturas = true,
                            bool traeCursos = true
        )
        {

            return GetObjectosUniversidad(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
        }
        public IReadOnlyList<ObjectoUniversidadBase> GetObjectosUniversidad(
                            out int conteoEvaluaciones,
                            out int conteoCursos,
                            out int conteoAsignaturas,
                            out int conteoAlumnos,
                            bool traeEvaluaciones = true,
                            bool traeAlumnos = true,
                            bool traeAsignaturas = true,
                            bool traeCursos = true
            )
        {
            conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = 0;
            var listObj = new List<ObjectoUniversidadBase>();
            listObj.Add(Universidad);
            if (traeCursos)
                listObj.AddRange(Universidad.Cursos);
            conteoCursos = Universidad.Cursos.Count;
            foreach (var curso in Universidad.Cursos)
            {
                if (traeAsignaturas)
                    listObj.AddRange(curso.Asignaturas);

                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traeAlumnos)
                    listObj.AddRange(curso.Alumnos);

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listObj.AddRange(alumno.Evaluaciones);
                    }
                }
            }

            return listObj.AsReadOnly();
        }

        //Lista de Objectos de Polimorfica
        public List<ObjectoUniversidadBase> GetObjectosUniversidad()
        {
            var listObj = new List<ObjectoUniversidadBase>();
            listObj.Add(Universidad);
            listObj.AddRange(Universidad.Cursos);

            foreach (var curso in Universidad.Cursos)
            {
                listObj.AddRange(curso.Asignaturas);
                listObj.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listObj.AddRange(alumno.Evaluaciones);
                }
            }

            return listObj;
        }

        #region Cargar Metodos 
        private void CargarEvaluaciones()
        {
            Random rdn = new Random();
            foreach (var curso in Universidad.Cursos)
            {
                foreach (var alumno in curso.Alumnos)
                {
                    foreach (var asignatura in curso.Asignaturas)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",

                                Nota = (float)Math.Round(10 * rdn.NextDouble(), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev); //antes de inicializar el .Add() se debe inicializar el metodo en Alumno.
                        }
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Universidad.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Programacion"} ,
                            new Asignatura{Nombre="Base de Datos"},
                            new Asignatura{Nombre="Matematica Discreta"},
                            new Asignatura{Nombre="Ingenieria de Software"},
                            new Asignatura{Nombre="Analisis Numerico"},
                            new Asignatura{Nombre="Circuito Electronico"},
                            new Asignatura{Nombre="Fisica"},
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Isabel", "Valentina", "Scarlet", "Daniel", "Teofilo", "Valerie", "Maria" };
            string[] apellido1 = { "Canache", "Velasquez", "Rodriguez", "Rojas", "Fernandez", "Torrez", "Suarez" };
            string[] nombre2 = { "Yadira", "Teresa", "Stephanie", "Carlos", "David", "Fabiola", "Fernanda", "Lilibeth" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Universidad.Cursos = new List<Curso>(){
                        new Curso() {Nombre = "401", Jornada = TiposJornada.Mañana},
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Mañana},
                        new Curso() {Nombre = "601", Jornada = TiposJornada.Mañana},
                        new Curso() {Nombre = "701", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "801", Jornada = TiposJornada.Mañana},
                        new Curso() {Nombre = "702", Jornada = TiposJornada.Mañana},
                        new Curso() {Nombre = "803", Jornada = TiposJornada.Noche},
                        new Curso() {Nombre = "505", Jornada = TiposJornada.Noche},
            };

            Random rnd = new Random();
            foreach (var c in Universidad.Cursos)
            {
                int cantRandom = rnd.Next(0, 5);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
        #endregion
    }
}