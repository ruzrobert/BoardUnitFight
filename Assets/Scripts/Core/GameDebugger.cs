using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebugger : MonoBehaviour
{
#if UNITY_EDITOR
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
#endif
}