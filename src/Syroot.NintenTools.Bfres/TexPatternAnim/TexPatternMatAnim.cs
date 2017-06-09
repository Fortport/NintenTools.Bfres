﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a texture pattern material animation in a <see cref="TexPatternAnim"/> subfile.
    /// </summary>
    [DebuggerDisplay(nameof(TexPatternMatAnim) + " {" + nameof(Name) + "}")]
    public class TexPatternMatAnim : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        internal int BeginCurve { get; set; }

        internal int BeginPatAnim { get; set; }

        internal string Name { get; set; }

        internal IList<PatternAnimInfo> PatternAnimInfos { get; private set; }

        /// <summary>
        /// Gets <see cref="AnimCurve"/> instances animating properties of objects stored in this section.
        /// </summary>
        public IList<AnimCurve> Curves { get; private set; }

        public IList<ushort> BaseDataList { get; private set; } // Flags?
        
        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            TexPatternMatAnimHead head = new TexPatternMatAnimHead(loader);
            using (loader.TemporarySeek())
            {
                BeginCurve = head.BeginCurve;
                BeginPatAnim = head.BeginPatAnim;
                Name = loader.GetName(head.OfsName);
                PatternAnimInfos = loader.LoadList<PatternAnimInfo>(head.OfsPatAnimInfoList, head.NumPatAnim);
                Curves = loader.LoadList<AnimCurve>(head.OfsCurveList, head.NumCurve);

                if (head.OfsBaseDataList != 0)
                {
                    loader.Position = head.OfsBaseDataList;
                    BaseDataList = loader.ReadUInt16s(head.NumPatAnim);
                }
            }
        }

        void IResData.Reference(ResFileLoader loader)
        {
        }
    }

    /// <summary>
    /// Represents the header of a <see cref="TexPatternMatAnim"/> instance.
    /// </summary>
    internal class TexPatternMatAnimHead
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        internal ushort NumPatAnim;
        internal ushort NumCurve;
        internal int BeginCurve;
        internal int BeginPatAnim;
        internal uint OfsName;
        internal uint OfsPatAnimInfoList;
        internal uint OfsCurveList;
        internal uint OfsBaseDataList;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal TexPatternMatAnimHead(ResFileLoader loader)
        {
            NumPatAnim = loader.ReadUInt16();
            NumCurve = loader.ReadUInt16();
            BeginCurve = loader.ReadInt32();
            BeginPatAnim = loader.ReadInt32();
            OfsName = loader.ReadOffset();
            OfsPatAnimInfoList = loader.ReadOffset();
            OfsCurveList = loader.ReadOffset();
            OfsBaseDataList = loader.ReadOffset();
        }
    }
}