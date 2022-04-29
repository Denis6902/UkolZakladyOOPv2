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
        private Subject Subject;

        /// <summary>
        /// Učitel přednášky
        /// </summary>
        public Teacher Teacher;

        /// <summary>
        /// Seznam všech přednášek
        /// </summary>
        public static List<Lecture> Lectures = new();

        /// <summary>
        /// Konstruktor. Přidá cvičení do seznamu přednášek a zvyší počet přednášek u daného předmětu.
        /// </summary>
        /// <param name="name">Nazev přednášky</param>
        /// <param name="computerRequired">Jestli je potřeba na přednášku počitač</param>
        /// <param name="credits">Počet kreditů za přednášku</param>
        /// <param name="subject">Předmět ke kterému je přednáška dělaná</param>
        public Lecture(string name, bool computerRequired, double credits, Subject subject)
        {
            Name = name;
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
            if (!Lectures.Exists(lecture =>
                    lecture.Name.ToLower() == lectureName.ToLower() && lecture.Subject.Year == Student.Year &&
                    lecture.Subject.Semester ==
                    CurrentSemester)) // kontrola jestli existuje přednáška s daným názvem v aktuálním ročníku a semestru
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                lectureName = Console.ReadLine();
                Console.Clear();
                selectLecture(lectureName, Student, CurrentSemester);
            }

            Lecture ChosenLecture = Lectures.Find(lecture =>
                lecture.Name.ToLower() == lectureName.ToLower() && lecture.Subject.Year == Student.Year &&
                lecture.Subject.Semester ==
                CurrentSemester); // vybere existující přednášku s daným názvem v aktuálním ročníku a semestru

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
        /// Vypíše všechny přednášky daného studenta
        /// </summary>
        /// <param name="StudentSubjectList">Registrované předmety daného studenta</param>
        public static void listAllRegisteredLectures(List<SubjectStudent> StudentSubjectList)
        {
            foreach (SubjectStudent SubjectStudent in StudentSubjectList)
            {
                foreach (Lecture Lecture in Lecture.Lectures.Where(Lecture =>
                             SubjectStudent.Subject == Lecture.Subject &&
                             SubjectStudent.Subject.Registered && SubjectStudent.Subject.Completed == false))
                {
                    Console.WriteLine(
                        $"{Lecture.Name} - {Lecture.Credits} kreditů, počítač {Lecture.isComputerRequired()}" +
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
        public static Lecture CreateLectureFromCzech(double credits, Subject Czech)
        {
            return new Lecture("Přednáška z Češtiny", false, credits, Czech);
        }

        public static Lecture CreateLectureFromEnglish(double credits, Subject English)
        {
            return new Lecture("Přednáška z Angličtiny", false, credits, English);
        }
    }
}