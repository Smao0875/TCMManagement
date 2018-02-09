using System.Collections.Generic;

namespace TCMManagement.BusinessLayer
{
    interface IEntityServices<T>
    {
        int CreateItem(T item);
        T GetItemById(int id);
        T SearchItemByName(string s);
        IEnumerable<T> GetAllItems();
        bool UpdateItem(int id, T item);
        bool DeleteItem(int id);
    }
}
