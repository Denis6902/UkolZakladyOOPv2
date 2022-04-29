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
        public double Mark;

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
        /// <param name="subject">Předmět</param>
        /// <param name="student">Student</param>
        public MarkSubject(double mark, Subject subject, Student student)
        {
            Mark = mark;
            Subject = subject;
            Student = student;
            Student.MarkSubjectList.Add(this);
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
        private int Level;

        /// <summary>
        /// Počet kreditů za cvičení
        /// </summary>
        public double Credits;

        /// <summary>
        /// Konstruktor. Přidá automaticky instanci do seznamu subjectStudentList.
        /// </summary>
        /// <param name="subject">Předmět</param>
        /// <param name="student">Student</param>
        /// <param name="level"></param>
        /// <param name="subjectStudentList">Seznam předmětu a studenta</param>
        public SubjectStudent(Subject subject, Student student, int level, List<SubjectStudent> subjectStudentList)
        {
            Subject = subject;
            Student = student;
            Level = level;
            Credits = subject.Credits;
            subjectStudentList.Add(this);
        }
    }
}