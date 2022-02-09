using System.Collections.Generic;

namespace UkolZakladyOOP
{
    public class Lecture
    {
        public string name;
        public bool computerRequired;
        public double credits;
        public Subject subject;
        public Teacher Teacher;
        public static List<Lecture> lectures = new();

        public Lecture(string name, bool computerRequired, double credits, Subject subject)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            this.Teacher = this.subject.teacher;
            lectures.Add(this);
            subject.lectureCount += 1;
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

    class LectureFactory
    {
        public static Lecture CreateLectureFromCzech(double credits, Subject Czech)
        {
            return new Lecture("Přednáška z Češtiny", false, credits, Czech);
        }

        public static Lecture CreateLectureFromEnglish(double credits, Subject English)
        {
            return new Lecture("Přednáška z Angličtiny", false, credits, English);
        }
    }
}