﻿using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a parameter animation info in a <see cref="ShaderParamMatAnim"/> instance.
    /// </summary>
    [DebuggerDisplay(nameof(ParamAnimInfo) + " {" + nameof(Name) + "}")]
    public class ParamAnimInfo : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the index of the first <see cref="AnimCurve"/> instance in the parent
        /// <see cref="ShaderParamMatAnim"/>.
        /// </summary>
        public ushort BeginCurve { get; set; }

        public ushort FloatCurveCount { get; set; }

        public ushort IntCurveCount { get; set; }

        /// <summary>
        /// Gets or sets the index of the first <see cref="AnimConstant"/> instance in the parent
        /// <see cref="ShaderParamMatAnim"/>.
        /// </summary>
        public ushort BeginConstant { get; set; }

        /// <summary>
        /// Gets or sets the number of <see cref="AnimConstant"/> instances used in the parent
        /// <see cref="ShaderParamMatAnim"/>.
        /// </summary>
        public ushort ConstantCount { get; set; }

        /// <summary>
        /// Gets or sets the index of the <see cref="ShaderParam"/> in the <see cref="Material"/>.
        /// </summary>
        public ushort SubBindIndex { get; set; }

        /// <summary>
        /// Gets the name of the animated <see cref="ShaderParam"/>.
        /// </summary>
        public string Name { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            BeginCurve = loader.ReadUInt16();
            FloatCurveCount = loader.ReadUInt16();
            IntCurveCount = loader.ReadUInt16();
            BeginConstant = loader.ReadUInt16();
            ConstantCount = loader.ReadUInt16();
            SubBindIndex = loader.ReadUInt16();
            Name = loader.LoadString();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
        }
    }
}
