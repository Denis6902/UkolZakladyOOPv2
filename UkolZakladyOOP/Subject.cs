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
        public SubjectType Type;

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
        /// Maximální počet skupin
        /// </summary>
        public int MaxGroupCount;

        /// <summary>
        /// Maximální počet studentů v jedné skupině
        /// </summary>  
        public int MaxStudentsInGroup;

        /// <summary>
        /// Seznam všech předmětů
        /// </summary>
        public static List<Subject> Subjects = new();

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
        /// <param name="maxGroupCount">Maximální počet skupin</param>
        /// <param name="maxStudentsInGroup">Maximální počet studentů v jedné skupině</param>
        public Subject(string name, SubjectType type, Teacher garantOfSubject, Teacher teacher, int credits,
            int year, Semester semester, int level, int maxGroupCount, int maxStudentsInGroup)
        {
            Name = name;
            Type = type;
            GarantOfSubject = garantOfSubject;
            Teacher = teacher;
            Credits = credits;
            Semester = semester;
            Year = year;
            Level = level;
            MaxGroupCount = maxGroupCount;
            MaxStudentsInGroup = maxStudentsInGroup;
            Subjects.Add(this);

            // Vytvoření skupin
            for (int i = 0; i < maxGroupCount; i++)
            {
                SubjectGroup SG = new(Teacher, maxStudentsInGroup);
            }
        }

        /// <summary>
        /// Vybraní předmětu
        /// </summary>
        /// <param name="subjectName">Jméno předmětu</param>
        /// <returns>Vybraný předmět</returns>
        public static Subject selectSubject(string subjectName)
        {
            // kontrola jestli existuje předmět s daným názvem
            while ((!Subjects.Exists(Subject => Subject.Name.ToLower() == subjectName.ToLower())) && subjectName != "")
            {
                Console.WriteLine("Neexistuje daný předmět");
                Console.WriteLine("Zadej název existujícího předmětu");
                subjectName = Console.ReadLine();
                Console.Clear();
                // pokud neexistuje, spustí znovu celý cyklus s novým vstupem od uživatele
            }

            if (subjectName != "")
            {
                Subject ChosenSubject = Subjects.Find(Subject => Subject.Name.ToLower() == subjectName.ToLower());
                // vybere předmět s daným názvem

                Console.WriteLine($"ChosenSubject = {ChosenSubject.Name}");
                return ChosenSubject; // vrátí předmět s daným názvem
            }
            else
            {
                return null;
            }
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
                
                // TODO: možná Subject.writeSubjectInfo(); ??
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
                
                // TODO: možná Subject.writeSubjectInfo(); ??
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
        /// Výpis informací o neregistrovaném předmětu
        /// </summary>
        public void writeSubjectInfo()
        {
            // výpis informací o předmětu
            Console.WriteLine(
                $"Předmět typu {Type.Name} s názvem {Name}" +
                $", za dokončení {Credits} kreditů," +
                $" garantem je {GarantOfSubject.returnFullName()}," +
                $" Semestr: {Semester} (Level {Level})" +
                $", zbýva {returnRemainingStudentCount()} míst");
        }

        /// <summary>
        /// Vrátí počet volných míst daného předmětu
        /// </summary>
        /// <returns>Počet volných míst</returns>
        public int returnRemainingStudentCount()
        {
            int count = 0;

            // projede všechny studenty
            Student.Students.ForEach(
                (OneStudent) =>
                {
                    // projede všechny registrované předměty
                    OneStudent.SubjectMarkList.ForEach(
                        (SM) =>
                        {
                            // pokud najde daný předmět, zvyší count o 1
                            if (SM.Subject == this)
                            {
                                count++;
                            }
                        }
                    );
                }
            );

            // vrátí násobek počtu skupin a maximálního počtu studentů v jedné skupině, odečtený od již obsazených míst
            return (MaxGroupCount * MaxStudentsInGroup) - count;
        }
    }


    /// <summary>
    /// Factory
    /// </summary>
    class SubjectFactory
    {
        public static Subject CreateCzech(string subjectName, Teacher teacher, Teacher garantOfSubject, int credits,
            int year, Semester semester, int subjectLevel, int maxGroupCount, int maxStudentsInGroup)
        {
            return new Subject(subjectName, SubjectType.SubjectsTypes.Find(ST => ST.Name == "Czech"), teacher,
                garantOfSubject, credits, year, semester, subjectLevel, maxGroupCount, maxStudentsInGroup);
        }

        public static Subject CreateEnglish(string subjectName, Teacher teacher, Teacher garantOfSubject,
            int credits, int year, Semester semester, int subjectLevel, int maxGroupCount, int maxStudentsInGroup)
        {
            return new Subject(subjectName, SubjectType.SubjectsTypes.Find(ST => ST.Name == "English"), teacher,
                garantOfSubject, credits, year, semester, subjectLevel, maxGroupCount, maxGroupCount);
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
        
        /// <summary>
        /// Seznam všech druhů předmětu
        /// </summary>
        public static List<SubjectType> SubjectsTypes = new();

        public SubjectType(string name, bool hasFactory)
        {
            Name = name;
            HasFactory = hasFactory;
        }
        
        public static void createNewSubjectType(string subjectTypeName)
        {
            SubjectsTypes.Add(new SubjectType(subjectTypeName, false));
        }
    }
}