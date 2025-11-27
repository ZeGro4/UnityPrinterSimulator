using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtController : MonoBehaviour {

	Animator animator;
	public Material material;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (animator != null)
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				animator.SetBool("IsMoving", true);
				StartCoroutine(ColorShirt());
			}
			
		}

	}

	private IEnumerator ColorShirt()
	{
		yield return new WaitForSeconds(6f);
		gameObject.GetComponent<MeshRenderer>().material = material;
	}
}
