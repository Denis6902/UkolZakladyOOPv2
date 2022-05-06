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
        /*/// Seznam předmětu a známky
        /// </summary>
        public static List<MarkSubject> MarkSubjectList = new();

        /// <summary>
        /// Seznam studentů a jejich předmětů
        /// </summary>
        private List<SubjectStudent> StudentSubjectList = new();*/

        public List<StudentSubjectMark> StudentSubjectMarkList = new();

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
                Student.StudentSubjectMarkList = null;
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
            List<StudentSubjectMark> ChosenStudentMarks =
                StudentSubjectMarkList.FindAll(markSubject =>
                    markSubject.Student == this); // vytvoří nový seznam známek jenom od daného studenta

            double sumOfAllMarks =
                ChosenStudentMarks.Sum(SSM => SSM.Mark); // uloží do sumOfAllMarks součet všech známek
            double countStudentMark = ChosenStudentMarks.Count; // uloží do sumOfAllMarks počet všech známek

            return sumOfAllMarks / countStudentMark;
        }

        /// <summary>
        /// Metoda k vybrání studenta
        /// </summary>
        /// <returns></returns>
        public static Student selectStudent()
        {
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
            if (Subject.Subjects.Count != 0)
            {
                listSubjectsForRegister(CurrentSemester); // vypíše předměty dostupné k registraci

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine();
                string subjectName = "English1_1";
                Console.WriteLine($"subjectName = {subjectName}");

                foreach (Subject Subject in Subject.Subjects.Where(Subject =>
                             Subject.Name.ToLower() == subjectName.ToLower() &&
                             Subject.Registered == false)) // projede přeměty kde se název předmětu rovná danému názvu
                {
                    Console.WriteLine(
                        $"{returnFullName()} jsi zapsaný do {Subject.Name} předmětu, semestr: {Subject.Semester}");
                    StudentSubjectMark StudentSubjectMark = new(this, Subject, StudentSubjectMarkList);
                    // přidá předmět se studentem do seznamu předmětů a studentů
                    StudentSubjectMark.Subject.Registered = true;
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
            int subjectLevel = 1; // aktuální urověň předmětu
            const int lengthForCompare = 3; // počet znaků, podle kterých bude porovnávat předměty
            string completedSubjectName = null;
            string nocompletedSubjectName = null;

            foreach (Subject Subject in Subject.Subjects.Where(Subject =>
                             Subject.Year == this.Year && Subject.Semester == CurrentSemester)
                         .OrderBy(subject =>
                             subject.Name)) //projede seřazené předměty podle jejich jména v aktuálním ročníku a semestru
            {
                if (Subject.Registered == false &&
                    Subject.Level ==
                    1) // jestli není předmět registrovaný a jeho úroveň je 1 vypíše daný předmět. (Vždy vypíše předměty s úrovní 1)
                {
                    Subject.writeSubjectInfo(Subject); // vypíše informace o předmětu
                }

                switch (Subject.Completed)
                {
                    case true: // jestli je daný předmět dokončený
                        subjectLevel =
                            Subject.Level +
                            1; // nastaví úroveň předmětu, který se hledá o jedna vyšší než úroveň daného předmětu. (Například pokud je dokončená Čeština 1, bude se nyní hledat předmět čeština s úrovní 2)
                        completedSubjectName =
                            Subject.Name.Substring(0,
                                lengthForCompare); // uloží část názvu předmětu do proměnné completedSubjectName
                        break;
                    case false: // jestli daný předmět není dokončený
                    {
                        nocompletedSubjectName =
                            Subject.Name.Substring(0,
                                lengthForCompare); // uloží část názvu předmětu do proměnné nocompletedSubjectName

                        if (Subject.Level == subjectLevel &&
                            completedSubjectName ==
                            nocompletedSubjectName) // jestli se úroveň daného předmětu rovná proměnné subjectLevel a taky se navzájem rovná část nazvů. (Například pokud je dokončená čeština s úrovní 1 ,hledáme předmět čeština s úrovní 2)
                        {
                            Subject.writeSubjectInfo(Subject); // vypíše informace o předmětu
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Vypíše všechny předměty daného studenta
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void listAllMySubjects(Semester CurrentSemester)
        {
            if (StudentSubjectMarkList.FindAll(SSM => SSM.Student == this).Count != 0) // kontrola jestli je počet registrovaných předmětů > 0
            {
                foreach (StudentSubjectMark StudentSubjectMark in StudentSubjectMarkList.Where(SSM =>
                             SSM.Subject.Year == Year &&
                             SSM.Subject.Semester == CurrentSemester &&
                             SSM.Subject.Registered &&
                             SSM.Student == this)) // projede registrované předměty studenta v aktuálním roce a semestru
                {
                    Subject.writeSubjectInfo(StudentSubjectMark.Subject, StudentSubjectMark.Mark);
                }
            }
            else
            {
                Console.WriteLine("Nemáš zaregistrovaný žádný předmět");
            }
        }

        /// <summary>
        /// Vypíše všechny dokončené předměty daného studenta
        /// </summary>
        public void listCompletedSubjects()
        {
            if (StudentSubjectMarkList.FindAll(SSM => SSM.Student == this && !double.IsNaN(SSM.Mark)).Count != 0) // kontrola jestli je počet známek > 0
            {
                foreach (StudentSubjectMark StudentSubjectMark in StudentSubjectMarkList.Where(SSM =>
                             this == SSM.Student)) // projede všechny předměty daného studenta
                {
                    Console.WriteLine($"Předmět {StudentSubjectMark.Subject.Name} (Level {StudentSubjectMark.Subject.Level})" +
                                      $" v semestru {StudentSubjectMark.Subject.Semester}," +
                                      $" kde je garantem {StudentSubjectMark.Subject.GarantOfSubject.returnFullName()}" +
                                      $" - známka {StudentSubjectMark.Mark}");
                }
            }
            else
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
            if (Lecture.Lectures.Count != 0 &&
                StudentSubjectMarkList.FindAll(SSM => SSM.Student == this).Count !=
                0) // kontrola jestli existují nějaké přednášky a jestli má daný student nějaké registrované předměty
            {
                Lecture.listAllRegisteredLectures(StudentSubjectMarkList); // výpis všech registrovaných přednášek

                Console.WriteLine("Zadejte název přednášky");

                //string lectureName = Console.ReadLine();
                string lectureName = "Přednáška z Angličtiny1_1";
                Lecture ChosenLecture = Lecture.selectLecture(lectureName, this, CurrentSemester); // výběr přednášky
                Console.WriteLine($"lectureName = {lectureName}");

                foreach (StudentSubjectMark StudentSubjectMark in StudentSubjectMarkList)
                {
                    Console.WriteLine($"Šel jsi na přednášku {ChosenLecture.Name} - {ChosenLecture.Credits} kreditů");
                    StudentSubjectMark.Credits -= ChosenLecture.Credits;
                    break;
                }
            }
        }


        /// <summary>
        /// Ukončení daného přeedmětu
        /// </summary>
        public void endSubject()
        {
            // projede všechny nedokončené předměty daného studenta
            foreach (StudentSubjectMark StudentSubjectMark in StudentSubjectMarkList.Where(SSM =>
                         SSM.Subject.Completed == false))
            {
                if (StudentSubjectMark.Credits > 0 && this == StudentSubjectMark.Student) //kontrola jestli jde předmět dokončit
                {
                    Console.WriteLine(
                        $"Do dokončení předmětu {StudentSubjectMark.Subject.Name} zbývá {StudentSubjectMark.Credits} kreditů");
                }

                if (StudentSubjectMark.Credits < 1 && this == StudentSubjectMark.Student)
                {
                    double mark = Random.Shared.Next(1, 5);
                    Console.WriteLine($"Dokončil jsi předmět {StudentSubjectMark.Subject.Name} s hodnocením {mark}");
                    StudentSubjectMark SSM = new(this, StudentSubjectMark.Subject, StudentSubjectMarkList, mark);
                    StudentSubjectMark.Subject.Completed = true;
                }
            }
        }

        /// <summary>
        /// Výpis všech registrovaných cvičení
        /// </summary>
        private void listAllAvailableExercise()
        {
            foreach (StudentSubjectMark StudentSubjectMark in StudentSubjectMarkList) // projede všechny předměty daného studenta
            {
                foreach (Exercise Exercise in Exercise.Exercises.Where(Exercise =>
                             StudentSubjectMark.Subject.Name == Exercise.Subject.Name &&
                             StudentSubjectMark.Subject.Registered)) //projede všechny registrované cvičení daného studenta 
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
            if (Exercise.Exercises.Count != 0 && StudentSubjectMarkList.FindAll(SSM => SSM.Student == this).Count != 0)
            {
                listAllAvailableExercise(); // vypíše všechny dostupné cvičení
                Console.WriteLine("Zadejte název cvičení");

                //string exerciseName = Console.ReadLine();
                string exerciseName = "Cvičení z Angličtiny1_1";
                Exercise ChosenExercise = Exercise.selectExercise(exerciseName, this, CurrentSemester); // výběr cvičení
                Console.WriteLine($"exerciseName = {exerciseName}");

                StudentSubjectMark StudentSubjectMark = StudentSubjectMarkList.Find(SSM =>
                    SSM.Subject == ChosenExercise.Subject && this == SSM.Student);

                StudentSubjectMark.Credits -= ChosenExercise.Credits;
                Console.WriteLine($"Dokončil jsi cvičení {ChosenExercise.Name} - {ChosenExercise.Credits} kreditů");
            }
        }
    }
}