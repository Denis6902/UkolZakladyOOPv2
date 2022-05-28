using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Třída učitele, dědí z Person
    /// </summary>
    public class Teacher : Person
    {
        /// <summary>
        /// Akademický titul učitele
        /// </summary>
        private string AcademicTitle;

        /// <summary>
        /// Seznam všech učitelů
        /// </summary>
        private static List<Teacher> Teachers = new();

        /// <summary>
        /// Konstruktor, který automaticky přidá učitele do seznamu učitelů
        /// </summary>
        /// <param name="academicTitle">Akademický titul učitele</param>
        /// <param name="firstName">Jméno učitele</param>
        /// <param name="lastName">Přijmení učitele</param>
        /// <param name="birthDate">Datum narození učitele</param>
        public Teacher(string academicTitle, string firstName, string lastName, DateTime birthDate)
            : base(firstName, lastName, birthDate)
        {
            AcademicTitle = academicTitle;
            Teachers.Add(this); // přidání učitele do seznamu učitelů
        }

        /// <summary>
        /// Informace o učiteli
        /// </summary>
        public override void aboutMe()
        {
            Console.WriteLine(
                $"Dobrý den, jmenuji se {AcademicTitle} {returnFullName()}" +
                $" a narodil/narodila jsem se {BirthDate:MM.dd.yyy}" +
                $" a aktuálně učím ve škole");
        }


        /// <summary>
        /// Vrátí počet předmetů, který daný učitel učí
        /// </summary>
        /// <returns>Počet předmetů</returns>
        private int returnSubjectsCount()
        {
            return Subject.Subjects.Count(Subject => Subject.Teacher == this);
            // projede všechny předměty, které učí daný učitel
        }

        /// <summary>
        /// Vrátí počet přednášek, který daný učitel učí
        /// </summary>
        /// <returns>Počet přednášek</returns>
        private int returnLecturesCount()
        {
            return Lecture.Lectures.Count(Lecture => Lecture.Teacher == this);
            // projede všechny přednášky, které učí daný učitel
        }


        /// <summary>
        /// Vypíše všechny učitele
        /// </summary>
        public static void listAllTeachers()
        {
            foreach (Teacher Teacher in Teachers) // projede seznam učitelů
            {
                Console.WriteLine($"{Teacher.returnFullName()} - vyučuje {Teacher.returnSubjectsCount()} předmětů" +
                                  $" a {Teacher.returnLecturesCount()} přednášky");
            }
        }

        /// <summary>
        /// Vybere učitele ze seznamu učitelů
        /// </summary>
        /// <returns>Daného učitele</returns>
        public static Teacher selectTeacher()
        {
            foreach (Teacher Teacher in Teachers)
            {
                Console.WriteLine(Teacher.returnFullName()); // vypíše celé jména všech učitelů ze seznamu teachers
            }

            //string teacherFullName = Console.ReadLine();
            string teacherFullName = "Pavel Novotný";

            // jestli neexistuje učitele s daným jménem, spustí znovu metodu
            while (!Teachers.Exists(Teacher => Teacher.returnFullName().ToLower() == teacherFullName.ToLower()))
            {
                Console.WriteLine("Neexistuje daný učitel");
                Console.WriteLine("Zadej jméno existujícího učitele");
                teacherFullName = Console.ReadLine();
            }

            Teacher ChosenTeacher =
                Teachers.Find(Teacher =>
                    Teacher.returnFullName().ToLower() ==
                    teacherFullName.ToLower()); // uloží do chosenTeacher daného učitele

            Console.WriteLine($"chosenTeacher = {ChosenTeacher.returnFullName()}");
            return ChosenTeacher;
        }

        /// <summary>
        /// Registarce předmětu
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void registerSubject(Semester CurrentSemester)
        {
            // projede všechny přeměty v aktuánlím semestru, které nikdo neučí
            foreach (Subject Subject in
                     Subject.Subjects.Where(Subject => CurrentSemester == Subject.Semester && Subject.Teacher == null))
            {
                Subject.writeSubjectInfo(Subject); // vypíše informace o předmětu
            }

            Console.WriteLine("Zadejte název předmětu");
            //string subjectName = Console.ReadLine();
            string subjectName = "xxx1_1"; // načtení názvu předmětu z konzole

            Subject SubjectForRegister = Subject.selectSubject(subjectName);
            // vybrání zvoleného předmětu a uložení do proměnné
            SubjectForRegister.Teacher = this; // nastaví zvolenému předmětu daného učitele
        }

        /// <summary>
        /// Vypíše všechny předměty daného učitele
        /// </summary>
        public void listAllMySubjects()
        {
            if (returnSubjectsCount() != 0) // Pokud je počet předmětů daného učitele jiný než 0 (žádný)
            {
                // projede seznam předmětů, kde je učitel předmětu roven danému učiteli
                foreach (Subject Subject in Subject.Subjects.Where(Subject => Subject.Teacher == this))
                {
                    Subject.writeSubjectInfo(Subject); // vypíše informace o předmětu
                }
            }
            else
            {
                Console.WriteLine("Neučíš žádný předmět"); // jinak vypíše že nemá žádný předmět
            }
        }

        /// <summary>
        /// Vytvoření nového předmětu
        /// </summary>
        public static void createSubject()
        {
            Console.WriteLine("Jak chcete předmět vytvořit");
            Console.WriteLine("1) Nový předmět");
            Console.WriteLine("2) Předmět ze šablony");
            //string howCreate = Console.ReadLine();
            string howCreate = "1"; // načtení z konzole jak chce daný učitel předmět vytvořit
            Console.WriteLine("howCreate = 1");

            Thread.Sleep(1000);
            Console.Clear();

            if (howCreate == "1")
            {
                createNewSubject(); // vytvoření úplně nového předmětu
            }

            howCreate = "2";
            Console.WriteLine("howCreate = 2");

            Thread.Sleep(1000);
            Console.Clear();

            if (howCreate == "2")
            {
                createSubjectFromTemplate(); // vytvoření předmětu ze šablony
            }
        }

        /// <summary>
        /// Vytvoření nového předmětu
        /// </summary>
        private static void createNewSubject()
        {
            SubjectType SubjectType = null;

            Console.WriteLine("Název předmětu:");
            //string name = Console.ReadLine();
            string subjectName = "Nový Předmět"; // načtení z konzole názvu nového předmětu
            Console.WriteLine($"subjectName = {subjectName}");

            foreach (SubjectType SType in Subject.SubjectsTypes)
            {
                Console.WriteLine(SType.Name); // výpis všech typů předmětů
            }

            Console.WriteLine("Vlastní");

            Console.WriteLine("Zvolte typ předmětu");
            string subjectTypeName = "Czech";
            Console.WriteLine($"subjectTypeName = {subjectTypeName}");

            // vytvoření nového typu předmětu
            if (subjectTypeName.ToLower() == "vlastní")
            {
                Console.WriteLine("Zadejte název nového typu předmětu:");
                subjectTypeName = Console.ReadLine();
                Subject.createNewSubjectType(subjectTypeName);
                SubjectType = Subject.SubjectsTypes.Find(ST => ST.Name.ToLower() == subjectTypeName.ToLower());
                Console.WriteLine($"SubjectType = {SubjectType.Name}");
            }

            // kontrola jestli existuje typ předmětu s daným názvem
            while (!Subject.SubjectsTypes.Exists(ST => ST.Name.ToLower() == subjectTypeName.ToLower()))
            {
                Console.WriteLine("Nesprávný typ předmětu");
                Console.WriteLine("Zvolte správný typ předmětu");
                subjectTypeName = Console.ReadLine();
            }

            SubjectType = Subject.SubjectsTypes.Find(ST => ST.Name.ToLower() == subjectTypeName.ToLower());
            Console.WriteLine($"SubjectType = {SubjectType.Name}");

            Console.WriteLine("Garant předmětu: ");
            Teacher GarantOfSubject = Teacher.selectTeacher(); // vybrání garanta předmětu

            Console.WriteLine("Učitel: ");
            Teacher ChosenTeacher = Teacher.selectTeacher(); // vybrání učitele 

            //double credits = double.Parse(Console.ReadLine());
            double credits = 50; // načtení počtu kreditů z konzole 
            Console.WriteLine($"credits = {credits}");

            Semester Semester = Semester.Summer; // načtení z konzole semestru 
            Console.WriteLine($"Semester = {Semester}");

            //int year = int.Parse(Console.ReadLine()); // načtení z konzole ročníku
            int year = Random.Shared.Next(1, 4);
            Console.WriteLine($"year = {year}");

            //int subjectLevel = int.Parse(Console.ReadLine()); // načtení úrovně předmětu z konzole
            int subjectLevel = Random.Shared.Next(1, 3);
            Console.WriteLine($"subjectLevel = {subjectLevel}");

            Subject NewSubject = new(subjectName, SubjectType, GarantOfSubject, ChosenTeacher, credits, year, Semester,
                subjectLevel);

            Thread.Sleep(10000);
        }

        /// <summary>
        /// Vytvoření předmětu ze šablony (factory)
        /// </summary>
        private static void createSubjectFromTemplate()
        {
            Console.WriteLine("Předmět jakého typu chcete vytvořit:");

            foreach (SubjectType SType in Subject.SubjectsTypes.Where(ST => ST.HasFactory))
            {
                Console.WriteLine(SType.Name); // výpis všech typů předmětů
            }

            string subjectTypeName = "Czech";

            // kontrola jestli existuje typ předmětu s daným názvem co má factory
            while (!Subject.SubjectsTypes.Exists(ST => ST.Name.ToLower() == subjectTypeName.ToLower() && ST.HasFactory))
            {
                Console.WriteLine("Nesprávný typ předmětu");
                Console.WriteLine("Zadejte správný typ předmětu");
                subjectTypeName = Console.ReadLine();
            }

            Console.WriteLine($"subjectTypeName = {subjectTypeName}");


            //string subjectName = Console.ReadLine();
            string subjectName = "další czech"; // vybrání názvu předmětu
            Console.WriteLine($"subjectName = {subjectName}");

            Teacher GarantOfSubject = Teacher.selectTeacher(); // vybrání garanta předmětu

            Teacher ChosenTeacher = Teacher.selectTeacher(); // vybrání učitele 

            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole
            double credits = 50;
            Console.WriteLine($"credits = {credits}");

            Semester Semester = Semester.Winter; // načtení semestru z konzole
            Console.WriteLine($"Semester = {Semester}");

            //int year = int.Parse(Console.ReadLine()); // načtení ročníku z konzole 
            int year = Random.Shared.Next(1, 4);
            Console.WriteLine($"year = {year}");

            //int subjectLevel = int.Parse(Console.ReadLine());/ / načtení úrovně předmětu z konzole
            int subjectLevel = Random.Shared.Next(1, 3);
            Console.WriteLine($"$subjectLevel = {subjectLevel}");

            switch (subjectTypeName.ToLower()) // podle zvoleného předmětu vytvoří daný předmět
            {
                case "czech":
                    Subject Czech = SubjectFactory.CreateCzech(subjectName, ChosenTeacher, GarantOfSubject, credits,
                        year,
                        Semester, subjectLevel);
                    break;

                case "english":
                    Subject English = SubjectFactory.CreateEnglish(subjectName, ChosenTeacher, GarantOfSubject, credits,
                        year,
                        Semester, subjectLevel);
                    break;

                default:
                    Console.WriteLine("Daný předmět nemá factory");
                    break;
            }

            Thread.Sleep(10000);
        }

        /// <summary>
        /// Vytvoření cvičení
        /// </summary>
        public static void createExercise()
        {
            Console.WriteLine("Jak chcete cvičení vytvořit");
            Console.WriteLine("1) Nové cvičení");
            Console.WriteLine("2) Cvičení ze šablony");
            //string howCreate = Console.ReadLine().ToLower();
            string howCreate = "1"; // načtení z konzole jak chce daný učitel vytvořit cvičení
            Console.WriteLine("howCreate = 1");

            if (howCreate == "1")
            {
                createNewExercise(); // vytvoření nového cvičení
            }

            Console.Clear();
            Thread.Sleep(1000);

            howCreate = "2";
            Console.WriteLine("howCreate = 2");

            if (howCreate == "2")
            {
                createExerciseFromTemplate(); // vytvoření cvičení ze šablony
            }
        }

        /// <summary>
        /// Vytvoření nového cvičení
        /// </summary>
        private static void createNewExercise()
        {
            string exerciseName = "Cvičení1";
            Console.WriteLine($"exerciseName = {exerciseName}");

            foreach (ExerciseType EType in Exercise.ExercisesTypes)
            {
                Console.WriteLine(EType.Name); // výpis všech typů cvičení
            }

            Console.WriteLine("Zvolte typ cvičení");
            string exerciseTypeName = "Cvičení z Češtiny";

            // kontrola jestli existuje typ cvičení s daným názvem
            while (!Exercise.ExercisesTypes.Exists(ET => ET.Name.ToLower() == exerciseTypeName.ToLower()))
            {
                Console.WriteLine("Nesprávný typ cvičení");
                Console.WriteLine("Zadejte správný typ cvičení");
                exerciseTypeName = Console.ReadLine();
            }

            ExerciseType ExerciseType =
                Exercise.ExercisesTypes.Find(ET => ET.Name.ToLower() == exerciseTypeName.ToLower());
            Console.WriteLine($"ExerciseType = {ExerciseType.Name}");

            //bool computerRequired = bool.Parse(Console.ReadLine()); // načtení z konzole jestli je potřeba PC
            bool computerRequired = false;
            Console.WriteLine($"computerRequired = {computerRequired}");

            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole
            double credits = 22;
            Console.WriteLine($"credits = {credits}");

            Subject.listSubjectsWithOnlyOneType(ExerciseType.SubjectType); // výpis všech předmětů daného typu
            string subjectName = "Czech1_1";
            Subject ChosenSubject =
                Subject.selectSubjectsWithOnlyOneType(subjectName,
                    ExerciseType.SubjectType); // vybrání předmětu daného typu

            Exercise NewExercise = new(exerciseName, ExerciseType, computerRequired, credits, ChosenSubject);
            // vytvoření nového předmětu

            Thread.Sleep(10000);
            Console.Clear();
        }

        /// <summary>
        /// Vytvoření cvičení ze šablony (factory)
        /// </summary>
        private static void createExerciseFromTemplate()
        {
            Console.WriteLine("Jaký typ cvičení chcete vytvořit?");
            foreach (ExerciseType EType in Exercise.ExercisesTypes.Where(ET => ET.HasFactory))
            {
                Console.WriteLine(EType.Name); // výpis všech typů cvičení
            }

            string exerciseType = "cvičení z češtiny";

            // kontrola jestli existuje typ cvičení s daným názvem co má factory
            while (!Exercise.ExercisesTypes.Exists(ET =>
                       ET.Name.ToLower() == exerciseType.ToLower() && ET.HasFactory))
            {
                Console.WriteLine("Nesprávný typ cvičení");
                Console.WriteLine("Zadejte správný typ cvičení");
                exerciseType = Console.ReadLine();
            }

            Console.WriteLine($"exerciseType = {exerciseType}");
            //string exerciseName = Console.ReadLine();
            string exerciseName = "Angličtina";
            Console.WriteLine($"exerciseName = {exerciseName}");

            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditl z konzole
            double credits = 50;
            Console.WriteLine($"credits = {credits}");

            switch (exerciseType.ToLower()) // vytvoření zvoleného cvičení 
            {
                case "cvičení z češtiny":
                {
                    createCzechExercise(exerciseName, credits);
                    break;
                }
                case "cvičení z angličtiny":
                {
                    createEnglishExercise(exerciseName, credits);
                    break;
                }
                default:
                    Console.WriteLine("Dané cvičení nejde vytvořit pomocí factory");
                    break;
            }
        }


        /// <summary>
        /// Výpis studentů podle průměrných známek
        /// </summary>
        public static void listStudentsByAverageMarks()
        {
            foreach (Student Student in Student.Students)
            {
                if (double.IsNaN(Student.calculateAverageMark()) ||
                    Student.calculateAverageMark() == 0) // Kontola jestli student nemá žádnou známku
                {
                    Console.WriteLine($"{Student.returnFullName()} nemá žádnou známku");
                }
                else // Jinak vypíše průměrné známky
                {
                    Console.WriteLine($"{Student.calculateAverageMark()} - průměrná známka ze všech předmětu studenta" +
                                      $" {Student.returnFullName()}");
                }
            }
        }

        /// <summary>
        /// Vytvoření přednášky
        /// </summary>
        public static void createLecture()
        {
            Console.WriteLine("Jak chcete přednášku vytvořit");
            Console.WriteLine("1) Nová přednáška");
            Console.WriteLine("2) Přednáška ze šablony");
            //string howCreate = Console.ReadLine().ToLower(); // načtení z konzole jak vytvořit novou přednášku
            string howCreate = "1";
            Console.WriteLine("howCreate = 1");

            if (howCreate == "1")
            {
                createNewLecture(); // vytvoření nové přednášky
            }

            Thread.Sleep(10000);
            Console.Clear();

            howCreate = "2";
            Console.WriteLine("howCreate = 2;");

            if (howCreate == "2")
            {
                createLectureFromTemplate(); // vytvoření přednášky ze šablony
            }
        }

        /// <summary>
        /// Vytvoření nové přednášky
        /// </summary>
        private static void createNewLecture()
        {
            //string lectureName = Console.ReadLine(); // načtení z konzole názvu přednášky
            string lectureName = "Přednáška 1";
            Console.WriteLine($"lectureName = {lectureName}");

            foreach (LectureType LType in Lecture.LecturesTypes)
            {
                Console.WriteLine(LType.Name); // výpis všech typů přednášek
            }

            Console.WriteLine("Zvolte typ přednášky");
            string lectureTypeName = "Přednáška z Češtiny";

            // kontrola jestli existuje typ přednášky s daným názvem
            while (!Lecture.LecturesTypes.Exists(LT => LT.Name.ToLower() == lectureTypeName.ToLower()))
            {
                Console.WriteLine("Nesprávný typ přednášky");
                Console.WriteLine("Zadejte správný typ přednášky");
                lectureTypeName = Console.ReadLine();
            }

            LectureType LectureType = Lecture.LecturesTypes.Find(LT => LT.Name.ToLower() == lectureTypeName.ToLower());
            Console.WriteLine($"LectureType = {LectureType.Name}");

            //bool computerRequired = bool.Parse(Console.ReadLine()); načtení z konzole jestli je potřeba PC
            bool computerRequired = false;
            Console.WriteLine($"computerRequired = {computerRequired}");

            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole 
            double credits = 22;
            Console.WriteLine($"credits = {credits}");

            Console.WriteLine("Předmět:");
            Subject.listSubjectsWithOnlyOneType(LectureType.SubjectType); // výpis všech předmětů daného typu

            //string subjectName = Console.ReadLine(); // načtení z konzole názvu předmětu
            string subjectName = "Czech1_1";
            Subject ChosenSubject =
                Subject.selectSubjectsWithOnlyOneType(subjectName,
                    LectureType.SubjectType); // vybrání předmětů daného typu

            Lecture NewLecture = new(lectureName, LectureType, computerRequired, credits, ChosenSubject);
            // vytvoření nové přednášky
        }

        /// <summary>
        /// Vytvoření nové přednášky ze šablony (factory)
        /// </summary>
        private static void createLectureFromTemplate()
        {
            foreach (LectureType lType in Lecture.LecturesTypes.Where(LT => LT.HasFactory))
            {
                Console.WriteLine(lType.Name); // výpis všech typů přednášek
            }

            Console.WriteLine("Jaký typ přednášky chcete vytvořit:");
            string lectureType = "přednáška z češtiny";

            // kontrola jestli existuje typ přednášky s daným názvem co má factory
            while (!Lecture.LecturesTypes.Exists(LT => LT.Name.ToLower() == lectureType.ToLower() && LT.HasFactory))
            {
                Console.WriteLine("Nesprávný typ přednášky");
                Console.WriteLine("Zadejte správný typ přednášky");
                lectureType = Console.ReadLine();
            }

            Console.WriteLine($"lectureType = {lectureType}");

            //string lectureName = Console.ReadLine();
            string lectureName = "Nová czech prednaska";
            Console.WriteLine($"lectureName = {lectureName}");

            //double credits = double.Parse(Console.ReadLine()); // načtení počtu krreditů z konzole
            double credits = 50;
            Console.WriteLine($"credits = {credits}");

            switch (lectureType.ToLower())
            {
                case "přednáška z češtiny":
                    createCzechLecture(lectureName, credits); // vytvoření přednášky z češtiny
                    break;
                case "přednáška z angličtiny":
                    createEnglishLecture(lectureName, credits); // vytvoření přednášky z angličtiny
                    break;
                default:
                    Console.WriteLine("Daná přednáška nejde vytvořit pomocí factory");
                    break;
            }
        }

        /// <summary>x
        /// Vytvoření přednášky z Češtiny
        /// </summary>
        /// <param name="lectureName">Název přednášky</param>
        /// <param name="credits">Počet kreditů</param>
        private static void createCzechLecture(string lectureName, double credits)
        {
            SubjectType SubjectTypeCzech =
                Subject.SubjectsTypes.Find(ST => ST.Name == "Czech"); // najde typ předmětu čeština
            Subject.listSubjectsWithOnlyOneType(SubjectTypeCzech); // vypíše všechny češtiny

            //string subjectName = Console.ReadLine();
            string subjectName = "Czech1_1";
            Subject ChosenSubject = Subject.selectSubjectsWithOnlyOneType(subjectName, SubjectTypeCzech);
            // výběr předmětu daného typu s daným názvem

            Lecture LectureFromCzech = LectureFactory.CreateLectureFromCzech(lectureName, credits, ChosenSubject);
            // vytvoření přednášky z Češtiny pomocí factory
        }

        /// <summary>
        /// Vytvoření přednášky z Angličtiny
        /// </summary>
        /// <param name="lectureName">Název přednášky</param>
        /// <param name="credits">Počet kreditů</param>
        private static void createEnglishLecture(string lectureName, double credits)
        {
            SubjectType SubjectTypeEnglish =
                Subject.SubjectsTypes.Find(ST => ST.Name == "English"); // najde typ předmětu angličtina
            Subject.listSubjectsWithOnlyOneType(SubjectTypeEnglish); // vypíše všechny angličtiny

            //string subjectName = Console.ReadLine();
            string subjectName = "english1_2";
            Subject ChosenSubject = Subject.selectSubjectsWithOnlyOneType(subjectName, SubjectTypeEnglish);
            // výběr předmětu daného typu s daným názvem

            Lecture LectureFromEnglish = LectureFactory.CreateLectureFromEnglish(lectureName, credits, ChosenSubject);
            // vytvoření přednášky z Angličtiny pomocí factory
        }

        /// <summary>
        /// Vytvoření cvičení z předmětu čeština
        /// </summary>
        /// <param name="exerciseName">Název cvičení</param>
        /// <param name="credits">Počet kreditů</param>
        private static void createCzechExercise(string exerciseName, double credits)
        {
            SubjectType SubjectTypeCzech =
                Subject.SubjectsTypes.Find(ST => ST.Name == "Czech"); // najde typ předmětu čeština
            Subject.listSubjectsWithOnlyOneType(SubjectTypeCzech); // vypíše všechny češtiny

            //string subjectName = Console.ReadLine();
            string subjectName = "czech1_2";
            Subject ChosenSubject = Subject.selectSubjectsWithOnlyOneType(subjectName, SubjectTypeCzech);
            // výběr předmětu daného typu s daným názvem

            Exercise ExerciseFromCzech = ExerciseFactory.CreateExerciseFromCzech(exerciseName, credits, ChosenSubject);
            // vytvoření cvičeni z Češtiny pomocí factory
        }

        /// <summary>
        /// Vytvoření cvičení z předmětu angličtina
        /// </summary>
        /// <param name="exerciseName">Název cvičení</param>
        /// <param name="credits">Počet kreditů</param>
        private static void createEnglishExercise(string exerciseName, double credits)
        {
            SubjectType SubjectTypeEnglish =
                Subject.SubjectsTypes.Find(ST => ST.Name == "English"); // najde typ předmětu angičtina
            Subject.listSubjectsWithOnlyOneType(SubjectTypeEnglish); // vypíše všechny angličtiny

            //string subjectName = Console.ReadLine();
            string subjectName = "english1_2";
            Subject ChosenSubject = Subject.selectSubjectsWithOnlyOneType(subjectName, SubjectTypeEnglish);
            // výběr předmětu daného typu s daným názvem

            Exercise ExerciseFromEnglish =
                ExerciseFactory.CreateExerciseFromEnglish(exerciseName, credits, ChosenSubject);
            // vytvoření cvičení z Angličtiny  pomocí factory
        }
    }
}