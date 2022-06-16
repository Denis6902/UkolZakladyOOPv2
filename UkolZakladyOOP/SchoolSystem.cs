using System;
using System.Threading;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Hlavní třída celého programu
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
        /// Počet kreditů potřeba k postupu do dalšího ročníku
        /// </summary>
        private int CreditsToAdvancement = 50;

        public SchoolSystem()
        {
            fillLists(); // naplnění seznamů daty
        }

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
                    Thread.Sleep(Delay);
                    studentMenu(ChosenStudent);
                    break;

                case "2": // výběr učitele
                    Console.WriteLine("Kdo jsi?");
                    Teacher ChosenTeacher = Teacher.selectTeacher();
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
                optionAsInt++; // automatický průchod - při každé iteraci cyklu se spustí další volba


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
                            ChosenStudent.checkNextSemester(CreditsToAdvancement);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 11:
                            Console.WriteLine("optionAsInt = 11"); // konec programu
                            //Environment.Exit(0);
                            Console.WriteLine("Ukončí program");
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 12:
                            Console.WriteLine("Ukázka prerekvizit (optionAsInt = 1)"); // Ukázka prerekvizit
                            ChosenStudent.registerSubject(CurrentSemester);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 13:
                            Console.Clear();
                            mainMenu(); // znovu spuštění hlavního menu, kvůli přepnutí na učitele
                            break;
                    }
                }
            } while (optionAsInt is > 0 and < 13);
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
                            Teacher.createSubject(Delay);
                            Thread.Sleep(Delay);
                            Console.Clear();
                            break;

                        case 7:
                            Console.WriteLine($"optionAsInt = {optionAsInt}"); // vytvoření nového cvíčení
                            Teacher.createExercise(Delay);
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
                            Teacher.createLecture(Delay);
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
                            CurrentSemester = Teacher.nextSemester(CurrentSemester, CreditsToAdvancement);
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
                    Console.WriteLine("10) Další semestr");
                    Console.WriteLine("11) Konec programu");
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

        /// <summary>
        /// Metoda k naplnění všech seznamů
        /// </summary>
        private void fillLists()
        {
            Subject.SubjectsTypes.Add(new SubjectType("Czech", true));
            Subject.SubjectsTypes.Add(new SubjectType("English", true));
            Subject.SubjectsTypes.Add(new SubjectType("xxx", false));

            Teacher Pavel = new("Ing.", "Pavel", "Novotný", new DateTime(1980, 2, 9));
            Teacher Aneta = new("Mgr.", "Aneta", "Nováková", new DateTime(1987, 1, 8));

            SubjectType SubjectTypeCzech = Subject.SubjectsTypes.Find(ST => ST.Name == "Czech");
            SubjectType SubjectTypeEnglish = Subject.SubjectsTypes.Find(ST => ST.Name == "English");
            SubjectType SubjectTypeXxx = Subject.SubjectsTypes.Find(ST => ST.Name == "xxx");

            Subject English1_1 = new("English1_1", SubjectTypeEnglish, Pavel, Pavel, 50, 1, Semester.Summer, 1);
            Subject xxx1_1 = new("xxx1_1", SubjectTypeXxx, Aneta, null, 50, 1, Semester.Summer, 1); // TEST_ONLY
            Subject Czech1_1 = new("Czech1_1", SubjectTypeCzech, Aneta, Aneta, 50, 1, Semester.Winter, 1);

            Subject English2_1 = new("English2_1", SubjectTypeEnglish, Pavel, Pavel, 50, 2, Semester.Summer, 1);
            Subject Czech2_1 = new("Czech2_1", SubjectTypeCzech, Aneta, Aneta, 50, 2, Semester.Winter, 1);

            Subject English3_1 = new("English3_1", SubjectTypeEnglish, Pavel, Pavel, 50, 3, Semester.Summer, 1);
            Subject Czech3_1 = new("Czech3_1", SubjectTypeCzech, Aneta, Aneta, 50, 3, Semester.Winter, 1);

            Subject English1_2 = new("English1_2", SubjectTypeEnglish, Pavel, Pavel, 50, 1, Semester.Summer, 2);
            Subject xxx_2 = new("xxx1_2", SubjectTypeXxx, Aneta, Aneta, 50, 1, Semester.Summer, 2); // TEST_ONLY
            Subject Czech1_2 = new("Czech1_2", SubjectTypeCzech, Aneta, Aneta, 50, 1, Semester.Winter, 2);

            Subject English2_2 = new("English2_2", SubjectTypeEnglish, Pavel, Pavel, 50, 2, Semester.Summer, 2);
            Subject Czech2_2 = new("Czech2_2", SubjectTypeCzech, Aneta, Aneta, 50, 2, Semester.Winter, 2);

            Subject English3_2 = new("English3_2", SubjectTypeEnglish, Pavel, Pavel, 50, 3, Semester.Summer, 2);
            Subject Czech3_2 = new("Czech3_2", SubjectTypeCzech, Aneta, Aneta, 50, 3, Semester.Winter, 2);

            Lecture.LecturesTypes.Add(new LectureType("Přednáška z Češtiny", SubjectTypeCzech, true));
            Lecture.LecturesTypes.Add(new LectureType("Přednáška z Angličtiny", SubjectTypeEnglish, true));
            Lecture.LecturesTypes.Add(new LectureType("ppp", SubjectTypeXxx, false));

            Exercise.ExercisesTypes.Add(new ExerciseType("Cvičení z Češtiny", SubjectTypeCzech, true));
            Exercise.ExercisesTypes.Add(new ExerciseType("Cvičení z Angličtiny", SubjectTypeEnglish, true));
            Exercise.ExercisesTypes.Add(new ExerciseType("ooo", SubjectTypeXxx, false));

            LectureType LectureTypeCzech = Lecture.LecturesTypes.Find(LT => LT.Name == "Přednáška z Češtiny");
            LectureType LectureTypeEnglish = Lecture.LecturesTypes.Find(LT => LT.Name == "Přednáška z Angličtiny");
            LectureType LectureTypePpp = Lecture.LecturesTypes.Find(LT => LT.Name == "ppp");

            ExerciseType ExerciseTypeCzech = Exercise.ExercisesTypes.Find(LT => LT.Name == "Cvičení z Češtiny");
            ExerciseType ExerciseTypeEnglish = Exercise.ExercisesTypes.Find(LT => LT.Name == "Cvičení z Angličtiny");
            ExerciseType ExerciseTypePpp = Exercise.ExercisesTypes.Find(LT => LT.Name == "ooo");

            Exercise ooo1_1 = new Exercise("ooo1_1", ExerciseTypePpp, false, xxx1_1); // TEST_ONLY
            Lecture ppp1_1 = new("ppp1_1", LectureTypePpp, false, xxx1_1); // TEST_ONLY

            Student Jakub = new("Jakub", "Novák", new DateTime(1999, 7, 2), new DateTime(2020, 10, 1),
                1);
            Student Pepa = new("Pepa", "Nový", new DateTime(1998, 9, 3), new DateTime(2020, 10, 2),
                1);
            Student Denis = new("Denis", "Vojtěch", new DateTime(1984, 9, 3), new DateTime(2020, 1, 2),
                1);

            Exercise ExerciseFromEnglish1_1 =
                new("Cvičení z Angličtiny1_1", ExerciseTypeEnglish, false, English1_1);
            Exercise ExerciseFromCzech1_1 = new("Cvičení z Češtiny1_1", ExerciseTypeCzech, false, Czech1_1);
            Lecture LectureFromEnglish1_1 = new("Přednáška z Angličtiny1_1", LectureTypeEnglish, false, English1_1);
            Lecture LectureFromCzech1_1 = new("Přednáška z Češtiny1_1", LectureTypeCzech, false, Czech1_1);

            Exercise ExerciseFromEnglish2_1 =
                new("Cvičení z Angličtiny2_1", ExerciseTypeEnglish, false, English2_1);
            Exercise ExerciseFromCzech2_1 = new("Cvičení z Češtiny2_1", ExerciseTypeCzech, false, Czech2_1);
            Lecture LectureFromEnglish2_1 = new("Přednáška z Angličtiny2_1", LectureTypeEnglish, false, English2_1);
            Lecture LectureFromCzech2_1 = new("Přednáška z Češtiny2_1", LectureTypeEnglish, false, Czech2_1);

            Exercise ExerciseFromEnglish3_1 =
                new("Cvičení z Angličtiny3_1", ExerciseTypeEnglish, false, English3_1);
            Exercise ExerciseFromCzech3_1 = new("Cvičení z Češtiny3_1", ExerciseTypeCzech, false, Czech3_1);
            Lecture LectureFromEnglish3_1 = new("Přednáška z Angličtiny3_1", LectureTypeEnglish, false, English3_1);
            Lecture LectureFromCzech3_1 = new("Přednáška z Češtiny3_1", LectureTypeEnglish, false, Czech3_1);

            Exercise ExerciseFromEnglish1_2 =
                new("Cvičení z Angličtiny1_2", ExerciseTypeEnglish, false, English1_2);
            Exercise ExerciseFromCzech1_2 = new("Cvičení z Češtiny1_2", ExerciseTypeCzech, false, Czech1_2);
            Lecture LectureFromEnglish1_2 = new("Přednáška z Angličtiny1_2", LectureTypeEnglish, false, English1_2);
            Lecture LectureFromCzech1_2 = new("Přednáška z Češtiny1_2", LectureTypeEnglish, false, Czech1_2);

            Exercise ExerciseFromEnglish2_2 =
                new("Cvičení z Angličtiny2_2", ExerciseTypeEnglish, false, English2_2);
            Exercise ExerciseFromCzech2_2 = new("Cvičení z Češtiny2_2", ExerciseTypeCzech, false, Czech2_2);
            Lecture LectureFromEnglish2_2 = new("Přednáška z Angličtiny2_2", LectureTypeEnglish, false, English2_2);
            Lecture LectureFromCzech2_2 = new("Přednáška z Češtiny2_2", LectureTypeEnglish, false, Czech2_2);

            Exercise ExerciseFromEnglish3_2 =
                new("Cvičení z Angličtiny3_2", ExerciseTypeEnglish, false, English3_2);
            Lecture LectureFromEnglish3_2 = new("Přednáška z Angličtiny3_2", LectureTypeEnglish, false, English3_2);
        }
    }
}