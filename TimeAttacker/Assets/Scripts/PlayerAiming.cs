using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Camera cam;
    public LayerMask wepLayer;
    public GameObject firingPoint;
    Vector2 mousePos;

    public delegate void FiringDelegate(Vector3 point, Vector2 rot);
    FiringDelegate firingMethod;
    public GameObject weapon;

    bool firing;
    bool switchWeapon;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform gun in transform)
        {
            if (gun.tag == "Weapon")
            { 
                weapon = gun.gameObject;
                firingMethod = weapon.gameObject.GetComponent<TestGun>().Fire;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        firing = Input.GetMouseButton(0);
        switchWeapon = Input.GetKeyDown(KeyCode.F);
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if(firing)
        {
            firingMethod(firingPoint.transform.position, lookDir);
        }
        if(switchWeapon)
        {
            Switch();
        }
    }

    void Switch()
    {
        GameObject closest = null;
        float closestDist = 5.0f;
        float dist;
        Collider2D[] weapons = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.2f, wepLayer);

        foreach(Collider2D w in weapons)
        {
            if (w.gameObject != weapon)
            {
                dist = Mathf.Pow(Mathf.Pow(Mathf.Abs(transform.position.x - w.transform.position.x), 2) + Mathf.Pow(Mathf.Abs(transform.position.y - w.transform.position.y), 2), 0.5f);
                if (dist < closestDist)
                {
                    closest = w.gameObject;
                }
            }
        }

        if (closest != null)
        {
            weapon.transform.SetParent(null);

            closest.transform.position = transform.position;
            closest.transform.rotation = transform.rotation;
            closest.transform.SetParent(transform);

            weapon = closest.gameObject;
            firingMethod = weapon.gameObject.GetComponent<TestGun>().Fire;

        }
    }
}
