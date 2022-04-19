using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Třída studenta
    /// </summary>
    public class Student : Person
    {
        /// <summary>
        /// Datum registrace
        /// </summary>
        public DateTime registrationDate;

        /// <summary>
        /// Seznam předmětu a známky
        /// </summary>
        public static List<MarkSubject> markSubjectList = new();

        /// <summary>
        /// Seznam studentů a jejich předmětů
        /// </summary>
        public List<SubjectStudent> studentSubjectList = new();

        /// <summary>
        /// V jakém je student ročníku
        /// </summary>
        public int year;

        /// <summary>
        /// Seznam všech studentů
        /// </summary>
        public static List<Student> students = new();

        /// <summary>
        /// Konstruktor. Přidá studenta do seznamu studentů
        /// </summary>
        /// <param name="firstName">Jméno</param>
        /// <param name="lastName">Příjmení</param>
        /// <param name="birthDate">Datum narození</param>
        /// <param name="registrationDate">Datum registrace</param>
        /// <param name="year">Aktuální ročník</param>
        public Student(string firstName, string lastName, DateTime birthDate, DateTime registrationDate,
            int year) : base(firstName,
            lastName, birthDate)
        {
            this.registrationDate = registrationDate;
            students.Add(this);
            this.year = year;
        }

        /// <summary>
        /// Změní ročník na další 
        /// </summary>
        public static void nextYear()
        {
            foreach (Student Student in students)
            {
                Student.year += 1;
                Console.WriteLine("Aktuální ročník studenta " + Student.returnFullName() + " je: " + Student.year);
                Student.studentSubjectList = null;
                Student.markSubjectList = null;
            }
        }

        /// <summary>
        /// Nastaví další semestr
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        /// <returns></returns>
        public static Semester nextSemester(Semester currentSemester)
        {
            switch (currentSemester)
            {
                case Semester.Summer:
                    currentSemester =
                        Semester.Winter; // pokud je aktuální semestr Summer, nastaví aktuální semestr na Winter
                    break;

                case Semester.Winter:
                    currentSemester =
                        Semester.Summer; // pokud je aktuální semestr Winter, nastaví aktuální semestr na Summer
                    Student.nextYear(); // nastaví další ročník
                    break;
            }

            Console.WriteLine("Aktuální semestr je: " + currentSemester);
            return currentSemester;
        }

        /// <summary>
        /// Vypíše informace o studentovi
        /// </summary>
        public override void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} {1} a narodil/narodila jsem se {2} a aktuálně studuji od {3}",
                firstName, lastName, birthDate.ToString("MM.dd.yyyy"), registrationDate.ToString("MM.dd.yyyy"));
        }

        /// <summary>
        /// Vypočítá průměr všech známek studenta
        /// </summary>
        /// <returns>Součet všech známek studenta / počet známek student</returns>
        public double calculateAverageMark()
        {
            List<MarkSubject> chosenStudentMarks =
                markSubjectList.FindAll(markSubject =>
                    markSubject.Student == this); // vytvoří nový seznam známek jenom od daného studenta

            double sumOfAllMarks =
                chosenStudentMarks.Sum(markSubject => markSubject.mark); // uloží do sumOfAllMarks součet všech známek
            double countStudentMark = chosenStudentMarks.Count; // uloží do sumOfAllMarks počet všech známek

            return sumOfAllMarks / countStudentMark;
        }

        /// <summary>
        /// Metoda k vybrání studenta
        /// </summary>
        /// <returns></returns>
        public static Student selectStudent()
        {
            foreach (Student Student in students)
            {
                Console.WriteLine(Student.firstName); // vypíše jména všech studentů ze seznamu students
            }

            //string studentName = Console.ReadLine();
            string studentName = "Pepa";

            if (!students.Exists(student =>
                    student.firstName.ToLower() ==
                    studentName.ToLower())) // jestli neexistuje student s daným jménem, spustí znovu metodu
            {
                Console.WriteLine("Neexistuje daný student");
                Console.WriteLine("Zadej jméno existujícího studenta");
                selectStudent();
            }

            Student chosenStudent =
                students.Find(student =>
                    student.firstName.ToLower() == studentName.ToLower()); // uloží do chosenStudent daného studenta

            return chosenStudent;
        }

        /// <summary>
        /// Metoda k registrování předmětu
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        public void registerSubject(Semester currentSemester)
        {
            if (Subject.subjects.Count != 0)
            {
                listSubjectsForRegister(currentSemester); // vypíše předměty dostupné k registraci

                Console.WriteLine("Zadejte název předmětu");
                //string subject = Console.ReadLine();
                string subjectName = "English1_1";
                Console.WriteLine("subject = English1_1");

                foreach (Subject Subject in Subject.subjects.Where(Subject =>
                             Subject.name.ToLower() == subjectName.ToLower() &&
                             Subject.registered == false)) //  projede přeměty kde se název předmětu rovná danému názvu
                {
                    Console.WriteLine(this.firstName + " jsi zapsaný do " + Subject.name + " předmětu, Semestr: " +
                                      Subject.semester);
                    SubjectStudent
                        SubjectStudent =
                            new(Subject, this, Subject.level,
                                this.studentSubjectList); // přidá předmět se studentem do seznamu předmětů a studentů
                    SubjectStudent.Subject.registered = true;
                }
            }
            else
            {
                Console.WriteLine("Nemáš žádné nedokončené předměty");
            }
        }

        /// <summary>
        /// Výpis všech předmětů dostupných k registraci
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        public void listSubjectsForRegister(Semester currentSemester)
        {
            int subjectLevel = 1;
            const int lengthForCompare = 3;
            string completedSubjectName = null;
            string nocompletedSubjectName = null;

            foreach (Subject Subject in Subject.subjects.Where(Subject =>
                             Subject.year == this.year && Subject.semester == currentSemester)
                         .OrderBy(subject => subject.name))
            {
                if (Subject.registered == false && Subject.level == 1)
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3} (Level {4})",
                        Subject.name, Subject.credits,
                        Subject.garantOfSubject.returnFullName(),
                        Subject.semester, Subject.level);
                }

                switch (Subject.completed)
                {
                    case true:
                        subjectLevel = Subject.level + 1;
                        completedSubjectName = Subject.name.Substring(0, lengthForCompare);
                        break;
                    case false:
                    {
                        nocompletedSubjectName = Subject.name.Substring(0, lengthForCompare);

                        if (Subject.level == subjectLevel && completedSubjectName == nocompletedSubjectName)
                        {
                            Console.WriteLine(
                                "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3} (Level {4})",
                                Subject.name, Subject.credits,
                                Subject.garantOfSubject.returnFullName(),
                                Subject.semester, Subject.level);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Vypíše všechny předměty daného studenta
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        public void listAllMySubjects(Semester currentSemester)
        {
            if (studentSubjectList.Count != 0) // kontrola jestli je počet registrovaných předmětů > 0
            {
                foreach (SubjectStudent SubjectStudent in studentSubjectList.Where(SubjectStudent =>
                             SubjectStudent.Subject.year == this.year &&
                             SubjectStudent.Subject.semester == currentSemester &&
                             SubjectStudent.Subject.registered &&
                             this == SubjectStudent
                                 .Student)) // projede registrované předměty studenta v aktuálním roce a semestru
                {
                    Console.WriteLine(
                        "Předmět {0}, k dokončení je potřeba {1} kreditů, garantem je {2}, Semestr: {3}",
                        SubjectStudent.Subject.name, SubjectStudent.credits,
                        SubjectStudent.Subject.garantOfSubject.returnFullName(),
                        SubjectStudent.Subject.semester);
                }
            }
            else
            {
                Console.WriteLine("Nejsi v žádném předmětu");
            }
        }

        /// <summary>
        /// Vypíše všechny dokončené předměty daného studenta
        /// </summary>
        public void listCompletedSubjects()
        {
            if (markSubjectList.Count != 0) // kontrola jestli je počet známek > 0
            {
                foreach (MarkSubject markSubject in
                         markSubjectList.Where(markSubject =>
                             this == markSubject.Student)) // projede všechny předměty daného student
                {
                    Console.WriteLine(
                        "Předmět {0}, garantem je {1}, Semestr: {2} (Level {3}) - známka {4}.",
                        markSubject.Subject.name, markSubject.Subject.garantOfSubject.returnFullName(),
                        markSubject.Subject.semester, markSubject.Subject.level, markSubject.mark);
                }
            }
            else
            {
                Console.WriteLine("Nedokončil jsi zatím žádný předmět");
            }
        }

        /// <summary>
        /// Jít na danou přednášku
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        public void goOnLecture(Semester currentSemester)
        {
            if (Lecture.lectures.Count != 0 &&
                studentSubjectList.Count !=
                0) // kontrola jestli existují nějaké přednášky a jestli má daný student nějaké registrované předměty
            {
                Lecture.listAllRegisteredLectures(studentSubjectList); // výpis všech registrovaných přednášek

                Console.WriteLine("Zadejte název přednášky");

                //string lectureName = Console.ReadLine();
                string lectureName = "Přednáška z Angličtiny1_1";
                Lecture chosenLecture = Lecture.selectLecture(lectureName, this, currentSemester); // výběr přednášky
                Console.WriteLine($"lectureName = {lectureName}");

                foreach (SubjectStudent SubjectStudent in studentSubjectList)
                {
                    Console.WriteLine("Šel jsi na přednášku {0} - {1} kreditů", chosenLecture.name,
                        chosenLecture.credits);
                    SubjectStudent.credits -= chosenLecture.credits;
                    break;
                }
            }
        }


        /// <summary>
        /// Ukončení daného přeedmětu
        /// </summary>
        public void endSubject()
        {
            foreach (SubjectStudent SubjectStudent in studentSubjectList.Where(SubjectStudent =>
                         SubjectStudent.Subject.completed ==
                         false)) // projede všechny nedokončené předměty daného studenta
            {
                if (SubjectStudent.credits > 0 &&
                    this == SubjectStudent.Student) // kontrola jestli jde předmět dokončit
                {
                    Console.WriteLine("Do dokončení předmětu {0} zbývá {1} kreditů", SubjectStudent.Subject.name,
                        SubjectStudent.credits);
                }

                if (SubjectStudent.credits < 1 && this == SubjectStudent.Student)
                {
                    Random r = new();
                    double mark = r.Next(1, 5);
                    Console.WriteLine("Dokončil jsi předmět " + SubjectStudent.Subject.name + " s hodnocením " +
                                      mark);
                    MarkSubject markSubject = new(mark, SubjectStudent.Subject, this);
                    SubjectStudent.Subject.completed = true;
                }
            }
        }

        /// <summary>
        /// Výpis všech registrovaných cvičení
        /// </summary>
        public void listAllAvailableExercise()
        {
            foreach (SubjectStudent SubjectStudent in studentSubjectList) // projede všechny předměty daného studenta
            {
                foreach (Exercise oneExercise in Exercise.exercises.Where(oneExercise =>
                             SubjectStudent.Subject.name == oneExercise.subject.name &&
                             SubjectStudent.Subject
                                 .registered)) // projede všechny registrované cvičení daného studenta 
                {
                    Console.WriteLine("{0} - {1} kreditů, počítač {2}", oneExercise.name,
                        oneExercise.credits, oneExercise.isComputerRequired());
                }
            }
        }

        /// <summary>
        /// Udělat cviření
        /// </summary>
        /// <param name="currentSemester">Aktuální semestr</param>
        public void doExercise(Semester currentSemester)
        {
            if (Exercise.exercises.Count != 0 &&
                studentSubjectList.Count !=
                0) // kontrola jestli existují nějaké předměty a jestli má daný student nějaké registrované předměty
            {
                listAllAvailableExercise(); // vypíše všechny dostupné cvičení
                Console.WriteLine("Zadejte název cvičení");

                //string exerciseName = Console.ReadLine();
                string exerciseName = "Cvičení z Angličtiny1_1";
                Exercise exercise = Exercise.selectExercise(exerciseName, this, currentSemester); // výběr cvičení
                Console.WriteLine($"exerciseName = {exerciseName}");


                SubjectStudent subjectStudent = studentSubjectList.Find(ss =>
                    ss.Subject == exercise.subject && this == ss.Student);

                subjectStudent.credits -= exercise.credits;
                Console.WriteLine("Dokončil jsi cvičení {0} - {1} kreditů", exercise.name,
                    exercise.credits);
            }
        }
    }
}