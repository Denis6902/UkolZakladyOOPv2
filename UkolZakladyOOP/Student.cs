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
        /// Seznam registrovaných předmětů a známek studenta
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
        /// Kredity získané za předměty
        /// </summary>
        public int Credits;

        /// <summary>
        /// Seznam dokončených cvičení
        /// </summary>
        public List<Exercise> CompletedExercises = new();

        /// <summary>
        /// Seznam dokončených přednášek
        /// </summary>
        public List<Lecture> CompletedLectures = new();

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
        /// Změní ročník všech studentů ze seznamu Students na následující
        /// </summary>
        public static void nextYear(int maxYear)
        {
            foreach (Student Student in Students)
            {
                if (Student.Year >= maxYear)
                {
                    Console.WriteLine($"Student {Student.returnFullName()} dokončíl tuto školu {Student.Year}");
                }
                else
                {
                    Student.Year += 1; // zvýšení ročníku daného studenta na následující
                    Console.WriteLine($"Aktuální ročník studenta {Student.returnFullName()} je {Student.Year}");
                    Student.SubjectMarkList = null; // vynulování seznamu známek daného studenta
                }
            }
        }

        /// <summary>
        /// Zjištění jestli můžu postoupit do dalšího semestru
        /// </summary>
        /// <param name="creditsToAdvancement">Kolik kreditů je potřeba k dokončení</param>
        public void checkNextSemester(int creditsToAdvancement)
        {
            Console.WriteLine(Credits >= creditsToAdvancement
                ? "Máš všechno splněno v aktuálním semestru"
                : $"Nemůžeš postoupit, potřebuješ ještě {creditsToAdvancement - Credits} kreditů");
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

            return sumOfAllMarks / countStudentMark; // vrátí součet všech známek studenta / počet známek student
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
                Console.WriteLine(Student.returnFullName()); // vypíše celé jména všech studentů ze seznamu students
            }

            //string studentFullName = Console.ReadLine();
            string studentFullName = "Pepa Nový";

            // jestli neexistuje student s daným celým jménem, spustí znovu cyklus
            while (!Students.Exists(Student => Student.returnFullName().ToLower() == studentFullName.ToLower()))
            {
                Console.WriteLine("Neexistuje daný student");
                Console.WriteLine("Zadej celé jméno existujícího studenta");
                studentFullName = Console.ReadLine();
            }

            Student ChosenStudent =
                Students.Find(Student =>
                    Student.returnFullName().ToLower() ==
                    studentFullName.ToLower()); // uloží do chosenStudent daného studenta

            Console.WriteLine($"ChosenStudent = {ChosenStudent.returnFullName()}");
            return ChosenStudent; // vrátí daného studenta
        }

        /// <summary>
        /// Metoda k registrování předmětu
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void registerSubject(Semester CurrentSemester)
        {
            //kontola jestli existuje nějaký neregistrovaný předmět v aktuánlím ročníku a semestru
            if (Subject.Subjects.Any(Subject =>
                    Subject.Year == Year && Subject.Semester == CurrentSemester &&
                    !SubjectMarkList.Exists(SM => SM.Subject == Subject)))
            {
                listSubjectsForRegister(CurrentSemester); // vypíše předměty dostupné k registraci

                Console.WriteLine("Zadejte název předmětu");
                //string subjectName = Console.ReadLine();
                string subjectName = "English1_1";

                // ukázka prerekvizit.
                if (SubjectMarkList.Exists(SM => SM.Subject.Name.ToLower() == subjectName.ToLower()))
                {
                    subjectName = "English1_2";
                }

                Console.WriteLine($"subjectName = {subjectName}");
                
                
                // kontrola jestli existuje neregistrovaný přemět v aktuálním roce a semestru s daným názvem,
                // který je pro něj dostupný
                while ((!Subject.Subjects.Exists(Subject =>
                           Subject.Name.ToLower() == subjectName.ToLower() && Subject.Year == Year &&
                           Subject.Semester == CurrentSemester && !SubjectMarkList.Exists(SM =>
                               SM.Subject == Subject))
                       || Subject.isPreviousSubjectCompleted(SubjectMarkList, subjectName)) && subjectName != ""
                      )
                {
                    Console.WriteLine("Neexistuje daný předmět");
                    Console.WriteLine("Zadej název existujícího předmětu");
                    subjectName = Console.ReadLine();
                }

                // pokud uživatel něco zadal
                if (subjectName != "")
                {
                    // najde a uloží do proměnné hledaný předmět podle zddaného jména
                    Subject ChosenSubject =
                        Subject.Subjects.Find(Subject => Subject.Name.ToLower() == subjectName.ToLower());

                    SubjectMark SubjectMark = new(ChosenSubject, SubjectMarkList);

                    Console.WriteLine(
                        $"{returnFullName()} jsi zapsaný do {SubjectMark.Subject.Name} předmětu, semestr: {SubjectMark.Subject.Semester}");
                }
            }
            else
            {
                Console.WriteLine("Nemáš žádné neregistrované předměty v aktuálním ročníku a semestru");
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
                    Subject.writeSubjectInfo(); // vypíše informace o předmětu
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
                    Subject.writeSubjectInfo(); // vypíše inforace o předmětu
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
                !SM.Completed && SM.Subject.Semester == CurrentSemester && SM.Subject.Year == Year);

            if (myNoCompletedSubjects
                .Any()) // kontrola jestli existují nějaké nedokončené registrované předměty v aktuálním roce a semestru
            {
                foreach (SubjectMark SubjectMark in
                         myNoCompletedSubjects) // projede nedokončené a registrované předměty v aktuálním ročnííku a semestru studenta
                {
                    SubjectMark.Subject.writeSubjectInfo(SubjectMark.Credits);
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

            if (myCompletedSubjects.Any()) // kontrola jestli existují nějaké dokončené předměty
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
            if (Lecture.Lectures.Any() && SubjectMarkList.FindAll(SM => !SM.Completed).Any()
                                       && Lecture.CheckAvailableLecturesCount(SubjectMarkList, CompletedLectures))
            {
                Lecture.listAllAvailableLectures(SubjectMarkList,
                    CompletedLectures); // výpis všech registrovatelných přednášek

                Console.WriteLine("Zadejte název přednášky");

                //string lectureName = Console.ReadLine();
                string lectureName = "Přednáška z Angličtiny1_1";
                Lecture ChosenLecture =
                    Lecture.selectLecture(lectureName, this, CurrentSemester, CompletedLectures); // výběr přednášky
                Console.WriteLine($"lectureName = {lectureName}");

                if (!SubjectMarkList.Exists(SM => SM.Subject == ChosenLecture.Subject && !SM.Completed))
                {
                    Console.WriteLine("Předmět dané přednášky nemáš zaregistrovaný");
                    Console.WriteLine("Zadej název přednášky jehož předmět máš zaregistrovaný:");
                    lectureName = Console.ReadLine();
                    ChosenLecture =
                        Lecture.selectLecture(lectureName, this, CurrentSemester, CompletedLectures); // výběr cvičení
                    Console.WriteLine($"lectureName = {lectureName}");
                }

                CompletedLectures.Add(ChosenLecture);

                Console.WriteLine($"Šel jsi na přednášku {ChosenLecture.Name}");
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
            foreach (SubjectMark SubjectMark in SubjectMarkList.FindAll(SM => !SM.Completed))
            {
                if (SubjectMark.Subject.LectureCount ==
                    CompletedLectures.FindAll(Lecture => Lecture.Subject == SubjectMark.Subject).Count &&
                    SubjectMark.Subject.ExerciseCount == CompletedExercises
                        .FindAll(Exercise => Exercise.Subject == SubjectMark.Subject)
                        .Count) // kontrola jestli jde předmět dokončit
                {
                    double mark = Random.Shared.Next(1, 5);
                    Console.WriteLine($"Dokončil jsi předmět {SubjectMark.Subject.Name} s hodnocením {mark}");
                    SubjectMark.Mark = mark;
                    SubjectMark.Completed = true;
                    Credits += SubjectMark.Subject.Credits;
                }
                else // pokud ne, tak...
                {
                    Console.WriteLine(
                        $"Pro dokončení předmětu {SubjectMark.Subject.Name} potřebuješ dokončit ještě {SubjectMark.Subject.ExerciseCount} cvičení " +
                        $"a {SubjectMark.Subject.LectureCount} přednášek, za dokončení získáš {SubjectMark.Credits} kreditů");
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
            if (Exercise.Exercises.Any() && SubjectMarkList.FindAll(SM => !SM.Completed).Any()
                                         && Exercise.CheckAvailableExercisesCount(SubjectMarkList, CompletedExercises))
            {
                Exercise.listAllAvailableExercise(SubjectMarkList,
                    CompletedExercises); // vypíše všechny dostupné cvičení
                Console.WriteLine("Zadejte název cvičení");

                //string exerciseName = Console.ReadLine();
                string exerciseName = "Cvičení z Angličtiny1_1";
                Console.WriteLine($"exerciseName = {exerciseName}");

                while (!SubjectMarkList.Exists(SM => SM.Subject == Exercise.Exercises.Find(
                           Exercise => Exercise.Name.ToLower() == exerciseName.ToLower()).Subject && !SM.Completed))
                {
                    Console.WriteLine("Předmět daného cvičení nemáš zaregistrovaný");
                    Console.WriteLine("Zadej název cvičení jehož předmět máš zaregistrovaný:");
                    exerciseName = Console.ReadLine();
                    Console.WriteLine($"exerciseName = {exerciseName}");
                }

                Exercise ChosenExercise =
                    Exercise.selectExercise(exerciseName, this, CurrentSemester, CompletedExercises); // výběr cvičení

                CompletedExercises.Add(ChosenExercise);

                Console.WriteLine($"Dokončil jsi cvičení {ChosenExercise.Name}");
            }
            else
            {
                Console.WriteLine("Není dostupné žádné cvičení");
            }
        }
    }
}