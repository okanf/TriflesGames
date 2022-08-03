using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cube : MonoBehaviour
{
    
    
	[SerializeField]bool _Collected;
	[SerializeField]int index;
	
	[SerializeField] int Color;
	
	[SerializeField] Color[] Colors;
	
	[SerializeField] GameObject Gem;
	
	bool colored;
	
	Renderer renderer;
	
	public GameObject[] smokes;
    
    
	void Start()
	{
		renderer = GetComponent<Renderer>();
		SetColor(Random.Range(1,4));
	}
	
	


	public void ChangeColorWhileDestroying() 
	{
		InvokeRepeating("ChangeColor", 0f, .05f);
	}

	void ChangeColor()
	{
		
		//int count=0;
		
		//print(count);
		
		//if (count<4)
		//{
			if (colored)
			{
				renderer.material.color = Colors[3];
				colored=false;
			}
			else
			{
				SetColor(Color);
				StartCoroutine(DestroyCube());
			}
			
			//count++;
		//}
		//else
		//{
			
		//}
		
	
		
	}
	
	private IEnumerator DestroyCube()
	{
		yield return new WaitForSeconds(0.1f);
		transform.GetComponent<Animator>().SetTrigger("CubeDestroy");
		
		GameObject gem = Instantiate(Gem,transform.position,Quaternion.identity);
		Destroy(gameObject);
		if (CubeCollector.Cubes.Count>0)
		{
			Trail.lastcubecolor = CubeCollector.Cubes.First().GetComponent<Cube>().GetColor();
		}
		
	
	}
	

	
	private void SetColor(int _color)
	{
		switch (_color)
		{
		case 1:renderer.material.color = Colors[0]; Color = _color; break;
		case 2:renderer.material.color = Colors[1]; Color = _color;	break;
		case 3:renderer.material.color = Colors[2]; Color = _color;	break;
		default:break;
		}
		
		
		
		colored=true;
	}
	
	
	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Barrier" || other.tag == "Lava")
		{
			
			if (!CubeCollector.Protected)
			{
				GameObject.Find("Trail").transform.GetComponent<TrailRenderer>().time=-1;
				Trail.TrailOff=true;
				transform.parent = null;
				transform.GetComponent<Rigidbody>().isKinematic=false;
				CubeCollector.Cubes.Remove(gameObject);
				CubeCollector.Instance.obstacle=true;
			
			
		
				StartCoroutine(ResetPosition(other));
			}
			else
			{
				other.GetComponent<Rigidbody>().isKinematic=false;
			}
			
		}
			
	}
	
	
	private IEnumerator ResetPosition(Collider other)
	{
		
		
		if (other.tag == "Barrier" )
		{
			yield return new WaitForSeconds(0.4f);
		}
		else if (other.tag == "Lava")
		{
			yield return new WaitForSeconds(0.3f);
		}
		

		GameObject.Find("Trail").transform.GetComponent<TrailRenderer>().time=3;
		Trail.TrailOff=false;
		Destroy(gameObject);
		CubeCollector.Instance.obstacle=false;
		if (CubeCollector.Cubes.Count>0)
		{
			Trail.lastcubecolor = CubeCollector.Cubes.First().GetComponent<Cube>().GetColor();
		}
		
		CubeCollector.Cubes.Reverse();
		CubeCollector.Instance.ResetCubesAndCharacterPositionAfterMatch();
		CubeMatchChecker.Instance.MatchCheckAfterShuffleOrOrder(CubeCollector.Cubes);
		
	}
    
	public bool Collected()
	{
		return _Collected;
	}
	
	public void MakeCollected()
	{
		_Collected = true;
	}
	
	public void SetIndex(int _index)
	{
	
		index = _index;

	}
	
	public int GetColor()
	{
	
		return Color;

	}
	
	public int GetIndex()
	{
	
		return index;

	}
	
}
