using System;
using System.Collections.Generic;
using System.Threading;

namespace UkolZakladyOOP
{
     public class Teacher : Person
    {
        public string academicTitle;
        public List<Subject> subjects = new();
        public List<Exercise> exercises = new();
        public List<Lecture> lectures = new();

        public Teacher(string academicTitle, int id, string firstName, string lastName, DateTime birthDate, List<Subject> subjects, List<Exercise> exercises, List<Teacher> teachers) : base(id, firstName, lastName, birthDate)
        {
            this.academicTitle = academicTitle;
            this.subjects = subjects;
            this.exercises = exercises;
            teachers.Add(this);
        }
        
        public override void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} {2} a narodil/narodila jsem se {3} a aktuálně učím ve škole", academicTitle, firstName, lastName, birthDate.ToString("MM.dd.yyyy"));
        }

        public int returnSubjectsCount()
        {
            int count = 0;
            foreach (Subject Subject in subjects)
            {
                if (Subject.teacher == this)
                {
                    count++;
                }
            }

            return count;
        }
        
        
        public static void listAllTeachers(List<Teacher> teachers)
        {
            foreach (Teacher Teacher in teachers)
            {
                Console.WriteLine("{0} - vyučuje {1} předmětů a {2} přednášky", Teacher.returnFullName(), Teacher.returnSubjectsCount(), Teacher.lectures.Count);
            }
        }

        public static void selectTeacher(List<Teacher> teachers, ref Teacher chosenTeacher)
        {
            bool end = false;
            do
            {
                Console.WriteLine("Kdo jsi?");
                foreach (Teacher Teacher in teachers)
                {
                    Console.WriteLine(Teacher.firstName);
                }

                //string teacherName = Console.ReadLine();
                string teacherName = "Pavel";
                Console.WriteLine("teacherName = Pavel");

                foreach (Teacher Teacher in teachers)
                {
                    if (Teacher.firstName.ToLower() == teacherName.ToLower())
                    {
                        chosenTeacher = Teacher;
                        end = true;
                    }
                }
            }
            while (end == false);
        }
        public void registerSubject(Semester currentSemester, List<Subject> subjects)
        {
            if (this.subjects.Count != 0)
            {
                foreach (Subject Subject in subjects)
                {
                    if (currentSemester == Subject.semester && Subject.teacher == null)
                    {
                        Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                    }
                }

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine(); 
                string subject = "x";
                Console.WriteLine("subject = x");

                foreach (Subject Subject in this.subjects.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(this.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " + Subject.semester);
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
                foreach (Subject Subject in subjects.ToArray())
                {
                    if (Subject.teacher == this)
                    {
                        Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }
        }
        public void createSubject(ref List<Student> students, ref List<Teacher> teachers, List<Subject> subjects)
        {
            bool end = false;
            Teacher garantOfSubject = null;
            Teacher teacher = null;

            Console.WriteLine("Jak chcete předmět vytvořit");
            Console.WriteLine("1) Nový předmět");
            Console.WriteLine("2) Předmět ze šablony");
            //string howCreate = Console.ReadLine().ToLower();
            string howCreate = "1";
            Console.WriteLine("howCreate = 1;");
            
            if (howCreate == "1")
            {
                Console.WriteLine("Jméno:");
                //string name = Console.ReadLine();
                string name = "Nový Předmět";
                Console.WriteLine("name = Nový předmět");

                do
                {
                    Console.WriteLine("Garant předmětu:");

                    foreach (Teacher Teacher in teachers)
                    {
                        Console.WriteLine(Teacher.firstName);
                    }

                    //string garantName = Console.ReadLine();
                    string garantName = "Pavel";
                    Console.WriteLine("garantName = Pavel");

                    foreach (Teacher Teacher in teachers)
                    {
                        if (Teacher.firstName.ToLower() == garantName.ToLower())
                        {
                            garantOfSubject = Teacher;
                            end = true;
                        }
                    }
                }
                while (end == false);

                end = false;
                
                do
                {
                    Console.WriteLine("Učitel:");

                    foreach (Teacher Teacher in teachers)
                    {
                        Console.WriteLine(Teacher.firstName);
                    }

                    //string garantName = Console.ReadLine();
                    string teacherName = "Pavel";
                    Console.WriteLine("teacherName = Pavel");

                    foreach (Teacher Teacher in teachers)
                    {
                        if (Teacher.firstName.ToLower() == teacherName.ToLower())
                        {
                            teacher = Teacher;
                            end = true;
                        }
                    }
                }
                while (end == false);

                Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;
                Console.WriteLine("credits = 50");

               /* foreach (string semestr in Enum.GetNames(typeof(Semester)))
                {
                    Console.WriteLine(semestr);
                }
                
                Console.WriteLine("Semestr:");
                string value = Console.ReadLine();
                Semester semester = (Semester)Enum.Parse(typeof(Semester), value);*/

                Semester semester = Semester.Summer;
                Console.WriteLine("semester = Semester.Summer");
                
                //int year = int.Parse(Console.ReadLine());
                Random random = new Random();
                int year = random.Next(1, 4);
                Console.WriteLine("year = " + year); 
                
                //int subjectLevel = int.Parse(Console.ReadLine());
                int subjectLevel = random.Next(1, 3);
                Console.WriteLine("subjectLevel = " + subjectLevel); 
                

                Subject Subject = new(name,garantOfSubject, teacher, credits, year ,semester, subjects,subjectLevel);

                foreach (Student Student in students)
                {
                    Student.subjects.Add(Subject);
                }

                foreach (Teacher Teacher in teachers)
                {
                    Teacher.subjects.Add(Subject);
                }

                Thread.Sleep(2500);
                Console.Clear();
                
                howCreate = "2";
                Console.WriteLine("howCreate = 2");
            }

            if (howCreate == "2")
            {
                Console.WriteLine("Jméno:");

                Console.WriteLine("Czech");
                Console.WriteLine("English");

                //string subject = Console.ReadLine();
                string subject = "Czech";
                Console.WriteLine("subject = Czech");

                
                do
                {
                    Console.WriteLine("Garant předmětu:");

                    foreach (Teacher Teacher in teachers)
                    {
                        Console.WriteLine(Teacher.firstName);
                    }

                    //string garantName = Console.ReadLine();
                    string garantName = "Pavel";
                    Console.WriteLine("garantName = Pavel");

                    foreach (Teacher Teacher in teachers)
                    {
                        if (Teacher.firstName.ToLower() == garantName.ToLower())
                        {
                            garantOfSubject = Teacher;
                            end = true;
                        }
                    }
                }
                while (end == false);

                end = false;
                
                do
                {
                    Console.WriteLine("Učitel:");

                    foreach (Teacher Teacher in teachers)
                    {
                        Console.WriteLine(Teacher.firstName);
                    }

                    //string garantName = Console.ReadLine();
                    string teacherName = "Pavel";
                    Console.WriteLine("teacherName = Pavel");

                    foreach (Teacher Teacher in teachers)
                    {
                        if (Teacher.firstName.ToLower() == teacherName.ToLower())
                        {
                            teacher = Teacher;
                            end = true;
                        }
                    }
                }
                while (end == false);

                Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;
                Console.WriteLine("credits = 50");

                /* foreach (string semestr in Enum.GetNames(typeof(Semester)))
                 {
                     Console.WriteLine(semestr);
                 }
                 
                 Console.WriteLine("Semestr:");
                 string value = Console.ReadLine();
                 Semester semester = (Semester)Enum.Parse(typeof(Semester), value);*/

                Semester semester = Semester.Summer;
                Console.WriteLine("semester = Semester.Summer");
                
                //int year = int.Parse(Console.ReadLine());
                Random random = new Random();
                int year = random.Next(1, 4);
                Console.WriteLine("year = " + year); 
                
                //int subjectLevel = int.Parse(Console.ReadLine());
                int subjectLevel = random.Next(1, 3);
                Console.WriteLine("subjectLevel = " + subjectLevel); 

                if (subject.ToLower() == "czech")
                {
                    Subject Czech = SubjectFactory.CreateCzech(teacher,garantOfSubject, credits, subjects,year ,semester,subjectLevel);
                    addSubjectToList(ref teachers, ref students, ref subjects, Czech);
                }
                
                if (subject.ToLower() == "english")
                {
                    Subject English = SubjectFactory.CreateEnglish(teacher,garantOfSubject, credits, subjects, year, semester,subjectLevel);
                    addSubjectToList(ref teachers, ref students, ref subjects, English);
                }

            }

        }

        public void createExercise(List<Exercise> exercises, List<Subject> subjects)
        {
            Console.WriteLine("Jak chcete cvičení vytvořit");
            Console.WriteLine("1) Nové cvičení");
            Console.WriteLine("2) Cvičení ze šablony");
            //string howCreate = Console.ReadLine().ToLower();
            string howCreate = "1";
            Console.WriteLine("howCreate = 1");

            if (howCreate == "1")
            {
                createNewExercise(this, exercises, subjects);
            }
            
            Thread.Sleep(5000);
            Console.Clear();
            
            howCreate = "2";
            Console.WriteLine("howCreate = 2");

            if (howCreate == "2")
            {
                createExerciseFromTemplate(this, exercises, subjects);
            }

        }

        public void listAllExercise()
        {
            if (exercises.Count == 0)
            {
                Console.WriteLine("Neexistuje žádné cvičení");
            }
            else
            {
                foreach (Exercise oneExercise in exercises)
                {
                    Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", oneExercise.name, oneExercise.credits, oneExercise.computerRequired, oneExercise.subject.name);

                }
            }
        }

        public static void listStudentsByAverageMarks(List<Student> students)
        {
            foreach (Student Student in students)
            {
                Console.WriteLine(Student.calculateAverage() + " - průměrná známka ze všech předmětu studenta " + Student.returnFullName());
            }
        }

        public void createLecture(List<Subject> subjects, List<Teacher> teachers)
        {
            Console.WriteLine("Jak chcete přednášku vytvořit");
            Console.WriteLine("1) Nová přednáška");
            Console.WriteLine("2) Přednáška ze šablony");
            //string howCreate = Console.ReadLine().ToLower();
            string howCreate = "1";
            Console.WriteLine("howCreate = 1;");
            
            if (howCreate == "1")
            {
                createNewLecture(teachers);
            }
            
            Thread.Sleep(5000);
            Console.Clear();

            howCreate = "2";
            Console.WriteLine("howCreate = 2;");
            
            if (howCreate == "2")
            {
                createLectureFromTemplate(subjects, teachers);
            }
        }

        public static void listAllLecture(List<Teacher> teachers)
        {
            foreach (Teacher Teacher in teachers)
            {
                foreach (Lecture Lecture in Teacher.lectures)
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, z {2}", Lecture.name, Lecture.credits, Lecture.subject.name);
                }
            }
        }

        public void addSubjectToList(ref List<Teacher> teachers, ref List<Student> students, ref List<Subject> subjects, Subject subject)
        {
            foreach (Student Student in students)
            {
                Student.subjects.Add(subject);

            }

            foreach (Teacher Teacher in teachers)
            {
                Teacher.subjects.Add(subject);
            }
        }

        public void addExerciseToList(Exercise exercise, ref List<Exercise> exercises)
        {
            exercises.Add(exercise);
        }

        public void addLectureToList(Lecture lecture, ref List<Lecture> lectures)
        {
            lectures.Add(lecture);
        }

        public void createNewExercise(Teacher chosenTeacher, List<Exercise> exercises, List<Subject> subjects)
        {
            Subject Subject = null;
            
            Console.WriteLine("Jméno:");
            //string nameOfExercise = Console.ReadLine();
            string nameOfExercise = "Cvičení1";
            Console.WriteLine("nameOfExercise = Cvičení1");
            
            Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine());
            bool computerRequired = false;
            Console.WriteLine("computerRequired = false");
            
            Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;
            Console.WriteLine("credits = 22");
            
            Console.WriteLine("Předmět?");

            foreach (Subject oneSubject in chosenTeacher.subjects)
            {
                Console.WriteLine(oneSubject.name);
            }
            
            //string subject = Console.ReadLine();
            string subject = "Czech1_1";
            Console.WriteLine("subject = Czech1_1");


            foreach (Subject oneSubject in chosenTeacher.subjects)
            {
                if (oneSubject.name.ToLower() == subject.ToLower())
                {
                    Subject = oneSubject;
                }
            }

            Exercise Exercise = new(nameOfExercise, computerRequired, credits, Subject, exercises);
        }

        public void createExerciseFromTemplate(Teacher chosenTeacher, List<Exercise> exercises, List<Subject> subjects)
        {
            {
                Subject Subject = null;
                
                bool end = false;
                Console.WriteLine("Jméno:");

                Console.WriteLine("Cvičení z Češtiny");
                Console.WriteLine("Cvičení z Angličtiny");

                //string exercise = Console.ReadLine().ToLower();
                string exercise = "Cvičení z Češtiny";
                Console.WriteLine("exercise = Cvičení z Češtiny");

                Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;
                Console.WriteLine("credits = 50");


                if (exercise == "cvičení z češtiny")
                {
                    if (end == false)
                    {
                        foreach (Subject oneSubject in subjects)
                        {
                            if (oneSubject.name == "Czech_3_2")
                            {
                                Console.WriteLine("subjectName = Czech_3_2");
                                Subject = oneSubject;
                                end = true;
                            }
                        }
                    }

                    if (end == false)
                    {
                        Console.WriteLine("Neexistuje daný předmět");
                        Console.ReadKey();
                    }

                    if (end == true)
                    {
                        Exercise ExerciseFromCzech = ExerciseFactory.CreateExerciseFromCzech(credits, Subject, exercises);
                        addExerciseToList(ExerciseFromCzech, ref this.exercises);
                    }
                }

                if (exercise == "cvičení z angličtiny")
                {
                    if (end == false)
                    {
                        foreach (Subject oneSubject in subjects)
                        {
                            if (oneSubject.name == "English3_2")
                            {
                                Subject = oneSubject;
                                end = true;
                            }
                        }
                    }

                    if (end == false)
                    {
                        Console.WriteLine("Neexistuje daný předmět");
                        Console.ReadKey();
                    }

                    if (end == true)
                    {
                        Exercise ExerciseFromEnglish = ExerciseFactory.CreateExerciseFromEnglish(credits, Subject, exercises);
                        addExerciseToList(ExerciseFromEnglish, ref this.exercises);
                    }

                }
            }
        }

        public void createNewLecture(List<Teacher> teachers)
        {
            Subject Subject = null;
            Teacher Teacher = null;
            
            Console.WriteLine("Jméno:");
            //string nameOfLecture = Console.ReadLine();
            string nameOfLecture = "Přednáška 1";
            Console.WriteLine("nameOfLecture = Přednáška 1");
            
            Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine());
            bool computerRequired = false;
            Console.WriteLine("computerRequired = false");
            
            Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;
            Console.WriteLine("credits = 22");
            
            Console.WriteLine("Předmět?");
            
            //string subject = Console.ReadLine();
            string subject = "Czech1_1";
            Console.WriteLine("subject = Czech1_1");


            foreach (Subject oneSubject in this.subjects)
            {
                if (oneSubject.name.ToLower() == subject.ToLower())
                {
                    Subject = oneSubject;
                }
            }

            Console.WriteLine("Učitel:");

            /*foreach (Teacher oneTeacher in teachers)
            {
                Console.WriteLine(oneTeacher.firstName);
            }*/

            //string nameTeacher = Console.ReadLine();
            string nameTeacher = "Pavel";
            Console.WriteLine("nameTeacher = Pavel");

            foreach (Teacher oneTeacher in teachers)
            {
                if (oneTeacher.firstName.ToLower() == nameTeacher.ToLower())
                {
                    Teacher = oneTeacher;
                }
            }

            Lecture Lecture = new(nameOfLecture, computerRequired, credits, Subject, lectures, Teacher);
        }

        public void createLectureFromTemplate(List<Subject> subjects, List<Teacher> teachers)
        {
            Subject Subject = null;
            Teacher Teacher = null;
            
            bool end = false;
            Console.WriteLine("Jméno:");

            Console.WriteLine("Přednáška z Češtiny");
            Console.WriteLine("Přednáška z Angličtiny");

            //string lecture = Console.ReadLine().ToLower();
            string lecture = "Přednáška z Češtiny";
            Console.WriteLine("lecture = Přednáška z MatČeštinyemy");

            Console.WriteLine("Počet kreditů k dokončení");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50;
            Console.WriteLine("credits = 50");

            Console.WriteLine("Učitel:");

            /*foreach(Teacher oneTeacher in teachers)
            {
                Console.WriteLine(oneTeacher.firstName);
            }*/

            //string nameTeacher = Console.ReadLine();
            string nameTeacher = "Pavel";
            Console.WriteLine("nameTeacher = Pavel");

            foreach(Teacher oneTeacher in teachers)
            {
                if(oneTeacher.firstName.ToLower() == nameTeacher.ToLower())
                {
                    Teacher = oneTeacher;
                }
            }

            if (lecture == "přednáška z češtiny")
            {
                if (end == false)
                {
                    foreach (Subject oneSubject in subjects)
                    {
                        if (oneSubject.name == "Czech3_2")
                        {
                            Subject = oneSubject;
                            end = true;
                        }
                    }
                }

                if (end == false)
                {
                    Console.WriteLine("Neexistuje daný předmět");
                    Console.ReadKey();
                }

                if (end == true)
                {
                    Lecture LectureFromCzech = LectureFactory.CreateLectureFromCzech(credits, Subject, lectures, Teacher);
                    addLectureToList(LectureFromCzech, ref lectures);
                }
            }

            if (lecture == "přednáška z angličtiny")
            {
                if (end == false)
                {
                    foreach (Subject oneSubject in subjects)
                    {
                        if (oneSubject.name == "English3_2")
                        {
                            Subject = oneSubject;
                            end = true;
                        }
                    }
                }

                if (end == false)
                {
                    Console.WriteLine("Neexistuje daný předmět");
                    Console.ReadKey();
                }

                if (end == true)
                {
                    Lecture LectureFromEnglish = LectureFactory.CreateLectureFromEnglish(credits, Subject, lectures, Teacher);
                    addLectureToList(LectureFromEnglish, ref lectures);
                }

            }
        }
    }

}