using System;
using System.Collections.Generic;

using System.Threading;


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
        public List<Subject> subjects = new();
        public List<Exercise> exercises = new();
        public static List<MarkSubject> markSubjectList = new();
        public static List<SubjectStudent> studentSubjectList = new();
        public double averageOfAllMarks;
        public int year;
        public int credits;


        public Student(int id,string firstName, string lastName, DateTime birthDate, DateTime registrationDate, List<Subject> subjects, List<Exercise> exercises, List<Student> students, int year) : base(id ,firstName, lastName, birthDate)
        {
            this.registrationDate = registrationDate;
            base.id = id;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjects = subjects;
            this.exercises = exercises;
            students.Add(this);
            this.year = year;
        }
        
        public static void nextYear(List<Student> students)
        {
            foreach (Student student in (students))
            {
                student.year += 1;
                Console.WriteLine("Aktuální ročník studenta " + student.returnFullName() + " je: " + student.year);
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
            }
            while (end == false);
        }

        public void registerSubject(Semester currentSemester)
        {
            if (this.subjects.Count != 0)
            {
                foreach (Subject Subject in this.subjects)
                {
                    if (Subject.year == this.year && Subject.semester == currentSemester)
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
                    if (Subject.name.ToLower() == subject.ToLower() && Subject.registered == false)
                    {
                        Console.WriteLine(this.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " + Subject.semester);
                        SubjectStudent SubjectStudent = new(Subject,this, Student.studentSubjectList);
                        subjects.Remove(Subject);
                    }
                }

                foreach (SubjectStudent SubjectStudent in studentSubjectList)
                {
                    SubjectStudent.Subject.registered = true;
                }

            }
            else
            {
                Console.WriteLine("Nemáš žádné nedokončené předměty");
            }
        }

        public void listAllMySubjects(Semester currentSemester)
        {
            if (subjects.Count != 0)
            {
                foreach (SubjectStudent SubjectStudent in studentSubjectList)
                {
                    if (SubjectStudent.Subject.year == this.year && SubjectStudent.Subject.semester == currentSemester && SubjectStudent.Subject.registered == true)
                    {
                        Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", SubjectStudent.Subject.name, SubjectStudent.Subject.credits, SubjectStudent.Subject.garantOfSubject.returnFullName(), SubjectStudent.Subject.semester);
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
                        Console.WriteLine("Předmět " + (markSubject.subject.name) +  " známka: " + markSubject.mark);
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
            Subject Subject = null;
            if (this.exercises.Count != 0 && this.subjects.Count != 0)
            {
                bool end = false;
                do
                {
                    foreach (Lecture oneLecture in lectures)
                    {
                        foreach (SubjectStudent SubjectStudent in studentSubjectList)
                        {
                            if (SubjectStudent.Subject == oneLecture.subject && SubjectStudent.Subject.registered == true)
                            {
                                Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", oneLecture.name, oneLecture.credits, oneLecture.computerRequired, oneLecture.subject.name);
                            }
                        }
                    }

                    Console.WriteLine("Zadejte název přednášky");

                    //string lectureName = Console.ReadLine();
                    string lectureName = "c";

                    if (lectureName == "")
                    {
                        break;
                    }

                    foreach (SubjectStudent SubjectStudent in studentSubjectList)
                    {
                        foreach (Lecture oneLecture in lectures)
                        {
                            if (oneLecture.name.ToLower() == lectureName.ToLower() && end == false)
                            {
                                Lecture = oneLecture;
                                SubjectStudent.Subject = Lecture.subject;
                                end = true;
                            }
                        }

                        if (end == true)
                        {
                            Console.WriteLine("Šel jsi na přednášku {0} - {1} kreditů", Lecture.name, Lecture.credits);
                            SubjectStudent.Subject.credits = SubjectStudent.Subject.credits - Lecture.credits;
                        }
                    }
                  
                }
                while (end == false);
            }
        }


        public void endSubject(ref List<MarkSubject> markSubjectList)
        {
            foreach (SubjectStudent SubjectStudent in studentSubjectList)
            {
                if (SubjectStudent.Subject.credits > 0)
                    Console.WriteLine("Do dokončení předmětu {0} zbývá {1} kreditů", SubjectStudent.Subject.name, SubjectStudent.Subject.credits);

                if (SubjectStudent.Subject.credits < 1)
                {
                    Random r = new();
                    double mark = r.Next(1, 5);
                    this.averageOfAllMarks += mark;
                    Console.WriteLine("Dokončil jsi předmět " + SubjectStudent.Subject.name + " s hodnocením " + mark);
                    MarkSubject markSubject = new(mark, SubjectStudent.Subject, this.id, markSubjectList);
                    SubjectStudent.Subject.completed = true;
                    SubjectStudent.Subject.registered = false;
                    
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
                            if (SubjectStudent.Subject.name == oneExercise.subject.name  && SubjectStudent.Subject.registered == true)
                            {
                                Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2}", oneExercise.name, oneExercise.credits, oneExercise.computerRequired);
                            }
                        }
                    }
                    Console.WriteLine("Zadejte název cvičení");

                    //string exerciseName = Console.ReadLine();
                    string exerciseName = "o";
                    Console.WriteLine("exerciseName = o");
                    
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
                        Console.WriteLine("Dokončil jsi cvičení {0} - {1} kreditů", Exercise.name, Exercise.credits);
                        Exercise.subject.credits = Exercise.subject.credits - Exercise.credits;
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
            if (this.subjectsToRegister.Count != 0)
            {
                foreach (Subject Subject in subjects)
                {
                    if (currentSemester == Subject.semester)
                    {
                        Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName(), Subject.semester );
                    }
                }

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine(); 
                string subject = "x";
                Console.WriteLine("subject = x");

                foreach (Subject Subject in this.subjectsToRegister.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(this.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " + Subject.semester);
                        this.subjectsToRegister.Remove(Subject);
                        this.registredSubjects.Add(Subject);
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
        }
        public void createSubject(ref List<Student> students, ref List<Teacher> teachers, List<Subject> subjects)
        {
            bool end = false;
            Teacher garantOfSubject = null;

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
                

                Subject Subject = new(name, garantOfSubject, credits, year ,semester, subjects,subjectLevel);

                foreach (Student Student in students)
                {
                    Student.subjects.Add(Subject);
                }

                foreach (Teacher Teacher in teachers)
                {
                    Teacher.subjectsToRegister.Add(Subject);
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
                Console.WriteLine("Math");
                Console.WriteLine("English");

                //string subject = Console.ReadLine();
                string subject = "Czech";
                Console.WriteLine("subject = Cuecj");

                
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
            
            Thread.Sleep(2500);
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
            Console.WriteLine("subject = Czech1_1");


            foreach (Subject oneSubject in chosenTeacher.subjectsToRegister)
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
                Console.WriteLine("Cvičení z Matematiky");
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
                            if (oneSubject.name == "Czech_1")
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
                            if (oneSubject.name == "English")
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

                if (exercise == "cvičení z matematiky")
                {
                    if (end == false)
                    {
                        foreach (Subject oneSubject in subjects)
                        {
                            if (oneSubject.name == "Math")
                            {
                                Subject = oneSubject;
                                end = true;
                            }
                        }
                    }

                    if (end == false)
                    {
                        Console.WriteLine("Neexistuje daný předmět");
                    }

                    if (end == true)
                    {
                        Exercise ExerciseFromMath = ExerciseFactory.CreateExerciseFromMath(credits, Subject, exercises);
                        addExerciseToList(ExerciseFromMath, ref this.exercises);
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

           /* foreach (Subject oneSubject in this.subjectsToRegister)
            {
                Console.WriteLine(oneSubject.name);
            }

            foreach (Subject oneSubject in this.registredSubjects)
            {
                Console.WriteLine(oneSubject.name);
            }*/

            //string subject = Console.ReadLine();
            string subject = "Czech1_1";
            Console.WriteLine("subject = Czech1_1");


            foreach (Subject oneSubject in this.subjectsToRegister)
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
            Console.WriteLine("Přednáška z Matematiky");
            Console.WriteLine("Přednáška z Angličtiny");

            //string lecture = Console.ReadLine().ToLower();
            string lecture = "Přednáška z Matematiky";
            Console.WriteLine("lecture = Přednáška z Matematiky");

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
                        if (oneSubject.name == "Czech1_1")
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
                        if (oneSubject.name == "English")
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

            if (lecture == "přednáška z matematiky")
            {
                if (end == false)
                {
                    foreach (Subject oneSubject in subjects)
                    {
                        if (oneSubject.name == "Math")
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
                    Lecture LectureFromMath = LectureFactory.CreateLectureFromMath(credits, Subject, lectures, Teacher);
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
        public bool registered = false;
        public bool completed = false;
        
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
        
        public SubjectStudent(Subject Subject,Student Student, List<SubjectStudent> subjectStudentList)
        {
            this.Subject = Subject;
            this.Student = Student;
            subjectStudentList.Add(this);
        }
    }
    
    public enum Semester
    {
        Winter,
        Summer
    };

}
