﻿using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.DataAccess.Interfaces;

public interface IRepository<T>
{
    public List<T> GetAll();
    public T? Get(int id);
    public T Add(T model);

    public void Delete(int id);

    public T? Update(int id, T model);
}