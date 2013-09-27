using UnityEngine;
using System.Collections;

public class ConstantInput : GenericInputSender
{
    #region Fields
    public Vector3 directionInput = Vector3.zero;
    #endregion

    #region Properties

    #endregion

    #region Functions
    protected virtual void Update()
    {
        if (reciever != null) reciever.OnDirectionInput(directionInput.x, directionInput.y, directionInput.z);
    }
    #endregion
}
