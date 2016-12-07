using UnityEngine;
using System.Collections;

public class Temp : MonoBehaviour {

	// Use this for initialization
    public PlayerController player;
    private HumanController[] human;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            fall();

    }

    public void fall()
    {

        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, 25);
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



    }
}
