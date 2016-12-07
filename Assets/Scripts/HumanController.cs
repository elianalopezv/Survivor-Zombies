using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

    
    public float humanLookDistance;
    public Transform playerTransform;
    public PlayerController player;
    private float playerDistance;
    public Rigidbody myMoney;
    public MoneyScript money;
    public GameObject bullet;
    public Transform bulletTransform;
    public Rigidbody bulletRb;
    public float bulletSpeed;
    public AudioClip shootClip;
    public AudioClip zombiePain;
    public Transform gun;
    private Vector3 bulletPosition;
    private float bulletDistance;
    private AudioSource source;


    
    


	// Use this for initialization
	void Start () {

       // bulletTransform = bulletTransform.GetComponent<Transform>(); 
        bulletPosition = gun.position;
        source = GetComponent<AudioSource>();
       // bullet.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (!player.gameOver && !player.winLevel)
        {


            playerDistance = Vector3.Distance(playerTransform.position, transform.position);
            bulletDistance = Vector3.Distance(bulletTransform.position, transform.position);
            if (playerDistance < humanLookDistance && !money.onFloor)
            {
                lookAtPlayer();
                shoot();

                if (bulletDistance > playerDistance)
                {
                    bulletTransform.position = bulletPosition;

                }
                //player.energyDown();

            }
            




        }

	}

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.maximumDeltaTime);
    }

    void shoot()

    {
       
       Quaternion rotation = Quaternion.LookRotation(playerTransform.position - bulletTransform.position);
       bulletTransform.rotation = Quaternion.Slerp(bulletTransform.rotation, rotation, Time.maximumDeltaTime);
       bulletTransform.Translate(playerTransform.position.normalized  * bulletSpeed * Time.deltaTime);
        
            
        //bulletTransform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        //bulletRb.AddForce((playerTransform.position - bulletTransform.position).normalized * bulletForce);
        //bullet.AddForce(playerTransform.transform.position * bulletForce);
        
    }

    
}
