using System.Collections.Generic;

namespace UkolZakladyOOP
{
    public class MarkSubject
    {
        public double mark;
        public Subject Subject;
        public Student Student;

        public MarkSubject(double mark, Subject Subject, Student Student, List<MarkSubject> markSubjectList)
        {
            this.mark = mark;
            this.Subject = Subject;
            this.Student = Student;
            markSubjectList.Add(this);
        }
    }

    public class SubjectStudent
    {
        public Subject Subject;
        public Student Student;
        public int level;
        public double credits;

        public SubjectStudent(Subject Subject, Student Student, int level, List<SubjectStudent> subjectStudentList)
        {
            this.Subject = Subject;
            this.Student = Student;
            this.level = level;
            this.credits = Subject.credits;
            subjectStudentList.Add(this);
        }
    }
}