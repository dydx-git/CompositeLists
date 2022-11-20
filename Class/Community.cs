using CompositeLists.Class;
using CompositeLists.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeLists
{
    internal class Community
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Persons Members { get; set; } = new Persons();
        public SegeratedPersons MaleMembers 
        {
            get => GetFilteredBySex(Sex.Male);
        }
        public SegeratedPersons FemaleMembers
        {
            get => GetFilteredBySex(Sex.Female);
        }

        private SegeratedPersons GetFilteredBySex(Sex sex)
        {
            var persons = SegeratedPersons.GetInstance(Members, sex);
            return persons.GetFiltered();
        }

        internal static Persons GetSampleMembers()
        {
            return new Persons()
            {
                new Person(23, "Rango", 48, Sex.Male),
                new Person(47, "Iggy", 19, Sex.Female),
                new Person(12, "Norbert", 21, Sex.Male),
                new Person(17, "Spyro", 54, Sex.Female),
                new Person(78, "Haku", 53, Sex.Male),
                new Person(44, "Jade", 33, Sex.Female),
                new Person(56, "Tiberius", 22, Sex.Male),
                new Person(88, "Reptar", 12, Sex.Female),
                new Person(91, "Igor", 100, Sex.Male),
                new Person(26, "Speedy", 24, Sex.Female)
            };
        }
    }
}
