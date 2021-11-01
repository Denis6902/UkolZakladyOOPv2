using System;
using System.Collections.Generic;

namespace UkolZakladyOOP
{
    public class Method
    {
        public void mainMenu(Exercise chosenExercise, Exercise DefaultExercise, Student chosenStudent, Student DefaultStudent, Teacher chosenTeacher, Teacher DefaultTeacher, Subject chosenSubject, Subject DefaultSubject, string who, bool end, List<Student> students, List<Teacher> teachers, string whoIAm, string option, string subject, List<string> averageMarksList, List<Lecture> lectures, Lecture chosenLecture, Lecture DefaultLecture, List<Exercise> exercises, List<Subject> subjects)
        {
            resetAllChosenObjects(ref chosenExercise, ref DefaultExercise, ref chosenStudent, ref DefaultStudent, ref DefaultTeacher, ref chosenTeacher, ref DefaultSubject, ref chosenSubject);

            do
            {
                Console.WriteLine("Kdo jsi?");
                Console.WriteLine("Student");
                Console.WriteLine("Ucitel");
                whoIAm = Console.ReadLine().ToLower();
                Console.Clear();
            }
            while (whoIAm != "student" && whoIAm != "ucitel");

            switch (whoIAm)
            {
                case "student":
                    DefaultStudent.selectStudent(students, ref chosenStudent);
                    studentMenu(whoIAm, chosenStudent, DefaultStudent, chosenTeacher, DefaultTeacher, option, subject, who, end, students, teachers, chosenExercise, DefaultExercise, chosenSubject, DefaultSubject, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
                    break;

                case "ucitel":
                    DefaultTeacher.selectTeacher(teachers, ref chosenTeacher);
                    teacherMenu(chosenStudent, DefaultStudent, whoIAm, chosenTeacher, DefaultTeacher, option, subject, who, end, chosenExercise, DefaultExercise, chosenSubject, DefaultSubject, students, teachers, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
                    break;
            }
        }

        public void resetAllChosenObjects(ref Exercise chosenExercise, ref Exercise DefaultExercise, ref Student chosenStudent, ref Student DefaultStudent, ref Teacher DefaultTeacher, ref Teacher chosenTeacher, ref Subject DefaultSubject, ref Subject chosenSubject)
        {
            chosenExercise = DefaultExercise;
            chosenStudent = DefaultStudent;
            chosenTeacher = DefaultTeacher;
            chosenSubject = DefaultSubject;
        }

        public void studentMenu(string whoIAm, Student chosenStudent, Student DefaultStudent, Teacher chosenTeacher, Teacher DefaultTeacher, string option, string subject, string who, bool end, List<Student> students, List<Teacher> teachers, Exercise chosenExercise, Exercise DefaultExercise, Subject chosenSubject, Subject DefaultSubject, List<string> averageMarksList, List<Lecture> lectures, Lecture chosenLecture, Lecture DefaultLecture, List<Exercise> exercises, List<Subject> subjects)
        {
            int optionAsInt;
            do
            {
                listAllChoices(whoIAm);

                if (chosenStudent.registredSubjects.Count != 0)
                {
                    Console.WriteLine("8) Jít na přednášku");
                    Console.WriteLine("9) Konec předmětu");
                    Console.WriteLine("10) Udělat cvičení");
                }

                option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);

                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            chosenStudent.registerSubject(ref chosenStudent);
                            break;

                        case 2:
                            chosenStudent.listAllSubjects();
                            break;

                        case 3:
                            mainMenu(chosenExercise, DefaultExercise, chosenStudent, DefaultStudent, chosenTeacher, DefaultTeacher, chosenSubject, DefaultSubject, who, end, students, teachers, whoIAm, option, subject, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
                            break;

                        case 4:
                            chosenStudent.aboutMe();
                            Console.ReadKey();
                            break;

                        case 5:
                            Environment.Exit(0);
                            break;

                        case 6:
                            chosenStudent.listCompletedSubjects();
                            Console.ReadKey();
                            break;

                        case 7:
                            chosenStudent.listAllTeachers(teachers);
                            Console.ReadKey();
                            break;

                        case 8:
                            chosenStudent.goOnLecture(lectures, ref chosenStudent, ref chosenLecture, ref chosenSubject, DefaultSubject, DefaultLecture);
                            break;

                        case 9:
                            chosenStudent.endSubject(ref chosenStudent);
                            Console.ReadKey();
                            break;

                        case 10:
                            chosenStudent.doExercise(ref chosenStudent, ref chosenExercise, DefaultExercise, ref chosenSubject, DefaultSubject);
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    studentMenu(whoIAm, chosenStudent, DefaultStudent, chosenTeacher, DefaultTeacher, option, subject, who, end, students, teachers, chosenExercise, DefaultExercise, chosenSubject, DefaultSubject, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
                }

            }
            while (optionAsInt > 0 && optionAsInt < 11);
        }

        public void teacherMenu(Student chosenStudent, Student DefaultStudent, string whoIAm, Teacher chosenTeacher, Teacher DefaultTeacher, string option, string subject, string who, bool end, Exercise chosenExercise, Exercise DefaultExercise, Subject chosenSubject, Subject DefaultSubject, List<Student> students, List<Teacher> teachers, List<string> averageMarksList, List<Lecture> lectures, Lecture chosenLecture, Lecture DefaultLecture, List<Exercise> exercises, List<Subject> subjects)
        {
            int optionAsInt;
            do
            {
                listAllChoices(whoIAm);
                option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);

                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            chosenTeacher.registerSubject(ref chosenTeacher);
                            break;

                        case 2:
                            chosenTeacher.listAllSubjects();
                            Console.ReadKey();
                            break;

                        case 3:
                            mainMenu(chosenExercise, DefaultExercise, chosenStudent, DefaultStudent, chosenTeacher, DefaultTeacher, chosenSubject, DefaultSubject, who, end, students, teachers, whoIAm, option, subject, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
                            break;

                        case 4:
                            chosenTeacher.aboutMe();
                            Console.ReadKey();
                            break;

                        case 5:
                            Environment.Exit(0);
                            break;

                        case 6:
                            chosenTeacher.createSubject(ref students, ref teachers, DefaultTeacher, subjects);
                            break;

                        case 7:
                            chosenTeacher.createExercise(ref chosenTeacher, ref chosenSubject, exercises, subjects);
                            break;

                        case 8:
                            chosenTeacher.listAllExercise();
                            Console.ReadKey();
                            break;

                        case 9:
                            chosenTeacher.listStudentsByAverageMarks(students, ref averageMarksList);
                            break;

                        case 10:
                            chosenTeacher.createLecture(ref chosenTeacher, ref chosenSubject, subjects);
                            break;
                        case 11:
                            chosenTeacher.listAllLecture(teachers);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    teacherMenu(chosenStudent, DefaultStudent, whoIAm, chosenTeacher, DefaultTeacher, option, subject, who, end, chosenExercise, DefaultExercise, chosenSubject, DefaultSubject, students, teachers, averageMarksList, lectures, chosenLecture, DefaultLecture, exercises, subjects);
                }

            }
            while (optionAsInt > 0 && optionAsInt < 12);
        }

        public void listAllChoices(string who)
        {
            Console.Clear();
            Console.WriteLine("1) Zapsat se na předmět");
            Console.WriteLine("2) Moje předměty");
            Console.WriteLine("3) Změnit osobu");
            Console.WriteLine("4) Kdo jsem");
            Console.WriteLine("5) Konec programu");

            switch (who)
            {
                case "student":
                    Console.WriteLine("6) Vypsat dokončené předměty");
                    Console.WriteLine("7) Seznam všech učitelů");
                    break;

                case "ucitel":
                    Console.WriteLine("6) Vytvořit nový předmět");
                    Console.WriteLine("7) Vytvořit nové cvičení");
                    Console.WriteLine("8) Seznam všech cvičení");
                    Console.WriteLine("9) Průměr všech známek u studentů");
                    Console.WriteLine("10) Vytvořit novou přednášku");
                    Console.WriteLine("11) Seznam všech přednášek");
                    break;
            }
        }
    }
}