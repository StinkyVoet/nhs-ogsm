using nhs_ogsm.Data;

namespace nhs_ogsm.Services;

public class StateContainerService
{
    public event Action? OnSidebarStateChange;
    
    public void NotifyStateChanged() => OnSidebarStateChange?.Invoke();
}