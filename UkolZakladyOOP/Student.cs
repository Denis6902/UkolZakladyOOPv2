using System;
using System.Collections.Generic;
using System.Threading;

namespace UkolZakladyOOP
{
    public class Student : Person
    {
        public DateTime registrationDate;
        public List<Subject> subjects = new();
        public List<Exercise> exercises = new();
        public static List<MarkSubject> markSubjectList = new();
        public List<SubjectStudent> studentSubjectList = new();
        public double averageOfAllMarks;
        public int year;
        public int credits;
        public static List<Student> students = new();


        public Student(int id, string firstName, string lastName, DateTime birthDate, DateTime registrationDate,
            List<Subject> subjects, List<Exercise> exercises, int year) : base(id, firstName,
            lastName, birthDate)
        {
            this.registrationDate = registrationDate;
            this.subjects = subjects;
            this.exercises = exercises;
            students.Add(this);
            this.year = year;
        }

        public static void nextYear()
        {
            foreach (Student Student in (students))
            {
                Student.year += 1;
                Console.WriteLine("Aktuální ročník studenta " + Student.returnFullName() + " je: " + Student.year);
                Student.averageOfAllMarks = 0;
                Student.studentSubjectList = null;
                Student.markSubjectList = null;
            }
        }

        public static void nextSemester(ref Semester currentSemester)
        {
            switch (currentSemester)
            {
                case Semester.Summer:
                    currentSemester = Semester.Winter;
                    break;

                case Semester.Winter:
                    currentSemester = Semester.Summer;
                    break;
            }

            Console.WriteLine("Aktuální semestr je: " + currentSemester);
        }

