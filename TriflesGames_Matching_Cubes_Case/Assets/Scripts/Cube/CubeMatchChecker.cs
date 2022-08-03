using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeMatchChecker : MonoBehaviour
{
	
	[SerializeField] static int lastcubecolor;
	[SerializeField] static int SerieCount=0;
	
	public static CubeMatchChecker Instance;
	
	public static int serieforprotected;
	
	//[SerializeField] static int _LastColor=0;
	//[SerializeField] static int _SerieCount=1;
	
	
	
	void Start()
	{
		Instance = this;
	}
	
	
	//public void MatchCheck(GameObject lastcube)
	//{
			
			
	//	print("girdi");
		
	//	if (lastcubecolor != 0)
	//	{
	//		print("renk verilmişti :"+lastcubecolor);
			
	//		if (lastcubecolor == lastcube.GetComponent<Cube>().GetColor())
	//		{
				
	//			SerieCount++;
	//			print("renk aynıydı :" + SerieCount + "lastcubecolor :" + lastcubecolor);
	//		}
	//		else
	//		{
	//			print("renk aynı değildi :"+lastcube.GetComponent<Cube>().GetColor());
	//			SerieCount=1;
	//			lastcubecolor = lastcube.GetComponent<Cube>().GetColor();
	//		}
	//	}
	//	else
	//	{
			
	//		lastcubecolor = lastcube.GetComponent<Cube>().GetColor();
	//		SerieCount++;
	//		print("renk verilmemişti :"+lastcubecolor);
	//	}
		
	//	if (SerieCount == 3)
	//	{
	//		print("3 renk aynı geldi :" + SerieCount);
	//		serieforprotected++;
			
			
			
	//		RemoveMatchedCubes();
			
	//		if (CubeCollector.Cubes.Count>0)
	//		{
	//			lastcubecolor=CubeCollector.Cubes.First().GetComponent<Cube>().GetColor(); 
	//			SerieCount=1;
	//		}
	//		else
	//		{
	//			lastcubecolor=0;
	//			SerieCount=0;
	//		}
			
			
	//	}
		
	//	if (serieforprotected==3)
	//	{
	//		serieforprotected=0;
	//		CubeCollector.Instance.SpeedUpAfter3MatchesAndProtect();
	//	}
		
	//}
	
	
	public void MatchCheckAfterShuffleOrOrder(List<GameObject> list)
	{
		
		
		int _LastColor=0;
		int _SerieCount=0;
		int index=0;
		
	
		
		for (int i = 0; i < list.Count; i++) 
		{
			
		
			index++;
		
			
			if (_LastColor!=0)
			{
				
				
				if (_LastColor== list[i].GetComponent<Cube>().GetColor())
				{
					
					
					
					_SerieCount++;
					
					
					
					if (_SerieCount==3)
					{
					
						RemoveMatchedCubesAfterShuffleOrOrder(index);
			
						CubeCollector.Instance.CheckMatchesAfterShuffleOrOrder();
						if (CubeCollector.Cubes.Count>0)
						{
							_LastColor=CubeCollector.Cubes.First().GetComponent<Cube>().GetColor();
						}
						
						serieforprotected++;
						
							if (serieforprotected==3)
							{
								serieforprotected=0;
								CubeCollector.Instance.SpeedUpAfter3MatchesAndProtect();
							}
					
					}
				}
				else
				{
					_SerieCount=1;
					_LastColor = list[i].GetComponent<Cube>().GetColor();
				
					
				
				}
			}
			else
			{
				_LastColor=list[i].GetComponent<Cube>().GetColor();
				_SerieCount++;
			
			
			}
		}
	}
		
	
	//private void RemoveMatchedCubes()
	//{
	//	CubeCollector collector = new CubeCollector();
	//	collector.RemoveMatchedCubes();
	//}
	
	private void RemoveMatchedCubesAfterShuffleOrOrder(int index)
	{
		CubeCollector collector = new CubeCollector();
		CubeCollector.Instance.RemoveMatchedCubesAfterShuffleOrOrder(index);
		CubeCollector.Instance.RemovefromList(index);
	}

}

