using System.Collections.Generic;

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