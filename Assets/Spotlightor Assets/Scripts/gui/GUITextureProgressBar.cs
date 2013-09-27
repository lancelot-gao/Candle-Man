using UnityEngine;
using System.Collections;

public class GUITextureProgressBar : MonoBehaviour
{
	public GUITexture bar;
	public bool vertical = false;

	private float _maxBarLength = -1;
	private float _progress = 0;

	public float MaxBarLength {
		get {
			if (_maxBarLength == -1)
				_maxBarLength = vertical ? bar.pixelInset.height : bar.pixelInset.width;
			return _maxBarLength;
		}
	}

	public float Progress {
		get { return this._progress; }
		set {
			_progress = Mathf.Clamp01 (value);
			Rect newInset = bar.pixelInset;
			if (vertical)
				newInset.height = MaxBarLength * _progress;
			else
				newInset.width = MaxBarLength * _progress;
			bar.pixelInset = newInset;
		}
	}
}
