using System;
using System.Collections.Generic;

namespace UkolZakladyOOP
{
    public class Method
    {
        public void mainMenu(Exercise chosenExercise, Student chosenStudent, Teacher chosenTeacher, Subject chosenSubject, List<Student> students, List<Teacher> teachers, string whoIAm, Lecture chosenLecture, List<Exercise> exercises, List<Subject> subjects, List<Lecture> lectureList, Semester currentSemester)
        {
            do
            {
                Console.WriteLine("Kdo jsi?");
                Console.WriteLine("1) Student");
                Console.WriteLine("2) Ucitel");
                whoIAm = Console.ReadLine().ToLower();
                Console.Clear();
            }
            while (whoIAm != "1" && whoIAm != "2");

            switch (whoIAm)
            {
                case "1":
                    Student.selectStudent(students, ref chosenStudent);
                    studentMenu(whoIAm, chosenStudent, chosenTeacher, students, teachers, chosenExercise, chosenSubject, chosenLecture, exercises, subjects, lectureList, currentSemester);
                    break;

                case "2":
                    Teacher.selectTeacher(teachers, ref chosenTeacher);
                    teacherMenu(chosenStudent, whoIAm, chosenTeacher, chosenExercise, chosenSubject, students, teachers, chosenLecture, exercises, subjects, lectureList, ref currentSemester);
                    break;
            }
        }
        
        public void studentMenu(string whoIAm, Student chosenStudent, Teacher chosenTeacher, List<Student> students, List<Teacher> teachers, Exercise chosenExercise, Subject chosenSubject, Lecture chosenLecture, List<Exercise> exercises, List<Subject> subjects, List<Lecture> lectureList, Semester currentSemester)
        {
            int optionAsInt;
            do
            {
                listAllChoices(whoIAm, currentSemester);

                if (chosenStudent.registredSubjects.Count != 0)
                {
                    Console.WriteLine("8) Jít na přednášku");
                    Console.WriteLine("9) Konec předmětu");
                    Console.WriteLine("10) Udělat cvičení");
                }

                string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);

                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            chosenStudent.registerSubject(ref chosenStudent, currentSemester);
                            break;

                        case 2:
                            chosenStudent.listAllMySubjects(currentSemester);
                            break;

                        case 3:
                            mainMenu(chosenExercise, chosenStudent, chosenTeacher, chosenSubject, students, teachers, whoIAm, chosenLecture, exercises, subjects, lectureList, currentSemester);
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
                            Teacher.listAllTeachers(teachers);
                            Console.ReadKey();
                            break;

                        case 8:
                            chosenStudent.goOnLecture(ref chosenStudent, ref chosenLecture, ref chosenSubject, lectureList);
                            break;

                        case 9:
                            chosenStudent.endSubject(ref chosenStudent, ref Student.markSubjectList);
                            Console.ReadKey();
                            break;

                        case 10:
                            chosenStudent.doExercise(ref chosenStudent, ref chosenExercise, ref chosenSubject);
                            Console.ReadKey();
                            break;

                    }
                }
                else
                {
                    Console.Clear();
                    studentMenu(whoIAm, chosenStudent, chosenTeacher, students, teachers, chosenExercise, chosenSubject, chosenLecture, exercises, subjects, lectureList, currentSemester);
                }

            }
            while (optionAsInt > 0 && optionAsInt < 11);
        }

        public void teacherMenu(Student chosenStudent, string whoIAm, Teacher chosenTeacher, Exercise chosenExercise, Subject chosenSubject, List<Student> students, List<Teacher> teachers, Lecture chosenLecture, List<Exercise> exercises, List<Subject> subjects, List<Lecture> lectureList, ref Semester currentSemester)
        {
            int optionAsInt;
            do
            {
                listAllChoices(whoIAm, currentSemester);
                string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);

                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            chosenTeacher.registerSubject(ref chosenTeacher, currentSemester, subjects);
                            break;

                        case 2:
                            chosenTeacher.listAllMySubjects(currentSemester);
                            Console.ReadKey();
                            break;

                        case 3:
                            mainMenu(chosenExercise, chosenStudent, chosenTeacher, chosenSubject, students, teachers, whoIAm, chosenLecture, exercises, subjects, lectureList, currentSemester);
                            break;

                        case 4:
                            chosenTeacher.aboutMe();
                            Console.ReadKey();
                            break;

                        case 5:
                            Environment.Exit(0);
                            break;

                        case 6:
                            chosenTeacher.createSubject(ref students, ref teachers, subjects);
                            break;

                        case 7:
                            chosenTeacher.createExercise(ref chosenTeacher, ref chosenSubject, exercises, subjects);
                            break;

                        case 8:
                            chosenTeacher.listAllExercise();
                            Console.ReadKey();
                            break;

                        case 9:
                            Teacher.listStudentsByAverageMarks(students);
                            break;

                        case 10:
                            chosenTeacher.createLecture(ref chosenTeacher, ref chosenSubject, subjects, teachers);
                            break;
                        case 11:
                            Teacher.listAllLecture(teachers);
                            break;
                        
                        case 12:
                            Student.nextSemester(ref currentSemester);
                            break;
                        
                        case 13:
                            Student.nextYear(students);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    teacherMenu(chosenStudent, whoIAm, chosenTeacher, chosenExercise, chosenSubject, students, teachers, chosenLecture, exercises, subjects, lectureList, ref currentSemester);
                }

            }
            while (optionAsInt > 0 && optionAsInt < 14);
        }

        public void listAllChoices(string whoIAm, Semester currentSemester)
        {
            Console.Clear();
            Console.WriteLine("Aktuální semestr: " + currentSemester);
            Console.WriteLine("1) Zapsat se na předmět");
            Console.WriteLine("2) Moje předměty");
            Console.WriteLine("3) Změnit osobu");
            Console.WriteLine("4) Kdo jsem");
            Console.WriteLine("5) Konec programu");

            switch (whoIAm)
            {
                case "1":
                    Console.WriteLine("6) Vypsat dokončené předměty");
                    Console.WriteLine("7) Seznam všech učitelů");
                    break;

                case "2":
                    Console.WriteLine("6) Vytvořit nový předmět");
                    Console.WriteLine("7) Vytvořit nové cvičení");
                    Console.WriteLine("8) Seznam všech cvičení");
                    Console.WriteLine("9) Průměr všech známek u studentů");
                    Console.WriteLine("10) Vytvořit novou přednášku");
                    Console.WriteLine("11) Seznam všech přednášek");
                    Console.WriteLine("12) Další semestr");
                    Console.WriteLine("13) Další ročník");
                    break;
            }
        }
    }
}