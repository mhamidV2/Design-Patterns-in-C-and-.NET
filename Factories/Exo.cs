using System;

public class Class1
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int compteur = 0;

        public Person CreatePerson(string personName)
        {
            return new Person
            {
                Id = compteur++,
                Name = personName
            };
        }
    }
}
