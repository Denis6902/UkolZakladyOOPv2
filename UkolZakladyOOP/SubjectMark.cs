using System.Collections.Generic;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Propojovací třída studenta, jeho předmětů a známek
    /// </summary>
    public class SubjectMark
    {
        /// <summary>
        /// Předmět
        /// </summary>
        public Subject Subject;

        /// <summary>
        /// Počet kreditů
        /// </summary>
        public double Credits;

        /// <summary>
        /// Známka
        /// </summary>
        public double Mark;

        /// <summary>
        /// Jestli je předmět dokončený 
        /// </summary>
        public bool Completed = false;
        
        /*/// <summary>
        /// Jestli jsou všechny cvičení daného předmětu dokončené
        /// </summary>
        public bool AllExercisesDone = false;
        
        /// <summary>
        /// Jestli jsou všechny přednášky daného předmětu dokončené
        /// </summary>
        public bool AllLecturesDone = false;*/

        /// <summary>
        /// Konstruktor. Přidá automaticky instanci do seznamu SubjectMarkList.
        /// </summary>
        /// <param name="subject">Předmět</param>
        /// <param name="SubjectMarkList">Seznam předmětů a známek studenta</param>
        public SubjectMark(Subject subject, List<SubjectMark> SubjectMarkList)
        {
            Subject = subject;
            Credits = Subject.Credits;
            SubjectMarkList.Add(this);
        }
    }
}