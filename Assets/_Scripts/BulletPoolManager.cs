using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    private static BulletPoolManager s_instance;
    public int MaxBullets = 20;

    //TODO: create a structure to contain a collection of bullets
    private Queue<GameObject> BulletPool;

    public static BulletPoolManager InstanceSingleton()
    {
        if(s_instance == null)
        {
            s_instance = GameObject.FindWithTag("Manager").GetComponent<BulletPoolManager>();
            //s_instance.MaxBullets = GameObject.FindWithTag("Manager").GetComponent<BulletPoolManager>().MaxBullets;
        }
        return s_instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        BulletPoolManager.InstanceSingleton();
        BulletPoolManager.InstanceSingleton().BulletPool = new Queue<GameObject>();

        // TODO: add a series of bullets to the Bullet Pool
        for (int i = 0; i < MaxBullets; i++)
        {
            //Debug.Log("hello");
            GameObject temp = bullet;
            temp.SetActive(true);
            temp.GetComponent<BulletController>().s_poolmanager = BulletPoolManager.InstanceSingleton();
            Instantiate(temp);
        }
        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int x = 0; x < bullets.Length; x++)
        {
            BulletPoolManager.InstanceSingleton().BulletPool.Enqueue(bullets[x]);
            bullets[x].transform.SetParent(BulletPoolManager.InstanceSingleton().gameObject.transform);
            //Debug.Log("hello");
        }
    }

    // Update is called once per frame
    public int BulletPoolSize()
    {
        return BulletPoolManager.InstanceSingleton().BulletPool.Count;
    }

    public bool BulletPoolEmpty()
    {
        if (BulletPoolManager.InstanceSingleton().BulletPool.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet()
    {
        GameObject obj_return = BulletPoolManager.InstanceSingleton().BulletPool.Dequeue();
        

        if (BulletPoolManager.InstanceSingleton().BulletPool.Count <= 1)
        {
            BulletPoolManager.InstanceSingleton().BulletPool.Enqueue(bullet);
        }
        else if(BulletPoolManager.InstanceSingleton().BulletPool.Count >= MaxBullets)
        {
            BulletPoolManager.InstanceSingleton().BulletPool.Dequeue();
        }
        obj_return.SetActive(true);

        return obj_return;
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {

        bullet.SetActive(false);
        //bullet.transform.position = new Vector3(-10, 0, 0);
        BulletPoolManager.InstanceSingleton().BulletPool.Enqueue(bullet);
    }
}
