using AmdarisInternship.API.Dtos.ModuleDtos;
using AmdarisInternship.API.Dtos.ModuleGradingDtos;
using AmdarisInternship.API.Dtos.ModuleWithModuleGradingTdos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Tests
{
    [TestClass]
    public class ModuleControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public ModuleControllerIntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [TestMethod]
        public async Task ShouldAccess_Get()
        {
            var request = "/api/Module";
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<List<ModuleWithModuleGradingDto>>(jsonFromPostResponse);
            Assert.IsTrue(obj.Count == 6);
        }

        [TestMethod]
        public async Task ShouldAccess_GetById()
        {
            var request = "/api/Module/1";
            var response = await _client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<ModuleWithModuleGradingDto>(jsonFromPostResponse);
                Assert.AreEqual(obj.Module.Name, "Module 1");
                Assert.IsTrue(obj.ModuleGradings.Count == 2);
            }
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task ShouldAccess_Post()
        {
            var request = new
            {
                Url = "/api/Module",
                Body = new
                {
                    module = new ModuleDto
                    {
                        Name = "Test"
                    },

                    moduleGradings = new List<ModuleGradingDto>
                    {
                        new ModuleGradingDto
                        {
                            Name = "Quiz 1",
                            Weight = 0.5f,
                            ModuleId = 10
                        },
                        new ModuleGradingDto
                        {
                            Name = "Quiz 2",
                            Weight = 0.5f,
                            ModuleId = 10
                        }
                    }
                }
            };

            var postResponse = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            postResponse.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task ShouldAccess_Put()
        {
            var request = new
            {
                Url = "/api/Module/1",
                Body = new
                {
                    module = new ModuleDto
                    {
                        Id = 1,
                        Name = "xxxxx"
                    },

                    moduleGradings = new List<ModuleGradingDto>
                    {
                        new ModuleGradingDto
                        {
                            Id = 1,
                            Name = "Quiz 1",
                            Weight = 0.5f,
                            ModuleId = 1
                        },
                        new ModuleGradingDto
                        {
                            Id = 2,
                            Name = "Quiz 2",
                            Weight = 0.5f,
                            ModuleId = 1
                        }
                    }
                }
            };

            var putResponse = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            putResponse.EnsureSuccessStatusCode();
        }
    }
}
