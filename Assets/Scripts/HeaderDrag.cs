using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.EventSystems;


public class HeaderDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	bool isPointing;
	Vector3 downPos;
	Vector3 dragPos;
	Vector3 movePos;
	Vector3 previousPos;
	public float speed = 3.0f;


	void Update()
	{
		if (isPointing)
		{
			movePos = (dragPos - downPos) * speed;
			transform.position = previousPos + movePos;
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isPointing = true;
		downPos = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		dragPos = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		isPointing = false;
		previousPos = transform.position;

		//	public void Stop()
		//	{
		//		pointerId = PanelManager.NON_EXISTING_TOUCH;
		//	}
	}
}

