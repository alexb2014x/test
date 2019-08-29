using UnityEngine;

public class Bonus : MonoBehaviour {

    // Типы бонусов
    const int health = 0, speed = 1;

    public int objType;

    int bonusQuantity;


    void Start ()
    {
		switch(objType)
        {
            case health:
                bonusQuantity = Random.Range(10, 20);
                break;

            case speed:
                bonusQuantity = Random.Range(2, 3);
                break;

            default:
                bonusQuantity = 0;
                break;
        }
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "PlayerBullet" || collider.gameObject.tag == "PlayerShip")
        {
            switch (objType)
            {
                case health:
                    GameObject.Find("playerShip").GetComponent<Moving>().health += bonusQuantity;
                    GameObject.Find("Background").GetComponent<Main>().updateTexts();
                    break;

                case speed:
                    GameObject.Find("playerShip").GetComponent<Ship>().initPlayerShipDT();
                    GameObject.Find("playerShip").GetComponent<Ship>().deltaStrikeTime /= bonusQuantity;
                    GameObject.Find("playerShip").GetComponent<Ship>().bonusSpeedStartTime = Time.time;
                    break;                
            }

            Destroy(gameObject);
        }
    }
}
