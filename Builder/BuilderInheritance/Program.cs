using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BuilderInheritance
{
    public class Person
    {
        public string Name;

        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {
        }

        public static Builder New => new Builder();

        public Person()
        {

        }

        public Person(string name, string position)
        {
            Name = name;
            Position = position;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    //class Foo : Bar<Foo>
    public class PersonInfoBuilder<SELF> 
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder<SELF> 
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var str = Person.New
            .Called("Foo")
            .WorksAsA("Bar")
            .Build();
            WriteLine(str);
            Read();
        }
    }
}
