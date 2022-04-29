using System;

namespace UkolZakladyOOP
{
    /// <summary>
    /// Třída osoby
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Jméno
        /// </summary>
        protected string FirstName;

        /// <summary>
        /// Přijmení
        /// </summary>
        protected string LastName;

        /// <summary>
        /// Datum narození
        /// </summary>
        protected DateTime BirthDate;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="firstName">Jméno</param>
        /// <param name="lastName">Přijmení</param>
        /// <param name="birthDate">Datum narození</param>
        protected Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        /// <summary>
        /// Informace o osobě
        /// </summary>
        public virtual void aboutMe()
        {
            Console.WriteLine($"Dobrý den, jmenuji se {returnFullName()}" +
                              $" a narodil/narodila jsem se {BirthDate:MM.dd.yyyy} a jsem pouze obyčejná osoba");
        }

        /// <summary>
        /// Vrací celé jméno
        /// </summary>
        /// <returns>firstname + lastname</returns>
        public string returnFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}