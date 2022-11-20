using CompositeLists.Enum;

namespace CompositeLists.Class
{
    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Unknown";
        public int Age { get; set; }
        public Sex Sex { get; set; }

        public Person(int id, string name, int age, Sex sex)
        {
            Id = id;
            Name = name;
            Age = age;
            Sex = sex;
        }

        public override string ToString() => $"Name: {Name}, Age: {Age}, Sex: {Sex}";
    }

    internal class Persons : List<Person>
    {
        public override string ToString()
        {
            var result = string.Empty;
            foreach (var person in this)
            {
                result += (person + "\n\r");
            }
            return result;
        }
    }

    internal class SegeratedPersons : Persons
    {
        private static SegeratedPersons? _instance = null;
        private static readonly object Padlock = new object();
        private static Persons _baseList = new();
        private static Sex _sex;

        private SegeratedPersons(Persons persons, Sex sex)
        {
            _baseList = persons;
            _sex = sex;
        }

        public static SegeratedPersons GetInstance(Persons persons, Sex sex)
        {
            // Thread-safe singleton
            lock (Padlock)
            {
                _instance ??= new SegeratedPersons(persons, sex);
                _sex = sex;
                _baseList = persons;
                return _instance;
            }
        }

        public new void Add(Person person)
        {
            if (person == null)
                return;
            if (!IsCompatible(person))
                ThrowIncompatibleException();
            else
                _baseList.Add(person);
        }

        public new Person this[int index]
        {
            get => _baseList[index];
            set
            {
                var selected = base[index];
                int indexInBase = _baseList.IndexOf(selected);
                _baseList[indexInBase] = value;
            }
        }

        public new void Clear()
        {
            var itemsToRemove = _baseList.Where(IsCompatible);
            itemsToRemove.ToList().ForEach(Remove);
        }

        public SegeratedPersons GetFiltered()
        {
            base.Clear();
            AddPersons(_sex);
            return this;
        }

        private void AddPersons(Sex sex)
        {
            foreach (var person in _baseList.Where(person =>
                         person.Sex == sex))
                base.Add(person);
        }

        internal new void Remove(Person person) => _baseList.Remove(person);

        private void ThrowIncompatibleException() => throw new InvalidOperationException($"Trying to add incompatible {_sex} to list");

        private bool IsCompatible(Person person) => person.Sex == _sex;
    }
}
