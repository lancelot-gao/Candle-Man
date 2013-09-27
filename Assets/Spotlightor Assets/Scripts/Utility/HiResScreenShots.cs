using UnityEngine;
using System.Collections;
using System.IO;

[RequireComponent(typeof(Camera))]
public class HiResScreenShots : MonoBehaviour
{
    public KeyCode key = KeyCode.K;
    public int resWidth = 4096;
    public int resHeight = 2232;
    public bool transparent = true;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;

            TextureFormat tf = transparent ? TextureFormat.ARGB32 : TextureFormat.RGB24;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, tf, false);

            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors 
            Destroy(rt);
			#if UNITY_STANDALONE_WIN
            byte[] bytes = screenShot.EncodeToPNG();
            string dir = Application.dataPath + "/screenshots/";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string filename = dir + "screen" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
			
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
			#endif
			#if UNITY_STANDALONE_OSX
            byte[] bytes = screenShot.EncodeToPNG();
            string dir = Application.dataPath + "/screenshots/";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string filename = dir + "screen" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
			
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
			#endif
        }
    }
}