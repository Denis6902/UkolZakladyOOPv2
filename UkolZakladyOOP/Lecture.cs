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

        public Lecture(string name, bool computerRequired, double credits, Subject subject, List<Lecture> lectures, Teacher Teacher)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            this.Teacher = Teacher;
            Teacher.lectures.Add(this);
            subject.lectureCount += 1;
        }
    }
    
    class LectureFactory
    {
        public static Lecture CreateLectureFromCzech(double credits, Subject Czech, List<Lecture> lectures, Teacher Teacher)
        {
            return new Lecture("Přednáška z Češtiny", false, credits, Czech, lectures, Teacher);
        }

        public static Lecture CreateLectureFromEnglish(double credits, Subject English, List<Lecture> lectures, Teacher Teacher)
        {
            return new Lecture("Přednáška z Angličtiny", false, credits, English, lectures, Teacher);
        }
    }
}