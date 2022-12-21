using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TK.Rendering.PostFX
{
    public class LightShaftRenderFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class Settings
        {
            public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            [HideInInspector] public Shader shader;
        }

        public Settings settings = new Settings();

        private LightShaftPass _pass;

        public override void Create()
        {
            this.name = "LightShaftPass";
            if (settings.shader == null)
            {

                settings.shader = Shader.Find("TK/PostFX/LightShaft");
            }
            _pass = new LightShaftPass(settings.renderPassEvent, settings.shader);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _pass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(_pass);
        }
    }
}