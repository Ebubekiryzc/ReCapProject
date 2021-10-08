using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.InMemory
{
    public class InMemoryRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected List<TEntity> Instances;

        public InMemoryRepositoryBase()
        {
            Instances = new List<TEntity>();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? Instances
                : Instances.AsQueryable().Where(filter).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Instances.AsQueryable().SingleOrDefault(filter);
        }

        public void Add(TEntity entity)
        {
            entity.GetType().GetProperty("Id")?.SetValue(entity, (FindMaxId() + 1));
            //Console.WriteLine("Kaydedilen değer " + entity.GetType().GetProperty("Id").GetValue(entity));
            Instances.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var indexOfEntity = FindEntity(entity);
            if (indexOfEntity == -1)
            {
                return;
            }

            foreach (var resultProperty in Instances[indexOfEntity].GetType().GetProperties())
            {
                foreach (var entityProperty in entity.GetType().GetProperties())
                {
                    if (resultProperty.Name == entityProperty.Name)
                    {
                        resultProperty.SetValue(Instances[indexOfEntity], entityProperty.GetValue(entity));
                    }
                }
            }
        }

        public void Delete(TEntity entity)
        {
            var result = FindEntity(entity);
            if (result == -1)
            {
                return;
            }

            Instances.RemoveAt(result);
        }

        private int FindEntity(TEntity entity)
        {
            var entityId = entity.GetType().GetProperty("Id")?.GetValue(entity);
            object? instanceId;
            for (int i = 0; i < Instances.Count; i++)
            {
                instanceId = Instances[i].GetType().GetProperty("Id")?.GetValue(Instances[i]);
                if (instanceId.Equals(entityId))
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindMaxId()
        {
            if (Instances.Count == 0) return 0;

            var result = Instances.Max(i => i.GetType().GetProperty("Id")?.GetValue(i));
            //Console.WriteLine(result.GetType().GetProperty("Id").GetValue(result));
            return (int)result;
        }
    }
}
