using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

class RecolorPass : ScriptableRenderPass
{
    Material _material;

    public override void Execute
      (ScriptableRenderContext context, ref RenderingData data)
    {
        if (_material == null) return;
        var cmd = data.commandBuffer;
        Blit(cmd, ref data, _material, 0);
    }

    public void Dispose()
    {
    }
}

public partial class RecolorFeature : ScriptableRendererFeature
{
    RecolorPass _pass;

    public override void Create()
      => _pass = new RecolorPass();

    protected override void Dispose(bool disposing)
      => _pass.Dispose();

    public override void AddRenderPasses
          (ScriptableRenderer renderer, ref RenderingData data)
    {
        _pass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        _pass.SetupMembers(_material, 0, true, false);
        renderer.EnqueuePass(_pass);
    }
}
