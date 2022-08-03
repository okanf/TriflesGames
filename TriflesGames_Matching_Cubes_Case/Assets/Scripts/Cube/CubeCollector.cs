using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeCollector : MonoBehaviour
{
    
    

	
	[SerializeField] public static List<GameObject> Cubes = new List<GameObject>();
	private static GameObject Player;
	[SerializeField] private GameObject air;
	private bool Ramp;
	public bool SpeedUP;
	private float JumpForce=1f;
	public bool obstacle;
	
	public static bool Protected;
	
	
	public static CubeCollector Instance;
	
	void Start()
	{
		Player = GameObject.FindWithTag("Player");
		Instance=this;
	}
	
	void FixedUpdate()
	{
		if (Ramp)
		{
			transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up*JumpForce,ForceMode.Impulse);
		}
	}
	
	void Update()
	{
		if (IsGrounded()&&!Ramp&&!obstacle)
		{
			transform.parent.transform.position = new Vector3(transform.position.x,0,transform.position.z);
			transform.parent.GetComponent<Rigidbody>().isKinematic=true;
			Trail.TrailOff=false;

		}
		
	}
    
	
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.tag=="Cube")
		{
			
			if (!other.GetComponent<Cube>().Collected())
			{
				CubeCollision(other.gameObject);
			}
			
		}
		else if(other.gameObject.tag=="gaterandom")
		{
			if (Cubes.Count>0)
			{
				GateRandomCollision(other.gameObject);
			}
			
		}
		else if(other.gameObject.tag=="gateorder")
		{
			if (Cubes.Count>0)
			{
				GateOrderCollision(other.gameObject);
			}
			
		}
		else if(other.gameObject.tag=="SpeedUP")
		{
			if (!SpeedUP)
			{
				Protected=true;
				SpeedUP=true;
				JumpForce = 1.1f;
				SpeedUPCollision();
			}
			
		}
		else if(other.gameObject.tag=="Ramp")
		{
		
			if (!SpeedUP)
			{
				Protected=true;
				SpeedUP=true;
				SpeedUPCollision();
			}
			
			RampCollision();
			
		
			
		}
		else if(other.gameObject.tag=="Finish")
		{
			foreach (var i in Cubes)
			{
				i.transform.GetComponent<Cube>().ChangeColorWhileDestroying();
			}
			
			Player.transform.localPosition = new Vector3(Player.transform.localPosition.x,-0.5f,Player.transform.localPosition.z);
			Trail.TrailOff=true;
			GameManager.Instance.gameStat=GameManager.GameStat.Finish;
		}
		
	}
	
	
	private void CubeCollision(GameObject other)
	{
		if (Cubes.Count>0)
		{
				
			foreach (GameObject i in Cubes)
			{
				i.GetComponent<Cube>().SetIndex(i.GetComponent<Cube>().GetIndex()+1);
				i.transform.localPosition = new Vector3(0,i.GetComponent<Cube>().GetIndex(),-0.5f);
			}
			
		}
			
		Cubes.Add(other.gameObject);
		SetCubeParent(other.gameObject);
		SetCubePosition(other.gameObject,other.GetComponent<Cube>().Collected());
		
		
		
	
		
		StartCoroutine(PlayerJumpfalse());

	}
	
	private IEnumerator PlayerJumpfalse()
	{
		yield return new WaitForSeconds(0.5f);
		Character.grounded=true;
		
		
	}
	
	private	void GateRandomCollision(GameObject other)
	{
		
		GateController gatecontroller = new GateController();
		Cubes = gatecontroller.ShuffleList(Cubes);
		
		foreach (GameObject i in Cubes)
		{
			
			GameObject[] smokes = i.GetComponent<Cube>().smokes;
			
			foreach (GameObject e in smokes)
			{
				e.SetActive(true);
			}
			
		}
		
		ResetCubesAndCharacterPositionAfterMatch();
		CheckMatchesAfterShuffleOrOrder();
		
		Trail.lastcubecolor = Cubes.First().GetComponent<Cube>().GetColor();
		
	}
	
	private	void GateOrderCollision(GameObject other)
	{
		GateController gatecontroller = new GateController();
		Cubes = gatecontroller.OrderList(Cubes);
		
		foreach (GameObject i in Cubes)
		{
			
			GameObject[] smokes = i.GetComponent<Cube>().smokes;
			
			foreach (GameObject e in smokes)
			{
				e.SetActive(true);
			}
			
		}
		
		ResetCubesAndCharacterPositionAfterMatch();
		CheckMatchesAfterShuffleOrOrder();
		
		Trail.lastcubecolor = Cubes.First().GetComponent<Cube>().GetColor();
		
		
	}
	
	public void SpeedUpAfter3MatchesAndProtect()
	{
		
		Protected=true;
		SpeedUP=true;
		JumpForce = 1.1f;
		StartCoroutine(SpeedUp());
		
	}
	
	private void SpeedUPCollision()
	{
		StartCoroutine(SpeedUp());
	}
	
	private IEnumerator SpeedUp()
	{
		
		
		transform.parent.transform.GetComponent<Movement>().forwardspeed +=10f;
		
		Player.GetComponent<Animator>().speed*=1.5f;
		air.SetActive(true);
		
		yield return new WaitForSeconds(3f);
		Protected=false;
		transform.parent.transform.GetComponent<Movement>().forwardspeed -=10f;
		air.SetActive(false);
		Player.GetComponent<Animator>().speed/=1.5f;
		SpeedUP=false;
		JumpForce = 1f;
		
	}
	
	private	void RampCollision()
	{
	
		Ramp=true;
		transform.parent.GetComponent<Rigidbody>().isKinematic=false;
		Trail.TrailOff=true;
	
		StartCoroutine(MakeRampFalse());
		
	}
	
	private	IEnumerator MakeRampFalse()
	{
		yield return new WaitForSeconds(0.3f);
		Ramp=false;
	}
	
	
	
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
	
	private void SetCubeParent(GameObject cube)
	{
		cube.transform.parent = transform;
	}
	
	
	public void SetCubePosition(GameObject cube,bool collected)
	{
		
		if (!collected)
		{
			
			
			cube.GetComponent<Animator>().SetTrigger("CubePopsUp");
			
			MakeCollected(cube);
			
			cube.GetComponent<Cube>().SetIndex(0);
			
			cube.transform.localPosition = new Vector3(0,cube.GetComponent<Cube>().GetIndex(),-0.5f);
			
			Player.transform.localPosition = new Vector3(0,Player.transform.localPosition.y+1,0);
			
			
			CubeMatchChecker.Instance.MatchCheckAfterShuffleOrOrder(Cubes);
			
			Trail.lastcubecolor = cube.GetComponent<Cube>().GetColor();
	
		}
		
	}
	
	public void MakeCollected(GameObject cube)
	{
		cube.GetComponent<Cube>().MakeCollected();
	}
	
	
	public void RemoveMatchedCubesAfterShuffleOrOrder(int index)
	{
		
		for (int i = index-1; i >= index-3; i--) 
		{
			Cubes[i].transform.GetComponent<Cube>().ChangeColorWhileDestroying();
		
		}

	}
	
	public void RemovefromList(int index)
	{
		Cubes.RemoveRange(index-3,3);
		
		if (Cubes.Count>0)
		{
			Cubes.Reverse();
		}
		
		StartCoroutine(ResetPos());
	}
	
	private IEnumerator ResetPos()
	{
		yield return new WaitForSeconds(.2f);
		ResetCubesAndCharacterPositionAfterMatch(true);
	}
	
	public void ResetCubesAndCharacterPositionAfterMatch(bool matched=false)
	{

		for (int i = 0; i < Cubes.Count; i++) 
		{
			
			Cubes[i].gameObject.transform.localPosition = new Vector3(0,i,-0.5f);
			Cubes[i].gameObject.transform.GetComponent<Cube>().SetIndex(i);
		}
		
		Player.transform.localPosition = new Vector3(0,-0.5f+Cubes.Count,0);
	
		if (Cubes.Count>0&&matched)
		{
			Cubes.Reverse();
		}
	
	}
	
	
	public void CheckMatchesAfterShuffleOrOrder()
	{
		CubeMatchChecker.Instance.MatchCheckAfterShuffleOrOrder(Cubes);
	}
	
}
