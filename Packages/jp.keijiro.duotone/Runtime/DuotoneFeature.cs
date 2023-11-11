using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Duotone {

sealed class DuotonePass : ScriptableRenderPass
{
    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        var target = data.cameraData.camera.GetComponent<DuotoneController>();
        if (target == null || !target.enabled) return;

        var cmd = CommandBufferPool.Get("Duotone");
        Blit(cmd, ref data, target.Material);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}

public sealed class DuotoneFeature : ScriptableRendererFeature
{
    DuotonePass _pass;

    public override void Create()
      => _pass = new DuotonePass
           { renderPassEvent = RenderPassEvent.AfterRendering };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace Duotone
