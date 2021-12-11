using System.Collections.Generic;

namespace UkolZakladyOOP
{ 
    public class Subject
    {
        public string name;
        public Teacher garantOfSubject;
        public Teacher teacher;
        public double credits;
        public int exerciseCount;
        public int lectureCount;
        public int year;
        public Semester semester;
        public int level;
        public bool registered = false;
        public bool completed = false;
        
        
        public Subject(string name, Teacher garantOfSubject,Teacher teacher, double credits, int year, Semester semester, List<Subject> subjects, int level)
        {
            this.name = name;
            this.garantOfSubject = garantOfSubject;
            this.teacher = teacher;
            this.credits = credits;
            this.semester = semester;
            this.year = year;
            subjects.Add(this);
            this.level = level;
        }
    }
    
    
    class SubjectFactory
    {
        public static Subject CreateCzech(Teacher teacher, Teacher garantOfSubject, double credits, List<Subject> subjects, int year, Semester semester, int subjectLevel)
        {
            return new Subject("Czech", teacher,garantOfSubject, credits,year, semester, subjects,subjectLevel);
        }

        public static Subject CreateEnglish(Teacher teacher, Teacher garantOfSubject, double credits, List<Subject> subjects, int year, Semester semester, int subjectLevel)
        {
            return new Subject("English",teacher, garantOfSubject, credits,year, semester, subjects,subjectLevel);
        }
    }

}