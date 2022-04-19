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
                true => "je potřeba", // jestli computerRequired = true vrátí "je potřeba"
                false => "není potřeba" // jestli computerRequired = false vrátí "není potřeba"
            };
        }

        /// <summary>
        /// Zvolení přednáška
        /// </summary>
        /// <param name="lectureName">Název cvičení</param>
        /// <param name="student">Daný student</param>
        /// <param name="currentSemester">Aktuální semestr</param>
        /// <returns>Danou přednášku</returns>
        public static Lecture selectLecture(string lectureName, Student student, Semester currentSemester)
        {
            if (!lectures.Exists(lecture =>
                    lecture.name.ToLower() == lectureName.ToLower() && lecture.subject.year == student.year &&
                    lecture.subject.semester ==
                    currentSemester)) // kontrola jestli existuje přednáška s daným názvem v aktuálním ročníku a semestru
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                lectureName = Console.ReadLine();
                Console.Clear();
                selectLecture(lectureName, student, currentSemester);
            }

            Lecture chosenLecture = lectures.Find(lecture =>
                lecture.name.ToLower() == lectureName.ToLower() && lecture.subject.year == student.year &&
                lecture.subject.semester ==
                currentSemester); // vybere existující přednášku s daným názvem v aktuálním ročníku a semestru

            return chosenLecture; // vratí přednášku s daným názvem v aktuálním ročníku a semestru
        }

        /// <summary>
        /// Výpis všech přednášek
        /// </summary>
        public static void listAllLectures()
        {
            if (lectures.Count == 0) // Jestli je počet přednášek větší než 0
            {
                Console.WriteLine("Neexistuje žádná přednáška");
            }
            else // Jinak vypíše přednášku ze seznamu přednášek
            {
                foreach (Lecture Lecture in lectures)
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, z {2}", Lecture.name,
                        Lecture.credits, Lecture.subject.name);
                }
            }
        }

        /// <summary>
        /// Vypíše všechny přednášky daného studenta
        /// </summary>
        /// <param name="studentSubjectList">Registrované předmety daného studenta</param>
        public static void listAllRegisteredLectures(List<SubjectStudent> studentSubjectList)
        {
            foreach (SubjectStudent SubjectStudent in studentSubjectList)
            {
                foreach (Lecture oneLecture in Lecture.lectures.Where(oneLecture =>
                             SubjectStudent.Subject == oneLecture.subject &&
                             SubjectStudent.Subject.registered && SubjectStudent.Subject.completed == false))
                {
                    Console.WriteLine("{0} - {1} kreditů, počítač {2} (Předmět {3})",
                        oneLecture.name, oneLecture.credits, oneLecture.isComputerRequired(),
                        oneLecture.subject.name);
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