using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed ;
    public float turnSpeed ;
    public Text MoneyCountText ;
    public Animator zombieAnimator;
    private int MoneyCount ;
    public Image Energy ;
    public Text GameOverText;
    public Button goMenuButton;
    public Text YouWinText;
    public Button nextLevelButton;
    public bool gameOver, winLevel;
    public Camera myCam, ATMCam;
    private Transform cameraTransform;
    private RectTransform energyBar;
    private Vector3 lostEnergy = new Vector3(0.001F, 0, 0) * 80;
    private Vector3 newPosition1 = new Vector3(0.0725F, 0, 0) * 80; // Cuando pierde energia
    private Vector3 extraEnergy = new Vector3(0.1F, 0, 0)*2;
    private Vector3 newPosition2= new Vector3(7, 0, 0)*2; // Cuando gana energia
    private int cont;
    public ATMScript ATM;
    public bool winMoney = false ;
    public GameObject myBarEnergy;
    public GameObject myBag;
    public AudioSource angry;
    //private Transform bulletTransform;
    

    void Start()
    {

        YouWinText.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        goMenuButton.gameObject.SetActive(false);
        MoneyCount = 0;
        MoneyCountText.text = MoneyCount.ToString();
        MoneyCountText.fontSize = 30;
        energyBar = Energy.GetComponent<RectTransform>();
        gameOver = false;
        cameraTransform = transform.Find("Main Camera");
        cont = 0;


    }
    void Update()
    {
        if (!gameOver && !winLevel )
        {

            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                zombieAnimator.SetBool("Walk", true);
            }

            else zombieAnimator.SetBool("Walk", false);

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

            if (ATM.endGame)
            {

                myCam.enabled = true;
                myBarEnergy.gameObject.SetActive(true);
                myBag.gameObject.SetActive(true);
                if (winMoney && !ATMCam.enabled)
                {
                    winMoney = false;
                    upMoney();
                }
                
                ATM.endGame = false;
            }
        }
        else 
        {
            if (cont < 100)
            {
                cameraTransform.Translate(-Vector3.forward * 10 * Time.deltaTime);
                cont++;
                zombieAnimator.SetBool("Walk", false);
            }
        }

    }
         

    void OnTriggerEnter(Collider other)
    {
        if (!gameOver)
        {


            if (other.gameObject.CompareTag("Money"))
            {

                other.gameObject.SetActive(false);
                ATMCam.enabled = true;

                myBarEnergy.gameObject.SetActive(false);
                myBag.gameObject.SetActive(false);

                ATM.play();



            }

            if (other.gameObject.CompareTag("Energy"))
            {
                angry.PlayDelayed(0);
                other.gameObject.SetActive(false);
                energyUp();

            }

            if (other.gameObject.CompareTag("Bullet"))
            {
                energyDown();
                angry.PlayDelayed(1);

            }

            if (other.gameObject.CompareTag("ExitWall"))
            {
                angry.PlayDelayed(0);
                winLevel = true;
                nextLevel();

            }

        }
 
    }

    public void quitMoney()
    {
        MoneyCount = MoneyCount - 1;
        MoneyCountText.text = MoneyCount.ToString();
    }

    public bool haveMoney()
    {
        return (MoneyCount > 0);
    }

    public void upMoney()
    {
        MoneyCount = MoneyCount + 1;
        MoneyCountText.text = MoneyCount.ToString();

    }

    public void energyDown()
    {
        if (energyBar.localScale.x - lostEnergy.x < 0)
        {
            energyBar.localScale = new Vector3(0, 0, 0);
            energyBar.localPosition = new Vector3(0,0,0);
 
            zombieAnimator.SetBool("Dead", true);

            gameOver = true;
            loseGame();
            //Game over
        }
        else{
            energyBar.localScale -= lostEnergy;
            energyBar.localPosition -= newPosition1;
        }
        

    }

    public void energyUp()
    {
        if (energyBar.localScale.x + extraEnergy.x > 1)
        {
            energyBar.localScale = new Vector3(1, 1, 1);
            energyBar.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            energyBar.localScale += extraEnergy;
            energyBar.localPosition += newPosition2;
        }

    }

    public void loseGame()
    {
        GameOverText.gameObject.SetActive(true);
 
        goMenuButton.gameObject.SetActive(true);
    }

    public void goMenu()
    {
        SceneManager.LoadScene(0);
    }

    void nextLevel()
    {
        YouWinText.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);

    }
}