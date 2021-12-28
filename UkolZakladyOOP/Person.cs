using System;

namespace UkolZakladyOOP
{
    public class Person
    {
        public int id;
        public string firstName;
        public string lastName;
        public DateTime birthDate;

        public Person(int id, string firstName, string lastName, DateTime birthDate)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public virtual void aboutMe()
        {
            Console.WriteLine("Dobrý den, jmenuji se {0} a narodil/narodila jsem se {1} a jsem pouze obyčejná osoba",
                returnFullName(), birthDate.ToString("MM.dd.yyyy"));
        }

        public string returnFullName()
        {
            return firstName + " " + lastName;
        }
    }
}