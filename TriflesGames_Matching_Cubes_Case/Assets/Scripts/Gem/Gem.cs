using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gem : MonoBehaviour
{
	[SerializeField] private Vector3 target;
	[SerializeField] private float gemmovespeed;
	
	
	void FixedUpdate()
	{
		
		transform.position = Vector3.MoveTowards(transform.position,new Vector3(8f,24f,transform.position.z),gemmovespeed*Time.deltaTime);
		
		if (transform.position.x > 5.5f)
		{
			PlayerPrefs.SetInt("Gem",PlayerPrefs.GetInt("Gem")+1);
			Destroy(gameObject);
		}
		
	}
	

}
