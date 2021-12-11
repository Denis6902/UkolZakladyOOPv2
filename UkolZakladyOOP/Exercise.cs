using System.Collections.Generic;

namespace UkolZakladyOOP
{
    public class Exercise
    {
        public string name;
        public bool computerRequired;
        public double credits;
        public Subject subject;


        public Exercise(string name, bool computerRequired, double credits, Subject subject, List<Exercise> exercises)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            exercises.Add(this);
            subject.exerciseCount += 1;
        }
        
    }


    class ExerciseFactory
    {
        public static Exercise CreateExerciseFromCzech(double credits, Subject Czech, List<Exercise> exercises)
        {
            return new Exercise("Cvičení z Češtiny", false, credits, Czech,exercises);
        }

        public static Exercise CreateExerciseFromEnglish(double credits, Subject English, List<Exercise> exercises)
        {
            return new Exercise("Cvičení z Angličtiny", false, credits, English,exercises);
        }
    }
}