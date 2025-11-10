namespace Facade;

public class CastingFacade(DeviceExplorer deviceExplorer)
{
    public async Task CastAsync(Guid deviceId, Guid videoId)
    {
        // Get hold of the Smart TV device
        IDevice device = await deviceExplorer.GetAsync(deviceId);

        if (device is not SmartTVDevice smartTVDevice)
        {
            throw new Exception("Smart TV not found");
        }

        // Get hold of a connection to the Smart TV
        Connection connection;

        try
        {
            connection = await smartTVDevice.ConnectionAsync();
        }
        catch
        {
            // (Turn on TV if creating connection fails)
            connection = await smartTVDevice.TurnOnAsync();
            await Task.Delay(2000);
        }

        // Launch the YouTube app
        IApp app = await connection.LaunchAppAsync("com.google.youtube");

        if (app is not YouTubeApp youTubeApp)
        {
            throw new Exception("Failed opening YouTube app");
        }

        // Start the video
        await youTubeApp.PlayAsync(videoId);
    }
}