﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FMDL subfile in a <see cref="ResFile"/>, storing model vertex data, skeletons and used materials.
    /// </summary>
    [DebuggerDisplay(nameof(Model) + " {" + nameof(Name) + "}")]
    public class Model : INamedResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FMDL";

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private string _name;

        // ---- EVENTS -------------------------------------------------------------------------------------------------

        /// <summary>
        /// Raised when the <see cref="Name"/> property was changed.
        /// </summary>
        public event EventHandler NameChanged;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="INamedResDataList{Model}"/> instances.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (_name != value)
                {
                    _name = value;
                    NameChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the path of the file which originally supplied the data of this instance.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the <see cref="Skeleton"/> instance to deform the model with animations.
        /// </summary>
        public Skeleton Skeleton { get; set; }

        /// <summary>
        /// Gets the <see cref="VertexBuffer"/> instances storing the vertex data used by the <see cref="Shapes"/>.
        /// </summary>
        public IList<VertexBuffer> VertexBuffers { get; private set; }

        /// <summary>
        /// Gets the <see cref="Shape"/> instances forming the surface of the model.
        /// </summary>
        public INamedResDataList<Shape> Shapes { get; private set; }

        /// <summary>
        /// Gets the <see cref="Material"/> instance applied on the <see cref="Shapes"/> to color their surface.
        /// </summary>
        public INamedResDataList<Material> Materials { get; private set; }

        /// <summary>
        /// Gets customly attached <see cref="UserData"/> instances.
        /// </summary>
        public INamedResDataList<UserData> UserData { get; private set; }

        public uint TotalVertices
        {
            get { return 0x1234567; } // TODO: Compute total vertices.
        }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            loader.CheckSignature(_signature);
            Name = loader.LoadString();
            Path = loader.LoadString();
            Skeleton = loader.Load<Skeleton>();
            uint ofsVertexBuffers = loader.ReadOffset();
            Shapes = loader.LoadDictList<Shape>();
            Materials = loader.LoadDictList<Material>();
            UserData = loader.LoadDictList<UserData>();
            ushort numVertexBuffer = loader.ReadUInt16();
            ushort numShape = loader.ReadUInt16();
            ushort numMaterial = loader.ReadUInt16();
            ushort numUserData = loader.ReadUInt16();
            uint totalVertices = loader.ReadUInt32();
            uint userPointer = loader.ReadUInt32();

            VertexBuffers = loader.LoadList<VertexBuffer>(numVertexBuffer, ofsVertexBuffers);
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.SaveString(Path);
            saver.Save(Skeleton);
            saver.SaveList(VertexBuffers);
            saver.SaveDictList(Shapes);
            saver.SaveDictList(Materials);
            saver.SaveDictList(UserData);
            saver.Write((ushort)VertexBuffers.Count);
            saver.Write((ushort)Shapes.Count);
            saver.Write((ushort)Materials.Count);
            saver.Write((ushort)UserData.Count);
            saver.Write(TotalVertices);
            saver.Write(0); // UserPointer
        }
    }
}