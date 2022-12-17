using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TK.Rendering.PostFX
{
    public class ColorScaleRenderFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class Settings
        {
            public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            [HideInInspector]public Shader shader;
        }

        public Settings settings = new Settings();

        private ColorScalePass _pass;

        public override void Create()
        {
            this.name = "ColorScale";
            if (settings.shader == null)
            {
                settings.shader = Shader.Find("TK/PostFX/ColorScale");

            }
            _pass = new ColorScalePass(settings.renderPassEvent, settings.shader);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _pass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(_pass);
        }
    }
}
