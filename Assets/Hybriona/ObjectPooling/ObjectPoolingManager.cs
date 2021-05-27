using System.Collections.Generic;
using UnityEngine;
namespace Hybriona
{
    public class ObjectPoolingManager
    {
        private static Dictionary<int, ObjectPool> poolCollection = new Dictionary<int, ObjectPool>();

        public static void Register(int objectId, BasePoolBehavior source, int preWarmCache = 0)
        {
            if(!poolCollection.ContainsKey(objectId))
            {
                poolCollection.Add(objectId, new ObjectPool(objectId, source, preWarmCache));
            }
            else
            {
                throw new System.Exception(string.Format("Specified object id {0} already registered!", objectId));
            }
        }

        public static void UnRegister(int objectId)
        {
            if (poolCollection.ContainsKey(objectId))
            {
                poolCollection.Remove(objectId);
            }
            else
            {
                throw new System.Exception(string.Format("Specified object id {0} doesn't exist!", objectId));
            }
        }


        public static BasePoolBehavior Fetch(int objectId)
        {
            if (poolCollection.ContainsKey(objectId))
            {
                return poolCollection[objectId].Fetch();
            }
            else
            {
                throw new System.Exception(string.Format("Specified object id {0} doesn't exist!", objectId));
            }
        }

        public static void Return(BasePoolBehavior basePool)
        {
            if (poolCollection.ContainsKey(basePool.objectId))
            {
                poolCollection[basePool.objectId].Return(basePool);
            }
            else
            {
                throw new System.Exception(string.Format("Specified object id {0} doesn't exist!", basePool.objectId));
            }
        }

        public static void CleanAll()
        {
            foreach(var poolData in poolCollection)
            {
                poolData.Value.Clean();
            }
            poolCollection.Clear();
        }
    }

    public class ObjectPool
    {
        private int objectId;
        private BasePoolBehavior source;

        private Queue<BasePoolBehavior> pool = new Queue<BasePoolBehavior>();

        public ObjectPool(int objectId, BasePoolBehavior source, int preWarmCache = 0)
        {
            this.objectId = objectId;
            this.source = source;
            this.source.OnSourceRegistered();
            for (int i=0;i<preWarmCache;i++)
            {
                GameObject dup = GameObject.Instantiate(this.source.gameObject,this.source.transform.parent,true);
                BasePoolBehavior script = dup.GetComponent<BasePoolBehavior>();
                script.objectId = this.objectId;
                script.OnInstantiated();
                script.OnReturnedToPool();
                pool.Enqueue(script);
            }
        }

        public BasePoolBehavior Fetch()
        {
            if(pool.Count == 0)
            {
                GameObject dup = GameObject.Instantiate(this.source.gameObject, this.source.transform.parent, true);
                BasePoolBehavior script = dup.GetComponent<BasePoolBehavior>();
                script.objectId = this.objectId;
                script.OnInstantiated();
                //script.name = source.name + "_Instantiated";
                return script;
            }
            else
            {
                BasePoolBehavior script = pool.Dequeue();
                script.OnFetchedFromPool();
                //script.name = source.name + "_FromPool";
                return script;
            }
        }

        public void Return(BasePoolBehavior basePool)
        {
            basePool.OnReturnedToPool();
            pool.Enqueue(basePool);
        }

        public void Clean()
        {
            source = null;
            pool.Clear();
        }
    }
}
