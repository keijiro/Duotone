using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace RecolorURP {

sealed class RecolorPass : ScriptableRenderPass
{
    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        var target = data.cameraData.camera.GetComponent<RecolorController>();
        if (target == null) return;

        var cmd = CommandBufferPool.Get("Recolor");
        Blit(cmd, ref data, target.Material);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}

public sealed class RecolorFeature : ScriptableRendererFeature
{
    RecolorPass _pass;

    public override void Create()
      => _pass = new RecolorPass
           { renderPassEvent = RenderPassEvent.AfterRendering };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace RecolorURP
