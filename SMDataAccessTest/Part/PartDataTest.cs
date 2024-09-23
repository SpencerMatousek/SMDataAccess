using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SMDataAccess.Data.IData;
using SMDataAccess;

namespace SMDataAccessTest.Part;

[TestClass]
public class PartDataTest
{
    public readonly IPartRepository _partRepository;
    public PartDataTest()
    {
        ServiceCollection services = new();

        var config = Initiate.InitConfiguration();
        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException();
        services.AddDependencyServices(connectionString);
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
            // Add other logging providers as needed
        });

        var provider = services.BuildServiceProvider();
        _partRepository = provider.GetService<IPartRepository>() ?? throw new ArgumentNullException();
    }

    [TestMethod]
    public async Task GetParts()
    {
        var response = await _partRepository.GetPaginatedAsync(1, 15);
        Assert.IsTrue(response.IsSuccessful);
    }
}
