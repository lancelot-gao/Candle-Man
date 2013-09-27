using UnityEngine;
using System.Collections;

public class DebugRenderingPath : GenericInputReciever
{
    #region Fields
    public RenderingPath[] renderingPaths;
    #endregion

    #region Properties

    #endregion

    #region Functions

    #endregion

    public override void OnDirectionInput(float x, float y, float z)
    {
        
    }

    public override void OnIndexInput(uint index)
    {
        if(renderingPaths.Length > 0 && index < renderingPaths.Length) Camera.main.renderingPath = renderingPaths[index];
    }
	
	protected override void OnBecameUnFunctional ()
	{
		
	}
	
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		
	}
}
