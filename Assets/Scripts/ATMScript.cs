using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ATMScript : MonoBehaviour {


    public Text Operation;
    public InputField answerField;
    public RectTransform timeBar;
    public Camera ATMCamera;
    public PlayerController player;
    public bool endGame = false ;
    private string answer;
    private int n, x = 0, y = 0, z = 0;
    private string op;
    private Vector3 timeDown = new Vector3(2, 0, 0) ;
    private Vector3 size;


    void Start()
    {
        size = timeBar.localScale;
    }
    void Update()
    {
        if (ATMCamera.enabled){

            if (timeBar.localScale.x > 0){
                timeBar.localScale -= timeDown * Time.deltaTime;

            answer = answerField.text;
            
            if (answer == n.ToString())
                {
                player.winMoney = true;
                endGame = true;
                ATMCamera.enabled = false;
                }
            }
            else{
                player.winMoney= false;
                endGame = true;
                ATMCamera.enabled = false;
            }
       
        }
    }


    public void play()

    {
        timeBar.localScale = size;
        answerField.text = "";
        x = Random.Range(0, 2);
        n = Random.Range(1, 9);

        if (x == 0)
        {
            y = Random.Range(10, 100);
            z = n + y;
            op = " - ";
        }
        else if (x == 1)
        {
            y = Random.Range(10, 100);
            z = n - y;
            op = " + ";
        }

        else if (x == 2)
        {
            y = Random.Range(2, 9);
            z = n * y;
            op = " / ";
        }


        Operation.text = z.ToString() + op + y.ToString() + " = ?" ;




    }

}