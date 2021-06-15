using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public int ammo;
    public float rof;

    public LineRenderer line;

    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = -rof;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 point, Vector2 rot)
    {
        if (Time.timeSinceLevelLoad - nextFire >= rof && ammo > 0)
        {
            ammo -= 1;
            StartCoroutine(Shoot(point, rot));
            nextFire = Time.timeSinceLevelLoad;
        }
    }

    IEnumerator Shoot(Vector3 point, Vector2 rot)
    {
        Vector2 start = new Vector2(point.x, point.y);
        RaycastHit2D hitInfo = Physics2D.Raycast(start, rot);

        if(hitInfo)
        {
            line.SetPosition(0, start);
            line.SetPosition(1, rot);
        }
        else
        {
            line.SetPosition(0, start);
            line.SetPosition(1, start + rot * 100);
        }

        line.enabled = true;
        yield return new WaitForSeconds(0.02f);
        line.enabled = false;


    }
}
