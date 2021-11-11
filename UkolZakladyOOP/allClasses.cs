using System;
using System.Collections.Generic;
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
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} a narodil/narodila jsem se {2} a jsem pouze obyčejná osoba", firstName, lastName, birthDate.ToString("MM.dd.yyyy"));
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
        public List<Exercise> exerciseList = new();
        public List<Mark_Subject> markSubjectList = new();
        public double averageOfAllMarks;


        public Student(int id,string firstName, string lastName, DateTime birthDate, DateTime registrationDate, List<Subject> subjectsToRegister, List<Exercise> exerciseList, List<Student> students, List<Mark_Subject> markSubjectList) : base(id ,firstName, lastName, birthDate)
        {
            this.registrationDate = registrationDate;
            base.id = id;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjectsToRegister = subjectsToRegister;
            this.exerciseList = exerciseList;
            students.Add(this);
        }

        public Student(int id,string firstName, string lastName, DateTime birthDate, DateTime registrationDate, List<Subject> subjectsToRegister, List<Exercise> exerciseList, List<Mark_Subject> markSubjectList) : base(id, firstName, lastName, birthDate)
        {
            this.registrationDate = registrationDate;
            base.id = id;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjectsToRegister = subjectsToRegister;
            this.exerciseList = exerciseList;
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

                //chosenStudent.firstName = Console.ReadLine();
                chosenStudent.firstName = "Pepa";
                Console.ReadKey();
                Console.Clear();

                foreach (Student Student in students)
                {
                    if (Student.firstName.ToLower() == chosenStudent.firstName.ToLower())
                    {
                        chosenStudent = Student;
                        end = true;
                    }
                }
            }
            while (end == false);
        }

        public void registerSubject(ref Student chosenStudent)
        {
            if (chosenStudent.subjectsToRegister.Count != 0)
            {
                foreach (Subject Subject in chosenStudent.subjectsToRegister)
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName());
                }
                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine(); 
                string subject = "Czech";
                Console.Clear();

                foreach (Subject Subject in chosenStudent.subjectsToRegister.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(chosenStudent.firstName + " jsi zapsaný do " + Subject.name + " předmětu");
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

        public void listAllSubjects()
        {
            if (registredSubjects.Count != 0)
            {
                foreach (Subject Subject in registredSubjects.ToArray())
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName());
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

        public void listAllTeachers(List<Teacher> teachers)
        {
            foreach (Teacher Teacher in teachers)
            {
                Console.WriteLine("{0} - vyučuje {1} předmětů a {2} přednášky", Teacher.returnFullName(), Teacher.registredSubjects.Count, Teacher.lectureList.Count);
            }
        }

        public void goOnLecture(List<Lecture> lectures, ref Student chosenStudent, ref Lecture chosenLecture, ref Subject chosenSubject, Subject DefaultSubject, Lecture DefaultLecture, List<Lecture> lectureList)
        {
            if (chosenStudent.exerciseList.Count != 0 && chosenStudent.registredSubjects.Count != 0)
            {
                bool end = false;
                do
                {
                    foreach (Lecture Lecture in lectureList)
                    {
                        if (chosenStudent.registredSubjects.Contains(Lecture.subject))
                        {
                            Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", Lecture.name, Lecture.credits, Lecture.computerRequired, Lecture.subject.name);
                        }
                    }

                    Console.WriteLine("Zadejte název přednášky");
                    Console.ReadKey();

                    chosenLecture.name = "Přednáška z češtiny";

                    if (chosenLecture.name == "")
                    {
                        break;
                    }

                    foreach (Lecture Lecture in lectureList)
                    {
                        if (Lecture.name.ToLower() == chosenLecture.name.ToLower() && end == false)
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
                        chosenSubject = DefaultSubject;
                        chosenLecture = DefaultLecture;
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
                    chosenStudent.markSubjectList.Add(markSubject);
                    chosenStudent.registredSubjects.Remove(Subject);
                }
            }
        }

        public void doExercise(ref Student chosenStudent, ref Exercise chosenExercise, Exercise DefaultExercise, ref Subject chosenSubject, Subject DefaultSubject)
        {
            if (chosenStudent.exerciseList.Count != 0 && chosenStudent.registredSubjects.Count != 0)
            {
                bool end = false;
                do
                {
                    foreach (Exercise Exercise in chosenStudent.exerciseList)
                    {
                        if (chosenStudent.registredSubjects.Contains(Exercise.subject))
                        {
                            Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2}", Exercise.name, Exercise.credits, Exercise.computerRequired);
                        }
                    }

                    Console.WriteLine("Zadejte název cvičení");

                    //chosenExercise.name = Console.ReadLine();
                    chosenExercise.name = "Cvičení z češtiny";

                    Console.Clear();

                    if (chosenExercise.name == "")
                    {
                        break;
                    }

                    foreach (Exercise Exercise in chosenStudent.exerciseList)
                    {
                        if (Exercise.name.ToLower() == chosenExercise.name.ToLower() && end == false)
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
                        chosenSubject = DefaultSubject;
                        chosenExercise = DefaultExercise;
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
        public List<Exercise> exerciseList = new();
        public List<Lecture> lectureList = new();

        public Teacher(string academicTitle, int id, string firstName, string lastName, DateTime birthDate, List<Subject> subjectsToRegister, List<Exercise> exerciseList, List<Teacher> teachers) : base(id, firstName, lastName, birthDate)
        {
            this.academicTitle = academicTitle;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjectsToRegister = subjectsToRegister;
            this.exerciseList = exerciseList;
            teachers.Add(this);
        }

        public Teacher(string academicTitle, int id,string firstName, string lastName, DateTime birthDate, List<Subject> subjectsToRegister, List<Exercise> exerciseList) : base(id, firstName, lastName, birthDate)
        {
            this.academicTitle = academicTitle;
            base.firstName = firstName;
            base.lastName = lastName;
            base.birthDate = birthDate;
            this.subjectsToRegister = subjectsToRegister;
            this.exerciseList = exerciseList;
        }

        public override void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} {2} a narodil/narodila jsem se {3} a aktuálně učím ve škole", academicTitle, firstName, lastName, birthDate.ToString("MM.dd.yyyy"));
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

                //chosenTeacher.firstName = Console.ReadLine();
                chosenTeacher.firstName = "Pavel";
                Console.ReadKey();
                Console.Clear();

                foreach (Teacher Teacher in teachers)
                {
                    if (Teacher.firstName.ToLower() == chosenTeacher.firstName.ToLower())
                    {
                        chosenTeacher = Teacher;
                        end = true;
                    }
                }
            }
            while (end == false);
        }
        public void registerSubject(ref Teacher chosenTeacher)
        {
            if (chosenTeacher.subjectsToRegister.Count != 0)
            {
                foreach (Subject Subject in chosenTeacher.subjectsToRegister)
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName());
                }

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine(); 
                string subject = "Czech";
                Console.Clear();

                foreach (Subject Subject in chosenTeacher.subjectsToRegister.ToArray())
                {
                    if ((Subject.name.ToLower()) == subject.ToLower())
                    {
                        Console.WriteLine(chosenTeacher.firstName + " jsi zapsaný do " + Subject.name + " předmětu");
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
        public void listAllSubjects()
        {
            if (registredSubjects.Count != 0)
            {
                foreach (Subject Subject in registredSubjects.ToArray())
                {
                    Console.WriteLine("Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}", Subject.name, Subject.credits, Subject.garantOfSubject.returnFullName());
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }

            Console.ReadKey();
        }
        public void createSubject(ref List<Student> students, ref List<Teacher> teachers, Teacher DefaultTeacher, List<Subject> subjects)
        {
            bool end = false;
            Teacher garantOfSubject = DefaultTeacher;

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

                    //garantOfSubject.firstName = Console.ReadLine();
                    garantOfSubject.firstName = "Pavel";
                    Console.Clear();

                    foreach (Teacher Teacher in teachers)
                    {
                        if (Teacher.firstName.ToLower() == garantOfSubject.firstName.ToLower())
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

                Subject Subject = new(name, garantOfSubject, credits, subjects);

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

                    //garantOfSubject.firstName = Console.ReadLine();
                    garantOfSubject.firstName = "Pavel";
                    Console.Clear();

                    foreach (Teacher Teacher in teachers)
                    {
                        if (Teacher.firstName.ToLower() == garantOfSubject.firstName.ToLower())
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

                if (subject.ToLower() == "czech")
                {
                    Subject Czech = SubjectFactory.CreateCzech(garantOfSubject, credits, subjects);
                    addSubjectToList(ref teachers, ref students, ref subjects, Czech);
                }

                if (subject.ToLower() == "math")
                {
                    Subject Math = SubjectFactory.CreateMath(garantOfSubject, credits, subjects);
                    addSubjectToList(ref teachers, ref students, ref subjects, Math);

                }

                if (subject.ToLower() == "english")
                {
                    Subject English = SubjectFactory.CreateMath(garantOfSubject, credits, subjects);
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
            if (exerciseList.Count == 0)
            {
                Console.WriteLine("Neexistuje žádné cvičení");
            }
            else
            {
                foreach (Exercise oneExercise in exerciseList)
                {
                    Console.WriteLine("{0} - {1} kreditů, počítač je potřeba {2} (Předmět {3})", oneExercise.name, oneExercise.credits, oneExercise.computerRequired, oneExercise.subject.name);

                }
            }
        }

        public void listStudentsByAverageMarks(List<Student> students, ref List<string> averageMarksList)
        {
            foreach (Student Student in students)
            {
                if (Student.markSubjectList.Count != 0)
                {
                    averageMarksList.Add(Student.calculateAverage() + " - průměrná známka ze všech předmětu studenta " + Student.returnFullName());
                }
            }

            foreach (string averageMark in averageMarksList.OrderBy(x => x))
            {
                Console.WriteLine(averageMark);
            }

            Console.ReadKey();
        }

        public void createLecture(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Subject> subjects)
        {
            Console.WriteLine("Jak chcete přednášku vytvořit");
            Console.WriteLine("Nová přednáška");
            Console.WriteLine("Přednáška ze šablony");
            string howCreate = Console.ReadLine().ToLower();

            Console.Clear();

            if (howCreate == "nová přednáška")
            {
                createNewLecture(ref chosenTeacher, ref chosenSubject);
            }

            if (howCreate == "přednáška ze šablony")
            {
                createLectureFromTemplate(ref chosenTeacher, ref chosenSubject, subjects);
            }
        }

        public void listAllLecture(List<Teacher> teachers)
        {
            foreach (Teacher Teacher in teachers)
            {
                foreach (Lecture Lecture in Teacher.lectureList)
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

        public void addExerciseToList(Exercise exercise, ref List<Exercise> exerciseList)
        {
            exerciseList.Add(exercise);
        }

        public void addLectureToList(Lecture lecture, ref List<Lecture> lectureList)
        {
            lectureList.Add(lecture);
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
                        Exercise ExerciseFromCzech = ExerciseFactory.CreateExerciseFromCzech(credits, chosenSubject);
                        addExerciseToList(ExerciseFromCzech, ref exerciseList);
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
                        Exercise ExerciseFromEnglish = ExerciseFactory.CreateExerciseFromEnglish(credits, chosenSubject);
                        addExerciseToList(ExerciseFromEnglish, ref exerciseList);
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
                        Exercise ExerciseFromMath = ExerciseFactory.CreateExerciseFromMath(credits, chosenSubject);
                        addExerciseToList(ExerciseFromMath, ref exerciseList);
                    }

                }
            }
        }

        public void createNewLecture(ref Teacher chosenTeacher, ref Subject chosenSubject)
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
            string subject = "Czech";

            foreach (Subject oneSubject in chosenTeacher.subjectsToRegister)
            {
                if (oneSubject.name.ToLower() == subject.ToLower())
                {
                    chosenSubject = oneSubject;
                }
            }

            Lecture Lecture = new(nameOfLecture, computerRequired, credits, chosenSubject, lectureList);
        }

        public void createLectureFromTemplate(ref Teacher chosenTeacher, ref Subject chosenSubject, List<Subject> subjects)
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

            if (lecture == "přednáška z češtiny")
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
                    Lecture LectureFromCzech = LectureFactory.CreateLectureFromCzech(credits, chosenSubject, lectureList);
                    addLectureToList(LectureFromCzech, ref lectureList);
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
                    Lecture LectureFromEnglish = LectureFactory.CreateLectureFromEnglish(credits, chosenSubject, lectureList);
                    addLectureToList(LectureFromEnglish, ref lectureList);
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
                    Lecture LectureFromMath = LectureFactory.CreateLectureFromMath(credits, chosenSubject, lectureList);
                    addLectureToList(LectureFromMath, ref lectureList);
                }

            }
        }
    }



    public class Subject
    {
        public string name;
        public Teacher garantOfSubject;
        public double credits;
        //public double mark;

        public Subject(string name, Teacher garantOfSubject, double credits, List<Subject> subjects)
        {
            this.name = name;
            this.garantOfSubject = garantOfSubject;
            this.credits = credits;
            garantOfSubject.registredSubjects.Add(this);
            subjects.Add(this);
        }

        public Subject(string name, Teacher garantOfSubject, double credits)
        {
            this.name = name;
            this.garantOfSubject = garantOfSubject;
            this.credits = credits;
            garantOfSubject.registredSubjects.Add(this);
        }

    }

    public class Lecture
    {
        public string name;
        public bool computerRequired;
        public double credits;
        public Subject subject;
        public Teacher teacher;

        public Lecture(string name, bool computerRequired, double credits, Subject subject, List<Lecture> lectureList)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
            teacher = subject.garantOfSubject;
            teacher.lectureList.Add(this);
            lectureList.Add(this);
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
        }

        public Exercise(string name, bool computerRequired, double credits, Subject subject)
        {
            this.name = name;
            this.computerRequired = computerRequired;
            this.credits = credits;
            this.subject = subject;
        }
    }

    class SubjectFactory
    {
        public static Subject CreateMath(Teacher garantOfSubject, double credits, List<Subject> subjects)
        {
            return new Subject("Math", garantOfSubject, credits, subjects);
        }

        public static Subject CreateCzech(Teacher garantOfSubject, double credits, List<Subject> subjects)
        {
            return new Subject("Czech", garantOfSubject, credits, subjects);
        }

        public static Subject CreateEnglish(Teacher garantOfSubject, double credits, List<Subject> subjects)
        {
            return new Subject("English", garantOfSubject, credits, subjects);
        }
    }

    class ExerciseFactory
    {
        public static Exercise CreateExerciseFromMath(double credits, Subject Math)
        {
            return new Exercise("Cvičení z Matematiky", false, credits, Math);
        }

        public static Exercise CreateExerciseFromCzech(double credits, Subject Czech)
        {
            return new Exercise("Cvičení z Češtiny", false, credits, Czech);
        }

        public static Exercise CreateExerciseFromEnglish(double credits, Subject English)
        {
            return new Exercise("Cvičení z Angličtiny", false, credits, English);
        }
    }

    class LectureFactory
    {
        public static Lecture CreateLectureFromMath(double credits, Subject Math, List<Lecture> lectureList)
        {
            return new Lecture("Přednáška z Matematiky", false, credits, Math, lectureList);
        }

        public static Lecture CreateLectureFromCzech(double credits, Subject Czech, List<Lecture> lectureList)
        {
            return new Lecture("Přednáška z Češtiny", false, credits, Czech, lectureList);
        }

        public static Lecture CreateLectureFromEnglish(double credits, Subject English, List<Lecture> lectureList)
        {
            return new Lecture("Přednáška z Angličtiny", false, credits, English, lectureList);
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

}
