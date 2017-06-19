﻿using System;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    // TODO: This class should possibly not exist, reference textures directly.

    /// <summary>
    /// Represents a reference to a <see cref="Texture"/> instance by name.
    /// </summary>
    [DebuggerDisplay(nameof(TextureRef) + " {" + nameof(Name) + "}")]
    public class TextureRef : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="ResDict{TextureRef}"/> instances. Typically the same as the <see cref="Texture.Name"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The referenced <see cref="Texture"/> instance.
        /// </summary>
        public Texture Texture { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            Name = loader.LoadString();
            Texture = loader.Load<Texture>();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.SaveString(Name);
            saver.Save(Texture);
        }
    }
}