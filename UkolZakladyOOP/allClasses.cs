using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace UkolZakladyOOP
{
    public class Person
    {
        public int id;
        public string firstName;
        public string lastName;
        public DateTime birthDate;

        public Person(int id,string firstName, string lastName, DateTime birthDate)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public virtual void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} a narodil/narodila jsem se {1} a jsem pouze obyčejná osoba", returnFullName(), birthDate.ToString("MM.dd.yyyy"));
        }

        public string returnFullName()
        {
            return firstName + " " + lastName;
        }
    }

    public class Student : Person
    {
        public DateTime registrationDate;
        public List<Subject> subjectsToRegister = new();
        public List<Subject> registredSubjects = new();
        public List<Exercise> exercises = new();
        public static List<Mark_Subject> markSubjectList = new();
        public double averageOfAllMarks;
        public int year;
        public int credits;


        public Student(int id,string firstName, string lastName, DateTime birthDate, DateTime registrationDate, List<Subject> subjectsToRegister, List<Exercise> exercises, List<Student> students, int year) : base(id ,firstName, lastName, birthDate)
        {
            this.registrationDate = registrationDate;
            base.id = id;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjectsToRegister = subjectsToRegister;
            this.exercises = exercises;
            students.Add(this);
            this.year = year;
        }
        
        public static void nextYear(List<Student> students)
        {
            foreach (Student student in (students))
            {
                student.year += 1;
            }
        }

        public static void nextSemester(ref Semester currentSemester)
        {
            switch (currentSemester)
            {
                case Semester.Letni:
                    currentSemester = Semester.Zimni;
                    break;
                                
                case Semester.Zimni:
                    currentSemester = Semester.Letni;
                    break;
            }
        }

        public override void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} a narodil/narodila jsem se {2} a aktuálně studuji od {3}", firstName, lastName, birthDate.ToString("MM.dd.yyyy"), registrationDate.ToString("MM.dd.yyyy"));
        }

        public double calculateAverage()
        {
            return averageOfAllMarks / markSubjectList.Count;
        }

        public static void selectStudent(List<Student> students, ref Student chosenStudent)
        {
            bool end = false;
            do
            {
                Console.WriteLine("Kdo jsi?");
                foreach (Student Student in students)
                {
                    Console.WriteLine(Student.firstName);
                }

                //studentName = Console.ReadLine();
                string studentName = "Pepa";
                Console.ReadKey();
                Console.Clear();

                foreach (Student Student in students)
                {
                    if (Student.firstName.ToLower() == studentName.ToLower())
                    {
                        chosenStudent = Student;
                        end = true;
                    }
                }
            }
            while (end == false);
        }

        public void registerSubject(ref Student chosenStudent, Semester currentSemester)
        {
            if (chosenStudent.subjectsToRegister.Count != 0)
            {
                foreach (Subject Subject in chosenStudent.subjectsToRegister)
                {
                    if (Subject.year == this.year && Subject.semester == currentSemester)
                    {
                        Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                    }
                }
                Console.WriteLine("Zadejte název předmětu");
                string subject = Console.ReadLine(); 
                Console.ReadKey();
                //string subject = "English1";
                Console.Clear();

                foreach (Subject Subject in chosenStudent.subjectsToRegister.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(chosenStudent.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " + Subject.semester);
                        Console.ReadKey();
                        chosenStudent.subjectsToRegister.Remove(Subject);
                        chosenStudent.registredSubjects.Add(Subject);
                    }

                }
            }
            else
            {
                Console.WriteLine("Nemáš žádné nedokončené předměty");
                Console.ReadKey();
            }
        }

        public void listAllMySubjects(Semester currentSemester)
        {
            if (registredSubjects.Count != 0)
            {
                foreach (Subject Subject in registredSubjects.ToArray())
                {
                    if(Subject.year == this.year && Subject.semester == currentSemester)
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }

            Console.ReadKey();
        }

        public void listCompletedSubjects()
        {
            if (markSubjectList.Count != 0)
            {
                foreach (Mark_Subject markSubject in markSubjectList)
                {
                    if (this.id == markSubject.studentId)
                    {
                        Console.WriteLine("Předmět " + (markSubject.subject.name) +  " známka: " + markSubject.mark);
                    }
                }                
            }
            else
            {
                Console.WriteLine("Nedokončil jsi zatím žádný předmět");
            }

        }
        
        public void goOnLecture(ref Student chosenStudent, ref Lecture chosenLecture, ref Subject chosenSubject, List<Lecture> lectures)
        {
            if (chosenStudent.exercises.Count != 0 && chosenStudent.registredSubjects.Count != 0)
            {
                bool end = false;
                do
                {
                    foreach (Lecture Lecture in lectures)
                    {
                        if (chosenStudent.registredSubjects.Contains(Lecture.subject))
                        {
                            Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", Lecture.name, Lecture.credits, Lecture.computerRequired, Lecture.subject.name);
                        }
                    }

                    Console.WriteLine("Zadejte název přednášky");
                    Console.ReadKey();

                    //string lectureName = Console.ReadLine();
                    string lectureName = "Přednáška z češtiny";

                    if (lectureName == "")
                    {
                        break;
                    }

                    foreach (Lecture Lecture in lectures)
                    {
                        if (Lecture.name.ToLower() == lectureName.ToLower() && end == false)
                        {
                            chosenLecture = Lecture;
                            chosenSubject = chosenLecture.subject;
                            end = true;
                        }
                    }

                    if (end == true)
                    {
                        Console.WriteLine("Šel jsi na přednášku {0} - {1} kreditů", chosenLecture.name, chosenLecture.credits);
                        Console.ReadKey();
                        chosenSubject.credits = chosenSubject.credits - chosenLecture.credits;
                    }
                }
                while (end == false);
            }
        }


        public void endSubject(ref Student chosenStudent, ref List<Mark_Subject> markSubjectList)
        {
            foreach (Subject Subject in chosenStudent.registredSubjects.ToArray())
            {
                if (Subject.credits > 0)
                    Console.WriteLine("Do dokončení předmětu {0} zbývá {1} kreditů", Subject.name, Subject.credits);

                if (Subject.credits < 1)
                {
                    Random r = new();
                    double mark = r.Next(1, 5);
                    chosenStudent.averageOfAllMarks += mark;
                    Console.WriteLine("Dokončil jsi předmět " + Subject.name + " s hodnocením " + mark);
                    Mark_Subject markSubject = new(mark, Subject, chosenStudent.id, markSubjectList);
                    chosenStudent.registredSubjects.Remove(Subject);
                }
            }
        }

        public void doExercise(ref Student chosenStudent, ref Exercise chosenExercise, ref Subject chosenSubject)
        {
            if (chosenStudent.exercises.Count != 0 && chosenStudent.registredSubjects.Count != 0)
            {
                bool end = false;
                do
                {
                    foreach (Exercise Exercise in chosenStudent.exercises)
                    {
                        if (chosenStudent.registredSubjects.Contains(Exercise.subject))
                        {
                            Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2}", Exercise.name, Exercise.credits, Exercise.computerRequired);
                        }
                    }

                    Console.WriteLine("Zadejte název cvičení");

                    //string exerciseName = Console.ReadLine();
                    string exerciseName = "Cvičení z češtiny";

                    Console.Clear();

                    if (exerciseName == "")
                    {
                        break;
                    }

                    foreach (Exercise Exercise in chosenStudent.exercises)
                    {
                        if (Exercise.name.ToLower() == exerciseName.ToLower() && end == false)
                        {
                            chosenExercise = Exercise;
                            chosenSubject = chosenExercise.subject;
                            end = true;
                        }
                    }

                    if (end == true)
                    {
                        Console.WriteLine("Dokončil jsi cvičení {0} - {1} kreditů", chosenExercise.name, chosenExercise.credits);

                        chosenSubject.credits = chosenSubject.credits - chosenExercise.credits;
                    }
                }
                while (end == false);
            }
        }
    }

    public class Teacher : Person
    {
        public string academicTitle;
        public List<Subject> subjectsToRegister = new();
        public List<Subject> registredSubjects = new();
        public List<Exercise> exercises = new();
        public List<Lecture> lectures = new();

        public Teacher(string academicTitle, int id, string firstName, string lastName, DateTime birthDate, List<Subject> subjectsToRegister, List<Exercise> exercises, List<Teacher> teachers) : base(id, firstName, lastName, birthDate)
        {
            this.academicTitle = academicTitle;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjectsToRegister = subjectsToRegister;
            this.exercises = exercises;
            teachers.Add(this);
        }
        
        public override void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} {2} a narodil/narodila jsem se {3} a aktuálně učím ve škole", academicTitle, firstName, lastName, birthDate.ToString("MM.dd.yyyy"));
        }
        
        
        public static void listAllTeachers(List<Teacher> teachers)
        {
            foreach (Teacher Teacher in teachers)
            {
                Console.WriteLine("{0} - vyučuje {1} předmětů a {2} přednášky", Teacher.returnFullName(), Teacher.registredSubjects.Count, Teacher.lectures.Count);
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
                Console.ReadKey();
                Console.Clear();

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
        public void registerSubject(ref Teacher chosenTeacher, Semester currentSemester, List<Subject> subjects)
        {
            if (chosenTeacher.subjectsToRegister.Count != 0)
            {
                foreach (Subject Subject in subjects)
                {
                    if (currentSemester == Subject.semester)
                    {
                        Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester );
                    }
                }

                Console.WriteLine("Zadejte název předmětu");
                string subject = Console.ReadLine(); 
                //string subject = "Czech";
                Console.Clear();

                foreach (Subject Subject in chosenTeacher.subjectsToRegister.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(chosenTeacher.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " + Subject.semester);
                        chosenTeacher.subjectsToRegister.Remove(Subject);
                        chosenTeacher.registredSubjects.Add(Subject);
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
            if (registredSubjects.Count != 0)
            {
                foreach (Subject Subject in registredSubjects.ToArray())
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester);
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }

            Console.ReadKey();
        }
        public void createSubject(ref List<Student> students, ref List<Teacher> teachers, List<Subject> subjects)
        {
            bool end = false;
            Teacher garantOfSubject = null;

            Console.WriteLine("Jak chcete předmět vytvořit");
            Console.WriteLine("1) Nový předmět");
            Console.WriteLine("2) Předmět ze šablony");
            string howCreate = Console.ReadLine().ToLower();

            Console.Clear();

            if (howCreate == "1")
            {
                Console.WriteLine("Jméno:");
                //string name = Console.ReadLine();
                string name = "Nový Předmět";
                Console.Clear();

                do
                {
                    Console.WriteLine("Garant předmětu:");

                    foreach (Teacher Teacher in teachers)
                    {
                        Console.WriteLine(Teacher.firstName);
                    }

                    //string garantName = Console.ReadLine();
                    string garantName = "Pavel";
                    Console.Clear();

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

                Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;

               /* foreach (string semestr in Enum.GetNames(typeof(Semester)))
                {
                    Console.WriteLine(semestr);
                }
                
                Console.WriteLine("Semestr:");
                string value = Console.ReadLine();
                Semester semester = (Semester)Enum.Parse(typeof(Semester), value);*/

                Semester semester = Semester.Letni;
                
                //int year = int.Parse(Console.ReadLine());
                Random random = new Random();
                int year = random.Next(1, 4);
                
                //int subjectLevel = int.Parse(Console.ReadLine());
                int subjectLevel = random.Next(1, 3);
                

                Subject Subject = new(name, garantOfSubject, credits, year ,semester, subjects,subjectLevel);

                foreach (Student Student in students)
                {
                    Student.subjectsToRegister.Add(Subject);
                }

                foreach (Teacher Teacher in teachers)
                {
                    Teacher.subjectsToRegister.Add(Subject);
                }
            }

            if (howCreate == "2")
            {
                Console.WriteLine("Jméno:");

                Console.WriteLine("Czech");
                Console.WriteLine("Math");
                Console.WriteLine("English");

                //string subject = Console.ReadLine();
                string subject = "Czech";

                Console.Clear();

                do
                {
                    Console.WriteLine("Garant předmětu:");

                    foreach (Teacher Teacher in teachers)
                    {
                        Console.WriteLine(Teacher.firstName);
                    }

                    //string garantName = Console.ReadLine();
                    string garantName = "Pavel";
                    Console.Clear();

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

                Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;
                
                /*foreach (string semestr in Enum.GetNames(typeof(Semester)))
                {
                    Console.WriteLine(semestr);
                }
                
                Console.WriteLine("Semestr:");
                string value = Console.ReadLine();
                Semester semester = (Semester)Enum.Parse(typeof(Semester), value);*/
                
                Semester semester = Semester.Letni;
                
                /*Console.WriteLine("Ročník:");
                int year = int.Parse(Console.ReadLine());*/

                Random random = new Random();
                int year = random.Next(1, 4);
                
                //int subjectLevel = int.Parse(Console.ReadLine());
                int subjectLevel = random.Next(1, 3);

                if (subject.ToLower() == "czech")
                {
                    Subject Czech = SubjectFactory.CreateCzech(garantOfSubject, credits, subjects,year ,semester,subjectLevel);
                    addSubjectToList(ref teachers, ref students, ref subjects, Czech);
                }

                if (subject.ToLower() == "math")
                {
                    Subject Math = SubjectFactory.CreateMath(garantOfSubject, credits, subjects, year, semester,subjectLevel);
                    addSubjectToList(ref teachers, ref students, ref subjects, Math);

                }

                if (subject.ToLower() == "english")
                {
                    Subject English = SubjectFactory.CreateEnglish(garantOfSubject, credits, subjects, year, semester,subjectLevel);
                    addSubjectToList(ref teachers, ref students, ref subjects, English);
                }

            }

        }

        public void createExercise(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Exercise> exercises, List<Subject> subjects)
        {
            Console.WriteLine("Jak chcete cvičení vytvořit");
            Console.WriteLine("1) Nové cvičení");
            Console.WriteLine("2) Cvičení ze šablony");
            string howCreate = Console.ReadLine().ToLower();

            Console.Clear();

            if (howCreate == "1")
            {
                createNewExercise(ref chosenTeacher, ref chosenSubject, exercises, subjects);
            }

            if (howCreate == "2")
            {
                createExerciseFromTemplate(ref chosenTeacher, ref chosenSubject, exercises, subjects);
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

            Console.ReadKey();
        }

        public void createLecture(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Subject> subjects, List<Teacher> teachers)
        {
            Console.WriteLine("Jak chcete přednášku vytvořit");
            Console.WriteLine("1) Nová přednáška");
            Console.WriteLine("2) Přednáška ze šablony");
            string howCreate = Console.ReadLine().ToLower();

            Console.Clear();

            if (howCreate == "1")
            {
                createNewLecture(ref chosenTeacher, ref chosenSubject, teachers);
            }

            if (howCreate == "2")
            {
                createLectureFromTemplate(ref chosenTeacher, ref chosenSubject, subjects, teachers);
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

            Console.ReadKey();
        }

        public void addSubjectToList(ref List<Teacher> teachers, ref List<Student> students, ref List<Subject> subjects, Subject subject)
        {
            foreach (Student Student in students)
            {
                Student.subjectsToRegister.Add(subject);

            }

            foreach (Teacher Teacher in teachers)
            {
                Teacher.subjectsToRegister.Add(subject);


                foreach (Subject Subject in subjects)
                {
                    if (Teacher.registredSubjects.Contains(Subject) && Teacher.subjectsToRegister.Contains(Subject))
                    {
                        Teacher.subjectsToRegister.Remove(Subject);
                    }
                }

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

        public void createNewExercise(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Exercise> exercises, List<Subject> subjects)
        {
            Console.WriteLine("Jméno:");
            //string nameOfExercise = Console.ReadLine();
            string nameOfExercise = "Cvičení1";
            Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine());
            bool computerRequired = false;
            Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 22;
            Console.WriteLine("Předmět?");

            foreach (Subject oneSubject in chosenTeacher.subjectsToRegister)
            {
                Console.WriteLine(oneSubject.name);
            }

            foreach (Subject oneSubject in chosenTeacher.registredSubjects)
            {
                Console.WriteLine(oneSubject.name);
            }

            //string subject = Console.ReadLine();
            string subject = "Czech";

            foreach (Subject oneSubject in chosenTeacher.subjectsToRegister)
            {
                if (oneSubject.name.ToLower() == subject.ToLower())
                {
                    chosenSubject = oneSubject;
                }
            }

            Exercise Exercise = new(nameOfExercise, computerRequired, credits, chosenSubject, exercises);
        }

        public void createExerciseFromTemplate(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Exercise> exercises, List<Subject> subjects)
        {
            {
                bool end = false;
                Console.WriteLine("Jméno:");

                Console.WriteLine("Cvičení z Češtiny");
                Console.WriteLine("Cvičení z Matematiky");
                Console.WriteLine("Cvičení z Angličtiny");

                //string exercise = Console.ReadLine().ToLower();
                string exercise = "Cvičení z Matematiky";
                Console.Clear();

                Console.WriteLine("Počet kreditů k dokončení");
                //double credits = double.Parse(Console.ReadLine());
                double credits = 50;

                if (exercise == "cvičení z češtiny")
                {
                    if (end == false)
                    {
                        foreach (Subject Subject in subjects)
                        {
                            if (Subject.name == "Czech")
                            {
                                chosenSubject = Subject;
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
                        Exercise ExerciseFromCzech = ExerciseFactory.CreateExerciseFromCzech(credits, chosenSubject, exercises);
                        addExerciseToList(ExerciseFromCzech, ref this.exercises);
                    }
                }

                if (exercise == "cvičení z angličtiny")
                {
                    if (end == false)
                    {
                        foreach (Subject Subject in subjects)
                        {
                            if (Subject.name == "English")
                            {
                                chosenSubject = Subject;
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
                        Exercise ExerciseFromEnglish = ExerciseFactory.CreateExerciseFromEnglish(credits, chosenSubject, exercises);
                        addExerciseToList(ExerciseFromEnglish, ref this.exercises);
                    }

                }

                if (exercise == "cvičení z matematiky")
                {
                    if (end == false)
                    {
                        foreach (Subject Subject in subjects)
                        {
                            if (Subject.name == "Math")
                            {
                                chosenSubject = Subject;
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
                        Exercise ExerciseFromMath = ExerciseFactory.CreateExerciseFromMath(credits, chosenSubject, exercises);
                        addExerciseToList(ExerciseFromMath, ref this.exercises);
                    }

                }
            }
        }

        public void createNewLecture(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Teacher> teachers)
        {
            Console.WriteLine("Jméno:");
            //string nameOfLecture = Console.ReadLine();
            string nameOfLecture = "Přednáška 1";
            Console.WriteLine("Nutnost PC? (true/false)");
            //bool computerRequired = bool.Parse(Console.ReadLine());
            bool computerRequired = true;
            Console.WriteLine("Počet kreditů:");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50;
            Console.WriteLine("Předmět?");

            foreach (Subject oneSubject in chosenTeacher.subjectsToRegister)
            {
                Console.WriteLine(oneSubject.name);
            }

            foreach (Subject oneSubject in chosenTeacher.registredSubjects)
            {
                Console.WriteLine(oneSubject.name);
            }

            //string subject = Console.ReadLine();
            string subject = "Czech1_1";

            foreach (Subject oneSubject in chosenTeacher.subjectsToRegister)
            {
                if (oneSubject.name.ToLower() == subject.ToLower())
                {
                    chosenSubject = oneSubject;
                }
            }

            Console.WriteLine("Učitel:");

            foreach (Teacher Teacher in teachers)
            {
                Console.WriteLine(Teacher.firstName);
            }

            //string nameTeacher = Console.ReadLine();
            string nameTeacher = "Pavel";

            foreach (Teacher Teacher in teachers)
            {
                if (Teacher.firstName.ToLower() == nameTeacher.ToLower())
                {
                    chosenTeacher = Teacher;
                }
            }

            Lecture Lecture = new(nameOfLecture, computerRequired, credits, chosenSubject, lectures, chosenTeacher);
        }

        public void createLectureFromTemplate(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Subject> subjects, List<Teacher> teachers)
        {
            bool end = false;
            Console.WriteLine("Jméno:");

            Console.WriteLine("Přednáška z Češtiny");
            Console.WriteLine("Přednáška z Matematiky");
            Console.WriteLine("Přednáška z Angličtiny");

            //string lecture = Console.ReadLine().ToLower();
            string lecture = "Přednáška z Matematiky";
            Console.Clear();

            Console.WriteLine("Počet kreditů k dokončení");
            //double credits = double.Parse(Console.ReadLine());
            double credits = 50;

            Console.WriteLine("Učitel:");

            foreach(Teacher Teacher in teachers)
            {
                Console.WriteLine(Teacher.firstName);
            }

            //string nameTeacher = Console.ReadLine();
            string nameTeacher = "Pavel";

            foreach(Teacher Teacher in teachers)
            {
                if(Teacher.firstName.ToLower() == nameTeacher.ToLower())
                {
                    chosenTeacher = Teacher;
                }
            }

            if (lecture == "přednáška z češtiny")
            {
                if (end == false)
                {
                    foreach (Subject Subject in subjects)
                    {
                        if (Subject.name == "Czech1_1")
                        {
                            chosenSubject = Subject;
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
                    Lecture LectureFromCzech = LectureFactory.CreateLectureFromCzech(credits, chosenSubject, lectures, chosenTeacher);
                    addLectureToList(LectureFromCzech, ref lectures);
                }
            }

            if (lecture == "přednáška z angličtiny")
            {
                if (end == false)
                {
                    foreach (Subject Subject in subjects)
                    {
                        if (Subject.name == "English")
                        {
                            chosenSubject = Subject;
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
                    Lecture LectureFromEnglish = LectureFactory.CreateLectureFromEnglish(credits, chosenSubject, lectures, chosenTeacher);
                    addLectureToList(LectureFromEnglish, ref lectures);
                }

            }

            if (lecture == "přednáška z matematiky")
            {
                if (end == false)
                {
                    foreach (Subject Subject in subjects)
                    {
                        if (Subject.name == "Math")
                        {
                            chosenSubject = Subject;
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
                    Lecture LectureFromMath = LectureFactory.CreateLectureFromMath(credits, chosenSubject, lectures, chosenTeacher);
                    addLectureToList(LectureFromMath, ref lectures);
                }

            }
        }
    }

    public class Subject
    {
        public string name;
        public Teacher garantOfSubject;
        public double credits;
        public int exerciseCount;
        public int lectureCount;
        public int year;
        public Semester semester;
        public int level;

        public Subject(string name, Teacher garantOfSubject, double credits, int year, Semester semester, List<Subject> subjects, int level)
        {
            this.name = name;
            this.garantOfSubject = garantOfSubject;
            this.credits = credits;
            this.semester = semester;
            this.year = year;
            subjects.Add(this);
            this.level = level;
        }
    }

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
            lectures.Add(this);
            subject.lectureCount += 1;
        }
    }


    public class Exercise
    {
        public string name;
        public bool computerRequired;
        public double credits;
        public Subject subject;


        public Exercise(string name, bool computerRequired, double credits, Subject subject, List<Exercise> exercises)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            exercises.Add(this);
            subject.exerciseCount += 1;
        }
        
    }

    class SubjectFactory
    {
        public static Subject CreateMath(Teacher garantOfSubject, double credits, List<Subject> subjects, int year, Semester semester, int subjectLevel)
        {
            return new Subject("Math",garantOfSubject, credits,year, semester, subjects,subjectLevel);
        }

        public static Subject CreateCzech(Teacher garantOfSubject, double credits, List<Subject> subjects, int year, Semester semester, int subjectLevel)
        {
            return new Subject("Czech", garantOfSubject, credits,year, semester, subjects,subjectLevel);
        }

        public static Subject CreateEnglish(Teacher garantOfSubject, double credits, List<Subject> subjects, int year, Semester semester, int subjectLevel)
        {
            return new Subject("English", garantOfSubject, credits,year, semester, subjects,subjectLevel);
        }
    }

    class ExerciseFactory
    {
        public static Exercise CreateExerciseFromMath(double credits, Subject Math, List<Exercise> exercises)
        {
            //return new Exercise("Cvičení z Matematiky", false, credits, Math);
            return new Exercise("Cvičení z Matematiky", false, credits, Math, exercises);
        }

        public static Exercise CreateExerciseFromCzech(double credits, Subject Czech, List<Exercise> exercises)
        {
            return new Exercise("Cvičení z Češtiny", false, credits, Czech,exercises);
        }

        public static Exercise CreateExerciseFromEnglish(double credits, Subject English, List<Exercise> exercises)
        {
            return new Exercise("Cvičení z Angličtiny", false, credits, English,exercises);
        }
    }

    class LectureFactory
    {
        public static Lecture CreateLectureFromMath(double credits, Subject Math, List<Lecture> lectures, Teacher Teacher)
        {
            return new Lecture("Přednáška z Matematiky", false, credits, Math, lectures, Teacher);
        }

        public static Lecture CreateLectureFromCzech(double credits, Subject Czech, List<Lecture> lectures, Teacher Teacher)
        {
            return new Lecture("Přednáška z Češtiny", false, credits, Czech, lectures, Teacher);
        }

        public static Lecture CreateLectureFromEnglish(double credits, Subject English, List<Lecture> lectures, Teacher Teacher)
        {
            return new Lecture("Přednáška z Angličtiny", false, credits, English, lectures, Teacher);
        }
    }

    public class Mark_Subject
    {
        public double mark;
        public Subject subject;
        public int studentId;
        public Mark_Subject(double mark, Subject subject, int studentId, List<Mark_Subject> markSubjectList)
        {
            this.mark = mark;
            this.subject = subject;
            this.studentId = studentId;
            markSubjectList.Add(this);
        }
    }
    
    public enum Semester
    {
        Zimni,
        Letni
    };

}
