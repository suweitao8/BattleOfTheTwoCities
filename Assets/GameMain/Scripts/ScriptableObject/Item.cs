using UnityEngine;

namespace GameMain
{
    [CreateAssetMenu(menuName = "GameMain/Item")]
    public class Item : ScriptableObject
    {
        [Header("Item")]
        public string itemName;
        public Sprite itemIcon;
    }
}