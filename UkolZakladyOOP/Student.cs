using System;
using System.Collections.Generic;
using System.Linq;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Třída studenta
    /// </summary>
    public class Student : Person
    {
        /// <summary>
        /// Datum registrace
        /// </summary>
        private DateTime RegistrationDate;

        /// <summary>
        /// Seznam studentů, jejich předmětů a známek
        /// </summary>
        private List<SubjectMark> SubjectMarkList = new();

        /// <summary>
        /// V jakém je student ročníku
        /// </summary>
        public int Year;

        /// <summary>
        /// Seznam všech studentů
        /// </summary>
        public static List<Student> Students = new();

        /// <summary>
        /// Konstruktor. Přidá studenta do seznamu studentů
        /// </summary>
        /// <param name="firstName">Jméno</param>
        /// <param name="lastName">Příjmení</param>
        /// <param name="birthDate">Datum narození</param>
        /// <param name="registrationDate">Datum registrace</param>
        /// <param name="year">Aktuální ročník</param>
        public Student(string firstName, string lastName, DateTime birthDate, DateTime registrationDate,
            int year) : base(firstName,
            lastName, birthDate)
        {
            RegistrationDate = registrationDate;
            Year = year;
            Students.Add(this);
        }

        /// <summary>
        /// Změní ročník na další 
        /// </summary>
        private static void nextYear()
        {
            foreach (Student Student in Students)
            {
                Student.Year += 1;
                Console.WriteLine($"Aktuální ročník studenta {Student.returnFullName()} je {Student.Year}");
                Student.SubjectMarkList = null;
            }
        }

        /// <summary>
        /// Nastaví další semestr
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        /// <returns></returns>
        public static Semester nextSemester(Semester CurrentSemester)
        {
            switch (CurrentSemester)
            {
                case Semester.Summer:
                    CurrentSemester =
                        Semester.Winter; // pokud je aktuální semestr Summer, nastaví aktuální semestr na Winter
                    break;

                case Semester.Winter:
                    CurrentSemester =
                        Semester.Summer; // pokud je aktuální semestr Winter, nastaví aktuální semestr na Summer
                    Student.nextYear(); // nastaví další ročník
                    break;
                default:
                    Console.WriteLine("Žádný zadaný semestr???");
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine($"Aktuální semestr je: {CurrentSemester}");
            return CurrentSemester;
        }

        /// <summary>
        /// Vypíše informace o studentovi
        /// </summary>
        public override void aboutMe()
        {
            Console.WriteLine($"Dobrý den, jmenuji se {returnFullName()} a narodil/narodila " +
                              $"jsem se {BirthDate:MM.dd.yyyy}" +
                              $" a aktuálně studuji od {RegistrationDate:MM.dd.yyyy}");
        }

        /// <summary>
        /// Vypočítá průměr všech známek studenta
        /// </summary>
        /// <returns>Součet všech známek studenta / počet známek student</returns>
        public double calculateAverageMark()
        {
            // uloží do seznamu ChosenStudentMarks všechny předměty daného studenta co má oznámkované
            List<SubjectMark> ChosenStudentMarks =
                SubjectMarkList.FindAll(SM =>
                    !double.IsNaN(SM.Mark)); // vytvoří nový seznam známek jenom od daného studenta

            double sumOfAllMarks =
                ChosenStudentMarks.Sum(SM => SM.Mark); // uloží do sumOfAllMarks součet všech známek
            double countStudentMark = ChosenStudentMarks.Count; // uloží do sumOfAllMarks počet všech známek

            return sumOfAllMarks / countStudentMark;
        }

        /// <summary>
        /// Metoda k vybrání studenta
        /// </summary>
        /// <returns>Daný student</returns>
        public static Student selectStudent()
        {
            // projede všechny studenty
            foreach (Student Student in Students)
            {
                Console.WriteLine(Student.FirstName); // vypíše jména všech studentů ze seznamu students
            }

            //string studentName = Console.ReadLine();
            string studentName = "Pepa";

            if (!Students.Exists(Student =>
                    Student.FirstName.ToLower() ==
                    studentName.ToLower())) // jestli neexistuje student s daným jménem, spustí znovu metodu
            {
                Console.WriteLine("Neexistuje daný student");
                Console.WriteLine("Zadej jméno existujícího studenta");
                selectStudent();
            }

            Student ChosenStudent =
                Students.Find(Student =>
                    Student.FirstName.ToLower() == studentName.ToLower()); // uloží do chosenStudent daného studenta

            return ChosenStudent;
        }

        /// <summary>
        /// Metoda k registrování předmětu
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void registerSubject(Semester CurrentSemester)
        {
            if (Subject.Subjects.Count != SubjectMarkList.Count)
            {
                listSubjectsForRegister(CurrentSemester); // vypíše předměty dostupné k registraci

                Console.WriteLine("Zadejte název předmětu");
                //string subjectName = Console.ReadLine();
                string subjectName = "English1_1";
                Console.WriteLine($"subjectName = {subjectName}");

                foreach (Subject Subject in Subject.Subjects.Where(Subject =>
                             Subject.Name.ToLower() == subjectName.ToLower() && Subject.Year == Year &&
                             Subject.Semester == CurrentSemester && !SubjectMarkList.Exists(SM =>
                                 SM.Subject == Subject))) // projede neregistrované přeměty v aktuálním roce a semestru kde se název předmětu rovná danému názvu
                {
                    Console.WriteLine(
                        $"{returnFullName()} jsi zapsaný do {Subject.Name} předmětu, semestr: {Subject.Semester}");
                    SubjectMark SubjectMark = new(Subject, SubjectMarkList);
                    // vytvoří novou instanci SubjectMark a ta automaticky přidá předmět do seznamu předmětů a známek
                }
            }
            else
            {
                Console.WriteLine("Nemáš žádné nedokončené předměty");
            }
        }

        /// <summary>
        /// Výpis všech předmětů dostupných k registraci
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        private void listSubjectsForRegister(Semester CurrentSemester)
        {
            int maxSubjectLevel = 2; // maximální úroveň předmětu
            int subjectLevel = 1; // aktuální hledaná úroveň předmětu
            const int lengthForCompare = 3; // počet znaků, podle kterých bude porovnávat předměty
            string completedSubjectName = null; // ořezaný název dokončeného předmětuc 

            // projede všechny předměty a aktuálním semestru a ročníku a seřadí je podle názvu
            foreach (Subject Subject in Subject.Subjects.Where(Subject =>
                             Subject.Year == this.Year && Subject.Semester == CurrentSemester)
                         .OrderBy(subject => subject.Name))
            {
                // pokud je úroveň předmětu 1 a není v seznamu registrovaných předmětů a známek
                if (Subject.Level == 1 && !SubjectMarkList.Exists(SM => SM.Subject == Subject))
                {
                    Subject.writeSubjectInfo(Subject); // vypíše informace o předmětu
                }

                // projede seznam registrovaných předmětů a známek kde se předmět (z prvního Foreach) rovná předmětu v seznamu (SubjectMark)
                // a kde je zároveň daný předmět v seznamu (SubjectMark) dokončený
                foreach (SubjectMark SubjectMark in SubjectMarkList.Where(SM => SM.Subject == Subject && SM.Completed))
                {
                    if (subjectLevel < maxSubjectLevel) // kontrola jestli se nepřesáhla maximální možná úroveň předmětu
                    {
                        subjectLevel += 1; // zvyší úroveň předmětu, kterou hledáme o 1
                    }

                    completedSubjectName =
                        Subject.Name.Substring(0,
                            lengthForCompare); // uloží první (lengthForCompare) znaky do proměnné completedSubjectName
                }

                // pokud se úroveň předmětu který hledáme (subjectLevel)
                // rovná úrovni předmětu z foreach a daný předmět není v seznamu registrovaných předmětů a známek
                // a název předmětu se rovná názvu předmětu který hledáme (completedSubjectName)
                if (subjectLevel == Subject.Level && !SubjectMarkList.Exists(SM => SM.Subject == Subject) &&
                    Subject.Name.Substring(0, lengthForCompare) == completedSubjectName)
                {
                    Subject.writeSubjectInfo(Subject); // vypíše inforace o předmětu
                }
            }
        }

        /// <summary>
        /// Vypíše všechny registrované nedokončené předměty v aktuálním semestru daného studenta
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void listAllMySubjects(Semester CurrentSemester)
        {
            // uloží do seznamu myNoCompletedSubjects všechny registrované nedokončené předměty
            // v aktuálním semestru ze seznamu SubjectMark
            List<SubjectMark> myNoCompletedSubjects = SubjectMarkList.FindAll(SM =>
                SM.Completed == false &&
                SM.Subject.Semester == CurrentSemester);

            if (myNoCompletedSubjects.Count != 0) // kontrola jestli není počet nedokončených registrovaných předmětů v aktuálním semestru 0
            {
                foreach (SubjectMark SubjectMark in
                         myNoCompletedSubjects) // projede nedokončené a registrované předměty v aktuálním semestru studenta
                {
                    Subject.writeSubjectInfo(SubjectMark.Subject, SubjectMark.Credits);
                }
            }
            else // jestli ne, tak...
            {
                Console.WriteLine("Nemáš zaregistrovaný žádný předmět");
            }
        }

        /// <summary>
        /// Vypíše všechny dokončené předměty daného studenta
        /// </summary>
        public void listCompletedSubjects()
        {
            //uloží do seznamu myCompletedSubjects všechny dokončené předměty
            List<SubjectMark> myCompletedSubjects = SubjectMarkList.FindAll(SM => SM.Completed);

            if (myCompletedSubjects.Count != 0) // kontrola jestli není počet dokončených předmětů 0
            {
                foreach (SubjectMark SubjectMark in
                         myCompletedSubjects) // projede všechny dokončené předměty daného studenta
                {
                    Console.WriteLine(
                        $"Předmět {SubjectMark.Subject.Name} (Level {SubjectMark.Subject.Level})" +
                        $" v semestru {SubjectMark.Subject.Semester}," +
                        $" kde je garantem {SubjectMark.Subject.GarantOfSubject.returnFullName()}" +
                        $" - známka {SubjectMark.Mark}");
                }
            }
            else // pokud ne, tak...
            {
                Console.WriteLine("Nedokončil jsi zatím žádný předmět");
            }
        }

        /// <summary>
        /// Jít na danou přednášku
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void goOnLecture(Semester CurrentSemester)
        {
            // kontrola jestli existují nějaké přednášky a jestli má daný student nějaké registrované a nedokončené předměty
            if (Lecture.Lectures.Count != 0 && SubjectMarkList.FindAll(SM => SM.Completed == false).Count != 0)
            {
                Lecture.listAllAvailableLectures(SubjectMarkList); // výpis všech registrovatelných přednášek

                Console.WriteLine("Zadejte název přednášky");

                //string lectureName = Console.ReadLine();
                string lectureName = "Přednáška z Angličtiny1_1";
                Lecture ChosenLecture = Lecture.selectLecture(lectureName, this, CurrentSemester); // výběr přednášky
                Console.WriteLine($"lectureName = {lectureName}");

                // projede všechny registrované předměty, kde se název předmětu rovná názvu předmětu vybrané přednášky
                foreach (SubjectMark SubjectMark in
                         SubjectMarkList.Where(SM => SM.Subject == ChosenLecture.Subject))
                {
                    Console.WriteLine($"Šel jsi na přednášku {ChosenLecture.Name} - {ChosenLecture.Credits} kreditů");
                    SubjectMark.Credits -= ChosenLecture.Credits;
                    break;
                }
            }
            else // pokud ne, tak...    
            {
                Console.WriteLine("Není k dispozici žádná přednáška");
            }
        }


        /// <summary>
        /// Ukončení daného přeedmětu
        /// </summary>
        public void endSubject()
        {
            // projede všechny nedokončené a registrované předměty daného studenta
            foreach (SubjectMark SubjectMark in SubjectMarkList.FindAll(SM => SM.Completed == false))
            {
                if (SubjectMark.Credits > 0) // kontrola jestli jde předmět dokončit
                {
                    Console.WriteLine(
                        $"Do dokončení předmětu {SubjectMark.Subject.Name} zbývá {SubjectMark.Credits} kreditů");
                }
                else // pokud ne, tak...
                {
                    double mark = Random.Shared.Next(1, 5);
                    Console.WriteLine($"Dokončil jsi předmět {SubjectMark.Subject.Name} s hodnocením {mark}");
                    SubjectMark.Mark = mark;
                    SubjectMark.Completed = true;
                }
            }
        }

        /// <summary>
        /// Výpis všech registrovaných cvičení
        /// </summary>
        private void listAllAvailableExercise()
        {
            foreach (SubjectMark StudentSubjectMark in
                     SubjectMarkList) // projede všechny předměty daného studenta
            {
                foreach (Exercise Exercise in Exercise.Exercises.Where(Exercise =>
                             StudentSubjectMark.Subject == Exercise.Subject &&
                             StudentSubjectMark.Completed == false)) //projede všechny dostupné cvičení daného studenta (cvičení jejichž předmět mají registrovaný a nedokončený)
                {
                    Console.WriteLine($"{Exercise.Name} - {Exercise.Credits} kreditů," +
                                      $" počítač {Exercise.isComputerRequired()}");
                }
            }
        }

        /// <summary>
        /// Udělat cviření
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void doExercise(Semester CurrentSemester)
        {
            // kontrola jestli existují nějaké předměty a jestli má daný student nějaké registrované předměty
            if (Exercise.Exercises.Count != 0 &&
                SubjectMarkList.FindAll(SM => SM.Completed == false).Count != 0)
            {
                listAllAvailableExercise(); // vypíše všechny dostupné cvičení
                Console.WriteLine("Zadejte název cvičení");

                //string exerciseName = Console.ReadLine();
                string exerciseName = "Cvičení z Angličtiny1_1";
                Exercise ChosenExercise = Exercise.selectExercise(exerciseName, this, CurrentSemester); // výběr cvičení
                Console.WriteLine($"exerciseName = {exerciseName}");

                SubjectMark SubjectMark = null;
                
                // TODO: možná udělat novou metodu
                if (SubjectMarkList.Exists(SM => SM.Subject == ChosenExercise.Subject))
                {
                    SubjectMark = SubjectMarkList.Find(SM =>
                        SM.Subject == ChosenExercise.Subject);
                }
                else
                {
                    Console.WriteLine("Předmět daného cvičení není v seznamu SubjectMark");
                    doExercise(CurrentSemester);
                }
                

                SubjectMark.Credits -= ChosenExercise.Credits;
                Console.WriteLine($"Dokončil jsi cvičení {ChosenExercise.Name} - {ChosenExercise.Credits} kreditů");
            }
            else
            {
                Console.WriteLine("Není dostupné žádné cvičení");
            }
        }
    }
}