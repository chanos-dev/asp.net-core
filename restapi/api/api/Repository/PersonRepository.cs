using System.Threading.Channels;
using api.Model;

namespace api.Repository;

public class PersonRepository : IPersonRepository
{
    private static List<Person> MemoryStorage { get; set; } = new()
    {
        new()
        {
            Id = 1,
            Age = 20,
            Name = "홍길동",
        },
        new ()
        {
            Id = 2,
            Age = 22,
            Name = "둘리",
        } 
    };

    public IEnumerable<Person> GetAll() => MemoryStorage;

    public Person Get(int id) => MemoryStorage.Find(p => p.Id == id);

    public Person Add(Person person)
    {
        MemoryStorage.Add((person));
        return person;
    }

    public bool Delete(Person person) => MemoryStorage.Remove(person);
    public bool Update(Person person)
    {
        var idx = MemoryStorage.FindIndex(p => p.Id == person.Id);
        if (idx < 0)
            return false;
        
        MemoryStorage[idx] = person;

        return true;
    }
}