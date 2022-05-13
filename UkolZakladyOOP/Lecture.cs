using System;
using System.Collections.Generic;
using System.Linq;

namespace UkolZakladyOOP
{
    public class Lecture
    {
        /// <summary>
        /// Nazev přednášky
        /// </summary>
        public string Name;

        /// <summary>
        /// Typ přednášky
        /// </summary>
        public LectureType LectureType;

        /// <summary>
        /// Jestli je potřeba na přednášku počitač
        /// </summary>
        private bool ComputerRequired;

        /// <summary>
        /// Počet kreditů za přednášku
        /// </summary>
        public double Credits;

        /// <summary>
        /// Předmět ke kterému je přednáška dělaná
        /// </summary>
        public Subject Subject;

        /// <summary>
        /// Učitel přednášky
        /// </summary>
        public Teacher Teacher;

        /// <summary>
        /// Seznam všech přednášek
        /// </summary>
        public static List<Lecture> Lectures = new();

        /// <summary>
        /// Seznam všech druhů přednášek
        /// </summary>
        public static List<LectureType> LecturesTypes = new();

        /// <summary>
        /// Konstruktor. Přidá cvičení do seznamu přednášek a zvyší počet přednášek u daného předmětu.
        /// </summary>
        /// <param name="name">Nazev přednášky</param>
        /// <param name="lectureType">Typ přednášky</param>
        /// <param name="computerRequired">Jestli je potřeba na přednášku počitač</param>
        /// <param name="credits">Počet kreditů za přednášku</param>
        /// <param name="subject">Předmět ke kterému je přednáška dělaná</param>
        public Lecture(string name, LectureType lectureType, bool computerRequired, double credits, Subject subject)
        {
            Name = name;
            LectureType = lectureType;
            ComputerRequired = computerRequired;
            Credits = credits;
            Subject = subject;
            Teacher = Subject.Teacher;
            Lectures.Add(this);
            subject.LectureCount += 1;
        }

        /// <summary>
        /// Jestli je počítač potřeba, vrátí string "je potřeba" jinak "není potřeba"
        /// </summary>
        /// <returns>je potřeba/není potřeba</returns>
        private string isComputerRequired()
        {
            return ComputerRequired switch
            {
                true => "je potřeba", // jestli computerRequired = true vrátí "je potřeba"
                false => "není potřeba" // jestli computerRequired = false vrátí "není potřeba"
            };
        }

        /// <summary>
        /// Zvolení přednáška
        /// </summary>
        /// <param name="lectureName">Název cvičení</param>
        /// <param name="Student">Daný student</param>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        /// <returns>Danou přednášku</returns>
        public static Lecture selectLecture(string lectureName, Student Student, Semester CurrentSemester)
        {
            // kontrola jestli existuje přednáška s daným názvem v aktuálním ročníku a semestru
            if (!Lectures.Exists(Lecture =>
                    Lecture.Name.ToLower() == lectureName.ToLower() && Lecture.Subject.Year == Student.Year &&
                    Lecture.Subject.Semester == CurrentSemester)) 
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                lectureName = Console.ReadLine();
                Console.Clear();
                selectLecture(lectureName, Student, CurrentSemester);
                // pokud neexistuje, spustí znovu celou metodu s novým vstupem od uživatele
            }

            Lecture ChosenLecture = Lectures.Find(Lecture =>
                Lecture.Name.ToLower() == lectureName.ToLower() && Lecture.Subject.Year == Student.Year &&
                Lecture.Subject.Semester == CurrentSemester);
            // vybere existující přednášku s daným názvem v aktuálním ročníku a semestru

            return ChosenLecture; // vratí přednášku s daným názvem v aktuálním ročníku a semestru
        }

        /// <summary>
        /// Výpis všech přednášek
        /// </summary>
        public static void listAllLectures()
        {
            if (Lectures.Count == 0) // Jestli je počet přednášek větší než 0
            {
                Console.WriteLine("Neexistuje žádná přednáška");
            }
            else // Jinak vypíše přednášku ze seznamu přednášek
            {
                foreach (Lecture Lecture in Lectures)
                {
                    Console.WriteLine($"Předmět {Lecture.Name}, k dokončení je potřeba {Lecture.Credits} kreditů," +
                                      $" z {Lecture.Subject.Name}");
                }
            }
        }

        /// <summary>
        /// Vypíše všechny přednášky dostupné pro registrované předměty daného studenta
        /// </summary>
        /// <param name="SubjectMarkList">Registrované předmety daného studenta</param>
        public static void listAllAvailableLectures(List<SubjectMark> SubjectMarkList)
        {
            foreach (SubjectMark SubjectMark in SubjectMarkList) // projede předměty daného studenta
            {
                // projede přednášky předmětů, které má daný student nedokončené
                foreach (Lecture Lecture in Lecture.Lectures.Where(
                             Lecture => SubjectMark.Subject == Lecture.Subject && SubjectMark.Completed == false))
                {
                    Console.WriteLine(
                        $"Přednáška typu {Lecture.LectureType.Name} s názvem {Lecture.Name} - {Lecture.Credits} kreditů, počítač {Lecture.isComputerRequired()}" +
                        $" (Předmět {Lecture.Subject.Name})");
                }
            }
        }
    }

    /// <summary>
    /// Factory
    /// </summary>
    class LectureFactory
    {
        public static Lecture CreateLectureFromCzech(string name, double credits, Subject Czech)
        {
            return new Lecture(name, Lecture.LecturesTypes.Find(LT => LT.Name == "Czech"), false, credits, Czech);
        }

        public static Lecture CreateLectureFromEnglish(string name, double credits, Subject English)
        {
            return new Lecture(name, Lecture.LecturesTypes.Find(LT => LT.Name == "Czech"), false,
                credits, English);
        }
    }

    public class LectureType
    {
        public string Name;
        public bool HasFactory;

        public LectureType(string name, bool hasFactory)
        {
            Name = name;
            HasFactory = hasFactory;
        }
    }
}