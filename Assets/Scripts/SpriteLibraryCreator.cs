using UnityEngine;
using UnityEditor;
using UnityEngine.U2D.Animation;
using System.Collections.Generic;

#if UNITY_EDITOR
//스프라이트 정보로 서브젝테이블 오브젝트로 만들어주는 스크립트
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
        // Sprite Library Asset을 생성합니다.
        SpriteLibraryAsset spriteLibrary = ScriptableObject.CreateInstance<SpriteLibraryAsset>();
        
        // 이름을 설정합니다.
        spriteLibrary.name = "MySpriteLibrary";
        foreach (var dic in _spriteDic)
        {
            for (int i = 0; i < dic.Value.Count; i++)
            {
                spriteLibrary.AddCategoryLabel(dic.Value[i], dic.Key, i.ToString());
            }
        }
        // Sprite Library Asset을 프로젝트에 저장합니다.
        AssetDatabase.CreateAsset(spriteLibrary, $"Assets/Resoures/{m_name}.asset");
        AssetDatabase.SaveAssets();
    }
}
#endif
