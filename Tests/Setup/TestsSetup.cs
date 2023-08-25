using AutoMapper;
using Moq;

namespace Tests.Setup;

public class TestsSetup : IDisposable
{
    #region Mock AutoMapper

    protected readonly IMapper AutoMapperMock;

    #endregion

    public TestsSetup()
    {
        var autoMapperProfile = new Application.AutoMapper.AutoMapper();
        var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
        AutoMapperMock = new Mapper(configuration);
    }

    protected static Mock<T> CriarInstancia<T>() where T : class
    {
        return new Mock<T>();
    }

    public void Dispose()
    {
    }
}