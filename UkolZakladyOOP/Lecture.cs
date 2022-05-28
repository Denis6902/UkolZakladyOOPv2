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
        private LectureType Type;

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
        /// <param name="type">Typ přednášky</param>
        /// <param name="computerRequired">Jestli je potřeba na přednášku počitač</param>
        /// <param name="credits">Počet kreditů za přednášku</param>
        /// <param name="subject">Předmět ke kterému je přednáška dělaná</param>
        public Lecture(string name, LectureType type, bool computerRequired, double credits, Subject subject)
        {
            Name = name;
            Type = type;
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
        /// Zvolení přednášky
        /// </summary>
        /// <param name="lectureName">Název cvičení</param>
        /// <param name="Student">Daný student</param>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        /// <returns>Danou přednášku</returns>
        public static Lecture selectLecture(string lectureName, Student Student, Semester CurrentSemester)
        {
            // kontrola jestli existuje přednáška s daným názvem v aktuálním ročníku a semestru
            while (!Lectures.Exists(Lecture =>
                       Lecture.Name.ToLower() == lectureName.ToLower() && Lecture.Subject.Year == Student.Year &&
                       Lecture.Subject.Semester == CurrentSemester))
            {
                Console.WriteLine("Neexistuje dané cvičení / Předmět dané přednášky nemáš zaregistrovaný");
                Console.WriteLine("Zadej název platného cvičení");
                lectureName = Console.ReadLine();
                Console.Clear();
                // pokud neexistuje, spustí znovu celý cyklus s novým vstupem od uživatele
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
            if (Lectures.Any()) // Kontrola jestli existují nějaké přednášky
            {
                foreach (Lecture Lecture in Lectures)
                {
                    Console.WriteLine($"Předmět {Lecture.Name}, k dokončení je potřeba {Lecture.Credits} kreditů," +
                                      $" z {Lecture.Subject.Name}");
                }
            }
            else // Pokud ne, tak...
            {
                Console.WriteLine("Neexistuje žádná přednáška");
            }
        }

        /// <summary>
        /// Vypíše všechny přednášky dostupné pro registrované předměty daného studenta
        /// </summary>
        /// <param name="SubjectMarkList">Registrované předmety daného studenta</param>
        public static void listAllAvailableLectures(List<SubjectMark> SubjectMarkList)
        {
            foreach (Lecture Lecture in Lecture.Lectures.Where(
                         Lecture => Lecture.Subject == SubjectMarkList
                             .Find(SM => SM.Subject == Lecture.Subject && !SM.Completed)?.Subject))
            {
                Console.WriteLine(
                    $"Přednáška typu {Lecture.Type.Name} s názvem {Lecture.Name} - {Lecture.Credits} kreditů, počítač {Lecture.isComputerRequired()}" +
                    $" (Předmět {Lecture.Subject.Name})");
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
        /// <summary>
        /// Název typu přednášky
        /// </summary>
        public string Name;

        /// <summary>
        /// Typ předmětu dané přednášky
        /// </summary>
        public SubjectType SubjectType;

        /// <summary>
        /// Jestli jde vytvořit přednáška daného typu pomocí Factory
        /// </summary>
        public bool HasFactory;

        public LectureType(string name, SubjectType subjectType, bool hasFactory)
        {
            Name = name;
            SubjectType = subjectType;
            HasFactory = hasFactory;
        }
    }
}