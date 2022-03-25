using System;

namespace UkolZakladyOOP
{
    class Program
    {
        static public void Main(string[] args)
        {
            Teacher Pavel = new("Ing.", "Pavel", "Novotný", new DateTime(1980, 2, 9));
            Teacher Aneta = new("Mgr.", "Aneta", "Nováková", new DateTime(1987, 1, 8));

            Subject English1_1 = new("English1_1", Pavel, Pavel, 50, 1, Semester.Summer, 1);
            Subject xxx1_1 = new("xxx1_1", Aneta, null, 50, 1, Semester.Summer, 1); // TEST_ONLY
            Subject Czech1_1 = new("Czech1_1", Aneta, Aneta, 50, 1, Semester.Winter, 1);

            Subject English2_1 = new("English2_1", Pavel, Pavel, 50, 2, Semester.Summer, 1);
            Subject Czech2_1 = new("Czech2_1", Aneta, Aneta, 50, 2, Semester.Winter, 1);

            Subject English3_1 = new("English3_1", Pavel, Pavel, 50, 3, Semester.Summer, 1);
            Subject Czech3_1 = new("Czech3_1", Aneta, Aneta, 50, 3, Semester.Winter, 1);

            Subject English1_2 = new("English1_2", Pavel, Pavel, 50, 1, Semester.Summer, 2);
            Subject xxx_2 = new("xxx1_2", Aneta, Aneta, 50, 1, Semester.Summer, 2); // TEST_ONLY
            Subject Czech1_2 = new("Czech1_2", Aneta, Aneta, 50, 1, Semester.Winter, 2);

            Subject English2_2 = new("English2_2", Pavel, Pavel, 50, 2, Semester.Summer, 2);
            Subject Czech2_2 = new("Czech2_2", Aneta, Aneta, 50, 2, Semester.Winter, 2);

            Subject English3_2 = new("English3_2", Pavel, Pavel, 50, 3, Semester.Summer, 2);
            Subject Czech3_2 = new("Czech3_2", Aneta, Aneta, 50, 3, Semester.Winter, 2);

            Exercise ooo1_1 = new Exercise("ooo1_1", false, 50, xxx1_1); // TEST_ONLY
            Lecture ppp1_1 = new("ppp1_1", false, 50, xxx1_1); // TEST_ONLY


            Student Jakub = new("Jakub", "Novák", new DateTime(1999, 7, 2), new DateTime(2020, 10, 1),
                1);
            Student Pepa = new("Pepa", "Nový", new DateTime(1998, 9, 3), new DateTime(2020, 10, 2),
                1);
            Student Denis = new("Denis", "Vojtěch", new DateTime(1984, 9, 3), new DateTime(2020, 1, 2),
                1);

            Exercise ExerciseFromEnglish1_1 = new("Cvičení z Angličtiny", false, 50, English1_1);
            Exercise ExerciseFromCzech1_1 = new("Cvičení z Češtiny", false, 50, Czech1_1);
            Lecture LectureFromEnglish1_1 = new("Přednáška z Angličtiny", false, 50, English1_1);
            Lecture LectureFromCzech1_1 = new("Přednáška z Češtiny", false, 50, Czech1_1);

            Exercise ExerciseFromEnglish2_1 = new("Cvičení z Angličtiny", false, 50, English2_1);
            Exercise ExerciseFromCzech2_1 = new("Cvičení z Češtiny", false, 50, Czech2_1);
            Lecture LectureFromEnglish2_1 = new("Přednáška z Angličtiny", false, 50, English2_1);
            Lecture LectureFromCzech2_1 = new("Přednáška z Češtiny", false, 50, Czech2_1);

            Exercise ExerciseFromEnglish3_1 = new("Cvičení z Angličtiny", false, 50, English3_1);
            Exercise ExerciseFromCzech3_1 = new("Cvičení z Češtiny", false, 50, Czech3_1);
            Lecture LectureFromEnglish3_1 = new("Přednáška z Angličtiny", false, 50, English3_1);
            Lecture LectureFromCzech3_1 = new("Přednáška z Češtiny", false, 50, Czech3_1);

            Exercise ExerciseFromEnglish1_2 = new("Cvičení z Angličtiny", false, 50, English1_2);
            Exercise ExerciseFromCzech1_2 = new("Cvičení z Češtiny", false, 50, Czech1_2);
            Lecture LectureFromEnglish1_2 = new("Přednáška z Angličtiny", false, 50, English1_2);
            Lecture LectureFromCzech1_2 = new("Přednáška z Češtiny", false, 50, Czech1_2);

            Exercise ExerciseFromEnglish2_2 = new("Cvičení z Angličtiny", false, 50, English2_2);
            Exercise ExerciseFromCzech2_2 = new("Cvičení z Češtiny", false, 50, Czech2_2);
            Lecture LectureFromEnglish2_2 = new("Přednáška z Angličtiny", false, 50, English2_2);
            Lecture LectureFromCzech2_2 = new("Přednáška z Češtiny", false, 50, Czech2_2);

            Exercise ExerciseFromEnglish3_2 = new("Cvičení z Angličtiny", false, 50, English3_2);
            Lecture LectureFromEnglish3_2 = new("Přednáška z Angličtiny", false, 50, English3_2);


            SchoolSystem schoolSystem = new();
            schoolSystem.mainMenu();
            
            // TODO: předělat: foreach + if -> foreach.where/find/exists, 
        }
    }
}