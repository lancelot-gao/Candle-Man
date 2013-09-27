using UnityEngine;
using System.Collections;

public class MouseWheelInput : GenericInputSender
{
    #region Fields
    public Vector3 inputAxis = Vector3.forward;
    public float strength = 1;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        if(reciever == null)return;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 input = inputAxis * scroll * strength;
            reciever.OnDirectionInput(input.x, input.y, input.z);
        }
    }
    #endregion
}
