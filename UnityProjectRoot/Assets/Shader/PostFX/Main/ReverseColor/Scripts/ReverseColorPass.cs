using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TK.Rendering.PostFX
{
    public class ReverseColorPass : CustomPostProcessingPass<ReverseColor>
    {

        public ReverseColorPass(RenderPassEvent renderPassEvent, Shader shader) : base(renderPassEvent, shader)
        {
        }

        protected override string RenderTag => "ReverseColor";

        protected override void BeforeRender(CommandBuffer commandBuffer, ref RenderingData renderingData)
        {
        }

        protected override void Render(CommandBuffer commandBuffer, ref RenderingData renderingData, RenderTargetIdentifier source,
            RenderTargetIdentifier dest)
        {
            commandBuffer.Blit(source, dest, Material);
        }

        protected override bool IsActive()
        {
            return Component.IsActive;
        }
    }
}
