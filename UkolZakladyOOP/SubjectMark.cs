using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Číslo skupiny
        /// </summary>
        public int GroupNumber;

        /// <summary>
        /// Skupina
        /// </summary>
        public SubjectGroup Group;

        /// <summary>
        /// Konstruktor. Přidá automaticky instanci do seznamu SubjectMarkList.
        /// </summary>
        /// <param name="subject">Předmět</param>
        /// <param name="SubjectMarkList">Seznam předmětů a známek studenta</param>
        /// <param name="groupNumber">Číslo skupiny</param>
        /// <param name="student">Daný student</param>
        public SubjectMark(Subject subject, List<SubjectMark> SubjectMarkList, int groupNumber, Student student)
        {
            Subject = subject;
            Credits = Subject.Credits;
            GroupNumber = (groupNumber <= Subject.MaxGroupCount && groupNumber > 0)
                ? groupNumber
                : returnRandomGroupNumber();
            // najde skupinu podle ID
            Group = SubjectGroup.SubjectGroups.Find(SG => SG.Id == GroupNumber);

            // pokud je volné místo
            if (Group.canIJoinGroup(groupNumber))
            {
                // přidá studenta to skupiny
                Group?.addToGroup(student);
            }
            else
            {
                // vybere náhodně skupinu
                Group = SubjectGroup.SubjectGroups.Find(SG => SG.Id == returnRandomGroupNumber());
            }

            SubjectMarkList.Add(this);
        }

        /// <summary>
        /// Metoda k vygenerování náhodného čísla, které je validní pro daný předmět.
        /// </summary>
        /// <returns>Náhodně validní číslo skupiny pro daný předmět</returns>
        private int returnRandomGroupNumber()
        {
            Console.WriteLine("Nesprávné číslo skupiny");
            Console.WriteLine("Generuji náhodné číslo");


            List<SubjectGroup> availableSubjectGroups =
                SubjectGroup.returnAvailableSubjectGroups()
                    .FindAll(SG => Subject.Groups.Contains(SG)); // uloží do sezanmu všechny skupony daného předmětu
            List<int> availableIdOfGroups = new List<int>(); // Seznam id dostupných skupin

            // Přidání všech daých id do seznamu
            availableSubjectGroups.ForEach(SG => availableIdOfGroups.Add(SG.Id));

            // vygerenuje náhodné číslo 0 - délka seznamu
            int randomNumber = Random.Shared.Next(0, availableIdOfGroups.Count);
            // vybere id ze seznamu id s indexem náhodného čísla
            int number = availableIdOfGroups[randomNumber];
            Console.WriteLine($"number = {number}");

            return number;
        }

        /// <summary>
        /// Výpis informací o registrrovaném předmětu
        /// </summary>
        public void writeSubjectMarkInfo()
        {
            // výpis informací o předmětu
            Console.WriteLine(
                $"Předmět typu {Subject.Type.Name} s názvem {Subject.Name}" +
                $", za dokončení {Credits} kreditů," +
                $" garantem je {Subject.GarantOfSubject.returnFullName()}," +
                $" Semestr: {Subject.Semester} (Level {Subject.Level})" +
                $", jsi ve skupině {GroupNumber}.");
        }
    }
}