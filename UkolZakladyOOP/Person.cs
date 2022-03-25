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
        public string firstName;
        /// <summary>
        /// Přijmení
        /// </summary>
        public string lastName;
        /// <summary>
        /// Datum narození
        /// </summary>
        public DateTime birthDate;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="firstName">Jméno</param>
        /// <param name="lastName">Přijmení</param>
        /// <param name="birthDate">Datum narození</param>
        public Person(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        /// <summary>
        /// Informace o osobě
        /// </summary>
        public virtual void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} a narodil/narodila jsem se {1} a jsem pouze obyčejná osoba",
                returnFullName(), birthDate.ToString("MM.dd.yyyy"));
        }

        /// <summary>
        /// Vrací celé jméno
        /// </summary>
        /// <returns>firstname + lastname</returns>
        public string returnFullName()
        {
            return firstName + " " + lastName;
        }
    }
}