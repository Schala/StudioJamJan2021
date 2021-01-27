using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] LayerMask targetMask;
	[SerializeField] Transform spawnPoint = null;
	[SerializeField] LineRenderer rayVisual = null;
	Camera pov = null;

	private void Awake()
	{
		rayVisual.positionCount = 2;
		rayVisual.enabled = false;
		pov = Camera.main;
	}

	private void OnMouseDown() => Shoot();

	private void OnMouseUp()
	{
		rayVisual.enabled = false;
	}

	void Shoot()
	{
		var targetFound = Physics.Raycast(spawnPoint.position, pov.transform.forward, out RaycastHit hit, Mathf.Infinity, targetMask);
		rayVisual.enabled = true;
		rayVisual.SetPosition(0, spawnPoint.position);
		rayVisual.SetPosition(1, targetFound ? hit.point : pov.transform.forward * 2f);
		if (!targetFound) return;

		var gw = hit.collider.GetComponent<GravityWarp>();
		if (gw == null) return;
		gw.Warp(true);
	}

	private void OnMouseDrag() => Shoot();
}
