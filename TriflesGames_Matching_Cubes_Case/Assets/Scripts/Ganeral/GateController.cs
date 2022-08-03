using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GateController : MonoBehaviour
{
	
	[SerializeField] static bool CollideRandom;
	[SerializeField] static bool CollideOrder;
	
	public List<GameObject> ShuffleList(List<GameObject> list)
	{
		if (!CollideRandom)
		{
			
			CollideRandom=true;
			
			for (int i = list.Count; i > 0; i--)
			{
				int rnd = UnityEngine.Random.Range(0, i);
     
				GameObject temp = list[i-1];
     
				list[i-1] = list[rnd];
				list[rnd] = temp;
			}
		
			
		}
		
		CollideOrder=false;
		return list;
		
	}
	
	public List<GameObject> OrderList(List<GameObject> list)
	{
		if (!CollideOrder)
		{
			CollideOrder=true;
			
			list.Sort((a,b) => a.GetComponent<Cube>().GetColor().CompareTo(b.GetComponent<Cube>().GetColor()));
		
		}
		
		CollideOrder=false;
		return list;
		
	}
}
