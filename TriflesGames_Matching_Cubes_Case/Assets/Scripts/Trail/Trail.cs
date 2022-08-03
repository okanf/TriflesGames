using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Trail : MonoBehaviour
{
   
   
	TrailRenderer TrailRenderer;
	[SerializeField] private Color[] Colors;
	public static int lastcubecolor;
	
	public static bool TrailOff;
	
	void Start()
	{
		TrailRenderer = GetComponent<TrailRenderer>(); 
	}
   
   
    void Update()
    {
	    if (CubeCollector.Cubes.Count > 0 && !TrailOff)
	    {
	    	GameObject.Find("Trail").transform.GetComponent<TrailRenderer>().time=3;
	    	
	    	SetColor(lastcubecolor);
	    }
	    else
	    {
	    	GameObject.Find("Trail").transform.GetComponent<TrailRenderer>().time=-1;
	    }
    }
    
	public void SetColor(int colorint)
	{
		
		switch (colorint)
		{
		case 1:TrailRenderer.material.color = Colors[0]; break;
		case 2:TrailRenderer.material.color = Colors[1]; break;
		case 3:TrailRenderer.material.color = Colors[2]; break;
		default:break;
		}
	}
}
