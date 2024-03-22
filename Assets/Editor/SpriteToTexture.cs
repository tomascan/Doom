using UnityEngine;
using UnityEditor;

public class SpriteToTexture : EditorWindow
{
    [MenuItem("Tools/Sprite to Texture")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<SpriteToTexture>("Convert Sprite to Texture");
    }

    private Sprite sprite;

    void OnGUI()
    {
        sprite = EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false) as Sprite;

        if (GUILayout.Button("Convert"))
        {
            if (sprite == null)
            {
                EditorUtility.DisplayDialog("Sprite to Texture", "Please select a sprite.", "OK");
                return;
            }

            string path = EditorUtility.SaveFilePanelInProject("Save Texture", sprite.name + ".png", "png", "Please enter a filename to save the texture to");

            if (!string.IsNullOrEmpty(path))
            {
                Texture2D texture = SpriteToTexture2D(sprite);

                // Check if the texture is readable
                if (texture == null)
                {
                    EditorUtility.DisplayDialog("Sprite to Texture", "Failed to create a readable texture. Is the sprite texture readable in the import settings?", "OK");
                    return;
                }

                byte[] pngData = texture.EncodeToPNG();

                // Check if encoding to PNG was successful
                if (pngData != null)
                {
                    System.IO.File.WriteAllBytes(path, pngData);
                    AssetDatabase.Refresh();
                    EditorUtility.DisplayDialog("Sprite to Texture", "Texture saved successfully at " + path, "OK");
                }
                else
                {
                    EditorUtility.DisplayDialog("Sprite to Texture", "Failed to encode texture to PNG.", "OK");
                }
            }
        }
    }

    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if (!sprite.texture.isReadable)
        {
            Debug.LogError("Sprite texture is not readable. The texture import settings must be changed to allow read/write.");
            return null;
        }

        Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
        newText.SetPixels(pixels);
        newText.Apply();
        return newText;
    }
}
