using nhs_ogsm.Data;

namespace nhs_ogsm.Services;

public class StateContainerService
{
    public event Action? OnSidebarStateChange;
    
    public void NotifyStateChanged() {
        Console.WriteLine("State Service Notified");
        OnSidebarStateChange?.Invoke();
    }
}