using UnityEngine;

public class Ship : Moving {

    public GameObject bullet;

    public float lastStrikeTime = 0;
    public float deltaStrikeTime;

    public float bonusSpeedStartTime = 0;
    float bonusSpeedDeltaTime = 0;

    public float yDownLimit;


    new void Start()
    {
        base.Start();

        // Положение прекращения стрельбы
        yDownLimit = GameObject.Find("playerShip").transform.position.y - 0.1f;


        switch (objType)
        {
            case playerShip:
                initPlayerShipDT();
                bonusSpeedDeltaTime = 5f;
                break;

            case enemyShip_1:
                deltaStrikeTime = 0.8f;
                break;

            case enemyShip_2:
                deltaStrikeTime = 1.2f;
                break;

            case enemyShip_3:
                deltaStrikeTime = 1.5f;
                break;
        }
    }


    new void Update()
    {
        base.Update();

        if (Time.time - lastStrikeTime > deltaStrikeTime && transform.position.y > yDownLimit)
        {
            switch (objType)
            {
                case playerShip:
                    GameObject bullet0 = Instantiate(bullet,
                                         new Vector2(transform.position.x - 0.1f,
                                                     transform.position.y + 0.1f), Quaternion.identity) as GameObject;

                    GameObject bullet1 = Instantiate(bullet,
                                                     new Vector2(transform.position.x + 0.1f,
                                                                 transform.position.y + 0.1f), Quaternion.identity) as GameObject;
                    lastStrikeTime = Time.time;
                    break;


                case enemyShip_1:
                    GameObject bullet2 = Instantiate(bullet,
                                         new Vector2(transform.position.x,
                                                     transform.position.y - 0.3f), Quaternion.Euler(0, 0, 180f)) as GameObject;
                    lastStrikeTime = Time.time;
                    break;
            }
        }

        if (bonusSpeedStartTime > 0 && Time.time - bonusSpeedStartTime > bonusSpeedDeltaTime)
        {
            Start();
            bonusSpeedStartTime = 0;
        }
    }


    public void initPlayerShipDT()
    {
        deltaStrikeTime = 0.4f;
    }

}
