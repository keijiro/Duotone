using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace Duotone {

sealed class DuotonePass : ScriptableRenderPass
{
    class PassData { public DuotoneController Controller { get; set; } }

    public override void RecordRenderGraph(RenderGraph graph,
                                           ContextContainer context)
    {
        // DuotoneController component reference
        var camera = context.Get<UniversalCameraData>().camera;
        var ctrl = camera.GetComponent<DuotoneController>();
        if (ctrl == null || !ctrl.enabled) return;

        // Not supported: Back buffer source
        var resource = context.Get<UniversalResourceData>();
        if (resource.isActiveTargetBackBuffer) return;

        // Destination texture allocation
        var source = resource.activeColorTexture;
        var desc = graph.GetTextureDesc(source);
        desc.name = "Duotone";
        desc.clearBuffer = false;
        desc.depthBufferBits = 0;
        var dest = graph.CreateTexture(desc);

        // Blit
        var param = new RenderGraphUtils.
          BlitMaterialParameters(source, dest, ctrl.Material, 0);
        graph.AddBlitPass(param, passName: "Duotone");

        // Destination texture as the camera texture
        resource.cameraColor = dest;
    }
}

public sealed class DuotoneFeature : ScriptableRendererFeature
{
    DuotonePass _pass;
    [SerializeField] RenderPassEvent _passEvent = RenderPassEvent.AfterRenderingPostProcessing;

    public override void Create()
      => _pass = new DuotonePass { renderPassEvent = _passEvent };

    public override void AddRenderPasses(ScriptableRenderer renderer,
                                         ref RenderingData data)
      => renderer.EnqueuePass(_pass);
}

} // namespace Duotone
