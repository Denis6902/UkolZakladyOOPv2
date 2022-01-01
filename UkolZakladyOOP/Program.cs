using System;
using System.Collections.Generic;

namespace UkolZakladyOOP
{
    class Program
    {
        static public void Main(string[] args)
        {
            Semester currentSemester = Semester.Summer;

            Teacher Pavel = new("Ing.", 1, "Pavel", "Novotný", new DateTime(1980, 2, 9));
            Teacher Aneta = new("Mgr.", 2, "Aneta", "Nováková", new DateTime(1987, 1, 8));

            Subject English1_1 = new("English1_1", Pavel, Pavel, 50, 1, Semester.Summer, 1);
            Subject Czech1_1 = new("Czech1_1", Aneta, Aneta, 50, 1, Semester.Summer, 1);

            Subject English2_1 = new("English2_1", Pavel, Pavel, 50, 2, Semester.Summer, 1);
            Subject Czech2_1 = new("Czech2_1", Aneta, Aneta, 50, 2, Semester.Winter, 1);

            Subject English3_1 = new("English3_1", Pavel, Pavel, 50, 3, Semester.Summer, 1);
            Subject Czech3_1 = new("Czech3_1", Aneta, Aneta, 50, 3, Semester.Winter, 1);

            Subject English1_2 = new("English1_2", Pavel, Pavel, 50, 1, Semester.Summer, 2);
            Subject Czech1_2 = new("Czech1_2", Aneta, Aneta, 50, 1, Semester.Summer, 2);

            Subject English2_2 = new("English2_2", Pavel, Pavel, 50, 2, Semester.Summer, 2);
            Subject Czech2_2 = new("Czech2_2", Aneta, Aneta, 50, 2, Semester.Winter, 2);

            Subject English3_2 = new("English3_2", Pavel, Pavel, 50, 3, Semester.Summer, 2);
            Subject Czech3_2 = new("Czech3_2", Aneta, Aneta, 50, 3, Semester.Winter, 2);

            Subject x = new("x", Pavel, null, 0, 1, Semester.Summer, 1); // TEST_ONLY
            Lecture c = new("c", false, 0, x, Pavel); // TEST_ONLY
            Exercise o = new Exercise("o", false, 0, x); // TEST_ONLY

            Student Jakub = new(1, "Jakub", "Novák", new DateTime(1999, 7, 2), new DateTime(2020, 10, 1),
                1);
            Student Pepa = new(2, "Pepa", "Nový", new DateTime(1998, 9, 3), new DateTime(2020, 10, 2),
                1);
            Student Denis = new(3, "Denis", "Vojtěch", new DateTime(1984, 9, 3), new DateTime(2020, 1, 2),
                1);

            Exercise ExerciseFromEnglish1_1 = new("Cvičení z Angličtiny", false, 50, English1_1);
            Exercise ExerciseFromCzech1_1 = new("Cvičení z Češtiny", false, 50, Czech1_1);
            Lecture LectureFromEnglish1_1 = new("Přednáška z Angličtiny", false, 50, English1_1, Pavel);
            Lecture LectureFromCzech1_1 = new("Přednáška z Češtiny", false, 50, Czech1_1, Aneta);

            Exercise ExerciseFromEnglish2_1 = new("Cvičení z Angličtiny", false, 50, English2_1);
            Exercise ExerciseFromCzech2_1 = new("Cvičení z Češtiny", false, 50, Czech2_1);
            Lecture LectureFromEnglish2_1 = new("Přednáška z Angličtiny", false, 50, English2_1, Pavel);
            Lecture LectureFromCzech2_1 = new("Přednáška z Češtiny", false, 50, Czech2_1, Aneta);

            Exercise ExerciseFromEnglish3_1 = new("Cvičení z Angličtiny", false, 50, English3_1);
            Exercise ExerciseFromCzech3_1 = new("Cvičení z Češtiny", false, 50, Czech3_1);
            Lecture LectureFromEnglish3_1 = new("Přednáška z Angličtiny", false, 50, English3_1, Pavel);
            Lecture LectureFromCzech3_1 = new("Přednáška z Češtiny", false, 50, Czech3_1, Aneta);

            Exercise ExerciseFromEnglish1_2 = new("Cvičení z Angličtiny", false, 50, English1_2);
            Exercise ExerciseFromCzech1_2 = new("Cvičení z Češtiny", false, 50, Czech1_2);
            Lecture LectureFromEnglish1_2 = new("Přednáška z Angličtiny", false, 50, English1_2, Pavel);
            Lecture LectureFromCzech1_2 = new("Přednáška z Češtiny", false, 50, Czech1_2, Aneta);

            Exercise ExerciseFromEnglish2_2 = new("Cvičení z Angličtiny", false, 50, English2_2);
            Exercise ExerciseFromCzech2_2 = new("Cvičení z Češtiny", false, 50, Czech2_2);
            Lecture LectureFromEnglish2_2 = new("Přednáška z Angličtiny", false, 50, English2_2, Pavel);
            Lecture LectureFromCzech2_2 = new("Přednáška z Češtiny", false, 50, Czech2_2, Aneta);

            Exercise ExerciseFromEnglish3_2 = new("Cvičení z Angličtiny", false, 50, English3_2);
            Lecture LectureFromEnglish3_2 = new("Přednáška z Angličtiny", false, 50, English3_2, Pavel);

            string whoIAm = "1";
            const int delay = 2500;

            Method Method = new();
            Method.mainMenu(whoIAm, currentSemester, delay);
        }
    }
}