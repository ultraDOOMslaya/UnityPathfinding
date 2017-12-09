using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {
	NavMeshAgent playerAgent;

	void Start ()
	{
		playerAgent = GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			GetInteraction ();
	}

	void GetInteraction () 
	{
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity)) 
		{
			GameObject interactedObject = interactionInfo.collider.gameObject;
			if (interactedObject.tag == "Interactable Object") {
				Debug.Log ("Interactable interacted");
			} else
			{

			}
		}
	}
}
