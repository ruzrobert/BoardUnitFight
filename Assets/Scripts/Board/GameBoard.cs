using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public static GameBoard Instance { get; private set; }

	[Space]
	[SerializeField] private BoardGenerator generator;

	public BoardGenerator Generator => generator;

	private void Awake()
	{
		Instance = this;
	}
}