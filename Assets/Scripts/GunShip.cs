using UnityEngine;

public class GunShip : Ship {

    public GameObject[] guns;

    new void Update()
    {
        base.Update();


        float angle = Mathf.Atan2(transform.position.y - GameObject.Find("playerShip").transform.position.y,
                                      transform.position.x - GameObject.Find("playerShip").transform.position.x);

        for (int i = 0; i < guns.Length; ++i)
            guns[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * angle + 90));


        if (Time.time - lastStrikeTime > deltaStrikeTime && transform.position.y > yDownLimit)
        {
            GameObject[] newBullets = new GameObject[guns.Length];

            for (int i = 0; i < newBullets.Length; ++i)
            {
                newBullets[i] = Instantiate(bullet,
                    new Vector2(guns[i].transform.position.x - 0.3f * Mathf.Sin(Mathf.Deg2Rad * guns[i].transform.rotation.eulerAngles.z), 
                    guns[i].transform.position.y + 0.3f * Mathf.Cos(Mathf.Deg2Rad * guns[i].transform.rotation.eulerAngles.z)),
                    guns[i].transform.rotation) as GameObject;
            }

            lastStrikeTime = Time.time;
        }             
    }
}
