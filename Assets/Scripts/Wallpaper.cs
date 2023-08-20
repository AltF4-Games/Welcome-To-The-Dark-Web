using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Wallpaper : MonoBehaviour
{
	private const UInt32 SPI_GETDESKWALLPAPER = 0x73;
	private const int MAX_PATH = 260;
	public UnityEngine.UI.RawImage img;

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int SystemParametersInfo(UInt32 uAction, int uParam, string lpvParam, int fuWinIni);

	private void Start()
	{
		img.texture = null;
		img.color = Color.white;
		StartCoroutine(LoadImg(GetCurrentDesktopWallpaper()));
		//Debug.Log(GetCurrentDesktopWallpaper());
	}

	private IEnumerator LoadImg(string url)
	{
		Texture2D tex;
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(url))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			if (tex.width > 8 || tex.height > 8)
			{
				img.texture = tex;
			}
			else
			{
				img.color = Color.blue;
				//print(Color.blue);
			}
		}
	}

	public string GetCurrentDesktopWallpaper()
	{
		string currentWallpaper = new string('\0', MAX_PATH);
		SystemParametersInfo(SPI_GETDESKWALLPAPER, currentWallpaper.Length, currentWallpaper, 0);
		return currentWallpaper.Substring(0, currentWallpaper.IndexOf('\0'));
	}
}