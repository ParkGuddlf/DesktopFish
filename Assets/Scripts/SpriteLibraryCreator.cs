using UnityEngine;
using UnityEditor;
using UnityEngine.U2D.Animation;
using System.Collections.Generic;

#if UNITY_EDITOR
//��������Ʈ ������ ���������̺� ������Ʈ�� ������ִ� ��ũ��Ʈ
public class SpriteLibraryCreator : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    string m_name;
    [SerializeField]
    string[] keyString;
    Dictionary<string, List<Sprite>> spriteDic = new Dictionary<string, List<Sprite>>();


    public void Seield()
    {
        for (int i = 0; i < keyString.Length; i++)
        {
            var asd = FindSpritesByName(keyString[i]);

            spriteDic.Add(keyString[i], asd);
        }

            CreateSpriteLibraryAsset(spriteDic);

    }

    List<Sprite> FindSpritesByName(string nameToFind)
    {
        List<Sprite> foundSprites = new List<Sprite>();

        foreach (Sprite sprite in sprites)
        {
            if (sprite.name.Contains(nameToFind))
            {
                foundSprites.Add(sprite);
            }
        }

        return foundSprites;
    }
    public void CreateSpriteLibraryAsset(Dictionary<string, List<Sprite>> _spriteDic)
    {
        // Sprite Library Asset�� �����մϴ�.
        SpriteLibraryAsset spriteLibrary = ScriptableObject.CreateInstance<SpriteLibraryAsset>();
        
        // �̸��� �����մϴ�.
        spriteLibrary.name = "MySpriteLibrary";
        foreach (var dic in _spriteDic)
        {
            for (int i = 0; i < dic.Value.Count; i++)
            {
                spriteLibrary.AddCategoryLabel(dic.Value[i], dic.Key, i.ToString());
            }
        }
        // Sprite Library Asset�� ������Ʈ�� �����մϴ�.
        AssetDatabase.CreateAsset(spriteLibrary, $"Assets/Resoures/{m_name}.asset");
        AssetDatabase.SaveAssets();
    }
}
#endif
