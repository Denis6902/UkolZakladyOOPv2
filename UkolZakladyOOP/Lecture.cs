using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UkolZakladyOOP
{
    public class Lecture
    {
        /// <summary>
        /// Nazev přednášky
        /// </summary>
        public string name;

        /// <summary>
        /// Jestli je potřeba na přednášku počitač
        /// </summary>
        public bool computerRequired;

        /// <summary>
        /// Počet kreditů za přednášku
        /// </summary>
        public double credits;

        /// <summary>
        /// Předmět ke kterému je přednáška dělaná
        /// </summary>
        public Subject subject;

        /// <summary>
        /// Učitel přednášky
        /// </summary>
        public Teacher Teacher;

        /// <summary>
        /// Seznam všech přednášek
        /// </summary>
        public static List<Lecture> lectures = new();

        /// <summary>
        /// Konstruktor. Přidá cvičení do seznamu přednášek a zvyší počet přednášek u daného předmětu.
        /// </summary>
        /// <param name="name">Nazev přednášky</param>
        /// <param name="computerRequired">Jestli je potřeba na přednášku počitač</param>
        /// <param name="credits">Počet kreditů za přednášku</param>
        /// <param name="subject">Předmět ke kterému je přednáška dělaná</param>
        public Lecture(string name, bool computerRequired, double credits, Subject subject)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            this.Teacher = this.subject.teacher;
            lectures.Add(this);
            subject.lectureCount += 1;
        }

        /// <summary>
        /// Jestli je počítač potřeba, vrátí string "je potřeba" jinak "není potřeba"
        /// </summary>
        /// <returns>je potřeba/není potřeba</returns>
        public string isComputerRequired()
        {
            return computerRequired switch
            {
                true => "je potřeba",
                false => "není potřeba"
            };
        }

        public static Lecture selectLecture(string lectureName)
        {
            if (!lectures.Exists(lecture => lecture.name.ToLower() == lectureName.ToLower()))
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                lectureName = Console.ReadLine();
                Console.Clear();
                selectLecture(lectureName);
            }

            Lecture chosenLecture = lectures.Find(lecture => lecture.name.ToLower() == lectureName.ToLower());

            return chosenLecture;
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