using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hybriona
{
    public class BasePoolBehavior : MonoBehaviour
    {

        public int objectId { get; set; }


        public virtual void ActivateFromPool()
        {

        }

        public virtual void OnInstantiated()
        {

        }

        public virtual void OnFetchedFromPool()
        {

        }

        public virtual void OnReturnedToPool()
        {

        }

        public virtual void OnSourceRegistered()
        {

        }
    }
}
