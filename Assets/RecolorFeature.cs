using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace RecolorURP {

sealed class RecolorPass : ScriptableRenderPass
{
    public Material material;

    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        if (material == null) return;

        var camera = data.cameraData.camera;
        if (camera.GetComponent<RecolorController>() == null) return;

        var cmd = CommandBufferPool.Get("Recolor");
        Blit(cmd, ref data, material, 0);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}

public sealed class RecolorFeature : ScriptableRendererFeature
{
    [HideInInspector, SerializeField] Material material = null;

    RecolorPass _pass;

    public override void Create()
      => _pass = new RecolorPass
           { material = material,
             renderPassEvent = RenderPassEvent.AfterRendering };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace RecolorURP
