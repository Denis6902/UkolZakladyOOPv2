using System.Collections.Generic;
using System;

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
        public ExerciseType ExerciseType;

        /// <summary>
        /// Jestli je potřeba na cvičení počítač
        /// </summary>
        private bool ComputerRequired;

        /// <summary>
        /// Počet kreditů za cvičení
        /// </summary>
        public double Credits;

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
        /// <param name="exerciseType">Typ cvičení</param>
        /// <param name="computerRequired">Jestli je potřeba na cvičení počítač</param>
        /// <param name="credits">Počet kreditů za cvičení</param>
        /// <param name="subject">Předmět ke kterému je předmět dělaný</param>
        public Exercise(string name, ExerciseType exerciseType, bool computerRequired, double credits, Subject subject)
        {
            Name = name;
            ExerciseType = exerciseType;
            ComputerRequired = computerRequired;
            Credits = credits;
            Subject = subject;
            Exercises.Add(this);
            subject.ExerciseCount += 1;
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
        /// <param name="CurrentSemester">Aktuánlí semestr</param>
        /// <returns>Vybrané cvičení</returns>
        public static Exercise selectExercise(string exerciseName, Student Student, Semester CurrentSemester)
        {
            // kontrola jestli existuje cvičení s daným názvem v aktuálním ročníku a semestru
            if (!Exercises.Exists(exercise =>
                    exercise.Name.ToLower() == exerciseName.ToLower() && exercise.Subject.Year == Student.Year &&
                    exercise.Subject.Semester == CurrentSemester))
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                exerciseName = Console.ReadLine();
                Console.Clear();
                selectExercise(exerciseName, Student, CurrentSemester);
                // pokud neexistuje, spustí znovu celou metodu s novým vstupem od uživatele
            }

            Exercise ChosenExercise = Exercises.Find(exercise =>
                exercise.Name.ToLower() == exerciseName.ToLower() && exercise.Subject.Year == Student.Year &&
                exercise.Subject.Semester == CurrentSemester);
            // vybere existující cvičení s daným názvem v aktuálním ročníku a semestru

            return ChosenExercise; // vratí cvičení s daným názvem v aktuálním ročníku a semestru
        }

        /// <summary>
        /// Výpis všech cvičení ze seznamu cvičení
        /// </summary>
        public static void listAllExercise()
        {
            if (Exercises.Count == 0) // Jestli je počet cvičení větší než 0
            {
                Console.WriteLine("Neexistuje žádné cvičení");
            }
            else // Jinak vypíše cvičení ze seznamu cvičení
            {
                foreach (Exercise Exercise in Exercises) // projede všechny cvičení ze seznamu cvičení
                {
                    Console.WriteLine(
                        $"Cvičení typu {Exercise.ExerciseType.Name} s názvem {Exercise.Name} - {Exercise.Credits} kreditů, " +
                        $"počítač {Exercise.isComputerRequired()} (Předmět {Exercise.Subject.Name}");
                }
            }
        }
    }

    /// <summary>
    /// Factory 
    /// </summary>
    class ExerciseFactory
    {
        public static Exercise CreateExerciseFromCzech(string name, double credits, Subject Czech)
        {
            return new Exercise(name, Exercise.ExercisesTypes.Find(ET => ET.Name == "Czech"), false, credits, Czech);
        }

        public static Exercise CreateExerciseFromEnglish(string name, double credits, Subject English)
        {
            return new Exercise(name, Exercise.ExercisesTypes.Find(ET => ET.Name == "Czech"), false, credits, English);
        }
    }

    public class ExerciseType
    {
        public string Name;
        public bool HasFactory;

        public ExerciseType(string name, bool hasFactory)
        {
            Name = name;
            HasFactory = hasFactory;
        }
    }
}