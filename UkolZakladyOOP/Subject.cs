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
        private SubjectType Type;

        /// <summary>
        /// Garant předmětu
        /// </summary>
        public Teacher GarantOfSubject;

        /// <summary>
        /// Učitel
        /// </summary>
        public Teacher Teacher;

        /// <summary>
        /// Počet kreditů získaných za dokončení
        /// </summary>
        public int Credits;

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
        /// <param name="type">Typ předmětu</param>
        /// <param name="garantOfSubject">Garant předmětu</param>
        /// <param name="teacher">Učitel předmětu</param>
        /// <param name="credits">Počet kreditů získaných za dokončení</param>
        /// <param name="year">Ročník pro jaký je daný předmět</param>
        /// <param name="semester">Semestr pro jaký je daný předmět</param>
        /// <param name="level">Úroveň předmětu</param>
        public Subject(string name, SubjectType type, Teacher garantOfSubject, Teacher teacher, int credits,
            int year,
            Semester semester, int level)
        {
            Name = name;
            Type = type;
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
            while (!Subjects.Exists(Subject => Subject.Name.ToLower() == subjectName.ToLower()))
            {
                Console.WriteLine("Neexistuje daný předmět");
                Console.WriteLine("Zadej název existujícího předmětu");
                subjectName = Console.ReadLine();
                Console.Clear();
                // pokud neexistuje, spustí znovu celý cyklus s novým vstupem od uživatele
            }

            Subject ChosenSubject = Subjects.Find(Subject => Subject.Name.ToLower() == subjectName.ToLower());
            // vybere předmět s daným názvem

            Console.WriteLine($"ChosenSubject = {ChosenSubject.Name}");
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
                    $"Předmět typu {Subject.Type.Name} s názvem {Subject.Name}, za dokončení získá {Subject.Credits} kreditů" +
                    $", garantem je {Subject.GarantOfSubject.returnFullName()}, semestr: {Subject.Semester}");
            }
        }

        /// <summary>
        /// Výpis všech předmětů jednoho typu
        /// například všech předmětů čeština
        /// </summary>
        /// <param name="subjectType">Typ předmětu (Czech, English,...)</param>
        public static void listSubjectsWithOnlyOneType(SubjectType subjectType)
        {
            // projede všechny předměty daného typu
            foreach (Subject Subject in Subject.Subjects.Where(Subject => Subject.Type == subjectType))
            {
                Console.WriteLine(
                    $"Předmět typu {Subject.Type.Name} s názvem {Subject.Name}, k dokončení je potřeba {Subject.Credits} kreditů," +
                    $" garantem je {Subject.GarantOfSubject.returnFullName()}, Semestr: {Subject.Semester}");
            }
        }

        /// <summary>
        /// Metoda k výběru předmětu jenom z jednoho typu (čeština, angličtina,...)
        /// </summary>
        /// <param name="subjectName">Název předmětu</param>
        /// <param name="subjectType">Typ předmětu</param>
        /// <returns>Daný předmět</returns>
        public static Subject selectSubjectsWithOnlyOneType(string subjectName, SubjectType subjectType)
        {
            // kontrola jestli existuje předmět daného typu s daným názvem
            while (!Subject.Subjects.Exists(Subject =>
                       Subject.Name.ToLower() == subjectName.ToLower() && Subject.Type == subjectType))
            {
                Console.WriteLine("Neexistuje (Nelze zvolit) daný předmět");
                Console.WriteLine("Zadej název existujícího předmětu daného typu");
                Subject.listSubjectsWithOnlyOneType(subjectType); // výpis všech předmětů daného typu
                subjectName = Console.ReadLine();
                Console.Clear();
            }

            Subject ChosenSubject = Subjects.Find(Subject =>
                Subject.Name.ToLower() == subjectName.ToLower()); // vrátí předmět s daným názvem

            Console.WriteLine($"ChosenSubject = {ChosenSubject.Name}");

            return ChosenSubject;
        }

        /// <summary>
        /// Výpis informací o předmětu
        /// </summary>
        /// <param name="Subject">Daný předmět k výpisu</param>
        /// <param name="creditsToFinish">Počet kreditů, kolik za dokončení získá</param>
        public void writeSubjectInfo(double creditsToFinish = Double.NaN)
        {
            // v případě že předmět není registrovaný,
            // tak se zavolá metoda bez parametru creditsToFinish
            // a tím pádem je parametr creditsToFinish NaN

            // pokud je creditsToFinish NaN, nastaví se daná proměnná na Subject.Credits
            if (Double.IsNaN(creditsToFinish))
            {
                creditsToFinish = Credits;
            }

            // výpis informací o předmětu
            Console.WriteLine(
                $"Předmět typu {Type.Name} s názvem {Name}" +
                $", za dokončení {creditsToFinish} kreditů," +
                $" garantem je {GarantOfSubject.returnFullName()}," +
                $" Semestr: {Semester} (Level {Level})");
        }

        public static void createNewSubjectType(string subjectTypeName)
        {
            SubjectsTypes.Add(new SubjectType(subjectTypeName, false));
        }
    }


    /// <summary>
    /// Factory
    /// </summary>
    class SubjectFactory
    {
        public static Subject CreateCzech(string subjectName, Teacher teacher, Teacher garantOfSubject, int credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject(subjectName, Subject.SubjectsTypes.Find(ST => ST.Name == "Czech"), teacher,
                garantOfSubject,
                credits, year, semester, subjectLevel);
        }

        public static Subject CreateEnglish(string subjectName, Teacher teacher, Teacher garantOfSubject,
            int credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject(subjectName, Subject.SubjectsTypes.Find(ST => ST.Name == "English"), teacher,
                garantOfSubject, credits, year, semester, subjectLevel);
        }
    }


    public class SubjectType
    {
        /// <summary>
        /// Název typu předmětu
        /// </summary>
        public string Name;

        /// <summary>
        /// Jestli jde vytvořit předmět daného typu pomocí Factory
        /// </summary>
        public bool HasFactory;

        public SubjectType(string name, bool hasFactory)
        {
            Name = name;
            HasFactory = hasFactory;
        }
    }
}