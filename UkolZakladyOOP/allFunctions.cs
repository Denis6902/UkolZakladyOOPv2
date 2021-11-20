﻿using System;
using System.Collections.Generic;
using System.Threading;

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
                //whoIAm = Console.ReadLine().ToLower();
                Console.WriteLine("whoIAm = " + whoIAm);
                Thread.Sleep(2500);
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
            int optionAsInt = 0;
            //int optionAsInt;
            do
            {
                listAllChoices(whoIAm, currentSemester);
                /*if (chosenStudent.registredSubjects.Count != 0)
                {
                    Console.WriteLine("7) Jít na přednášku");
                    Console.WriteLine("8) Konec předmětu");
                    Console.WriteLine("5) Udělat cvičení");
                }*/

                /*string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);*/
                
                bool number = true;
                optionAsInt++;
                Thread.Sleep(2500);
                Console.Clear();
                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            Console.WriteLine("optionAsInt = 1");
                            chosenStudent.registerSubject(currentSemester);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 2:
                            Console.WriteLine("optionAsInt = 2");
                            chosenStudent.listAllMySubjects(currentSemester);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine("optionAsInt = 3");
                            //mainMenu(chosenExercise, chosenStudent, chosenTeacher, chosenSubject, students, teachers, whoIAm, chosenLecture, exercises, subjects, lectureList, currentSemester);
                            Console.WriteLine("Spustí znovu celý program");
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 4:
                            Console.WriteLine("optionAsInt = 4");
                            chosenStudent.aboutMe();
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 5:
                            Console.WriteLine("optionAsInt = 5");
                            chosenStudent.doExercise();
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        
                        case 6:
                            Console.WriteLine("optionAsInt = 6");
                            Teacher.listAllTeachers(teachers);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine("optionAsInt = 7");
                            chosenStudent.goOnLecture(lectureList);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 8:
                            Console.WriteLine("optionAsInt = 8");
                            chosenStudent.endSubject(ref Student.markSubjectList);
                            Thread.Sleep(2500);
                            Console.Clear();                            
                            break;
                        
                        case 9:
                            Console.WriteLine("optionAsInt = 9");
                            chosenStudent.listCompletedSubjects();
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        
                        case 10:
                            Console.WriteLine("optionAsInt = 10");
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                    }

                    if (optionAsInt == 11)
                    {
                        whoIAm = "2";
                        mainMenu(chosenExercise, chosenStudent, chosenTeacher, chosenSubject, students, teachers, whoIAm, chosenLecture, exercises, subjects, lectureList, currentSemester);
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
            int optionAsInt = 9;
            do
            {
                listAllChoices(whoIAm, currentSemester);
                /*string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);*/
                
                
                bool number = true;
                optionAsInt++;
                Thread.Sleep(2500);
                Console.Clear();
                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            Console.WriteLine("optionAsInt = 1");
                            chosenTeacher.registerSubject(currentSemester, subjects);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 2:
                            Console.WriteLine("optionAsInt = 2");
                            chosenTeacher.listAllMySubjects(currentSemester);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine("optionAsInt = 3");
                            //mainMenu(chosenExercise, chosenStudent, chosenTeacher, chosenSubject, students, teachers, whoIAm, chosenLecture, exercises, subjects, lectureList, currentSemester);
                            Console.WriteLine("Spustí znovu celý program");
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 4:
                            Console.WriteLine("optionAsInt = 4");
                            chosenTeacher.aboutMe();
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 5:
                            Console.WriteLine("optionAsInt = 5");
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 6:
                            Console.WriteLine("optionAsInt = 6");
                            chosenTeacher.createSubject(ref students, ref teachers, subjects);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine("optionAsInt = 7");
                            chosenTeacher.createExercise(exercises, subjects);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 8:
                            Console.WriteLine("optionAsInt = 8");
                            chosenTeacher.listAllExercise();
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 9:
                            Console.WriteLine("optionAsInt = 9");
                            Teacher.listStudentsByAverageMarks(students);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;

                        case 10:
                            Console.WriteLine("optionAsInt = 10");
                            chosenTeacher.createLecture(subjects, teachers);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        
                        case 11:
                            Console.WriteLine("optionAsInt = 11");
                            Teacher.listAllLecture(teachers);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        
                        case 12:
                            Console.WriteLine("optionAsInt = 12");
                            Student.nextSemester(ref currentSemester);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                        
                        case 13:
                            Console.WriteLine("optionAsInt = 13");
                            Student.nextYear(students);
                            Thread.Sleep(2500);
                            Console.Clear();
                            break;
                    }
                    
                    if (optionAsInt == 14)
                    {
                        Environment.Exit(0);
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

            
            switch (whoIAm)
            {
                case "1":
                    Console.WriteLine("1) Zapsat se na předmět");
                    Console.WriteLine("2) Moje předměty");
                    Console.WriteLine("3) Změnit osobu / Hlavní menu");
                    Console.WriteLine("4) Kdo jsem");
                    Console.WriteLine("5) Udělat cvičení");
                    Console.WriteLine("6) Seznam všech učitelů");
                    Console.WriteLine("7) Jít na přednášku");
                    Console.WriteLine("8) Konec předmětu");
                    Console.WriteLine("9) Vypsat dokončené předměty");
                    Console.WriteLine("10) Konec programu");
                    break;

                case "2":
                    Console.WriteLine("1) Zapsat se na předmět");
                    Console.WriteLine("2) Moje předměty");
                    Console.WriteLine("3) Změnit osobu / Hlavní menu");
                    Console.WriteLine("4) Kdo jsem");
                    Console.WriteLine("5) Konec programu");
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