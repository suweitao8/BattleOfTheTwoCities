//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace GameMain
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        public static PlayerInputComponent PlayerInput
        {
            get;
            private set;
        }

        public static FocusElementComponent FocusElement
        {
            get;
            private set;
        }

        public static LayerComponent Layer
        {
            get;
            private set;
        }
        
        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            PlayerInput = UnityGameFramework.Runtime.GameEntry.GetComponent<PlayerInputComponent>();
            FocusElement = UnityGameFramework.Runtime.GameEntry.GetComponent<FocusElementComponent>();
            Layer = UnityGameFramework.Runtime.GameEntry.GetComponent<LayerComponent>();
        }
    }
}
