using UnityEngine;
using System.Collections;

public class ColorTransitionManager : iTweenBasedTransitionManager
{
    #region Fields
    public Color colorIn = Color.white;
    public Color colorOut = Color.black;

    public bool includeChildren = false;
    #endregion

    #region Properties

    #endregion

    #region Functions
    protected override void DoTransitionIn(bool instant)
    {
		iTween.Stop(gameObject, "ColorTo");
        if (instant){
			if(guiTexture != null)guiTexture.color = colorIn;
			if(guiText != null)guiText.material.color = colorIn;
			if(renderer != null && renderer.material != null)renderer.material.color = colorIn;
			TransitionInComplete();
		}
        else iTween.ColorTo(gameObject, iTween.Hash("color", colorIn, "ignoretimescale", ignoreTimeScale, "time", durationIn, "delay", delayIn, "easetype", easeIn, "includeChildren", includeChildren, "oncomplete", "TransitionInComplete"));
    }

    protected override void DoTransitionOut(bool instant)
    {
		iTween.Stop(gameObject, "ColorTo");
        if (instant) {
			if(guiTexture != null)guiTexture.color = colorOut;
			if(guiText != null)guiText.material.color = colorOut;
			if(renderer != null && renderer.material != null)renderer.material.color = colorOut;
			TransitionOutComplete();
		}
        else iTween.ColorTo(gameObject, iTween.Hash("color", colorOut, "ignoretimescale", ignoreTimeScale, "time", durationOut, "delay", delayOut, "easetype", easeOut, "includeChildren", includeChildren, "oncomplete", "TransitionOutComplete"));
    }
    #endregion
}
