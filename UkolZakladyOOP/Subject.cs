using System;
using System.Collections.Generic;
using System.Linq;

namespace UkolZakladyOOP
{
    public class Subject
    {
        /// <summary>
        /// Jméno 
        /// </summary>
        public string name;

        /// <summary>
        /// Garant předmětu
        /// </summary>
        public Teacher garantOfSubject;

        /// <summary>
        /// Učitel
        /// </summary>
        public Teacher teacher;

        /// <summary>
        /// Počet kreditů potřeba k dokončení
        /// </summary>
        public double credits;

        /// <summary>
        /// Počet cvičení daného předmětu
        /// </summary>
        public int exerciseCount;

        /// <summary>
        /// Počet přednášek daného předmětu
        /// </summary>
        public int lectureCount;

        /// <summary>
        /// Ročník pro jaký je daný předmět
        /// </summary>
        public int year;

        /// <summary>
        /// Semestr pro jaký je daný předmět
        /// </summary>
        public Semester semester;

        /// <summary>
        /// Úroveň předmětu
        /// </summary>
        public int level;

        /// <summary>
        /// Jestli je předmět registrovaný
        /// </summary>
        public bool registered = false;

        /// <summary>
        /// Jestli je předmět dokončený
        /// </summary>
        public bool completed = false;

        /// <summary>
        /// Seznam všech předmětů
        /// </summary>
        public static List<Subject> subjects = new();

        /// <summary>
        /// Konstruktor. Automaticky přídá předmět d  seznamu předmětů
        /// </summary>
        /// <param name="name">Název</param>
        /// <param name="garantOfSubject">Garant předmětu</param>
        /// <param name="teacher">Učitel předmětu</param>
        /// <param name="credits">Počet kreditů potřeba k dokončení</param>
        /// <param name="year">Ročník pro jaký je daný předmět</param>
        /// <param name="semester">Semestr pro jaký je daný předmět</param>
        /// <param name="level">Úroveň předmětu</param>
        public Subject(string name, Teacher garantOfSubject, Teacher teacher, double credits, int year,
            Semester semester, int level)
        {
            this.name = name;
            this.garantOfSubject = garantOfSubject;
            this.teacher = teacher;
            this.credits = credits;
            this.semester = semester;
            this.year = year;
            subjects.Add(this);
            this.level = level;
        }

        /// <summary>
        /// Vybraní předmětu
        /// </summary>
        /// <param name="subjectName">Jméno předmětu</param>
        /// <returns>Vybraný předmět</returns>
        public static Subject selectSubject(string subjectName)
        {
            if (!subjects.Exists(s =>
                    s.name.ToLower() == subjectName.ToLower())) // kontrola jestli existuje předmět s daným názvem
            {
                Console.WriteLine("Neexistuje daný předmět");
                Console.WriteLine("Zadej název existujícího předmětu");
                subjectName = Console.ReadLine();
                Console.Clear();
                selectSubject(subjectName);
            }

            Subject Subject =
                subjects.Find(s => s.name.ToLower() == subjectName.ToLower()); // vybere předmět s daným názvem

            Console.WriteLine($"subject = {subjectName}");
            return Subject; // vrátí předmět s daným názvem
        }

        /// <summary>
        /// Výpis všech existujících předmětů
        /// </summary>
        public static void listAllSubjects()
        {
            foreach (Subject Subject in Subject.subjects) // projede všechny předměty ze seznamu předmětů
            {
                Console.WriteLine(
                    "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                    Subject.name, Subject.credits,
                    Subject.garantOfSubject.returnFullName(),
                    Subject.semester);
            }
        }

        /// <summary>
        /// Výpis všech předmětů jednoho typu
        /// například všech předmětů čeština
        /// </summary>
        /// <param name="subjectName">Typ předmětu (Czech, English,...)</param>
        public static void listOnlyOneTypeSubjects(string subjectName)
        {
            foreach (Subject Subject in
                     Subject.subjects.Where(s =>
                         s.name.Substring(0, 3).ToLower() ==
                         subjectName.Substring(0, 3)
                             .ToLower())) // projede všechny předměty které začínají první tři písmena z daného slova
            {
                Console.WriteLine(
                    "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                    Subject.name, Subject.credits,
                    Subject.garantOfSubject.returnFullName(),
                    Subject.semester);
            }
        }

        /// <summary>
        /// Metoda k výběru předmětu jenom z jednoho typu (čeština, angličtina,...)
        /// </summary>
        /// <param name="subjectName">Název předmětu</param>
        /// <param name="subjectType">Typ předmětu</param>
        /// <returns>Daný předmět</returns>
        public static Subject selectOnlyOneTypeSubject(string subjectName, string subjectType)
        {
            if (!Subject.subjects.Exists(subject =>
                    subject.name.ToLower() == subjectName) ||
                subjectName.ToLower().Substring(0, 3) !=
                subjectType.ToLower().Substring(0, 3)) // kontrola jestli existuje předmět daného typu s daným názvem
            {
                Console.WriteLine("Předmět?");
                Subject.listOnlyOneTypeSubjects(subjectType); // výpis všech předmětů daného typu
                subjectName = Console.ReadLine();
                selectOnlyOneTypeSubject(subjectName, subjectType);
            }

            return subjects.Find(subject => subject.name.ToLower() == subjectName.ToLower()); // vrátí předmět s daným názvem
        }
    }


    /// <summary>
    /// Factory
    /// </summary>
    class SubjectFactory
    {
        public static Subject CreateCzech(Teacher teacher, Teacher garantOfSubject, double credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject("Czech", teacher, garantOfSubject, credits, year, semester, subjectLevel);
        }

        public static Subject CreateEnglish(Teacher teacher, Teacher garantOfSubject, double credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject("English", teacher, garantOfSubject, credits, year, semester, subjectLevel);
        }
    }
}