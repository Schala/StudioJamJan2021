using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager main = null;

	public AudioSource respawnSound = null;

	private void Awake()
	{
		if (main != null)
			Destroy(gameObject);

		main = this;
		DontDestroyOnLoad(gameObject);
	}
}
