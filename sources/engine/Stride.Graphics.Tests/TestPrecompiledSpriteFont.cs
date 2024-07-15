// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xunit;

namespace Stride.Graphics.Tests
{
    /// <summary>
    /// DEPRECATED. Precompiled fonts are not supported anymore and will be merged as a feature of the other fonts (Offline/SDF) soon
    /// </summary>
    public class TestPrecompiledSpriteFont : TestSpriteFont
    {
        public TestPrecompiledSpriteFont()
            : base("PrecompiledFonts/", "pre")
        {
        }

        /// <summary>
        /// Run the test
        /// </summary>
        [Fact(Skip = "Only run when working on graphic related changes and tests, otherwise results are results are not deterministic and inconsistent between hardware. " +
            "TODO: Remove this skip when we get teamcity agent functional again.")]
        public void RunTestPrecompiledSpriteFont()
        {
            RunGameTest(new TestPrecompiledSpriteFont());
        }
    }
}
