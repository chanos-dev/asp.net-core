﻿using api.Model;

namespace api.Repository;

public interface IPersonRepository
{
    IEnumerable<Person> GetAll();
    Person Get(int id);
    Person Add(Person person);
}