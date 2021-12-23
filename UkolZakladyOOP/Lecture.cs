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

        public Lecture(string name, bool computerRequired, double credits, Subject subject,
            Teacher Teacher)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            this.Teacher = Teacher;
            lectures.Add(this);
            subject.lectureCount += 1;
        }
    }

    class LectureFactory
    {
        public static Lecture CreateLectureFromCzech(double credits, Subject Czech,
            Teacher Teacher)
        {
            return new Lecture("Přednáška z Češtiny", false, credits, Czech, Teacher);
        }

        public static Lecture CreateLectureFromEnglish(double credits, Subject English,
            Teacher Teacher)
        {
            return new Lecture("Přednáška z Angličtiny", false, credits, English, Teacher);
        }
    }
}