using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	
}
