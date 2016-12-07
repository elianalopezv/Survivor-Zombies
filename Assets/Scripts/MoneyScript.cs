using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyScript : MonoBehaviour {

    public PlayerController player;
    public float humanSpeed;
    public Transform humanTransform;
    private HumanController[] human;
    public Button moneyBag;
    public bool onFloor = false;

	void Start () {
        moneyBag = moneyBag.GetComponent<Button>();
        player = player.GetComponent<PlayerController>();
        gameObject.SetActive(false);
        
	}

    void Update()
    {


        Ray moneyRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(moneyRay, out hit, 5))
        {
          
            if (hit.collider.CompareTag("Floor"))
            {
                
                onFloor = true;

                if (Vector3.Distance(humanTransform.position, transform.position) > 2) 
                {
                    Quaternion rotation = Quaternion.LookRotation(transform.position - humanTransform.position);
                    humanTransform.rotation = Quaternion.Slerp(humanTransform.rotation, rotation, Time.maximumDeltaTime);
                }
                humanTransform.position = Vector3.MoveTowards(humanTransform.position, transform.position - new Vector3(0,0,2), humanSpeed * Time.deltaTime);
            }
        }
    }


    /*public void fall()
    {
     
            
            Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, 20);
            int i = 0;
            while (i < hitColliders.Length)
            {
               human = hitColliders[i].GetComponentsInParent<HumanController>();
               foreach (HumanController h in human)
               {
                   if (player.haveMoney())
                   {
                       h.myMoney.gameObject.SetActive(true);
                       h.myMoney.constraints = RigidbodyConstraints.None;
                       player.quitMoney();
                   }

               }
                i++;
            }
           
        

    }*/


}
