//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-12-07 23:57:06.895
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    /// <summary>
    /// 实体表。
    /// </summary>
    public class DRCameraSetting : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取实体编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取左边界。
        /// </summary>
        public float MinX
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取右边界。
        /// </summary>
        public float MaxX
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取下边界。
        /// </summary>
        public float MinY
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取边界宽。
        /// </summary>
        public float BorderSize
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            MinX = float.Parse(columnStrings[index++]);
            MaxX = float.Parse(columnStrings[index++]);
            MinY = float.Parse(columnStrings[index++]);
            BorderSize = float.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    MinX = binaryReader.ReadSingle();
                    MaxX = binaryReader.ReadSingle();
                    MinY = binaryReader.ReadSingle();
                    BorderSize = binaryReader.ReadSingle();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
