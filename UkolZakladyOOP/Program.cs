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
            List<Student> students = new();
            List<Teacher> teachers = new();
            List<Lecture> lectures = new();
            Semester currentSemester = Semester.Summer;

            Teacher Pavel = new("Ing.",1, "Pavel", "Novotný", new DateTime(1980, 2, 9), null, exercises, teachers);
            Teacher Aneta = new("Mgr.",2, "Aneta", "Nováková", new DateTime(1987, 1, 8), null, exercises, teachers);
            
            Subject English1_1 = new("English1_1", Pavel, 50,1, Semester.Summer, subjects,1);
            Subject Czech1_1 = new("Czech1_1", Aneta, 50,1,Semester.Winter,subjects,1);
            
            Subject English2_1 = new("English2_1", Pavel, 50,2,Semester.Summer, subjects,1);
            Subject Czech2_1 = new("Czech2_1", Aneta, 50,2,Semester.Winter, subjects,1);
            
            Subject English3_1 = new("English3_1", Pavel, 50,3,Semester.Summer, subjects,1);
            Subject Czech3_1 = new("Czech3_1", Aneta, 50,3,Semester.Winter, subjects,1);
            
            Subject English1_2 = new("English1_2", Pavel, 50,1, Semester.Summer, subjects,1);
            Subject Czech1_2 = new("Czech1_2", Aneta, 50,1,Semester.Winter,subjects,1);
            
            Subject English2_2 = new("English2_2", Pavel, 50,2,Semester.Summer, subjects,1);
            Subject Czech2_2 = new("Czech2_2", Aneta, 50,2,Semester.Winter, subjects,1);
            
            Subject English3_2 = new("English3_2", Pavel, 50,3,Semester.Summer, subjects,1);
            Subject Czech3_2 = new("Czech3_2", Aneta, 50,3,Semester.Winter, subjects,1);
            
            Subject x = new("x", Pavel, 0,1,Semester.Summer, subjects,1); // TEST_ONLY
            Lecture c = new("c", false, 0, x, lectures, Pavel); // TEST_ONLY
            Exercise o = new Exercise("o", false, 0, x, exercises);

            Pavel.subjectsToRegister = subjects.ToList();
            Aneta.subjectsToRegister = subjects.ToList();

            Student Jakub = new(1,"Jakub", "Novák", new DateTime(1999, 7, 2), new DateTime(2020, 10, 1), subjects.ToList(), exercises, students,1);
            Student Pepa = new(2,"Pepa", "Nový", new DateTime(1998, 9, 3), new DateTime(2020, 10, 2), subjects.ToList(), exercises, students,1);
            Student Denis = new(3,"Denis", "Vojtěch", new DateTime(1984, 9, 3), new DateTime(2020, 1, 2), subjects.ToList(), exercises, students,1);
            
            //Pepa.subjects.Add(x); // TEST_ONLY

            Exercise ExerciseFromEnglish1_1 = new("Cvičení z Angličtiny", false, 50, English1_1, exercises);
            Exercise ExerciseFromCzech1_1 = new("Cvičení z Češtiny", false, 50, Czech1_1, exercises);
            Lecture LectureFromEnglish1_1 = new("Přednáška z Angličtiny", false, 50, English1_1, lectures, Pavel);
            Lecture LectureFromCzech1_1 = new("Přednáška z Češtiny", false, 50, Czech1_1, lectures, Aneta);
            
            Exercise ExerciseFromEnglish2_1 = new("Cvičení z Angličtiny", false, 50, English2_1, exercises);
            Exercise ExerciseFromCzech2_1 = new("Cvičení z Češtiny", false, 50, Czech2_1, exercises);
            Lecture LectureFromEnglish2_1 = new("Přednáška z Angličtiny", false, 50, English2_1, lectures, Pavel);
            Lecture LectureFromCzech2_1 = new("Přednáška z Češtiny", false, 50, Czech2_1, lectures, Aneta);
            
            Exercise ExerciseFromEnglish3_1 = new("Cvičení z Angličtiny", false, 50, English3_1, exercises);
            Exercise ExerciseFromCzech3_1 = new("Cvičení z Češtiny", false, 50, Czech3_1, exercises);
            Lecture LectureFromEnglish3_1 = new("Přednáška z Angličtiny", false, 50, English3_1, lectures, Pavel);
            Lecture LectureFromCzech3_1 = new("Přednáška z Češtiny", false, 50, Czech3_1, lectures, Aneta);

            Exercise ExerciseFromEnglish1_2 = new("Cvičení z Angličtiny", false, 50, English1_2, exercises);
            Exercise ExerciseFromCzech1_2 = new("Cvičení z Češtiny", false, 50, Czech1_2, exercises);
            Lecture LectureFromEnglish1_2 = new("Přednáška z Angličtiny", false, 50, English1_2, lectures, Pavel);
            Lecture LectureFromCzech1_2 = new("Přednáška z Češtiny", false, 50, Czech1_2, lectures, Aneta);
            
            Exercise ExerciseFromEnglish2_2 = new("Cvičení z Angličtiny", false, 50, English2_2, exercises);
            Exercise ExerciseFromCzech2_2 = new("Cvičení z Češtiny", false, 50, Czech2_2, exercises);
            Lecture LectureFromEnglish2_2 = new("Přednáška z Angličtiny", false, 50, English2_2, lectures, Pavel);
            Lecture LectureFromCzech2_2 = new("Přednáška z Češtiny", false, 50, Czech2_2, lectures, Aneta);
            
            Exercise ExerciseFromEnglish3_2 = new("Cvičení z Angličtiny", false, 50, English3_2, exercises);
            Exercise ExerciseFromCzech3_2 = new("Cvičení z Češtiny", false, 50, Czech3_2, exercises);
            Lecture LectureFromEnglish3_2 = new("Přednáška z Angličtiny", false, 50, English3_2, lectures, Pavel);
            Lecture LectureFromCzech3_2 = new("Přednáška z Češtiny", false, 50, Czech3_2, lectures, Aneta);
            
            //string whoIAm = null;
            string whoIAm = "1";
            Teacher chosenTeacher = null;
            Student chosenStudent = null;
            Exercise chosenExercise = null;
            Subject chosenSubject = null;
            Lecture chosenLecture = null;
            
            Method Method = new();
            Method.mainMenu(chosenExercise, chosenStudent, chosenTeacher, chosenSubject, students, teachers, whoIAm, chosenLecture, exercises, subjects, lectures, currentSemester);
        }
    }
}