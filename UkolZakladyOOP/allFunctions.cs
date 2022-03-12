using System;
using System.Threading;

namespace UkolZakladyOOP
{
    public class SchoolSystem
    {
        /// <summary>
        /// Aktuální vybraný typ osoby (Student = 1, Učitel = 2)
        /// </summary>
        private string whoIAm = null;

        /// <summary>
        /// Délka prodlevy mezi metodama
        /// </summary>
        private const int delay = 2500;

        /// <summary>
        /// Aktuální semestr
        /// </summary>
        private Semester currentSemester = Semester.Summer;

        /// <summary>
        /// Hlavní menu 
        /// </summary>
        public void mainMenu()
        {
            do
            {
                Console.WriteLine("Kdo jsi?");
                Console.WriteLine("1) Student");
                Console.WriteLine("2) Ucitel");
                switch (whoIAm) // 1 = Student, 2 - Učitel
                {
                    case null:
                        whoIAm = "1";
                        break;
                    case "1":
                        whoIAm = "2";
                        break;
                }

                //whoIAm = Console.ReadLine();
                Console.WriteLine("whoIAm = " + whoIAm);
                Thread.Sleep(delay);
                Console.Clear();
            } while (whoIAm != "1" && whoIAm != "2");

            switch (whoIAm)
            {
                case "1": // výběr studenta
                    Console.WriteLine("Kdo jsi?");
                    Student chosenStudent = Student.selectStudent();
                    Console.WriteLine($"chosenStudent = {chosenStudent.returnFullName()}");
                    Thread.Sleep(delay);
                    studentMenu(chosenStudent);
                    break;

                case "2": // výběr učitele
                    Console.WriteLine("Kdo jsi?");
                    Teacher chosenTeacher = Teacher.selectTeacher();
                    Console.WriteLine($"chosenTeacher = {chosenTeacher.returnFullName()}");
                    Thread.Sleep(delay);
                    teacherMenu(chosenTeacher);
                    break;
            }
        }

        /// <summary>
        /// Menu studenta
        /// </summary>
        /// <param name="chosenStudent">Daný student</param>
        private void studentMenu(Student chosenStudent)
        {
            int optionAsInt = 0;
            //int optionAsInt;
            do
            {
                listAllChoices(chosenStudent); // výpis jednotlivých možností

                /*string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);*/

                bool number = true;
                optionAsInt++; // automatický průchod - při každé iteraci cyklu se spustí další volba


                Thread.Sleep(delay);
                Console.Clear();
                if (number) // kontrola jestli je optionAsInt číslo
                {
                    switch (optionAsInt) // jednotlívé volby
                    {
                        case 1:
                            Console.WriteLine("optionAsInt = 1");
                            chosenStudent.registerSubject(currentSemester); // zapsaní se na předmět
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 2:
                            Console.WriteLine("optionAsInt = 2"); // zapsané předměty
                            chosenStudent.listAllMySubjects(currentSemester);
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine("optionAsInt = 3"); // Hlavní menu
                            //mainMenu(currentSemester, delay);
                            Console.WriteLine("Spustí znovu celý program");
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 4:
                            Console.WriteLine("optionAsInt = 4"); // Informace o aktuálním studentovi
                            chosenStudent.aboutMe();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 5:
                            Console.WriteLine("optionAsInt = 5"); // udělat cvičení
                            chosenStudent.doExercise(currentSemester);
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 6:
                            Console.WriteLine("optionAsInt = 6"); // Seznam všech učitelů
                            Teacher.listAllTeachers();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine("optionAsInt = 7"); // jít na přednášku
                            chosenStudent.goOnLecture(currentSemester);
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 8:
                            Console.WriteLine("optionAsInt = 8"); // konec předmětu
                            chosenStudent.endSubject();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 9:
                            Console.WriteLine("optionAsInt = 9"); // vypsat dokončené předměty
                            chosenStudent.listCompletedSubjects();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 10:
                            Console.WriteLine("optionAsInt = 10"); // konec programu
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 11:
                            Console.WriteLine("Ukázka prerekvizit (optionAsInt = 1)"); // Ukázka prerekvizit
                            chosenStudent.registerSubject(currentSemester);
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;
                    }

                    if (optionAsInt == 12)
                    {
                        mainMenu(); // znovu spuštění hlavního menu, kvůli přepnutí na učitele
                    }
                }
            } while (optionAsInt > 0 && optionAsInt < 12);
        }

        /// <summary>
        /// Menu učitele
        /// </summary>
        /// <param name="chosenTeacher">Daný učitel</param>
        private void teacherMenu(Teacher chosenTeacher)
        {
            int optionAsInt = 0;
            //int optionAsInt;

            do
            {
                listAllChoices(chosenTeacher); // výpis jednotlivých možností

                /*string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);*/


                bool number = true;
                optionAsInt++; // automatický průchod - při každé iteraci cyklu se spustí další volba

                Thread.Sleep(delay);
                Console.Clear();
                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            Console.WriteLine("optionAsInt = 1"); // zapsaní se na předmět, který ještě nikdo neučí
                            chosenTeacher.registerSubject(currentSemester);
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 2:
                            Console.WriteLine("optionAsInt = 2"); // předměty které daný učitel učí
                            chosenTeacher.listAllMySubjects();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine("optionAsInt = 3");
                            //mainMenu(currentSemester, delay); // Hlavní menu
                            Console.WriteLine("Spustí znovu celý program");
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 4:
                            Console.WriteLine("optionAsInt = 4"); // Informace o aktuálním učitelovi
                            chosenTeacher.aboutMe();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 5:
                            Console.WriteLine("optionAsInt = 5"); // Konec programu
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 6:
                            Console.WriteLine("optionAsInt = 6"); // vytvoření nového předmmětu
                            Teacher.createSubject();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine("optionAsInt = 7"); // vytvoření nového cvíčení
                            Teacher.createExercise();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 8:
                            Console.WriteLine("optionAsInt = 8"); // výpis všech cvičení
                            Teacher.listAllExercise();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 9:
                            Console.WriteLine("optionAsInt = 9"); // průměr všech známek u studentů
                            Teacher.listStudentsByAverageMarks();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 10:
                            Console.WriteLine("optionAsInt = 10"); // vytvoření nové přednášky
                            Teacher.createLecture();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 11:
                            Console.WriteLine("optionAsInt = 11"); // výpis všech přednášek
                            Teacher.listAllLectures();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 12:
                            Console.WriteLine("optionAsInt = 12"); // další semestr
                            currentSemester = Student.nextSemester(currentSemester);
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;

                        case 13:
                            Console.WriteLine("optionAsInt = 13"); // výpis všech předmětů
                            Subject.listAllSubjects();
                            Thread.Sleep(delay);
                            Console.Clear();
                            break;
                    }

                    if (optionAsInt == 14)
                    {
                        Environment.Exit(0); // konec programu
                    }
                }
            } while (optionAsInt > 0 && optionAsInt < 14);
        }

        /// <summary>
        /// Výpis všech možností
        /// </summary>
        /// <param name="chosenPerson">Daná osoba, která metodu volá</param>
        private void listAllChoices(Person chosenPerson)
        {
            Console.Clear();
            Console.WriteLine("Aktuální semestr: " + currentSemester);

            switch (chosenPerson) // podle toho kdo zavolá metodu, vypíše jednotlivé možnosti
            {
                case Student: //pokud metodu zavolá student
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

                case Teacher: // pokud metodu zavolá učitel
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
                    Console.WriteLine("13) Seznam všech předmětů");
                    break;
            }
        }
    }
}