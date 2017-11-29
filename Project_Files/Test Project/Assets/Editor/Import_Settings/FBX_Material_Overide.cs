using UnityEngine;
using System.Collections;
using UnityEditor;

public class FBX_Material_Overide : AssetPostprocessor
{
    void OnPreprocessModel(){
        ModelImporter importer = assetImporter as ModelImporter;
        string name = importer.assetPath.ToLower();

        if(name.Substring(name.Length - 4, 4) == ".fbx"){
            importer.importMaterials = false;
        }

    }

}
