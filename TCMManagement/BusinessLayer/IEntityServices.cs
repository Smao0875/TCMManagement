using System;
using System.Collections.Generic;

namespace TCMManagement.BusinessLayer
{
    public interface IEntityServices<T> : IDisposable
        where T : class
    {
        T CreateItem(T item);
        T GetItemById(int id);
        T SearchItemByName(string s);
        IEnumerable<T> GetAllItems();
        bool UpdateItem(int id, T item);
        bool DeleteItem(int id);

        int SaveChanges();
        void MarkAsModified(T item);
    }
}
