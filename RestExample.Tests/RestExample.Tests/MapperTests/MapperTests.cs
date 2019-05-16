using AutoMapper;
using NUnit.Framework;
using RestExample.Config;

namespace RestExample.Tests.MapperTests
{
    public class MapperTests
    {
        [Test]
        public void ValidateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(typeof(RestExampleMapper)));

            mapperConfig.AssertConfigurationIsValid();
        }
    }
}
