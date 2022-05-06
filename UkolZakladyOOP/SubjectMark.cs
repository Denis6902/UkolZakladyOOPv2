using System;
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
        /// Jestli je předmět registrovaný 
        /// </summary>
        public bool Registered = true;
        
        /// <summary>
        /// Jestli je předmět dokončený 
        /// </summary>
        public bool Completed = false;

        /// <summary>
        /// Konstruktor. Přidá automaticky instanci do seznamu SubjectMarkList.
        /// </summary>
        /// <param name="subject">Předmět</param>
        /// <param name="SubjectMarkList">List SubjectMarkList</param>
        /// <param name="mark">Známka (není povinná)</param>
        public SubjectMark(Subject subject, List<SubjectMark> SubjectMarkList, double mark = Double.NaN)
        {
            Subject = subject;
            Credits = Subject.Credits;
            Mark = mark;
            SubjectMarkList.Add(this);
        }
    }
}