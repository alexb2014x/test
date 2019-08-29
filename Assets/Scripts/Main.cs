using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    public Camera mainCamera;
    public Vector2 eastNorthPoint;

    public GameObject menu;
    public Text menuLoseText;
    public Button[] menuResumeButtons;

    public GameObject playerShip;
    public GameObject bullet;
    public GameObject[] enemy;
    public GameObject[] bonus;

    float playerSpeedShip = 10f;
    float playerAccuracyX = 0.1f; 
    float nextPlayerShipX = 0;

    float lastInstantiateTime = 0f;

    public int points = 0;
    public Text textPoints;
    public Text textHealth;


    void Start ()
    {
        menuLoseText.gameObject.SetActive(false);
        menu.SetActive(false);
        menu.transform.position = Vector2.zero;

        eastNorthPoint = new Vector2(mainCamera.orthographicSize * mainCamera.aspect - 0.5f, mainCamera.orthographicSize);

        updateTexts();
    }


    void Update ()
    {
        if (Time.time - lastInstantiateTime > 2f)
        {
            GameObject newEnemy = Instantiate(enemy[Random.Range(0, enemy.Length)],
                                             new Vector2(Random.Range(-eastNorthPoint.x, eastNorthPoint.x),
                                                         eastNorthPoint.y + 0.5f), Quaternion.identity) as GameObject;

            if (Random.Range(0f, 1f) > 0.8f)
            {
                GameObject newBonus = Instantiate(bonus[Random.Range(0, bonus.Length)],
                                                 new Vector2(Random.Range(-eastNorthPoint.x, eastNorthPoint.x),
                                                             eastNorthPoint.y + 0.5f), Quaternion.identity) as GameObject;
            }

            lastInstantiateTime = Time.time;
        }


        if (Input.GetMouseButtonDown(0) && !menu.activeSelf && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 0)
        {
            nextPlayerShipX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

            if (nextPlayerShipX - eastNorthPoint.x > 0)
                nextPlayerShipX = eastNorthPoint.x;
            else 
                if (nextPlayerShipX + eastNorthPoint.x < 0)
                nextPlayerShipX = -eastNorthPoint.x;
        }


        if (Mathf.Abs(nextPlayerShipX - playerShip.transform.position.x) > playerAccuracyX)       
            playerShip.GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeedShip * Mathf.Sign(nextPlayerShipX - playerShip.transform.position.x), 0);     
        else      
            playerShip.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        

        checkObjects2Destroy("PlayerBullet");
        checkObjects2Destroy("Asteroid");
        checkObjects2Destroy("EnemyBullet");
        checkObjects2Destroy("EnemyShip");
        checkObjects2Destroy("BonusHealth");
        checkObjects2Destroy("BonusSpeed");
    }


    void checkObjects2Destroy(string tag)
    {
        GameObject[] obj2Destroy = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < obj2Destroy.Length; i++)
            if (obj2Destroy[i].transform.position.x - eastNorthPoint.x > 1f ||
                obj2Destroy[i].transform.position.x + eastNorthPoint.x < -1f ||
                obj2Destroy[i].transform.position.y - eastNorthPoint.y > 2f ||
                obj2Destroy[i].transform.position.y + eastNorthPoint.y < -2f)
                Destroy(obj2Destroy[i]);
    }


    public void updateTexts()
    {
        textPoints.text = points + "";
        textHealth.text = playerShip.GetComponent<Moving>().health + "";
    }


    public void endGame()
    {
        pauseButton();
        menuLoseText.gameObject.SetActive(true);
        for (int i = 0; i < menuResumeButtons.Length; i++)
            menuResumeButtons[i].interactable = false;
    }


    public void pauseButton()
    {
        if (!menu.activeSelf)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menu.SetActive(false);
        }
    }

    public void retryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void exitButton()
    {
        Application.Quit();
    }

}

