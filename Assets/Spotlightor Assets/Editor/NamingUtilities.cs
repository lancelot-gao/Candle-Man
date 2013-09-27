using UnityEngine;
using System.Collections;
using UnityEditor;

public class NamingUtilities : ScriptableWizard
{
	[MenuItem("Assets/Rename Material by Main Texture", false, 90)]
	public static void NameMaterialByMainTexture ()
	{
		Object[] selectedMaterials = Selection.GetFiltered (typeof(Material), SelectionMode.Assets);
		foreach (Object o in selectedMaterials) {
			Material selectedMaterial = o as Material;
			if (selectedMaterial.mainTexture != null) {
				AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (selectedMaterial), selectedMaterial.mainTexture.name);
			}
		}
	}

	[MenuItem("Assets/Format Asset Name", false, 91)]
	public static void FormatAssetName ()
	{
		Object[] selectedMaterials = Selection.objects;
		foreach (Object o in selectedMaterials) {
			string formatedName = o.name;
			formatedName = formatedName.ToLower ();
			formatedName = formatedName.Replace (" ", "_");
			if (formatedName != o.name)
				AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (o), formatedName);
		}
	}

	public string newNameFormat = "[N]";
	public int counterStartNumber = 0;
	public int counterDigits = 2;
	public string find = "";
	public string replace = "";

	[MenuItem("GameObject/Batch Rename %m")]
	static void DisplayWizard ()
	{
		ScriptableWizard.DisplayWizard<NamingUtilities> ("Batch Rename", "Rename");
	}

	private void OnWizardCreate ()
	{
		Object[] objects = Selection.GetFiltered (typeof(Object), SelectionMode.TopLevel);
		
		Undo.RegisterUndo (objects, "Batch Reanme");
		for (int i = 0; i < objects.Length; i++) {
			Object obj = objects [i];
			
			string oldName = obj.name;
			string newName = GetNewName (oldName, i);
			
			if (AssetDatabase.IsMainAsset (obj))
				AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (obj), newName);
			else
				obj.name = newName;
		}
	}
	
	private void OnWizardUpdate ()
	{
		Object[] objects = Selection.GetFiltered (typeof(Object), SelectionMode.TopLevel);
		if (objects.Length > 0) 
			helpString = string.Format ("{0} => {1}\nAvailable Codes:\n- [N]: Current Name\n- [C]: Counter Number", objects [0].name, GetNewName (objects [0].name, 0));
		else 
			helpString = "Select at least 1 object please.";
	}
	
	private string GetNewName (string oldName, int index)
	{
		if (find != "") 
			oldName = oldName.Replace (find, replace);
			
		string newName = newNameFormat.Replace ("[N]", oldName);
		
		string digitString = string.Format (GetDigitFormatString (), index + counterStartNumber);
		newName = newName.Replace ("[C]", digitString);
			
		return newName;
	}
	
	private string GetDigitFormatString ()
	{
		string digitFormatString = "{0:";
		
		counterDigits = Mathf.Max (1, counterDigits);
		for (int i = 0; i < counterDigits; i++)
			digitFormatString += "0";
		digitFormatString += "}";
		
		return digitFormatString;
	}
}
