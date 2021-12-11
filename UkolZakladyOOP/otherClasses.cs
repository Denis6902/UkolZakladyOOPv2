using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace UkolZakladyOOP
{
    public class MarkSubject
    {
        public double mark;
        public Subject subject;
        public int studentId;

        public MarkSubject(double mark, Subject subject, int studentId, List<MarkSubject> markSubjectList)
        {
            this.mark = mark;
            this.subject = subject;
            this.studentId = studentId;
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