//using UnityEditor;

//public static class LayerUtilities
//{
//    public static void CreateLayer(string name)
//    {
//        //  https://forum.unity3d.com/threads/adding-layer-by-script.41970/reply?quote=2274824
//        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
//        SerializedProperty layers = tagManager.FindProperty("layers");
//        bool LayerExists = false;
//        for (int i = 8; i < layers.arraySize; i++)
//        {
//            SerializedProperty layerSP = layers.GetArrayElementAtIndex(i);

//            if (layerSP.stringValue == name)
//            {
//                LayerExists = true;
//                break;
//            }

//        }
//        if (!LayerExists)
//        {
//            for (int j = 8; j < layers.arraySize; j++)
//            {
//                SerializedProperty layerSP = layers.GetArrayElementAtIndex(j);
//                if (layerSP.stringValue == "")
//                {
//                    layerSP.stringValue = name;
//                    tagManager.ApplyModifiedProperties();
//                    break;
//                }
//            }
//        }
//    }
//}
