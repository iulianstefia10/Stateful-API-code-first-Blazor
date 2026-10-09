using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Stateful_API_Code_first.Profiles;

namespace MSTest_Test_Project
{
    [TestClass]
    public class AutoMapperTests
    {
        [TestMethod]
        public void AutoMapper_Configuration_IsValid()
        {
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SeriesProfile>());

            
            config.AssertConfigurationIsValid();
        }
    }
}