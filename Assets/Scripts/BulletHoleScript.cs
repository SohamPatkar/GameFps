using System.Collections;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        bulletHoleSpawner();
    }

    void bulletHoleSpawner()
    {
        StartCoroutine(BulletDestroyer());
    }

    IEnumerator BulletDestroyer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
