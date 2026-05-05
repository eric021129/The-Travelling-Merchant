using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabIconGenerator
{
    private const int IconSize = 512;
    private const string OutputFolder = "Assets/Icons";

    [MenuItem("Tools/Generate Prefab Icon")]
    public static void GenerateIconForSelected()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is GameObject prefab)
            {
                GenerateIcon(prefab);
            }
        }
    }

    private static void GenerateIcon(GameObject prefab)
    {
        // Instantiate prefab far away to avoid scene objects
        GameObject instance = Object.Instantiate(prefab);
        instance.transform.position = new Vector3(0, -1000, 0);

        Bounds bounds = GetBounds(instance);
        float maxDim = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);

        // Create camera
        GameObject camObj = new GameObject("IconCamera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.orthographic = true;
        cam.orthographicSize = maxDim * 0.75f;   // fit with padding
        cam.nearClipPlane = 0.01f;
        cam.farClipPlane = maxDim * 10f;

        // Camera in a 3/4 angle
        Vector3 dir = new Vector3(0.7f, 0.6f, -1f).normalized;
        cam.transform.position = bounds.center + dir * (maxDim * 3f);
        cam.transform.LookAt(bounds.center);

        // Lighting — two lights for better visibility
        GameObject lightObj = new GameObject("IconLight");
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.0f;
        light.color = Color.white;
        lightObj.transform.rotation = Quaternion.Euler(45, -30, 0);

        GameObject fillLightObj = new GameObject("IconFillLight");
        Light fillLight = fillLightObj.AddComponent<Light>();
        fillLight.type = LightType.Directional;
        fillLight.intensity = 0.5f;
        fillLight.color = Color.white;
        fillLightObj.transform.rotation = Quaternion.Euler(-30, 150, 0);

        // RenderTexture with proper format
        RenderTexture rt = new RenderTexture(IconSize, IconSize, 24, RenderTextureFormat.ARGB32);
        rt.antiAliasing = 8;
        cam.targetTexture = rt;

        RenderTexture prevActive = RenderTexture.active;
        RenderTexture.active = rt;

        cam.Render();

        Texture2D tex = new Texture2D(IconSize, IconSize, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, IconSize, IconSize), 0, 0);
        tex.Apply();

        RenderTexture.active = prevActive;
        cam.targetTexture = null;

        // Cleanup
        Object.DestroyImmediate(rt);
        Object.DestroyImmediate(instance);
        Object.DestroyImmediate(camObj);
        Object.DestroyImmediate(lightObj);
        Object.DestroyImmediate(fillLightObj);

        // Save
        byte[] png = tex.EncodeToPNG();
        Directory.CreateDirectory(OutputFolder);
        string path = $"{OutputFolder}/{prefab.name}.png";
        File.WriteAllBytes(path, png);
        AssetDatabase.Refresh();

        TextureImporter ti = (TextureImporter)AssetImporter.GetAtPath(path);
        if (ti != null)
        {
            ti.textureType = TextureImporterType.Sprite;
            ti.alphaIsTransparency = true;
            ti.mipmapEnabled = false;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        Debug.Log($"Saved icon: {path} ({IconSize}x{IconSize})");
    }

    private static Bounds GetBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds(obj.transform.position, Vector3.one);

        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }
        return bounds;
    }
}