using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Advanced_Programming
{
    internal abstract class Person
    {
        public abstract PersonPosition Position { get; }
        protected string ID { get; set; }
        protected string Name { get; set; }
        protected DateTime DateOfBirth { get; set; }
        protected string Email { get; set; }
        protected string Address { get; set; }
        protected string Division { get; set; }

        public string SetID(string value) => ID = value;
        public string SetName(string value) => Name = value;
        public DateTime SetDateOfBirth(DateTime value) => DateOfBirth = value;
        public string SetEmail(string value) => Email = value;
        public string SetAddress(string value) => Address = value;
        public string SetDivision(string value) => Division = value;

        public string GetID() => ID;
        
        public string GetName() => Name;
        
        public Person(string[] inputFields)
        {
            ID = inputFields[0];
            Name = inputFields[1];
            DateOfBirth = DateTime.Parse(inputFields[2], CultureInfo.CreateSpecificCulture("de-DE"));
            Email = inputFields[3];
            Address = inputFields[4];
            Division = inputFields[5];
        }

        public string DiplayPerson()
        {
            var typeOfDivision = GetType().Name == "Student" ? "CLASS" : "DEPARTMENT";
            return
                "ID: " + ID +
                " | NAME: " + Name +
                " | DATE OF BIRTH: " + DateOfBirth.ToString("dd-MM-yyyy") +
                " | EMAIL: " + Email +
                " | ADDRESS: " + Address +
                " | " + typeOfDivision + ": " + Division;
        }
    }

    public enum PersonPosition
    {
        Student,
        Lecturer
    }

    internal class Student : Person
    {
        public Student(string[] inputFields) : base(inputFields)
        {
        }

        public override PersonPosition Position { get => PersonPosition.Student; }
    }

    internal class Lecturer : Person
    {
        public Lecturer(string[] inputFields) : base(inputFields)
        {
        }

        public override PersonPosition Position { get => PersonPosition.Lecturer; }
    }
}