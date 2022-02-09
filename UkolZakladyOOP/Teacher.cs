using System;
using System.Collections.Generic;
using System.Threading;

namespace UkolZakladyOOP
{
    public class Teacher : Person
    {
        public string academicTitle;
        public static List<Teacher> teachers = new();

        public Teacher(string academicTitle, int id, string firstName, string lastName, DateTime birthDate) : base(id,
            firstName, lastName,
            birthDate)
        {
            this.academicTitle = academicTitle;
            teachers.Add(this);
        }

        public override void aboutMe()
        {
            Console.WriteLine(
                "Dobrý den, jmenuji se {0} {1} {2} a narodil/narodila jsem se {3} a aktuálně učím ve škole",
                academicTitle, firstName, lastName, birthDate.ToString("MM.dd.yyyy"));
        }

        public int returnSubjectsCount()
        {
            int count = 0;
            foreach (Subject Subject in Subject.subjects)
            {
                if (Subject.teacher == this)
                {
                    count++;
                }
            }

            return count;
        }

        public int returnLecturesCount()
        {
            int count = 0;
            foreach (Lecture Lecture in Lecture.lectures)
            {
                if (Lecture.Teacher == this)
                {
                    count++;
                }
            }

            return count;
        }


        public static void listAllTeachers()
        {
            foreach (Teacher Teacher in teachers)
            {
                Console.WriteLine("{0} - vyučuje {1} předmětů a {2} přednášky", Teacher.returnFullName(),
                    Teacher.returnSubjectsCount(), Teacher.returnLecturesCount());
            }
        }

        public static Teacher selectTeacher()
        {
            Teacher chosenTeacher = null;
            bool end = false;
            do
            {
                foreach (Teacher Teacher in teachers)
                {
                    Console.WriteLine(Teacher.firstName);
                }

                //string teacherName = Console.ReadLine();
                string teacherName = "Pavel";

                foreach (Teacher Teacher in teachers)
                {
                    if (Teacher.firstName.ToLower() == teacherName.ToLower())
                    {
                        chosenTeacher = Teacher;
                        end = true;
                    }
                }
            } while (end == false);

            return chosenTeacher;
        }

