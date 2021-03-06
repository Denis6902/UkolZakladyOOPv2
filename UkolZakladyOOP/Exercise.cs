using System.Collections.Generic;
using System;
using System.Linq;

namespace UkolZakladyOOP
{
    public class Exercise
    {
        /// <summary>
        /// Jméno cvičení
        /// </summary>
        public string Name;

        /// <summary>
        /// Typ cvičení
        /// </summary>
        private ExerciseType Type;

        /// <summary>
        /// Jestli je potřeba na cvičení počítač
        /// </summary>
        private bool ComputerRequired;

        /// <summary>
        /// Předmět ke kterému je cvičení dělané
        /// </summary>
        public Subject Subject;

        /// <summary>
        /// Seznam všech cvičení
        /// </summary>
        public static List<Exercise> Exercises = new();

        /// <summary>
        /// Seznam všech druhů cvičení
        /// </summary>
        public static List<ExerciseType> ExercisesTypes = new();

        /// <summary>
        /// Konstruktor. Přidá cvičení do seznamu cvíčení a zvyší počet cvičení u daného předmětu.
        /// </summary>
        /// <param name="name">Název cvičení</param>
        /// <param name="type">Typ cvičení</param>
        /// <param name="computerRequired">Jestli je potřeba na cvičení počítač</param>
        /// <param name="subject">Předmět ke kterému je cvičení dělané</param>
        public Exercise(string name, ExerciseType type, bool computerRequired, Subject subject)
        {
            Name = name;
            Type = type;
            ComputerRequired = computerRequired;
            Subject = subject;
            Exercises.Add(this);
            Subject.ExerciseCount += 1;
        }

        /// <summary>
        /// Jestli je počítač potřeba, vrátí string "je potřeba" jinak "není potřeba"
        /// </summary>
        /// <returns>je potřeba/není potřeba</returns>
        public string isComputerRequired()
        {
            return ComputerRequired switch
            {
                true => "je potřeba", // jestli computerRequired = true vrátí "je potřeba"
                false => "není potřeba" // jestli computerRequired = false vrátí "není potřeba"
            };
        }

        /// <summary>
        /// Vrátí dané cvičení
        /// </summary>
        /// <param name="exerciseName">Název cvičení</param>
        /// <param name="Student">Daný student</param>
        /// <param name="CurrentSemester">Aktuální semestr</param>
        /// <param name="CompletedExercises">Seznam dokončených cvičení</param>
        /// <returns>Vybrané cvičení</returns>
        public static Exercise selectExercise(string exerciseName, Student Student, Semester CurrentSemester,
            List<Exercise> CompletedExercises)
        {
            // kontrola jestli existuje cvičení s daným názvem v aktuálním ročníku a semestru
            while (!Exercises.Exists(Exercise =>
                       Exercise.Name.ToLower() == exerciseName.ToLower() && Exercise.Subject.Year == Student.Year &&
                       Exercise.Subject.Semester == CurrentSemester && !CompletedExercises.Contains(Exercise)))
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                exerciseName = Console.ReadLine();
                Console.Clear();
                // pokud neexistuje, spustí znovu celý cyklus s novým vstupem od uživatele
            }

            Exercise ChosenExercise = Exercises.Find(Exercise =>
                Exercise.Name.ToLower() == exerciseName.ToLower() && Exercise.Subject.Year == Student.Year &&
                Exercise.Subject.Semester == CurrentSemester);
            // vybere existující cvičení s daným názvem v aktuálním ročníku a semestru

            return ChosenExercise; // vratí cvičení s daným názvem v aktuálním ročníku a semestru
        }

        /// <summary>
        /// Výpis všech cvičení ze seznamu cvičení
        /// </summary>
        public static void listAllExercise()
        {
            if (Exercises.Any()) // Kontrola jestli existuje nějaké cvičení v seznamu
            {
                foreach (Exercise Exercise in Exercises) // projede všechny cvičení ze seznamu cvičení
                {
                    Console.WriteLine(
                        $"Cvičení typu {Exercise.Type.Name} s názvem {Exercise.Name}, " +
                        $"počítač {Exercise.isComputerRequired()} (Předmět {Exercise.Subject.Name}");
                }
            }
            else // Pokud ne, tak ...
            {
                Console.WriteLine("Neexistuje žádné cvičení");
            }
        }

        /// <summary>
        /// Výpis všech registrovaných cvičení
        /// </summary>
        /// <param name="SubjectMarkList">Registrované předmety daného studenta</param>
        /// <param name="CompletedExercises">Seznam dokončených cvičení</param>
        public static void listAllAvailableExercise(List<SubjectMark> SubjectMarkList,
            List<Exercise> CompletedExercises)
        {
            foreach (SubjectMark SubjectMark in
                     SubjectMarkList) // projede všechny předměty daného studenta
            {
                foreach (Exercise Exercise in Exercise.Exercises.Where(Exercise =>
                             SubjectMark.Subject == Exercise.Subject &&
                             !SubjectMark.Completed &&
                             !CompletedExercises
                                 .Contains(
                                     Exercise))) //projede všechny dostupné cvičení daného studenta (cvičení jejichž předmět mají registrovaný a nedokončený)
                {
                    Console.WriteLine($"{Exercise.Name}," +
                                      $" počítač {Exercise.isComputerRequired()}");
                }
            }
        }

        /// <summary>
        /// Vrátí jestli jsou dostupně nějaké cvičení
        /// </summary>
        /// <param name="SubjectMarkList">Registrované předmety daného studenta</param>
        /// <param name="CompletedExercises">Seznam dokončených cvičení</param>
        /// <returns></returns>
        public static bool CheckAvailableExercisesCount(List<SubjectMark> SubjectMarkList,
            List<Exercise> CompletedExercises)
        {
            return SubjectMarkList.Where(SM => !SM.Completed).Any(SubjectMark =>
                SubjectMark.Subject.ExerciseCount != CompletedExercises.Count(CE => CE.Subject == SubjectMark.Subject));
        }
    }

    /// <summary>
    /// Factory 
    /// </summary>
    class ExerciseFactory
    {
        public static Exercise CreateExerciseFromCzech(string name, Subject Czech)
        {
            return new Exercise(name, Exercise.ExercisesTypes.Find(ET => ET.Name == "Cvičení z Češtiny"), false, Czech);
        }

        public static Exercise CreateExerciseFromEnglish(string name, Subject English)
        {
            return new Exercise(name, Exercise.ExercisesTypes.Find(ET => ET.Name == "Cvičení z Angličtiny"), false,
                English);
        }
    }

    public class ExerciseType
    {
        /// <summary>
        /// Název typu cvičení
        /// </summary>
        public string Name;

        /// <summary>
        /// Typ předmětu daného cvičení
        /// </summary>
        public SubjectType SubjectType;

        /// <summary>
        /// Jestli jde vytvořit cvičení daného typu pomocí Factory
        /// </summary>
        public bool HasFactory;

        public ExerciseType(string name, SubjectType subjectType, bool hasFactory)
        {
            Name = name;
            SubjectType = subjectType;
            HasFactory = hasFactory;
        }
    }
}