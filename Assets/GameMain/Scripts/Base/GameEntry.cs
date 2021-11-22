//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarForce
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        private static void AlwayLoadLauncher()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            SceneManager.LoadScene(0);
        }
        
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
        }
    }
}
