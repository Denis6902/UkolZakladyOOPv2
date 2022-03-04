using System;
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
        public static List<Subject> subjects = new();

        public Subject(string name, Teacher garantOfSubject, Teacher teacher, double credits, int year,
            Semester semester, int level)
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

        public static Subject selectSubject(string subjectName)
        {
            Subject Subject = null;

            bool end = false;

            do
            {
                foreach (Subject oneSubject in Subject.subjects)
                {
                    if (oneSubject.name.ToLower() == subjectName.ToLower())
                    {
                        Subject = oneSubject;
                        end = true;
                    }
                }

                if (end == false)
                {
                    Console.WriteLine("Neexistuje daný předmět");
                    Console.WriteLine("Zadej název existujícího předmětu");
                    subjectName = Console.ReadLine();
                }
            } while (end == false);

            Console.WriteLine($"subject = {subjectName}");
            return Subject;
        }

        public static void listAllSubjects()
        {
            foreach (Subject Subject in Subject.subjects)
            {
                Console.WriteLine(
                    "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                    Subject.name, Subject.credits,
                    Subject.garantOfSubject.returnFullName(),
                    Subject.semester);
            }
        }

        public static void listOnlyOneTypeSubjects(string subject)
        {
            foreach (Subject Subject in Subject.subjects)
            {
                if (Subject.name.Substring(0, 3) == subject.ToLower())
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                        Subject.name, Subject.credits,
                        Subject.garantOfSubject.returnFullName(),
                        Subject.semester);
                }
            }
        }
    }


    class SubjectFactory
    {
        public static Subject CreateCzech(Teacher teacher, Teacher garantOfSubject, double credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject("Czech", teacher, garantOfSubject, credits, year, semester, subjectLevel);
        }

        public static Subject CreateEnglish(Teacher teacher, Teacher garantOfSubject, double credits,
            int year, Semester semester, int subjectLevel)
        {
            return new Subject("English", teacher, garantOfSubject, credits, year, semester, subjectLevel);
        }
    }
}