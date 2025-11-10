// client code
using Facade;

var castingFacade = new CastingFacade(
    new DeviceExplorer());

await castingFacade.CastAsync(
    deviceId: Guid.NewGuid(),
    videoId: Guid.NewGuid());