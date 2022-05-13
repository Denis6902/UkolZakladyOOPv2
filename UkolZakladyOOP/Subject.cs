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
        public string Name;

        /// <summary>
        /// Typ předmětu
        /// </summary>
        public SubjectType SubjectType;

        /// <summary>
        /// Garant předmětu
        /// </summary>
        public Teacher GarantOfSubject;

        /// <summary>
        /// Učitel
        /// </summary>
        public Teacher Teacher;

        /// <summary>
        /// Počet kreditů potřeba k dokončení
        /// </summary>
        public double Credits;

        /// <summary>
        /// Počet cvičení daného předmětu
        /// </summary>
        public int ExerciseCount;

        /// <summary>
        /// Počet přednášek daného předmětu
        /// </summary>
        public int LectureCount;

        /// <summary>
        /// Ročník pro jaký je daný předmět
        /// </summary>
        public int Year;

        /// <summary>
        /// Semestr pro jaký je daný předmět
        /// </summary>
        public Semester Semester;

        /// <summary>
        /// Úroveň předmětu
        /// </summary>
        public int Level;

        /// <summary>
        /// Seznam všech předmětů
        /// </summary>
        public static List<Subject> Subjects = new();

        /// <summary>
        /// Seznam všech druhů předmětu
        /// </summary>
        public static List<SubjectType> SubjectsTypes = new();

        /// <summary>
        /// Konstruktor. Automaticky přídá předmět d  seznamu předmětů
        /// </summary>
        /// <param name="name">Název</param>
        /// <param name="subjectType">Typ předmětu</param>
        /// <param name="garantOfSubject">Garant předmětu</param>
        /// <param name="teacher">Učitel předmětu</param>
        /// <param name="credits">Počet kreditů potřeba k dokončení</param>
        /// <param name="year">Ročník pro jaký je daný předmět</param>
        /// <param name="semester">Semestr pro jaký je daný předmět</param>
        /// <param name="level">Úroveň předmětu</param>
        public Subject(string name, SubjectType subjectType, Teacher garantOfSubject, Teacher teacher, double credits,
            int year,
            Semester semester, int level)
        {
            Name = name;
            SubjectType = subjectType;
            GarantOfSubject = garantOfSubject;
            Teacher = teacher;
            Credits = credits;
            Semester = semester;
            Year = year;
            Level = level;
            Subjects.Add(this);
        }

        /// <summary>
        /// Vybraní předmětu
        /// </summary>
        /// <param name="subjectName">Jméno předmětu</param>
        /// <returns>Vybraný předmět</returns>
        public static Subject selectSubject(string subjectName)
        {
            // kontrola jestli existuje předmět s daným názvem
            if (!Subjects.Exists(Subject => Subject.Name.ToLower() == subjectName.ToLower()))
            {
                Console.WriteLine("Neexistuje daný předmět");
                Console.WriteLine("Zadej název existujícího předmětu");
                subjectName = Console.ReadLine();
                Console.Clear();
                selectSubject(subjectName); // pokud neexistuje, spustí znovu celou metodu s novým vstupem od uživatele
            }

            Subject ChosenSubject = Subjects.Find(Subject => Subject.Name.ToLower() == subjectName.ToLower());
            // vybere předmět s daným názvem

            Console.WriteLine($"subjectName = {subjectName}");
            return ChosenSubject; // vrátí předmět s daným názvem
        }

        /// <summary>
        /// Výpis všech existujících předmětů
        /// </summary>
        public static void listAllSubjects()
        {
            foreach (Subject Subject in Subject.Subjects) // projede všechny předměty ze seznamu předmětů
            {
                Console.WriteLine(
                    $"Předmět typu {Subject.SubjectType.Name} s názvem {Subject.Name}, k dokončení je potřeba {Subject.Credits} kreditů" +
                    $", garantem je {Subject.GarantOfSubject.returnFullName()}, semestr: {Subject.Semester}");
            }
        }

        /// <summary>
        /// Výpis všech předmětů jednoho typu
        /// například všech předmětů čeština
        /// </summary>
        /// <param name="subjectType">Typ předmětu (Czech, English,...)</param>
        public static void listOnlyOneTypeSubjects(SubjectType subjectType)
        {
            // projede všechny předměty daného typu
            foreach (Subject Subject in Subject.Subjects.Where(Subject => Subject.SubjectType == subjectType))
            {
                Console.WriteLine(
                    $"Předmět typu {Subject.SubjectType.Name} s názvem {Subject.Name}, k dokončení je potřeba {Subject.Credits} kreditů," +
                    $" garantem je {Subject.GarantOfSubject.returnFullName()}, Semestr: {Subject.Semester}");
            }
        }

        /// <summary>
        /// Metoda k výběru předmětu jenom z jednoho typu (čeština, angličtina,...)
        /// </summary>
        /// <param name="subjectName">Název předmětu</param>
        /// <param name="subjectType">Typ předmětu</param>
        /// <returns>Daný předmět</returns>
        public static Subject selectOnlyOneTypeSubject(string subjectName, SubjectType subjectType)
        {
            // kontrola jestli existuje předmět daného typu s daným názvem
            if (!Subject.Subjects.Exists(Subject => Subject.SubjectType == subjectType))
            {
                Console.WriteLine("Zvolte předmět");
                Subject.listOnlyOneTypeSubjects(subjectType); // výpis všech předmětů daného typu

                Console.WriteLine("Neexistuje daný předmět");
                Console.WriteLine("Zadej název existujícího předmětu");
                subjectName = Console.ReadLine();
                Console.Clear();
                selectOnlyOneTypeSubject(subjectName, subjectType);
                // pokud neexistuje, spustí znovu celou metodu s novým vstupem od uživatele
            }

            Subject ChosenSubject = Subjects.Find(Subject =>
                Subject.Name.ToLower() == subjectName.ToLower()); // vrátí předmět s daným názvem

            return ChosenSubject;
        }

        /// <summary>
        /// Výpis informací o předmětu
        /// </summary>
        /// <param name="Subject">Daný předmět k výpisu</param>
        /// <param name="credits">Počet kreditů</param>
        public static void writeSubjectInfo(Subject Subject, double credits = Double.NaN)
        {
            if (Double.IsNaN(credits))
            {
                credits = Subject.Credits;
            }

            Console.WriteLine(
                $"Předmět typu {Subject.SubjectType.Name} s názvem {Subject.Name}" +
                $", k dokončení je potřeba {credits} kreditů," +
                $" garantem je {Subject.GarantOfSubject.returnFullName()}," +
                $" Semestr: {Subject.Semester} (Level {Subject.Level})");
        }
    }


    /// <summary>
    /// Factory
    /// </summary>
    class SubjectFactory
    {
        public static Subject CreateCzech(string subjectName, Teacher teacher, Teacher garantOfSubject, double credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject(subjectName, Subject.SubjectsTypes.Find(ST => ST.Name == "Czech"), teacher,
                garantOfSubject,
                credits, year, semester, subjectLevel);
        }

        public static Subject CreateEnglish(string subjectName, Teacher teacher, Teacher garantOfSubject,
            double credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject(subjectName, Subject.SubjectsTypes.Find(ST => ST.Name == "English"), teacher,
                garantOfSubject, credits, year, semester, subjectLevel);
        }
    }

    public class SubjectType
    {
        public string Name;
        public bool HasFactory;

        public SubjectType(string name, bool hasFactory)
        {
            Name = name;
            HasFactory = hasFactory;
        }
    }
}