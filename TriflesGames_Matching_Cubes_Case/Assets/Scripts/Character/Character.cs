using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	
	public static bool grounded=true;
	
	private bool finished;
	
	private bool IsGrounded()
	{
		bool hitsomething=false;
		
		float extra=.01f;
		
		BoxCollider collider = transform.GetComponent<BoxCollider>();
		
		RaycastHit hit;
			
		if (Physics.Raycast(collider.bounds.center, Vector3.down, out hit, collider.bounds.extents.y +extra))
		{
			hitsomething=true;
		}
			
		return hitsomething;
	
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag=="Cube")
		{
			//transform.GetComponent<Rigidbody>().isKinematic=true;
			grounded=false;
		}
		else if (other.tag == "Barrier")
		{
			if (CubeCollector.Cubes.Count==0 && !CubeCollector.Instance.SpeedUP)
			{
				other.GetComponent<Rigidbody>().isKinematic=true;
				GameManager.Instance.gameStat=GameManager.GameStat.Failed;
			}
			else
			{
				other.GetComponent<Rigidbody>().isKinematic=false;
			}
		
		}
		else if(other.gameObject.tag=="Lava")
		{
			
			if (CubeCollector.Cubes.Count==0 && !CubeCollector.Instance.SpeedUP)
			{
				GameManager.Instance.gameStat=GameManager.GameStat.Failed;
			}

		}
		
	}
	
	
	
	
	
	void Update()
	{
	

		if (GameManager.Instance.gameStat == GameManager.GameStat.play)
		{
			
			if (CubeCollector.Cubes.Count>0)
			{
				transform.GetComponent<Animator>().SetTrigger("idle");
			}
			else
			{
				transform.GetComponent<Animator>().SetTrigger("Run");
				transform.localRotation = new Quaternion(0,0,0,0);
			}
		}
		else if(GameManager.Instance.gameStat == GameManager.GameStat.Failed)
		{
			transform.GetComponent<Animator>().SetTrigger("idle");
			transform.localRotation = Quaternion.Euler(-85,0,0);
		}
		else if(GameManager.Instance.gameStat == GameManager.GameStat.Finish)
		{
			transform.GetComponent<Animator>().SetTrigger("idle");
			transform.localRotation = Quaternion.Euler(0,160,0);
		}
		
		
	
	}
}
