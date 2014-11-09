using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileManager : Singleton <TileManager> {

	public Dictionary<string, Dictionary<string, Texture>> dictTexture = new Dictionary<string, Dictionary<string, Texture>> (); //folder, list_texture

	public Dictionary<string, Texture> GetFolder (string folder) {
		Dictionary<string, Texture> dict = null;
		dictTexture.TryGetValue (folder, out dict);

		if (dict == null) {
			Texture[] texes = Resources.LoadAll<Texture>(folder);
			Array.Sort <Texture> (texes, Ultil.CompareTextureName);

			dict = new Dictionary<string, Texture> ();
			for  (int i = 0; i < texes.Length; ++i) {
				dict[texes[i].name] = texes[i];
			}

			dictTexture[folder] = dict;
		}

		return dict;
	}

	public Texture GetTexture (string name) {
		string folder = Ultil.GetFolderTile (name);
		Dictionary<string, Texture> dict = GetFolder (folder);
		Texture t = null;

		if (dict != null) {
			dict.TryGetValue (name, out t);
		}

		return t;
	}
}
