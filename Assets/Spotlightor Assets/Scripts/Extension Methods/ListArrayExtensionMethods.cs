using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public static class ListArrayExtensionMethods
{

	public static void Shuffle<T> (this IList<T> list)
	{
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = Random.Range (0, n + 1);
			T tempValueHolder = list [k];
			list [k] = list [n];
			list [n] = tempValueHolder;
		}
	}

	public static void ShuffleSlowButGood<T> (this IList<T> list)
	{
		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider ();
		int n = list.Count;
		while (n > 1) {
			byte[] box = new byte[1];
			do
				provider.GetBytes (box); while (!(box[0] < n * (byte.MaxValue / n)));
			int k = (box [0] % n);
			n--;
			T value = list [k];
			list [k] = list [n];
			list [n] = value;
		}
	}
}
