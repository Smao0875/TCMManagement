using System;
using System.Collections.Generic;
using static TCMManagement.BusinessLayer.Constants;

namespace TCMManagement.BusinessLayer
{
    public interface IEntityServices<T> : IDisposable
        where T : class
    {
        T CreateItem(T item);
        T GetItemById(int id);
        T SearchItem(string s);
        IEnumerable<T> GetItems(IEnumerable<KeyValuePair<string, string>> queryParams);
        bool UpdateItem(int id, T item);
        bool DeleteItem(int id);

        int SaveChanges();
        void MarkAsModified(T item);
    }
}
