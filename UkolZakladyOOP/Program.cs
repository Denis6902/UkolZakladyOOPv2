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
            List<Lecture> lectureList = new();
            List<string> averageMarksList = new();
            List<Mark_Subject> markSubjectList = new();

            Student DefaultStudent = new(0,"Default", "Default", new DateTime(0001, 1, 1), new DateTime(0001, 1, 1), subjects.ToList(), exercises, markSubjectList,0);
            Teacher DefaultTeacher = new("Default",0, "Default", "Default", new DateTime(0001, 1, 1), subjects.ToList(), exercises);
            Subject DefaultSubject = new("Default", DefaultTeacher, 0, 0);
            Lecture DefaultLecture = new("Default", true, 0, DefaultSubject, lectureList);
            Exercise DefaultExercise = new("Default exercise", true, 0, DefaultSubject);
            Mark_Subject DefaultMarkSubject = new(0, DefaultSubject, 0, markSubjectList);

            Teacher Pavel = new("Ing.",1, "Pavel", "Novotný", new DateTime(1980, 2, 9), null, exercises, teachers);
            Teacher Aneta = new("Mgr.",2, "Aneta", "Nováková", new DateTime(1987, 1, 8), null, exercises, teachers);
            Subject English = new("English", Pavel, 50, subjects,1);
            Subject Czech = new("Czech", Aneta, 50, subjects,1);
            Subject x = new("x", Pavel, 0, subjects,1); // TEST_ONLY
            Lecture c = new("c", false, 0, x, lectureList); // TEST_ONLY

            Pavel.subjectsToRegister = subjects.ToList();
            Aneta.subjectsToRegister = subjects.ToList();

            Student Jakub = new(1,"Jakub", "Novák", new DateTime(1999, 7, 2), new DateTime(2020, 10, 1), subjects.ToList(), exercises, students, markSubjectList,1);
            Student Pepa = new(2,"Pepa", "Nový", new DateTime(1998, 9, 3), new DateTime(2020, 10, 2), subjects.ToList(), exercises, students,markSubjectList,1);
            Student Denis = new(3,"Denis", "Vojtěch", new DateTime(1984, 9, 3), new DateTime(2020, 1, 2), subjects.ToList(), exercises, students, markSubjectList,1);
            Exercise ExerciseFromEnglish = new("Cvičení z Angličtiny", false, 50, English, exercises);
            Exercise ExerciseFromCzech = new("Cvičení z Češtiny", false, 50, Czech, exercises);
            Lecture LectureFromEnglish = new("Přednáška z Angličtiny", false, 50, English, lectureList);
            Lecture LectureFromCzech = new("Přednáška z Češtiny", false, 50, Czech, lectureList);

            Pepa.registredSubjects.Add(x); // TEST_ONLY

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
            Method.mainMenu(chosenExercise, DefaultExercise, chosenStudent, DefaultStudent, chosenTeacher, DefaultTeacher, chosenSubject, DefaultSubject, who, end, students, teachers, whoIAm, option, subject, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects, lectureList, markSubjectList);
        }
    }
}