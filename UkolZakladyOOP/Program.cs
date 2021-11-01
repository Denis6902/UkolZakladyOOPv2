using System;
using System.Collections.Generic;
using System.Linq;

namespace UkolZakladyOOP
{
    class Program
    {
        static public void Main(string[] args)
        {
            List<Subject> subjects = new();
            List<Exercise> exercises = new();
            List<Lecture> lectures = new();
            List<Student> students = new();
            List<Teacher> teachers = new();
            List<string> averageMarksList = new();

            Student DefaultStudent = new("Default", "Default", new DateTime(0001, 1, 1), new DateTime(0001, 1, 1), subjects.ToList(), exercises);
            Teacher DefaultTeacher = new("Default", "Default", "Default", new DateTime(0001, 1, 1), subjects.ToList(), exercises);
            Subject DefaultSubject = new("Default", DefaultTeacher, 0);
            Lecture DefaultLecture = new("Default", true, 0, DefaultSubject);
            Exercise DefaultExercise = new("Default exercise", true, 0, DefaultSubject);

            Teacher Pavel = new("Ing.", "Pavel", "Novotný", new DateTime(1980, 2, 9), null, exercises, teachers);
            Teacher Aneta = new("Mgr.", "Aneta", "Nováková", new DateTime(1987, 1, 8), null, exercises, teachers);
            Subject English = new("English", Pavel, 100, subjects);
            Subject Czech = new("Czech", Aneta, 100, subjects);
            Subject x = new("x", Pavel, 0, subjects); // TEST_ONLY
            Lecture c = new("c", false, 0, x); // TEST_ONLY

            Pavel.subjectsToRegister = subjects.ToList();
            Aneta.subjectsToRegister = subjects.ToList();

            Student Jakub = new("Jakub", "Novák", new DateTime(1999, 7, 2), new DateTime(2020, 10, 1), subjects.ToList(), exercises, students);
            Student Pepa = new("Pepa", "Nový", new DateTime(1998, 9, 3), new DateTime(2020, 10, 2), subjects.ToList(), exercises, students);
            Student Denis = new("Denis", "Vojtěch", new DateTime(1984, 9, 3), new DateTime(2020, 1, 2), subjects.ToList(), exercises, students);
            Exercise ExerciseFromEnglish = new("Cvičení z Angličtiny", false, 50, English, exercises);
            Exercise ExerciseFromCzech = new("Cvičení z Češtiny", false, 50, Czech, exercises);
            Lecture LectureFromEnglish = new("Přednáška z Angličtiny", false, 50, English);
            Lecture LectureFromCzech = new("Přednáška z Češtiny", false, 50, Czech);

            Denis.registredSubjects.Add(x); // TEST_ONLY

            bool end = false;
            string who = " ";
            string option = " ";
            string whoIAm = " ";
            string subject = " ";
            Teacher chosenTeacher = DefaultTeacher;
            Student chosenStudent = DefaultStudent;
            Exercise chosenExercise = DefaultExercise;
            Subject chosenSubject = DefaultSubject;
            Lecture chosenLecture = DefaultLecture;



            Method Method = new();
            Method.mainMenu(chosenExercise, DefaultExercise, chosenStudent, DefaultStudent, chosenTeacher, DefaultTeacher, chosenSubject, DefaultSubject, who, end, students, teachers, whoIAm, option, subject, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
        }
    }
}