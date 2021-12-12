using UnityEngine;

namespace GameMain
{
    [CreateAssetMenu(menuName = "GameMain/Item", fileName = "Item", order = 1)]
    public class Item : ScriptableObject
    {
        [Header("Item")]
        public string itemName;
        public Sprite itemIcon;
    }
}