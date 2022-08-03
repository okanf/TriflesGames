using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	
	[SerializeField] public float forwardspeed;
	[SerializeField] private float slidingspeed;
	
	private Touch touch;
	
	public bool finished;
	
	public static Movement Instance;
	
	
	public Vector3 firstpos;
	public Vector3 lastpos;
	
	
	void Start()
	{
		Instance=this;
	}

    
    void Update()
	{
    	
    	
    	
		if (Input.GetAxis("Horizontal")>0 || Input.GetAxis("Horizontal")<0 || Input.GetMouseButtonDown(0))
		{	
			if(GameManager.Instance.gameStat != GameManager.GameStat.Failed && GameManager.Instance.gameStat != GameManager.GameStat.Finish)
			{
				GameManager.Instance.gameStat=GameManager.GameStat.play;
			}
			
		}
		
		
		
		if (GameManager.Instance.gameStat == GameManager.GameStat.play)
		{
			
			
			transform.Translate(0,0,forwardspeed*Time.deltaTime);
			
			if (transform.position.x <= -1.5f)
			{
				transform.position = new Vector3(-1.5f,transform.position.y,transform.position.z);
			}
			else if (transform.position.x >= 1.5f)
			{
				transform.position = new Vector3(1.5f,transform.position.y,transform.position.z);
			}
			
			
			if (Input.GetAxis("Horizontal")>0 || Input.GetAxis("Horizontal")<0)
			{
				float hor = Input.GetAxis("Horizontal")*slidingspeed*Time.deltaTime;
				transform.Translate(hor,0,0);
			}
			
			
			
			
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mousepos = Input.mousePosition;
				mousepos.z=10f;
				firstpos = Camera.main.ScreenToWorldPoint(mousepos);
			}
		
			if (Input.GetMouseButton(0))
			{
				
				if (transform.position.x >= -1.5f && transform.position.x <= 1.5f)
				{
					Vector3 mousepos = Input.mousePosition;
					mousepos.z=10f;
					lastpos = Camera.main.ScreenToWorldPoint(mousepos);
					Vector3 diff = lastpos-firstpos;
					
					float hor = diff.x*slidingspeed*2*Time.deltaTime;
					
					transform.Translate(hor,0,0);
					
				}

			}
		
		
		}
		else if(GameManager.Instance.gameStat == GameManager.GameStat.Failed || GameManager.Instance.gameStat == GameManager.GameStat.Finish)
		{
			transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		}

	    
	    //if (Input.touchCount > 0)
	    //{
	    //	touch = Input.GetTouch(0);
	    	
	    //	if (touch.phase == TouchPhase.Moved)
	    //	{
		//		transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * slidingspeed, transform.position.y,transform.position.z);
	    //	}
	    //}
	    
	    
    }
}
