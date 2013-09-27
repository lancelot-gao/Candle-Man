using UnityEngine;
using System.Collections;

/// <summary>
/// 在动画播放当中动态创建一些特效(vfxPrefab)
/// </summary>
public class AnimationVfxCreator : MonoBehaviour
{
    #region Fields
    public Transform vfxPrefab;
    public Transform vfxPositionHolder;
    public bool autoDestroy = false;
    public float autoDestroyDelay = 1;
    #endregion

    #region Properties

    #endregion

    #region Functions
    public void CreateVFX()
    {
        if (vfxPrefab != null && vfxPositionHolder != null)
        {
            Transform vfx = GameObject.Instantiate(vfxPrefab, vfxPositionHolder.position, vfxPositionHolder.rotation) as Transform;
            vfx.parent = vfxPositionHolder;
            if (autoDestroy) GameObject.Destroy(vfx.gameObject, autoDestroyDelay);
        }
    }
    #endregion
}
