using UnityEngine;
using System.Collections;

public class PlayerCandle : MonoBehaviour
{
	public Transform candle;
	public float candleMinScale = 0.1f;
	public Transform wpCandleTop;
	public Transform candleTop;
	private LightController lightController;
	// Use this for initialization
	void Start ()
	{
		lightController = transform.FindInParent<LightController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		candle.localScale = new Vector3 (1, 1, Mathf.Lerp (candleMinScale, 1, lightController.LightTimeLeftPercent));
		candleTop.transform.position = wpCandleTop.position;
	}
}
