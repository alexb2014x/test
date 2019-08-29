using UnityEngine;

public class Moving : MonoBehaviour {

    // Типы объектов
    public const int asteroid = 0, playerBullet = 1, playerShip = 2, enemyBullet = 3, enemyShip_1 = 4, enemyShip_2 = 5, enemyShip_3 = 6;

    public int objType;

    public int health;

    public Vector2 speed;


    public void Start ()
    {
        switch(objType)
        {
            case asteroid:
                speed = new Vector2(0, Random.Range(-0.5f, -5f));
                health = 1;
                transform.Rotate(new Vector3(0, 0, Random.Range(0, 360f)));
                break;

            case playerBullet:
                speed = new Vector2(0, 5f);
                health = 1;
                break;

            case playerShip:
                health = 100;
                break;

            case enemyBullet:
                float maxSpeed = 10f;

                speed = new Vector2(-maxSpeed * Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z), 
                                     maxSpeed * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z));
                health = 1;
                break;

            case enemyShip_1:
                speed = new Vector2(0, -1.5f);
                health = 10;
                transform.Rotate(new Vector3(0, 0, 180f));
                break;

            case enemyShip_2:
                speed = new Vector2(0, -1f);
                health = 20;
                transform.Rotate(new Vector3(0, 0, 180f));
                break;

            case enemyShip_3:
                health = 30;
                transform.Rotate(new Vector3(0, 0, 180f));
                break;
        }

    }


    public void Update ()
    {
        if (objType != playerShip && Time.timeScale > 0)
        {
            GetComponent<Rigidbody2D>().velocity = speed; 


            if (objType == enemyShip_2)
            {
                speed = new Vector2(speed.y * Mathf.Sin(5f * Time.time), speed.y);
            }

            if (objType == enemyShip_3)
            {
                speed = new Vector2(0, -3.5f * (1 + Mathf.Sin(6f * Time.time) + 0.3f * Mathf.Sin(24f * Time.time)));
            }

        }
	}


    void OnCollisionEnter2D(Collision2D collider)
    {
        if (objType == playerShip)
        {
            if (!(collider.gameObject.tag == "PlayerBullet" || collider.gameObject.tag == "BonusHealth" || collider.gameObject.tag == "BonusSpeed"))
                health -= 5;

            GameObject.Find("Background").GetComponent<Main>().updateTexts();

            if (health <= 0)
            {
                GameObject.Find("Background").GetComponent<Main>().endGame();
            }
        }
        else
        {
            health -= 5;

            if (health <= 0)
            {
                if ((objType == asteroid || objType == enemyShip_1 || objType == enemyShip_2 || objType == enemyShip_3)
                    && collider.gameObject.tag == "PlayerBullet")
                {
                    int plusPoints;
                    switch (objType)
                    {
                        case asteroid:
                            plusPoints = 1;
                            break;

                        case enemyShip_1:
                            plusPoints = 2;
                            break;

                        case enemyShip_2:
                            plusPoints = 3;
                            break;

                        case enemyShip_3:
                            plusPoints = 5;
                            break;

                        default:
                            plusPoints = 0;
                            break;
                    }

                    GameObject.Find("Background").GetComponent<Main>().points += plusPoints;
                    GameObject.Find("Background").GetComponent<Main>().updateTexts();
                }

                Destroy(gameObject);
            }
        }       
    }
}
