using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TCMManagement.BusinessLayer
{
    public static class Utils
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) 
        {
            return enumerable == null || !enumerable.Any();
        }

        private static Dictionary<Type, EntitySetBase> _mappingCache = new Dictionary<Type, EntitySetBase>();

#region Soft delete code
        // Borrowed from :
        // https://putshello.wordpress.com/2014/08/20/entity-framework-soft-deletes-are-easy/
        // We should also consier:
        // https://www.codeguru.com/csharp/csharp/soft-deleting-entities-cleanly-using-entity-framework-6-interceptors.html
        public static void SoftDeleteEntry(DbContext context) 
        { 
            foreach(var entry in context.ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted)){
                Type entryEntityType = entry.Entity.GetType();

                string tableName = GetTableName(entryEntityType, context);
                string primaryKeyName = GetPrimaryKeyName(entryEntityType, context);

                string sql = string.Format(
                                        "UPDATE {0} SET IsDeleted = 1 WHERE {1} = @id",
                                            tableName, primaryKeyName);

                context.Database.ExecuteSqlCommand(
                    sql,
                    new SqlParameter("@id", entry.OriginalValues[primaryKeyName]));

                // prevent hard delete            
                entry.State = EntityState.Detached;
            }
        }

        private static string GetTableName(Type type, DbContext context)
        {
            EntitySetBase es = GetEntitySet(type, context);

            return string.Format("[{0}].[{1}]",
                es.MetadataProperties["Schema"].Value,
                es.MetadataProperties["Table"].Value);
        }

        private static string GetPrimaryKeyName(Type type, DbContext context)
        {
            EntitySetBase es = GetEntitySet(type, context);

            return es.ElementType.KeyMembers[0].Name;
        }

        private static EntitySetBase GetEntitySet(Type type, DbContext context)
        {
            if (!_mappingCache.ContainsKey(type))
            {
                ObjectContext octx = ((IObjectContextAdapter)context).ObjectContext;

                string typeName = ObjectContext.GetObjectType(type).Name;

                var es = octx.MetadataWorkspace
                                .GetItemCollection(DataSpace.SSpace)
                                .GetItems<EntityContainer>()
                                .SelectMany(c => c.BaseEntitySets
                                                .Where(e => e.Name == typeName))
                                .FirstOrDefault();

                if (es == null)
                    throw new ArgumentException("Entity type not found in GetTableName", typeName);

                _mappingCache.Add(type, es);
            }

            return _mappingCache[type];
        }
#endregion
    }
}