using System.Collections.Generic;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Propojovací třída známky, předmětu a studenta
    /// </summary>
    public class MarkSubject
    {
        /// <summary>
        /// Známka
        /// </summary>
        public double mark;
        /// <summary>
        /// Předmět
        /// </summary>
        public Subject Subject;
        /// <summary>
        /// Student
        /// </summary>
        public Student Student;

        /// <summary>
        /// Konstruktor. Přidá automaticky instanci do seznamu markSubjectList.
        /// </summary>
        /// <param name="mark">Známka</param>
        /// <param name="Subject">Předmět</param>
        /// <param name="Student">Student</param>
        /// <param name="markSubjectList">Seznam známky, předmětu a studenta</param>
        public MarkSubject(double mark, Subject Subject, Student Student, List<MarkSubject> markSubjectList)
        {
            this.mark = mark;
            this.Subject = Subject;
            this.Student = Student;
            markSubjectList.Add(this);
        }
    }

    /// <summary>
    /// Propojovací třída předmětu a studenta
    /// </summary>
    public class SubjectStudent
    {
        /// <summary>
        /// Předmět
        /// </summary>
        public Subject Subject;
        /// <summary>
        /// Student
        /// </summary>
        public Student Student;
        /// <summary>
        /// Úroveň předmětu
        /// </summary>
        public int level;
        /// <summary>
        /// Počet kreditů za cvičení
        /// </summary>
        public double credits;

        /// <summary>
        /// Konstruktor. Přidá automaticky instanci do seznamu subjectStudentList.
        /// </summary>
        /// <param name="Subject">Předmět</param>
        /// <param name="Student">Student</param>
        /// <param name="level"></param>
        /// <param name="subjectStudentList">Seznam předmětu a studenta</param>
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