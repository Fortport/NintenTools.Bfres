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
        
        /// <summary>
        /// Gets the name of the animated <see cref="Material"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the list of <see cref="PatternAnimInfo"/> instances.
        /// </summary>
        public IList<PatternAnimInfo> PatternAnimInfos { get; private set; }

        /// <summary>
        /// Gets <see cref="AnimCurve"/> instances animating properties of objects stored in this section.
        /// </summary>
        public IList<AnimCurve> Curves { get; private set; }

        /// <summary>
        /// Gets the initial <see cref="PatternAnimInfo"/> indices.
        /// </summary>
        public IList<ushort> BaseDataList { get; private set; }
        
        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            TexPatternMatAnimHead head = new TexPatternMatAnimHead(loader);
            using (loader.TemporarySeek())
            {
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
        internal int BeginCurve; // First curve index relative to all.
        internal int BeginPatAnim; // First PatternAnimInfo index relative to all.
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