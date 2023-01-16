using UnityEngine;
using System.Collections;

public class flareround : MonoBehaviour {
	private GameObject flaregun;
	private FlareGun flare;
	public AudioClip pickupSound;	

	// Use this for initialization
	void Start () 
	{
		flaregun = GameObject.Find("flaregun");
		flare = flaregun.GetComponent<FlareGun>();
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		
		if(other.tag == "Player" && flare.spareRounds < flare.maxSpareRounds)
		{
			GetComponent<AudioSource>().PlayOneShot(pickupSound);			
			flare.spareRounds++;
			Destroy(this.gameObject,pickupSound.length);				
		}
		
	}
}

