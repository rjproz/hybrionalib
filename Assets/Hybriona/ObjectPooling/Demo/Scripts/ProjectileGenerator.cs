using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hybriona;
public class ProjectileGenerator : MonoBehaviour
{
    public Projectile sourceCube;
    public Projectile sourceSphere;
    public Transform throwPoint;
    public float throwForceMultiplier = 10;

    public enum ObjectPoolId { Cube = 0, Sphere = 1 };
    void Start()
    {
        Application.targetFrameRate = 60;
        //sourceCube.gameObject.SetActive(false);
        //sourceSphere.gameObject.SetActive(false);

        ObjectPoolingManager.Register((int)ObjectPoolId.Cube, sourceCube,20);
        ObjectPoolingManager.Register((int)ObjectPoolId.Sphere, sourceSphere,20);

        StartCoroutine(GenerationProcess());
    }

    private IEnumerator GenerationProcess()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true)
        {
            Projectile projectile = null;
            bool createCube = Random.Range(0, 2) == 0 ? true : false;
            
            if (createCube)
            {

                projectile = (Projectile) ObjectPoolingManager.Fetch((int)ObjectPoolId.Cube);
            }
            else
            {
                projectile = (Projectile)ObjectPoolingManager.Fetch((int)ObjectPoolId.Sphere);
            }
            
            projectile.transform.position = throwPoint.position;
            projectile.ActivateFromPool();
            projectile.rigidbody.AddForce(new Vector3(Random.Range(-20,20),50,Random.Range(-20,20)) * throwForceMultiplier,ForceMode.Acceleration);
            yield return wait;
        }
    }


    private void OnDestroy()
    {
        ObjectPoolingManager.CleanAll();
    }
}
