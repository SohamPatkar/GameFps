using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootingPlace;

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && PlayerMovement.Instance.isPickedCheck())
        {
            RaycastHit hit;
            if (Physics.Raycast(shootingPlace.transform.position, shootingPlace.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Instantiate(bulletPrefab, hit.point, Quaternion.identity);
                bulletPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }
}
