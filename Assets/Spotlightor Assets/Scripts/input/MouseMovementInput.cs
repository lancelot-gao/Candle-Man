using UnityEngine;
using System.Collections;

public class MouseMovementInput : GenericInputSender
{
    #region Fields
    public enum MouseButtonState { DONT_CARE, L_BUTTON_DOWN, L_BUTTON_NOT_DOWN };
    public MouseButtonState mouseState = MouseButtonState.DONT_CARE;
    public float strength = 1;
    protected float mouseMoveX = 0;
    protected float mouseMoveY = 0;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        if(reciever == null)return;
        if (mouseState == MouseButtonState.L_BUTTON_DOWN && !Input.GetMouseButton(0)) return; // Mouse drag only
        if (mouseState == MouseButtonState.L_BUTTON_NOT_DOWN && Input.GetMouseButton(0)) return;
        mouseMoveX = Input.GetAxis("Mouse X");
        mouseMoveY = Input.GetAxis("Mouse Y");
        reciever.OnDirectionInput(mouseMoveX * strength, mouseMoveY * strength, 0);
    }
    #endregion
}