        public override void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} a narodil/narodila jsem se {2} a aktuálně studuji od {3}",
                firstName, lastName, birthDate.ToString("MM.dd.yyyy"), registrationDate.ToString("MM.dd.yyyy"));
        }

        public double calculateAverage()
        {
            return averageOfAllMarks / markSubjectList.Count;
        }


        public static void selectStudent(ref Student chosenStudent)
        {
            bool end = false;
            do
            {
                Console.WriteLine("Kdo jsi?");
                foreach (Student Student in students)
                {
                    Console.WriteLine(Student.firstName);
                }

                //string studentName = Console.ReadLine();
                string studentName = "Pepa";
                Console.WriteLine("chosenStudent = Pepa");
                Thread.Sleep(1000);
                Console.Clear();

                foreach (Student Student in students)
                {
                    if (Student.firstName.ToLower() == studentName.ToLower())
                    {
                        chosenStudent = Student;
                        end = true;
                    }
                }
            } while (end == false);
        }

        public void registerSubject(Semester currentSemester)
        {
            int englishLevel = 1;
            int czechLevel = 1;

            foreach (SubjectStudent SubjectStudent in studentSubjectList)
            {
                if ((SubjectStudent.Subject.name).Substring(0, 3) == "Eng" && SubjectStudent.Subject.completed == true)
                {
                    englishLevel = SubjectStudent.Subject.level + 1;
                }
            }

            foreach (SubjectStudent SubjectStudent in studentSubjectList)
            {
                if ((SubjectStudent.Subject.name).Substring(0, 3) == "Cze" && SubjectStudent.Subject.completed == true)
                {
                    czechLevel = SubjectStudent.Subject.level + 1;
                }
            }

            if (this.subjects.Count != 0)
            {
                foreach (Subject Subject in this.subjects)
                {
                    if (Subject.year == this.year && Subject.semester == currentSemester)
                    {
                        if (englishLevel == Subject.level && Subject.name.Substring(0, 1) == "E")
                        {
                            Console.WriteLine(
                                "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3} (Level {4})",
                                Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(),
                                Subject.semester, Subject.level);
                        }

                        if (czechLevel == Subject.level && Subject.name.Substring(0, 1) == "C")
                        {
                            Console.WriteLine(
                                "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3} (Level {4})",
                                Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(),
                                Subject.semester, Subject.level);
                        }
                    }
                }

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine(); 
                string subject = "English1_1";
                Console.WriteLine("subject = English1_1");

                foreach (Subject Subject in this.subjects.ToArray())
                {
                    if (Subject.name.ToLower() == subject.ToLower())
                    {
                        Console.WriteLine(this.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " +
                                          Subject.semester);
                        SubjectStudent SubjectStudent = new(Subject, this, Subject.level, this.studentSubjectList);
                        SubjectStudent.Subject.registered = true;
                        subjects.Remove(Subject);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nemáš žádné nedokončené předměty");
            }
        }

        public void listAllMySubjects(Semester currentSemester)
        {
            if (studentSubjectList.Count != 0)
            {
                foreach (SubjectStudent SubjectStudent in studentSubjectList)
                {
                    if (SubjectStudent.Subject.year == this.year &&
                        SubjectStudent.Subject.semester == currentSemester &&
                        SubjectStudent.Subject.registered == true && this.firstName == SubjectStudent.Student.firstName)
                    {
                        Console.WriteLine(
                            "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                            SubjectStudent.Subject.name, SubjectStudent.credits,
                            SubjectStudent.Subject.garantOfSubject.returnFullName(), SubjectStudent.Subject.semester);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }

            //Console.ReadKey();
        }

        public void listCompletedSubjects()
        {
            if (markSubjectList.Count != 0)
            {
                foreach (MarkSubject markSubject in markSubjectList)
                {
                    if (this.id == markSubject.studentId)
                    {
                        Console.WriteLine("Předmět " + (markSubject.subject.name) + " známka: " + markSubject.mark);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nedokončil jsi zatím žádný předmět");
            }
        }

        public void goOnLecture(List<Lecture> lectures)
        {
            Lecture Lecture = null;
            //Subject Subject = null;
            if (this.exercises.Count != 0 && studentSubjectList.Count != 0)
            {
                bool end = false;
                do
                {
                    foreach (SubjectStudent SubjectStudent in studentSubjectList)
                    {
                        foreach (Lecture oneLecture in lectures)
                        {
                            if (SubjectStudent.Subject == oneLecture.subject &&
                                SubjectStudent.Subject.registered == true && SubjectStudent.Subject.completed == false)
                            {
                                Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})",
                                    oneLecture.name, oneLecture.credits, oneLecture.computerRequired,
                                    oneLecture.subject.name);
                            }
                        }
                    }

                    Console.WriteLine("Zadejte název přednášky");

                    //string lectureName = Console.ReadLine();
                    string lectureName = "Přednáška z Angličtiny";
                    Console.WriteLine("lectureName = Přednáška z Angličtiny");


                    if (lectureName == "")
                    {
                        break;
                    }

                    foreach (SubjectStudent SubjectStudent in studentSubjectList)
                    {
                        foreach (Lecture oneLecture in lectures)
                        {
                            if (oneLecture.name.ToLower() == lectureName.ToLower() && end == false &&
                                SubjectStudent.Subject == oneLecture.subject)
                            {
                                Lecture = oneLecture;
                                end = true;
                            }
                        }

                        if (end == true)
                        {
                            Console.WriteLine("Šel jsi na přednášku {0} - {1} kreditů", Lecture.name, Lecture.credits);
                            SubjectStudent.credits = SubjectStudent.credits - Lecture.credits;
                            break;
                        }
                    }
                } while (end == false);
            }
        }


        public void endSubject()
        {
            foreach (SubjectStudent SubjectStudent in studentSubjectList)
            {
                if (SubjectStudent.credits > 0 && SubjectStudent.Subject.completed == false &&
                    firstName == SubjectStudent.Student.firstName)
                {
                    Console.WriteLine("Do dokončení předmětu {0} zbývá {1} kreditů", SubjectStudent.Subject.name,
                        SubjectStudent.credits);
                }

                if (SubjectStudent.credits < 1 && SubjectStudent.Subject.completed == false &&
                    firstName == SubjectStudent.Student.firstName)
                {
                    Random r = new();
                    double mark = r.Next(1, 5);
                    this.averageOfAllMarks += mark;
                    Console.WriteLine("Dokončil jsi předmět " + SubjectStudent.Subject.name + " s hodnocením " + mark);
                    MarkSubject markSubject = new(mark, SubjectStudent.Subject, this.id, markSubjectList);
                    SubjectStudent.Subject.completed = true;
                }
            }
        }

        public void doExercise()
        {
            if (this.exercises.Count != 0 && this.subjects.Count != 0)
            {
                Exercise Exercise = null;
                bool end = false;
                do
                {
                    foreach (SubjectStudent SubjectStudent in studentSubjectList)
                    {
                        foreach (Exercise oneExercise in this.exercises)
                        {
                            if (SubjectStudent.Subject.name == oneExercise.subject.name &&
                                SubjectStudent.Subject.registered == true)
                            {
                                Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2}", oneExercise.name,
                                    oneExercise.credits, oneExercise.computerRequired);
                            }
                        }
                    }

                    Console.WriteLine("Zadejte název cvičení");

                    //string exerciseName = Console.ReadLine();
                    string exerciseName = "Cvičení z Angličtiny";
                    Console.WriteLine("exerciseName = Cvičení z Angličtiny");

                    if (exerciseName == "")
                    {
                        break;
                    }

                    foreach (Exercise oneExercise in this.exercises)
                    {
                        if (oneExercise.name.ToLower() == exerciseName.ToLower() && end == false)
                        {
                            Exercise = oneExercise;
                            end = true;
                        }
                    }

                    if (end == true)
                    {
                        foreach (SubjectStudent SubjectStudent in studentSubjectList)
                        {
                            if (SubjectStudent.Subject == Exercise.subject &&
                                this.firstName == SubjectStudent.Student.firstName)
                            {
                                SubjectStudent.credits = SubjectStudent.credits - Exercise.credits;
                                Console.WriteLine("Dokončil jsi cvičení {0} - {1} kreditů", Exercise.name,
                                    Exercise.credits);
                            }
                        }
                    }
                } while (end == false);
            }
        }
    }
}