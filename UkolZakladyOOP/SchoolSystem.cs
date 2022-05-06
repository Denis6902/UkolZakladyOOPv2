using System;
using System.Threading;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Hlavní třída celeého programu
    /// </summary>
    public class SchoolSystem
    {
        /// <summary>
        /// Aktuální vybraný typ osoby (Student = 1, Učitel = 2)
        /// </summary>
        private string WhoIAm;

        /// <summary>
        /// Délka prodlevy mezi metodama
        /// </summary>
        private const int Delay = 2500;

        /// <summary>
        /// Aktuální semestr
        /// </summary>
        private Semester CurrentSemester = Semester.Summer;

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
                switch (WhoIAm) // 1 = Student, 2 - Učitel
                {
                    case null:
                        WhoIAm = "1";
                        break;
                    case "1":
                        WhoIAm = "2";
                        break;
                }
                
                Console.WriteLine($"WhoIAm = {WhoIAm}");
                Thread.Sleep(Delay);
                Console.Clear();
            } while (WhoIAm != "1" && WhoIAm != "2");

            switch (WhoIAm)
            {
                case "1": // výběr studenta
                    Console.WriteLine("Kdo jsi?");
                    Student ChosenStudent = Student.selectStudent();
                    Console.WriteLine($"ChosenStudent = {ChosenStudent.returnFullName()}");
                    Thread.Sleep(Delay);
                    studentMenu(ChosenStudent);
                    break;

                case "2": // výběr učitele
                    Console.WriteLine("Kdo jsi?");
                    Teacher ChosenTeacher = Teacher.selectTeacher();
                    Console.WriteLine($"chosenTeacher = {ChosenTeacher.returnFullName()}");
                    Thread.Sleep(Delay);
                    teacherMenu(ChosenTeacher);
                    break;
            }
        }

        /// <summary>
        /// Menu studenta
        /// </summary>
        /// <param name="ChosenStudent">Daný student</param>
        private void studentMenu(Student ChosenStudent)
        {
            int optionAsInt = 0;
            //int optionAsInt;
            do
            {
                listAllChoices(ChosenStudent); // výpis jednotlivých možností
                
                /*string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);*/

                bool number = true;
                optionAsInt++;// automatický průchod - při každé iteraci cyklu se spustí další volba


                Thread.Sleep(Delay);
                Console.Clear();
                if (number) // kontrola jestli je optionAsInt číslo
                {
                    switch (optionAsInt) // jednotlívé volby
                    {
                        case 1:
                            Console.WriteLine("optionAsInt = 1");
                            ChosenStudent.registerSubject(CurrentSemester); // zapsaní se na předmět
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 2:
                            Console.WriteLine("optionAsInt = 2"); // zapsané předměty
                            ChosenStudent.listAllMySubjects(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine("optionAsInt = 3"); // Hlavní menu
                            //mainMenu();
                            Console.WriteLine("Spustí znovu hlavní menu");
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 4:
                            Console.WriteLine("optionAsInt = 4"); // Informace o aktuálním studentovi
                            ChosenStudent.aboutMe();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 5:
                            Console.WriteLine("optionAsInt = 5"); // udělat cvičení
                            ChosenStudent.doExercise(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 6:
                            Console.WriteLine("optionAsInt = 6"); // Seznam všech učitelů
                            Teacher.listAllTeachers();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine("optionAsInt = 7"); // jít na přednášku
                            ChosenStudent.goOnLecture(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 8:
                            Console.WriteLine("optionAsInt = 8"); // konec předmětu
                            ChosenStudent.endSubject();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 9:
                            Console.WriteLine("optionAsInt = 9"); // vypsat dokončené předměty
                            ChosenStudent.listCompletedSubjects();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 10:
                            Console.WriteLine("optionAsInt = 10"); // konec programu
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 11:
                            Console.WriteLine("Ukázka prerekvizit (optionAsInt = 1)"); // Ukázka prerekvizit
                            ChosenStudent.registerSubject(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;
                    }

                    if (optionAsInt == 12)
                    {
                        mainMenu(); // znovu spuštění hlavního menu, kvůli přepnutí na učitele
                    }
                }
            } while (optionAsInt is > 0 and < 12);
        }

        /// <summary>
        /// Menu učitele
        /// </summary>
        /// <param name="ChosenTeacher">Daný učitel</param>
        private void teacherMenu(Teacher ChosenTeacher)
        {
            int optionAsInt = 0;
            //int optionAsInt;

            do
            {
                listAllChoices(ChosenTeacher); // výpis jednotlivých možností

                /*string option = Console.ReadLine();
                Console.Clear();
                bool number = int.TryParse(option, out optionAsInt);*/


                bool number = true;
                optionAsInt++; // automatický průchod - při každé iteraci cyklu se spustí další volba

                Thread.Sleep(Delay);
                Console.Clear();
                if (number)
                {
                    switch (optionAsInt)
                    {
                        case 1:
                            Console.WriteLine(
                                $"optionAsInt = {optionAsInt}"); // zapsaní se na předmět, který ještě nikdo neučí
                            ChosenTeacher.registerSubject(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 2:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // předměty které daný učitel učí
                            ChosenTeacher.listAllMySubjects();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // spustí zvovu hlavní menu
                            //mainMenu(); // Hlavní menu
                            Console.WriteLine("Spustí znovu celý program");
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 4:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // Informace o aktuálním učitelovi
                            ChosenTeacher.aboutMe();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 5:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // Konec programu
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 6:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // vytvoření nového předmmětu
                            Teacher.createSubject();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // vytvoření nového cvíčení
                            Teacher.createExercise();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 8:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // výpis všech cvičení
                            Exercise.listAllExercise();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 9:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // průměr všech známek u studentů
                            Teacher.listStudentsByAverageMarks();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 10:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // vytvoření nové přednášky
                            Teacher.createLecture();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 11:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // výpis všech přednášek
                            Lecture.listAllLectures();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 12:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // další semestr
                            CurrentSemester = Student.nextSemester(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 13:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // výpis všech předmětů
                            Subject.listAllSubjects();
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;
                        case 14:
                            Environment.Exit(0); // konec programu
                            break;
                    }
                }
            } while (optionAsInt is > 0 and < 14);
        }

        /// <summary>
        /// Výpis všech možností
        /// </summary>
        /// <param name="ChosenPerson">Daná osoba, která metodu volá</param>
        private void listAllChoices(Person ChosenPerson)
        {
            Console.Clear();
            Console.WriteLine($"Aktuální semestr: {CurrentSemester}");

            switch (ChosenPerson) // podle toho kdo zavolá metodu, vypíše jednotlivé možnosti
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