using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FacetedBuilder
{
    public class Person
    {
        //adress
        public string StreetAddress, Postcode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder //facade
    {
        //reference!
        protected Person Person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(Person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.Person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            Person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            Person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            Person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            Person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            Person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            Person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            Person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            Person.AnnualIncome = amount;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("123 rue C#")
                      .In("Paris")
                .Works.At("Nike")
                      .AsA("Engineer")
                      .Earning(150);

            WriteLine(person);
            Read();
        }
    }
}
