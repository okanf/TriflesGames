using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
	[SerializeField] GameObject Target;
	[SerializeField] private Vector3 distance;
    

	void LateUpdate()
	{
    	
	
		
		if (GameManager.Instance.gameStat == GameManager.GameStat.Failed || GameManager.Instance.gameStat == GameManager.GameStat.Finish)
		{
			Vector3 position = transform.position ;
			position = (Target.transform.position + new Vector3(2.5f,10f,-10f));
			
			Quaternion quaternion = Quaternion.Euler(40f,-15f,0);
			
			transform.position=Vector3.Lerp(transform.position,position,Time.deltaTime);
			transform.rotation=Quaternion.Lerp(transform.rotation,quaternion,Time.deltaTime);
		}
		else
		{
			Vector3 position = transform.position ;
			//position.y = (Target.transform.position + distance).y;
			position.z = (Target.transform.position + distance).z;
		
    	
			transform.position=Vector3.Lerp(transform.position,position,Time.deltaTime);
		}
	    
	
	}
    
}
