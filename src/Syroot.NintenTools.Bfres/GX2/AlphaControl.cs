﻿using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres.GX2
{
    /// <summary>
    /// Represents GX2 settings controlling additional alpha blending options.
    /// </summary>
    public class AlphaControl
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _alphaFuncBit = 0, _alphaFuncBits = 3;
        private const int _alphaFuncEnabledBit = 3;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaControl"/> class.
        /// </summary>
        public AlphaControl()
        {
        }

        internal AlphaControl(uint value, float refValue)
        {
            Value = value;
            RefValue = refValue;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets a value indicating whether alpha testing is enabled at all.
        /// </summary>
        public bool AlphaTestEnabled
        {
            get { return Value.GetBit(_alphaFuncEnabledBit); }
            set { Value = Value.SetBit(_alphaFuncEnabledBit, value); }
        }

        /// <summary>
        /// Gets or sets the comparison functions to use for alpha testing.
        /// </summary>
        public GX2CompareFunction AlphaFunc
        {
            get { return (GX2CompareFunction)Value.Decode(_alphaFuncBit, _alphaFuncBits); }
            set { Value = Value.Encode((uint)value, _alphaFuncBit, _alphaFuncBits); }
        }

        /// <summary>
        /// Gets or sets the reference value used for alpha testing.
        /// </summary>
        public float RefValue { get; set; }

        internal uint Value { get; set; }
    }
}