using System.Collections.Generic;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace UkolZakladyOOP
{
    public class Exercise
    {
        /// <summary>
        /// Jméno cvičení
        /// </summary>
        public string name;

        /// <summary>
        /// Jestli je potřeba na cvičení počítač
        /// </summary>
        public bool computerRequired;

        /// <summary>
        /// Počet kreditů za cvičení
        /// </summary>
        public double credits;

        /// <summary>
        /// Předmět ke kterému je cvičení dělané
        /// </summary>
        public Subject subject;

        /// <summary>
        /// Seznam všech cvičení
        /// </summary>
        public static List<Exercise> exercises = new();

        /// <summary>
        /// Konstruktor. Přidá cvičení do seznamu cvíčení a zvyší počet cvičení u daného předmětu.
        /// </summary>
        /// <param name="name">Název cvičení</param>
        /// <param name="computerRequired">Jestli je potřeba na cvičení počítač</param>
        /// <param name="credits">Počet kreditů za cvičení</param>
        /// <param name="subject">Předmět ke kterému je předmět dělaný</param>
        public Exercise(string name, bool computerRequired, double credits, Subject subject)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            exercises.Add(this);
            subject.exerciseCount += 1;
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

        /// <summary>
        /// Vrátí dané cvičení
        /// </summary>
        /// <param name="exerciseName">Název cvičeneí</param>
        /// <param name="student">Daný student</param>
        /// <returns>Vybrané cvičení</returns>
        public static Exercise selectExercise(string exerciseName, Student student)
        {
            if (!exercises.Exists(exercise =>
                    exercise.name.ToLower() == exerciseName.ToLower() && exercise.subject.year == student.year))
            {
                Console.WriteLine("Neexistuje dané cvičení");
                Console.WriteLine("Zadej název existujícího cvičení");
                exerciseName = Console.ReadLine();
                Console.Clear();
                selectExercise(exerciseName, student);
            }

            Exercise chosenExercise = exercises.Find(exercise =>
                exercise.name.ToLower() == exerciseName.ToLower() && exercise.subject.year == student.year);

            return chosenExercise;
        }
        
        /// <summary>
        /// Výpis všech cvičení
        /// </summary>
        public static void listAllExercise()
        {
            if (exercises.Count == 0) // Jestli je počet cvičení větší než 0
            {
                Console.WriteLine("Neexistuje žádné cvičení");
            }
            else // Jinak vypíše cvičení ze seznamu cvičení
            {
                foreach (Exercise exercise in exercises)
                {
                    Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", exercise.name,
                        exercise.credits, exercise.computerRequired, exercise.subject.name);
                }
            }
        }
    }

    /// <summary>
    /// Factory 
    /// </summary>
    class ExerciseFactory
    {
        public static Exercise CreateExerciseFromCzech(double credits, Subject Czech)
        {
            return new Exercise("Cvičení z Češtiny", false, credits, Czech);
        }

        public static Exercise CreateExerciseFromEnglish(double credits, Subject English)
        {
            return new Exercise("Cvičení z Angličtiny", false, credits, English);
        }
    }
}