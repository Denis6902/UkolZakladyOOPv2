using System.Collections.Generic;
using System;

namespace UkolZakladyOOP
{
    public class Exercise
    {
        public string name;
        public bool computerRequired;
        public double credits;
        public Subject subject;
        public static List<Exercise> exercises = new();


        public Exercise(string name, bool computerRequired, double credits, Subject subject)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            exercises.Add(this);
            subject.exerciseCount += 1;
        }

        public string isComputerRequired()
        {
            switch (computerRequired)
            {
                case true:
                    return "je potřeba";
                    break;

                case false:
                    return "není potřeba";
                    break;
            }
        }

        public static Exercise selectExercise(string exerciseName)
        {
            Exercise chosenExercise = null;

            bool end = false;

            do
            {
                foreach (Exercise oneExercise in Exercise.exercises)
                {
                    if (oneExercise.name.ToLower() == exerciseName.ToLower())
                    {
                        chosenExercise = oneExercise;
                        end = true;
                    }
                }

                if (end == false)
                {
                    Console.WriteLine("Neexistuje daný předmět");
                    Console.WriteLine("Zadej název existujícího předmětu");
                    exerciseName = Console.ReadLine();
                }
            } while (end == false);

            Console.WriteLine($"exercise = {exerciseName}");


            return chosenExercise;
        }
    }


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