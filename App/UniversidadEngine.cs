using System;
using System.Collections.Generic;
using System.Linq;
using CoreUniversidad.Entidades;

namespace CoreUniversidad
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
        

        private void CargarEvaluaciones()
        {
            foreach (var curso in Universidad.Cursos)
            {
                foreach (var alumno in curso.Alumnos)
                {
                    foreach (var asignatura in curso.Asignaturas)
                    {
                        Random rdn = new Random();
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",

                                Nota = (float)(5 * rdn.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev); //antes de inicializar el .Add() se debe inicializar el metodo en Alumno.
                        }
                    }
                }
            }
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
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

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
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
    }
}