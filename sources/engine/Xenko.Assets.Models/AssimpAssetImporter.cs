// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using Xenko.Core.Assets;
using Xenko.Core;
using Xenko.Core.Diagnostics;
using Xenko.Core.IO;
using Xenko.Animations;
using Xenko.Assets.Textures;
using Xenko.Importer.Common;

namespace Xenko.Assets.Models
{
    public class AssimpAssetImporter : ModelAssetImporter
    {
        static AssimpAssetImporter()
        {
            NativeLibrary.PreloadLibrary("assimp-vc120-mt.dll");
        }

        // Supported file extensions for this importer
        private const string FileExtensions = ".dae;.3ds;.obj;.blend;.x;.md2;.md3;.dxf;.ply";

        private static readonly Guid Uid = new Guid("30243FC0-CEC7-4433-977E-95DCA29D846E");

        public override Guid Id => Uid;

        public override string Description => "Assimp importer used for creating entities, 3D Models or animations assets";

        public override string SupportedFileExtensions => FileExtensions;

        /// <inheritdoc/>
        public override EntityInfo GetEntityInfo(UFile localPath, Logger logger, AssetImporterParameters importParameters)
        {
            var meshConverter = new Importer.AssimpNET.MeshConverter(logger);
            var entityInfo = meshConverter.ExtractEntity(localPath.FullPath, null, importParameters.IsTypeSelectedForOutput(typeof(TextureAsset)));
            return entityInfo;
        }

        /// <inheritdoc/>
        public override void GetAnimationDuration(UFile localPath, Logger logger, AssetImporterParameters importParameters, out TimeSpan startTime, out TimeSpan endTime)
        {
            var meshConverter = new Importer.AssimpNET.MeshConverter(logger);
            var sceneData = meshConverter.ConvertAnimation(localPath.FullPath, "");

            startTime = CompressedTimeSpan.MaxValue; // This will go down, so we start from positive infinity
            endTime = CompressedTimeSpan.MinValue;   // This will go up, so we start from negative infinity

            foreach (var animationClip in sceneData.AnimationClips)
            {
                foreach (var animationCurve in animationClip.Value.Curves)
                {
                    foreach (var compressedTimeSpan in animationCurve.Keys)
                    {
                        if (compressedTimeSpan < startTime)
                            startTime = compressedTimeSpan;
                        if (compressedTimeSpan > endTime)
                            endTime = compressedTimeSpan;
                    }
                }
            }

            if (startTime == CompressedTimeSpan.MaxValue)
                startTime = CompressedTimeSpan.Zero;
            if (endTime == CompressedTimeSpan.MinValue)
                endTime = CompressedTimeSpan.Zero;
        }
    }
}
