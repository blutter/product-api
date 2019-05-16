using AutoMapper;
using NUnit.Framework;
using RestExample.Config;

namespace RestExampleApi.Tests.MapperTests
{
    public class MapperTests
    {
        [Test]
        public void ValidateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(typeof(RestExampleApiMapper)));

            mapperConfig.AssertConfigurationIsValid();
        }
    }
}
