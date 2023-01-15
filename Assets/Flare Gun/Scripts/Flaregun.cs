using UnityEngine;

public class Flaregun : MonoBehaviour {
	
	public Rigidbody flareBullet;
	public Transform barrelEnd;
	public GameObject muzzleParticles;
	public AudioClip flareShotSound;
	public AudioClip noAmmoSound;	
	public AudioClip reloadSound;	
	public int bulletSpeed = 2000;
	public int maxSpareRounds = 9999;
	public int spareRounds = 999;
	public int currentRound = 1;

	public bool autoReload = true;

	private Animation anim;
	private AudioSource player;

    private void Start()
    {
        anim = GetComponent<Animation>();
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () 
	{		
		if (!anim?.isPlaying ?? true)
		{
			if (Input.GetMouseButtonDown(0)) 							
			{
				if (currentRound > 0)
				{
					Shoot();
				}
                else
                {
                    anim?.Play("noAmmo");
                    player?.PlayOneShot(noAmmoSound);
                }
            }
            else if (currentRound <= 0 && autoReload && spareRounds > 0)
            {
                Reload();
            }            
		}
        if (Input.GetKeyDown(KeyCode.R) && !anim.isPlaying)
		{
			Reload();			
		}        
    }

    void Shoot()
	{
        currentRound--;
        if (currentRound <= 0)
        {
            currentRound = 0;                        
        }        
		
		anim?.CrossFade("Shoot");
		player?.PlayOneShot(flareShotSound);
		
			
		Rigidbody bulletInstance;
		//INSTANTIATING THE FLARE PROJECTILE
		bulletInstance = Instantiate(flareBullet,barrelEnd.position,barrelEnd.rotation) as Rigidbody;
		Debug.Log($"FireTime: {Time.time}");	
		bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE
        //INSTANTIATING THE GUN'S MUZZLE SPARKS				
        Instantiate(muzzleParticles, barrelEnd.position,barrelEnd.rotation);
        Reload();
	}
	
	void Reload()
	{
		if (spareRounds >= 1 && currentRound == 0)
		{
			player?.PlayOneShot(reloadSound);			
			spareRounds--;
			currentRound++;
			anim?.CrossFade("Reload");
		}
		
	}
}
