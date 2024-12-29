using System.Collections.Generic;
using System;
public class GenericResourcePool<T> where T : class
{
    public List<PooledObject<T>> PooledItems = new List<PooledObject<T>>();

    public virtual T GetItem<U>() where U : T
    {
        if (PooledItems.Count > 0)
        {
            PooledObject<T> pooledObject = PooledItems.Find(item => !item.IsUsed && item.Item is U);
            if (pooledObject != null)
            {
                pooledObject.IsUsed = true;
                return pooledObject.Item;
            }
        }
        return CreateNewPooledObject<U>();
    }
    public T CreateNewPooledObject<U>() where U : T
    {
        PooledObject<T> pooledObject = new PooledObject<T>();
        pooledObject.Item = CreateItem<U>();
        pooledObject.IsUsed = true;
        PooledItems.Add(pooledObject);
        return pooledObject.Item;
    }
    protected virtual T CreateItem<U>() where U : T
    {
        throw new NotImplementedException("CreateItm() method not implemented.");
    }
    public virtual void ReturnItem(T item)
    {
        PooledObject<T> pooledItem = PooledItems.Find(i => i.Item.Equals(item));
        pooledItem.IsUsed = false;
    }
}
public class PooledObject<T> where T : class
{
    public T Item;
    public bool IsUsed;
}
