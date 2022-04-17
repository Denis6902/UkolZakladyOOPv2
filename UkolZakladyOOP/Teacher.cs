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
        public string academicTitle;

        /// <summary>
        /// Seznam všech učitelů
        /// </summary>
        public static List<Teacher> teachers = new();

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
            this.academicTitle = academicTitle;
            teachers.Add(this); // přidání učitele do seznamu učitelů
        }

        /// <summary>
        /// Informace o učiteli
        /// </summary>
        public override void aboutMe()
        {
            Console.WriteLine(
                "Dobrý den, jmenuji se {0} {1} {2} a narodil/narodila jsem se {3} a aktuálně učím ve škole",
                academicTitle, firstName, lastName, birthDate.ToString("MM.dd.yyyy"));
        }

        /// <summary>
        /// Vrátí počet předmetů, který daný učitel učí
        /// </summary>
        /// <returns>Počet předmetů</returns>
        public int returnSubjectsCount()
        {
            int count = 0;
            foreach (Subject Subject in Subject.subjects) // Projede všechny předměty
            {
                if (Subject.teacher == this) // Pokud se učitel předmětu rovná danému učiteli 
                {
                    count++; // Zvyší počet předmětu, které daný učitel učí o 1
                }
            }

            return count;
        }

        /// <summary>
        /// Vrátí počet přednášek, který daný učitel učí
        /// </summary>
        /// <returns>Počet přednášek</returns>
        public int returnLecturesCount()
        {
            int count = 0;
            foreach (Lecture Lecture in Lecture.lectures) // Projede všechny přednášky
            {
                if (Lecture.Teacher == this) // Pokud se učitel přednásky rovná danému učiteli 
                {
                    count++; // Zvyší počet přednášek, které daný učitel učí o 1
                }
            }

            return count;
        }


        /// <summary>
        /// Vypíše všechny učitele
        /// </summary>
        public static void listAllTeachers()
        {
            foreach (Teacher Teacher in teachers)
            {
                Console.WriteLine("{0} - vyučuje {1} předmětů a {2} přednášky", Teacher.returnFullName(),
                    Teacher.returnSubjectsCount(), Teacher.returnLecturesCount());
            }
        }

        /// <summary>
        /// Vybere učitele ze seznamu učitelů
        /// </summary>
        /// <returns>Daného učitele</returns>
        public static Teacher selectTeacher()
        {
            Teacher chosenTeacher = null;
            bool end = false;
            do
            {
                foreach (Teacher Teacher in teachers) // Vypíše jména všech učitelů ze seznamu učitelů
                {
                    Console.WriteLine(Teacher.firstName);
                }

                //string teacherName = Console.ReadLine(); // načtení jména z konzole 
                string teacherName = "Pavel";

                foreach (Teacher Teacher in teachers) // projede seznam učitelů
                {
                    if (Teacher.firstName.ToLower() ==
                        teacherName.ToLower()) // pokud se zadané jméno rovná jménu v seznamu
                    {
                        chosenTeacher = Teacher; // vybere zvoleného učitele
                        end = true;
                    }
                }
            } while (end == false);

            return chosenTeacher;
        }

        /// <summary>
        /// Registarce předmětu
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        public void registerSubject(Semester currentSemester)
        {
            foreach (Subject Subject in Subject.subjects) // Projede seznam všech předmětů
            {
                if (currentSemester == Subject.semester &&
                    Subject.teacher == null) // Pokud je předmět v aktuálním semestru a předmět nemá žádného učitele
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                        Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                }
            }

            Console.WriteLine("Zadejte název předmětu");
            //string subjectName = Console.ReadLine();
            string subjectName = "xxx1_1"; // načtení názvu předmětu z konzole
            Subject SubjectForRegister =
                Subject.selectSubject(subjectName); // // vybrání zvoleného předmětu a uložení do proměnné

            SubjectForRegister.teacher = this; // nastaví zvolenému předmětu daného učitele
        }

        /// <summary>
        /// Vypíše všechny předměty daného učitele
        /// </summary>
        public void listAllMySubjects()
        {
            if (returnSubjectsCount() != 0) // Pokud je počet předmětů daného učitele jiný než 0 (žádný)
            {
                foreach (Subject Subject in Subject.subjects.Where(subject => subject.teacher == this)) // projede seznam předmětů kde je učitel předmětu roven danému učiteli
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                        Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
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
        public static void createNewSubject()
        {
            //Console.WriteLine("Jméno:");
            //string name = Console.ReadLine();
            string name = "Nový Předmět"; // načtení z konzole názvu nového předmětu
            Console.WriteLine("name = Nový předmět");

            Console.WriteLine("Garant předmětu: ");
            Teacher GarantOfSubject = UkolZakladyOOP.Teacher.selectTeacher(); // vybrání garanta předmětu
            Console.WriteLine("garantOfSubject = Pavel");

            Console.WriteLine("Učitel: ");
            Teacher Teacher = Teacher.selectTeacher(); // vybrání učitele 
            Console.WriteLine("teacher = Pavel");

            Console.WriteLine("Počet kreditů k dokončení = 50");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50; // načtení počtu kreditů z konzole 

            Semester Semester = Semester.Summer; // načtení z konzole semestru 
            Console.WriteLine("semester = Semester.Summer");

            //int year = int.Parse(Console.ReadLine()); // načtení z konzole ročníku
            Random random = new Random();
            int year = random.Next(1, 4);
            Console.WriteLine("year = " + year);

            //int subjectLevel = int.Parse(Console.ReadLine()); // načtení úrovně předmětu z konzole
            int subjectLevel = random.Next(1, 3);
            Console.WriteLine("subjectLevel = " + subjectLevel);

            Subject Subject = new(name, GarantOfSubject, Teacher, credits, year, Semester, subjectLevel);

            Thread.Sleep(10000);
            Console.Clear();
        }

        /// <summary>
        /// Vytvoření předmětu ze šablony (factory)
        /// </summary>
        public static void createSubjectFromTemplate()
        {
            string subject = "";

            do // vybrání jestli daný učitel chce vytvořit předmět Čeština nebo Angličtina.
            {
                Console.WriteLine("Předmět:");
                Console.WriteLine("Czech");
                Console.WriteLine("English");
                //subject = Console.ReadLine();
                subject = "Czech";
                Console.WriteLine("subject = " + subject);
            } while (subject.ToLower() != "czech" && subject.ToLower() != "english");

            Console.Clear();

            Console.WriteLine("Garant předmětu: ");
            Teacher GarantOfSubject = UkolZakladyOOP.Teacher.selectTeacher(); // Vybrání garanta předmětu
            Console.WriteLine("garantOfSubject = Pavel");

            Console.WriteLine("Učitel: ");
            Teacher Teacher = Teacher.selectTeacher(); // vybrání učitele předmětu
            Console.WriteLine("teacher = Pavel");

            Console.WriteLine("Počet kreditů k dokončení = 50");
            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole
            double credits = 50;

            Semester semester = Semester.Winter; // načtení semestru z konzole
            Console.WriteLine("Semester = Semester.Winter");

            //int year = int.Parse(Console.ReadLine()); // načtení ročníku z konzole 
            Random random = new Random();
            int year = random.Next(1, 4);
            Console.WriteLine("year = " + year);

            //int subjectLevel = int.Parse(Console.ReadLine());
            int subjectLevel = random.Next(1, 3); // načtení úrovně předmětu z konzole
            Console.WriteLine("subjectLevel = " + subjectLevel);

            switch (subject.ToLower()) // podle zvoleného předmětu vytvoří daný předmět
            {
                case "czech":
                    Subject Czech = SubjectFactory.CreateCzech(Teacher, GarantOfSubject, credits, year,
                        semester, subjectLevel);
                    break;

                case "english":
                    Subject English = SubjectFactory.CreateEnglish(Teacher, GarantOfSubject, credits, year,
                        semester, subjectLevel);
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
        public static void createNewExercise()
        {
            //Console.WriteLine("Jméno:");
            //string nameOfExercise = Console.ReadLine(); // načtení z konzole názvu cvičení
            string name = "Cvičení1";
            Console.WriteLine("nameOfExercise = Cvičení1");

            //Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine()); // načtení z konzole jestli je potřeba PC
            bool computerRequired = false;
            Console.WriteLine("computerRequired = false");

            //Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditů z konzole
            double credits = 22;
            Console.WriteLine("credits = 22");

            Subject.listAllSubjects(); // výpis všech předmětů

            string subjectName = "Czech1_1";
            Subject subject = Subject.selectSubject(subjectName); // vybrání předmwtu

            Exercise Exercise = new(name, computerRequired, credits, subject); // vytvoření nového předmětu

            Thread.Sleep(10000);
            Console.Clear();
        }

        /// <summary>
        /// Vytvoření cvičení ze šablony (factory)
        /// </summary>
        public static void createExerciseFromTemplate()
        {
            {
                string exercise = "";

                do // vybrání jestli daný učitel chce vytvořit cvičení z Češtiny nebo Angličtiny.
                {
                    Console.WriteLine("Jméno:");
                    Console.WriteLine("Cvičení z Češtiny");
                    Console.WriteLine("Cvičení z Angličtiny");
                    //string exercise = Console.ReadLine().ToLower();
                    exercise = "cvičení z češtiny";
                    Console.WriteLine("exercise = " + exercise);
                } while (exercise.ToLower() != "cvičení z češtiny" && exercise.ToLower() != "cvičení z angličtiny");

                //Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine()); // načtení počtu kreditl z konzole
                double credits = 50;
                Console.WriteLine("credits = 50");

                switch (exercise.ToLower()) // vytvoření zvoleného cvičení 
                {
                    case "cvičení z češtiny":
                    {
                        Console.WriteLine("Předmět?");
                        UkolZakladyOOP.Subject.listOnlyOneTypeSubjects("czech"); // výpis všech předmětů Čeština
                        //string subjectName = Console.ReadLine();
                        string subjectName = "Czech3_2"; // načtení názvu předmětu z konzole  
                        Subject Subject = Subject.selectSubject(subjectName); // vybrání předmětu

                        Exercise ExerciseFromCzech =
                            ExerciseFactory.CreateExerciseFromCzech(credits, Subject); // vytvoření předmětu
                        break;
                    }
                    case "cvičení z angličtiny":
                    {
                        Console.WriteLine("Předmět?");
                        UkolZakladyOOP.Subject.listOnlyOneTypeSubjects("english"); // výpis všech předmětů Čeština
                        //string subjectName = Console.ReadLine();
                        string subjectName = "English3_2"; // načtení názvu předmětu z konzole  
                        Subject Subject = Subject.selectSubject(subjectName); // vybrání předmětu

                        Exercise ExerciseFromEnglish =
                            ExerciseFactory.CreateExerciseFromEnglish(credits, Subject); // vytvoření předmětu
                        break;
                    }
                }
            }
        }
        

        /// <summary>
        /// Výpis studentů podle průměrných známek
        /// </summary>
        public static void listStudentsByAverageMarks()
        {
            foreach (Student Student in Student.students)
            {
                if (double.IsNaN(Student.calculateAverageMark()) ||
                    Student.calculateAverageMark() == 0) // Jestli student nemá žádné známky
                {
                    Console.WriteLine(Student.returnFullName() + " nemá žádnou známku");
                }
                else // Jinak vypíše průměrné známky
                {
                    Console.WriteLine(Student.calculateAverageMark() +
                                      " - průměrná známka ze všech předmětu studenta " +
                                      Student.returnFullName());
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
            Console.WriteLine("howCreate = 1;");

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
        public static void createNewLecture()
        {
            Console.WriteLine("Jméno = Přednáška 1");
            //string nameOfLecture = Console.ReadLine(); // načtení z konzole názvu přednášky
            string nameOfLecture = "Přednáška 1";

            Console.WriteLine("Nutnost PC? (true/false) = false");
            //bool computerRequired = bool.Parse(Console.ReadLine()); načtení z konzole jestli je potřeba PC
            bool computerRequired = false;

            Console.WriteLine("Počet kreditů = 22"); // načtení počtu kreditl z konzole 
            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;

            Console.WriteLine("Předmět:");
            Subject.listOnlyOneTypeSubjects("czech"); // výpis všech předmětů Čeština 
            //string subjectName = Console.ReadLine();
            string subjectName = "Czech1_1"; // načtení z konzole názvu předmětu
            Subject chosenSubject = Subject.selectSubject(subjectName); // vybrání předmětů

            Lecture Lecture = new(nameOfLecture, computerRequired, credits, chosenSubject); // vytvoření nové přednášky
        }

        /// <summary>
        /// Vytvoření nové přednášky ze šablony (factory)
        /// </summary>
        public static void createLectureFromTemplate()
        {
            string lecture = "";

            do // vybrání jestli daný učitel chce vytvořit přednášku z Češtiny nebo Angličtiny.
            {
                Console.WriteLine("Jméno:");
                Console.WriteLine("Přednáška z Češtiny");
                Console.WriteLine("Přednáška z Angličtiny");
                //string lecture = Console.ReadLine().ToLower();
                lecture = "přednáška z češtiny";
                Console.WriteLine("lecture = " + lecture);
            } while (lecture.ToLower() != "přednáška z češtiny" && lecture.ToLower() != "přednáška z angličtiny");


            Console.WriteLine("Počet kreditů k dokončení");
            //double credits = double.Parse(Console.ReadLine()); // načtení počtu krreditů z konzole
            double credits = 50;
            Console.WriteLine("credits = 50");


            switch (lecture.ToLower())
            {
                case "přednáška z češtiny":
                    createCzechLecture(credits); // vyvtoření přednášky z češtiny
                    break;
                case "přednáška z angličtiny":
                    createEnglishLecture(credits); // vytvoření přednášky z angličtiny
                    break;
            }
        }

        /// <summary>
        /// Vytvoření přednášky z Češtiny
        /// </summary>
        /// <param name="credits">Počet kreditů</param>
        public static void createCzechLecture(double credits)
        {
            Console.WriteLine("Předmět:");
            Subject.listOnlyOneTypeSubjects("czech"); // výpis všech předmětů Čeština
            //string subjectName = Console.ReadLine();
            string subjectName = "Czech3_2"; // načtení názvu předmětu z konzole
            Subject chosenSubject = Subject.selectSubject(subjectName); // vybrání daného předmětu

            Lecture LectureFromCzech =
                LectureFactory.CreateLectureFromCzech(credits, chosenSubject); // vytvoření přednášky z Češtiny
        }

        /// <summary>
        /// Vytvoření přednášky z Angličtiny
        /// </summary>
        /// <param name="credits">Počet kreditů</param>
        public static void createEnglishLecture(double credits)
        {
            Console.WriteLine("Předmět:");
            Subject.listOnlyOneTypeSubjects("english"); // výpis všech předmětů Angličtina
            //string subjectName = Console.ReadLine();
            string subjectName = "English3_2"; // načtení názvu předmětu z konzole
            Subject chosenSubject = Subject.selectSubject(subjectName); // vybrání daného předmětu

            Lecture LectureFromEnglish =
                LectureFactory.CreateLectureFromEnglish(credits, chosenSubject); // vytvoření přednášky z Angličtiny
        }
    }
}