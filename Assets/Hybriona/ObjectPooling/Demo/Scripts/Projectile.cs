using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hybriona;
public class Projectile : BasePoolBehavior
{
    public float life = 5;
    public Rigidbody rigidbody;


    public override void ActivateFromPool()
    {
        rigidbody.isKinematic = false;
        StopAllCoroutines();
        //gameObject.SetActive(true);
        StartCoroutine(LifeCycle());
    }

    public override void OnReturnedToPool()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.isKinematic = true;
        transform.position = Vector3.down * 500f;
        //gameObject.SetActive(false);
    }

    public override void OnInstantiated()
    {
        gameObject.SetActive(true);
    }
    public override void OnSourceRegistered()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LifeCycle ()
    {
        float timer = 0;
        while(timer <= life)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //GameObject.Destroy(gameObject);
        ObjectPoolingManager.Return(this);
    }


}
