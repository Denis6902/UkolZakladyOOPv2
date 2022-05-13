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
            return Subject.Subjects.Count(Subject =>
                Subject.Teacher == this); // projede všechny předměty, které učí daný učitel
        }

        /// <summary>
        /// Vrátí počet přednášek, který daný učitel učí
        /// </summary>
        /// <returns>Počet přednášek</returns>
        private int returnLecturesCount()
        {
            return Lecture.Lectures.Count(Lecture =>
                Lecture.Teacher == this); // projede všechny přednášky, které učí daný učitel
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
                Console.WriteLine(Teacher.FirstName); // vypíše jména všech učitelů ze seznamu teachers
            }

            //string teacherName = Console.ReadLine();
            string teacherName = "Pavel";

            if (!Teachers.Exists(Teacher =>
                    Teacher.FirstName.ToLower() ==
                    teacherName.ToLower())) // jestli neexistuje učitele s daným jménem, spustí znovu metodu
            {
                Console.WriteLine("Neexistuje daný učitel");
                Console.WriteLine("Zadej jméno existujícího učitele");
                selectTeacher();
            }

            Teacher ChosenTeacher =
                Teachers.Find(Teacher =>
                    Teacher.FirstName.ToLower() == teacherName.ToLower()); // uloží do chosenTeacher daného učitele

            return ChosenTeacher;
        }

        /// <summary>
        /// Registarce předmětu
        /// </summary>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        public void registerSubject(Semester CurrentSemester)
        {
            foreach (Subject Subject in
                     Subject.Subjects.Where( // projede všechny přeměty v aktuánlím semestru, které nikdo neučí
                         Subject => CurrentSemester == Subject.Semester && Subject.Teacher == null))
            {
                Subject.writeSubjectInfo(Subject); // vypíše informace o předmětu
            }

            Console.WriteLine("Zadejte název předmětu");
            //string subjectName = Console.ReadLine();
            string subjectName = "xxx1_1"; // načtení názvu předmětu z konzole

            Subject SubjectForRegister =
                Subject.selectSubject(subjectName); // // vybrání zvoleného předmětu a uložení do proměnné
            SubjectForRegister.Teacher = this; // nastaví zvolenému předmětu daného učitele
        }

        /// <summary>
        /// Vypíše všechny předměty daného učitele
        /// </summary>
        public void listAllMySubjects()
        {
            if (returnSubjectsCount() != 0) // Pokud je počet předmětů daného učitele jiný než 0 (žádný)
            {
                foreach (Subject Subject in
                         Subject.Subjects.Where(Subject => Subject.Teacher == this))
                    // projede seznam předmětů
                    // kde je učitel předmětu
                    // roven danému učiteli
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
            Console.WriteLine("howCreate = 1;");

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
            //Console.WriteLine("Jméno:");
            //string name = Console.ReadLine();
            string subjectName = "Nový Předmět"; // načtení z konzole názvu nového předmětu
            Console.WriteLine($"subjectName = {subjectName}");

            Console.WriteLine("Garant předmětu: ");
            Teacher GarantOfSubject = Teacher.selectTeacher(); // vybrání garanta předmětu
            Console.WriteLine($"GarantOfSubject = {GarantOfSubject.returnFullName()}");

            Console.WriteLine("Učitel: ");
            Teacher ChosenTeacher = Teacher.selectTeacher(); // vybrání učitele 
            Console.WriteLine($"ChosenTeacher = {ChosenTeacher.returnFullName()}");

            Console.WriteLine("Počet kreditů k dokončení = 50");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50; // načtení počtu kreditů z konzole 

            Semester Semester = Semester.Summer; // načtení z konzole semestru 
            Console.WriteLine($"Semester = {Semester}");

            //int year = int.Parse(Console.ReadLine()); // načtení z konzole ročníku
            int year = Random.Shared.Next(1, 4);
            Console.WriteLine($"year = {year}");

            //int subjectLevel = int.Parse(Console.ReadLine()); // načtení úrovně předmětu z konzole
            int subjectLevel = Random.Shared.Next(1, 3);
            Console.WriteLine($"subjectLevel = {subjectLevel}");

            Subject Subject = new(subjectName, GarantOfSubject, ChosenTeacher, credits, year, Semester, subjectLevel);

            Thread.Sleep(10000);
            Console.Clear();
        }

        /// <summary>
        /// Vytvoření předmětu ze šablony (factory)
        /// </summary>
        private static void createSubjectFromTemplate()
        {
            string subjectName = "";

            do // vybrání jestli daný učitel chce vytvořit předmět Čeština nebo Angličtina.
            {
                Console.WriteLine("Předmět:");
                Console.WriteLine("Czech");
                Console.WriteLine("English");
                //subject = Console.ReadLine();
                subjectName = "Czech";
                Console.WriteLine($"subjectName = {subjectName}");
            } while (subjectName.ToLower() != "czech" && subjectName.ToLower() != "english");

            Console.Clear();

            Console.WriteLine("Garant předmětu: ");
            Teacher GarantOfSubject = Teacher.selectTeacher(); // vybrání garanta předmětu
            Console.WriteLine($"GarantOfSubject = {GarantOfSubject.returnFullName()}");

            Console.WriteLine("Učitel: ");
            Teacher ChosenTeacher = Teacher.selectTeacher(); // vybrání učitele 
            Console.WriteLine($"ChosenTeacher = {ChosenTeacher.returnFullName()}");

            Console.WriteLine("Počet kreditů k dokončení = 50");
            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole
            double credits = 50;

            Semester Semester = Semester.Winter; // načtení semestru z konzole
            Console.WriteLine($"Semester = {Semester}");

            //int year = int.Parse(Console.ReadLine()); // načtení ročníku z konzole 
            int year = Random.Shared.Next(1, 4);
            Console.WriteLine($"year = {year}");

            //int subjectLevel = int.Parse(Console.ReadLine());/ / načtení úrovně předmětu z konzole
            int subjectLevel = Random.Shared.Next(1, 3);
            Console.WriteLine($"$subjectLevel = {subjectLevel}");

            switch (subjectName.ToLower()) // podle zvoleného předmětu vytvoří daný předmět
            {
                case "czech":
                    Subject Czech = SubjectFactory.CreateCzech(ChosenTeacher, GarantOfSubject, credits, year,
                        Semester, subjectLevel);
                    break;

                case "english":
                    Subject English = SubjectFactory.CreateEnglish(ChosenTeacher, GarantOfSubject, credits, year,
                        Semester, subjectLevel);
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
            //Console.WriteLine("Jméno:");
            //string nameOfExercise = Console.ReadLine(); // načtení z konzole názvu cvičení
            string exerciseName = "Cvičení1";
            Console.WriteLine($"exerciseName = {exerciseName}");

            //Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine()); // načtení z konzole jestli je potřeba PC
            bool computerRequired = false;
            Console.WriteLine($"computerRequired = {computerRequired}");

            //Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole
            double credits = 22;
            Console.WriteLine($"credits = {credits}");

            Subject.listAllSubjects(); // výpis všech předmětů
            string subjectName = "Czech1_1";
            Subject ChosenSubject = Subject.selectSubject(subjectName); // vybrání předmwtu

            Exercise Exercise = new(exerciseName, computerRequired, credits,
                ChosenSubject); // vytvoření nového předmětu

            Thread.Sleep(10000);
            Console.Clear();
        }

        /// <summary>
        /// Vytvoření cvičení ze šablony (factory)
        /// </summary>
        private static void createExerciseFromTemplate()
        {
            {
                string exerciseName = "";

                do // vybrání jestli daný učitel chce vytvořit cvičení z Češtiny nebo Angličtiny.
                {
                    // TODO: udělat automatický výpis cyklem (cvičení z ...)
                    Console.WriteLine("Jakou přednášku chcete vytvořit?");

                    foreach (ExerciseType exerciseType in Exercise.ExercisesTypes)
                    {
                        Console.WriteLine(exerciseType.Name);
                    }

                    //exercise = Console.ReadLine().ToLower();
                    exerciseName = "cvičení z češtiny";
                    Console.WriteLine($"exerciseName = {exerciseName}");
                } while (!Exercise.ExercisesTypes.Exists(ET => ET.Name.ToLower() == exerciseName && ET.HasFactory == true));

                Console.ReadKey();
                

                //Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditl z konzole
                double credits = 50;
                Console.WriteLine($"credits = {credits}");

                switch (exerciseName.ToLower()) // vytvoření zvoleného cvičení 
                {
                    case "cvičení z češtiny":
                    {
                        createCzechExercise(credits);
                        break;
                    }
                    case "cvičení z angličtiny":
                    {
                        createEnglishExercise(credits);
                        break;
                    }
                    default:
                        Console.WriteLine("Dané cvičení nejde vytvořit pomocí factory");
                        break;
                }
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
                    Student.calculateAverageMark() == 0) // Jestli student nemá žádné známky
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
            //string nameOfLecture = Console.ReadLine(); // načtení z konzole názvu přednášky
            string lectureName = "Přednáška 1";
            Console.WriteLine($"lectureName = {lectureName}");

            //bool computerRequired = bool.Parse(Console.ReadLine()); načtení z konzole jestli je potřeba PC
            bool computerRequired = false;
            Console.WriteLine($"computerRequired = {computerRequired}");

            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;
            Console.WriteLine($"credits = {credits}"); // načtení počtu kreditl z konzole 

            Console.WriteLine("Předmět:");
            Subject.listOnlyOneTypeSubjects("czech"); // výpis všech předmětů Čeština 
            //string subjectName = Console.ReadLine();
            string subjectName = "Czech1_1"; // načtení z konzole názvu předmětu
            Subject ChosenSubject = Subject.selectSubject(subjectName); // vybrání předmětů

            Lecture Lecture = new(lectureName, computerRequired, credits, ChosenSubject); // vytvoření nové přednášky
        }

        /// <summary>
        /// Vytvoření nové přednášky ze šablony (factory)
        /// </summary>
        private static void createLectureFromTemplate()
        {
            string lectureName = "";

            do // vybrání jestli daný učitel chce vytvořit přednášku z Češtiny nebo Angličtiny.
            {
                Console.WriteLine("Jméno:");

                foreach (LectureType lectureType in Lecture.LecturesTypes.Where(LT => LT.HasFactory == true))
                {
                    Console.WriteLine(lectureType.Name);
                }
                
                //string lecture = Console.ReadLine().ToLower();
                lectureName = "přednáška z češtiny";
                Console.WriteLine($"lectureName = {lectureName}");
            } while (!Lecture.LecturesTypes.Exists(LT => LT.Name.ToLower() == lectureName && LT.HasFactory == true));
            
            //Console.WriteLine("Počet kreditů k dokončení");
            //double credits = double.Parse(Console.ReadLine()); // načtení počtu krreditů z konzole
            double credits = 50;
            Console.WriteLine($"credits = {credits}");


            switch (lectureName.ToLower())
            {
                case "přednáška z češtiny":
                    createCzechLecture(credits); // vyvtoření přednášky z češtiny
                    break;
                case "přednáška z angličtiny":
                    createEnglishLecture(credits); // vytvoření přednášky z angličtiny
                    break;
                default:
                    Console.WriteLine("Daná přednáška nejde vytvořit pomocí factory");
                    break;
            }
        }

        /// <summary>x
        /// Vytvoření přednášky z Češtiny
        /// </summary>
        /// <param name="credits">Počet kreditů</param>
        private static void createCzechLecture(double credits)
        {
            Subject.listOnlyOneTypeSubjects("czech"); // vypíše všechny češtiny
            //string subjectName = Console.ReadLine();
            string subjectName = "Czech1_1";
            Console.Clear();
            Subject ChosenSubject = Subject.selectOnlyOneTypeSubject(subjectName, "czech");
            // výběr předmětu daného typu s daným názvem

            Lecture LectureFromCzech = LectureFactory.CreateLectureFromCzech(credits, ChosenSubject);
            // vytvoření přednášky z Češtiny pomocí factory
        }

        /// <summary>
        /// Vytvoření přednášky z Angličtiny
        /// </summary>
        /// <param name="credits">Počet kreditů</param>
        private static void createEnglishLecture(double credits)
        {
            Subject.listOnlyOneTypeSubjects("english"); // vypíše všechny angličtiny
            //string subjectName = Console.ReadLine();
            string subjectName = "english1_2";
            Subject ChosenSubject = Subject.selectOnlyOneTypeSubject(subjectName, "english");
            // výběr předmětu daného typu s daným názvem

            Lecture LectureFromEnglish = LectureFactory.CreateLectureFromEnglish(credits, ChosenSubject);
            // vytvoření přednášky z Angličtiny pomocí factory
        }

        /// <summary>
        /// Vytvoření cvičení z předmětu čeština
        /// </summary>
        /// <param name="credits">Počet kreditů</param>
        private static void createCzechExercise(double credits)
        {
            Subject.listOnlyOneTypeSubjects("czech"); // vypíše všechny češtiny
            //string subjectName = Console.ReadLine();
            string subjectName = "czech1_2";
            Subject ChosenSubject = Subject.selectOnlyOneTypeSubject(subjectName, "czech");
            // výběr předmětu daného typu s daným názvem

            Exercise ExerciseFromCzech = ExerciseFactory.CreateExerciseFromCzech(credits, ChosenSubject);
            // vytvoření cvičeni z Češtiny pomocí factory
        }

        /// <summary>
        /// Vytvoření cvičení z předmětu angličtina
        /// </summary>
        /// <param name="credits">Počet kreditů</param>
        private static void createEnglishExercise(double credits)
        {
            Subject.listOnlyOneTypeSubjects("english"); // vypíše všechny angličtiny
            //string subjectName = Console.ReadLine();
            string subjectName = "english1_2";
            Subject ChosenSubject = Subject.selectOnlyOneTypeSubject(subjectName, "english");
            // výběr předmětu daného typu s daným názvem

            Exercise ExerciseFromEnglish = ExerciseFactory.CreateExerciseFromEnglish(credits, ChosenSubject);
            // vytvoření cvičení z Angličtiny  pomocí factory
        }
    }
}