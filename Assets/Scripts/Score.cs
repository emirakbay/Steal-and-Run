using UnityEngine;

[CreateAssetMenu]
public class Score : ScriptableObject
{
	[SerializeField]
	private int _value;

	public int Value
	{
		get { return _value; }
		set { _value = value; }
	}

	public void Reset()
	{
		_value = 0;		
	}
}