        public void registerSubject(Semester currentSemester)
        {
            if (Subject.subjects.Count != 0)
            {
                foreach (Subject Subject in Subject.subjects)
                {
                    if (currentSemester == Subject.semester && Subject.teacher == null)
                    {
                        Console.WriteLine(
                            "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                            Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                    }
                }

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine(); 
                string subject = "xxx1_1";
                Console.WriteLine("subject = xxx1_1");

                foreach (Subject Subject in Subject.subjects.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(this.firstName + " nyní učíš " + Subject.name + " Semestr: " +
                                          Subject.semester);
                        Subject.teacher = this;
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
            if (this.returnSubjectsCount() != 0)
            {
                foreach (Subject Subject in Subject.subjects.ToArray())
                {
                    if (Subject.teacher == this)
                    {
                        Console.WriteLine(
                            "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                            Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }
        }

        public static void createSubject()
        {
            Console.WriteLine("Jak chcete předmět vytvořit");
            Console.WriteLine("1) Nový předmět");
            Console.WriteLine("2) Předmět ze šablony");
            //string howCreate = Console.ReadLine();
            string howCreate = "1";
            Console.WriteLine("howCreate = 1;");

            Thread.Sleep(1000);
            Console.Clear();

            if (howCreate == "1")
            {
                createNewSubject();
            }

            howCreate = "2";
            Console.WriteLine("howCreate = 2");

            Thread.Sleep(1000);
            Console.Clear();

            if (howCreate == "2")
            {
                createSubjectFromTemplate();
            }
        }

        public static void createNewSubject()
        {
            //Console.WriteLine("Jméno:");
            //string name = Console.ReadLine();
            string name = "Nový Předmět";
            Console.WriteLine("name = Nový předmět");

            Console.WriteLine("Garant předmětu: ");
            Teacher garantOfSubject = Teacher.selectTeacher();
            Console.WriteLine("garantOfSubject = Pavel");

            Console.WriteLine("Učitel: ");
            Teacher teacher = Teacher.selectTeacher();
            Console.WriteLine("teacher = Pavel");

            Console.WriteLine("Počet kreditů k dokončení = 50");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50;

            Semester semester = Semester.Summer;
            Console.WriteLine("semester = Semester.Summer");

            //int year = int.Parse(Console.ReadLine());
            Random random = new Random();
            int year = random.Next(1, 4);
            Console.WriteLine("year = " + year);

            //int subjectLevel = int.Parse(Console.ReadLine());
            int subjectLevel = random.Next(1, 3);
            Console.WriteLine("subjectLevel = " + subjectLevel);

            Subject Subject = new(name, garantOfSubject, teacher, credits, year, semester, subjectLevel);

            Thread.Sleep(10000);
            Console.Clear();
        }

        public static void createSubjectFromTemplate()
        {
            Console.WriteLine("Jméno:");

            Console.WriteLine("Czech");
            Console.WriteLine("English");

            //string subject = Console.ReadLine();
            string subject = "Czech";
            Console.WriteLine("subject = Czech");

            Console.WriteLine("Garant předmětu: ");
            Teacher garantOfSubject = Teacher.selectTeacher();
            Console.WriteLine("garantOfSubject = Pavel");

            Console.WriteLine("Učitel: ");
            Teacher teacher = Teacher.selectTeacher();
            Console.WriteLine("teacher = Pavel");

            Console.WriteLine("Počet kreditů k dokončení = 50");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50;

            Semester semester = Semester.Winter;
            Console.WriteLine("Semester = Semester.Winter");

            //int year = int.Parse(Console.ReadLine());
            Random random = new Random();
            int year = random.Next(1, 4);
            Console.WriteLine("year = " + year);

            //int subjectLevel = int.Parse(Console.ReadLine());
            int subjectLevel = random.Next(1, 3);
            Console.WriteLine("subjectLevel = " + subjectLevel);

            if (subject.ToLower() == "czech")
            {
                Subject Czech = SubjectFactory.CreateCzech(teacher, garantOfSubject, credits, year,
                    semester, subjectLevel);
            }

            if (subject.ToLower() == "english")
            {
                Subject English = SubjectFactory.CreateEnglish(teacher, garantOfSubject, credits, year,
                    semester, subjectLevel);
            }

            Thread.Sleep(10000);
        }

        public static void createExercise()
        {
            Console.WriteLine("Jak chcete cvičení vytvořit");
            Console.WriteLine("1) Nové cvičení");
            Console.WriteLine("2) Cvičení ze šablony");
            //string howCreate = Console.ReadLine().ToLower();
            string howCreate = "1";
            Console.WriteLine("howCreate = 1");

            if (howCreate == "1")
            {
                createNewExercise();
            }
            
            Console.Clear();
            Thread.Sleep(1000);

            howCreate = "2";
            Console.WriteLine("howCreate = 2");

            if (howCreate == "2")
            {
                createExerciseFromTemplate();
            }
        }

        public static void createNewExercise()
        {
            //Console.WriteLine("Jméno:");
            //string nameOfExercise = Console.ReadLine();
            string nameOfExercise = "Cvičení1";
            Console.WriteLine("nameOfExercise = Cvičení1");

            //Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine());
            bool computerRequired = false;
            Console.WriteLine("computerRequired = false");

            //Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;
            Console.WriteLine("credits = 22");

            //Console.WriteLine("Předmět?");

            Subject.listAllSubjects();
            
            string subjectName = "Czech1_1";
            Console.WriteLine("subject = Czech1_1");
            Subject subject = Subject.selectSubject(subjectName);

            Exercise Exercise = new(nameOfExercise, computerRequired, credits, subject);

            Thread.Sleep(10000);
            Console.Clear();
        }

        public static void createExerciseFromTemplate()
        {
            {
                Console.WriteLine("Jméno:");

                Console.WriteLine("Cvičení z Češtiny");
                Console.WriteLine("Cvičení z Angličtiny");

                //string exercise = Console.ReadLine().ToLower();
                string exercise = "cvičení z češtiny";
                Console.WriteLine("exercise = Cvičení z Češtiny");

                //Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;
                Console.WriteLine("credits = 50");

                if (exercise == "cvičení z češtiny")
                {
                    string subjectName = "Czech3_2";
                    Console.WriteLine("subject = Czech3_2");
                    Subject subject = Subject.selectSubject(subjectName);

                    Exercise ExerciseFromCzech =
                        ExerciseFactory.CreateExerciseFromCzech(credits, subject);
                }

                if (exercise == "cvičení z angličtiny")
                {
                    string subjectName = "English3_2";
                    Console.WriteLine("subject = English3_2");
                    Subject subject = Subject.selectSubject(subjectName);

                    Exercise ExerciseFromEnglish =
                        ExerciseFactory.CreateExerciseFromEnglish(credits, subject);
                    Exercise.exercises.Add(ExerciseFromEnglish);
                }
            }
        }

        public static void listAllExercise()
        {
            if (Exercise.exercises.Count == 0)
            {
                Console.WriteLine("Neexistuje žádné cvičení");
            }
            else
            {
                foreach (Exercise oneExercise in Exercise.exercises)
                {
                    Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", oneExercise.name,
                        oneExercise.credits, oneExercise.computerRequired, oneExercise.subject.name);
                }
            }
        }

        public static void listStudentsByAverageMarks()
        {
            foreach (Student Student in Student.students)
            {
                if (double.IsNaN(Student.calculateAverageMark()) && Student.calculateAverageMark() == 0)
                {
                    Console.WriteLine(Student.returnFullName() + " nemá žádnou známku");
                }
                else
                {
                    Console.WriteLine(Student.calculateAverageMark() +
                                      " - průměrná známka ze všech předmětu studenta " +
                                      Student.returnFullName());
                }
            }
        }

        public static void createLecture()
        {
            Console.WriteLine("Jak chcete přednášku vytvořit");
            Console.WriteLine("1) Nová přednáška");
            Console.WriteLine("2) Přednáška ze šablony");
            //string howCreate = Console.ReadLine().ToLower();
            string howCreate = "1";
            Console.WriteLine("howCreate = 1;");

            if (howCreate == "1")
            {
                createNewLecture();
            }

            Thread.Sleep(10000);
            Console.Clear();

            howCreate = "2";
            Console.WriteLine("howCreate = 2;");

            if (howCreate == "2")
            {
                createLectureFromTemplate();
            }
        }

        public static void listAllLectures()
        {
            foreach (Lecture Lecture in Lecture.lectures)
            {
                Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, z {2}", Lecture.name,
                    Lecture.credits, Lecture.subject.name);
            }
        }


        public static void createNewLecture()
        {
            Subject Subject = null;

            Console.WriteLine("Jméno = Přednáška 1");
            //string nameOfLecture = Console.ReadLine();
            string nameOfLecture = "Přednáška 1";

            Console.WriteLine("Nutnost PC? (true/false) = false");
            //bool computerRequired = bool.Parse(Console.ReadLine());
            bool computerRequired = false;

            Console.WriteLine("Počet kreditů = 22");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;

            Console.WriteLine("Předmět = Czech1_1");
            //string subject = Console.ReadLine();
            string subject = "Czech1_1";

            foreach (Subject oneSubject in Subject.subjects)
            {
                if (oneSubject.name.ToLower() == subject.ToLower())
                {
                    Subject = oneSubject;
                }
            }

            Lecture Lecture = new(nameOfLecture, computerRequired, credits, Subject);
        }

        public static void createLectureFromTemplate()
        {
            Console.WriteLine("Jméno:");

            Console.WriteLine("Přednáška z Češtiny");
            Console.WriteLine("Přednáška z Angličtiny");

            //string lecture = Console.ReadLine().ToLower();
            string lecture = "přednáška z češtiny";
            Console.WriteLine("lecture = Přednáška z Češtiny");

            Console.WriteLine("Počet kreditů k dokončení");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50;
            Console.WriteLine("credits = 50");


            if (lecture == "přednáška z češtiny")
            {
                createCzechSubject(credits);
            }

            if (lecture == "přednáška z angličtiny")
            {
                createEnglishSubject(credits);
            }
        }

        public static void createCzechSubject(double credits)
        {
            Console.WriteLine("Předmět:");
            foreach (Subject subject in Subject.subjects)
            {
                if (subject.name.Substring(0, 3) == "Cze")
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                        subject.name, subject.credits,
                        subject.garantOfSubject.returnFullName(),
                        subject.semester);
                }
            }

            string subjectName = "Czech3_2";
            Console.WriteLine("subject = Czech3_2");
            Subject chosenSubject = Subject.selectSubject(subjectName);

            Lecture LectureFromCzech =
                LectureFactory.CreateLectureFromCzech(credits, chosenSubject);
        }

        public static void createEnglishSubject(double credits)
        {
            Console.WriteLine("Předmět:");
            foreach (Subject subject in Subject.subjects)
            {
                if (subject.name.Substring(0, 3) == "Eng")
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                        subject.name, subject.credits,
                        subject.garantOfSubject.returnFullName(),
                        subject.semester);
                }
            }

            string subjectName = "English3_2";
            Console.WriteLine("subject = English3_2");
            Subject chosenSubject = Subject.selectSubject(subjectName);

            Lecture LectureFromEnglish =
                LectureFactory.CreateLectureFromEnglish(credits, chosenSubject);
        }
    }
}