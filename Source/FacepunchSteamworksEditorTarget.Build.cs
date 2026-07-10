using Flax.Build;

public class FacepunchSteamworksEditorTarget : GameProjectEditorTarget
{
#if BUILD_RELEASE
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for editor
        Modules.Add("FacepunchSteamworks");
        Modules.Add("FacepunchSteamworksEditor");
    }
#endif
}
