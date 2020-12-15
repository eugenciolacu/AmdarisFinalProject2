using AmdarisInternship.API.Controllers;
using AmdarisInternship.API.Dtos.ModuleDtos;
using AmdarisInternship.API.Dtos.ModuleGradingDtos;
using AmdarisInternship.API.Dtos.ModuleWithModuleGradingTdos;
using AmdarisInternship.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AmdarisInternship.API.Tests
{
    [TestClass]
    public class ModuleControllerUnitTests
    {
        private IList<ModuleWithModuleGradingDto> _moduleWithModuleGradingsList;

        private Mock<IModuleModuleGradingsService> _mockModuleModuleGradingsService;

        private ModuleController _moduleController;

        private Mock<IMapper> _mockMapper;


        [TestInitialize]
        public void Initializer()
        {
            _mockModuleModuleGradingsService = new Mock<IModuleModuleGradingsService>();

            _mockMapper = new Mock<IMapper>();

            _moduleController = new ModuleController(_mockModuleModuleGradingsService.Object, _mockMapper.Object);

            _moduleWithModuleGradingsList = new List<ModuleWithModuleGradingDto>
            {
                new ModuleWithModuleGradingDto
                {
                    Module = new ModuleDto
                    {
                        Id = 1,
                        Name = "Module 1 Test"
                    },

                    ModuleGradings = new List<ModuleGradingDto>
                    {
                        new ModuleGradingDto
                        {
                            Id = 1,
                            Name = "Quiz 1 Test",
                            Weight = 0.5f,
                            ModuleId = 1
                        },

                        new ModuleGradingDto
                        {
                            Id = 2,
                            Name = "Quiz 2 Test",
                            Weight = 0.5f,
                            ModuleId = 1
                        }
                    }
                },

                new ModuleWithModuleGradingDto
                {
                    Module = new ModuleDto
                    {
                        Id = 2,
                        Name = "Module 2 Test"
                    },

                    ModuleGradings = new List<ModuleGradingDto>
                    {
                        new ModuleGradingDto
                        {
                            Id = 3,
                            Name = "Theory 1 Test",
                            Weight = 0.1f,
                            ModuleId = 2
                        },

                        new ModuleGradingDto
                        {
                            Id = 4,
                            Name = "Theory 2 Test",
                            Weight = 0.1f,
                            ModuleId = 2
                        },

                        new ModuleGradingDto
                        {
                            Id = 5,
                            Name = "Practice Test",
                            Weight = 0.6f,
                            ModuleId = 2
                        },

                        new ModuleGradingDto
                        {
                            Id = 6,
                            Name = "Quiz Test",
                            Weight = 0.2f,
                            ModuleId = 2
                        }
                    }
                },

                new ModuleWithModuleGradingDto
                {
                    Module = new ModuleDto
                    {
                        Id = 3,
                        Name = "Module 1 Test"
                    },

                    ModuleGradings = new List<ModuleGradingDto>
                    {
                        new ModuleGradingDto
                        {
                            Id = 7,
                            Name = "Quiz Test",
                            Weight = 1f,
                            ModuleId = 3
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void GetTest()
        {
            var expected = _moduleWithModuleGradingsList;
            _mockModuleModuleGradingsService.Setup(x => x.GetModulesWithModuleGradings()).Returns(expected);

            var modulesWithModuleGradings = _moduleController.Get() as OkObjectResult;
            var result = modulesWithModuleGradings.Value as List<ModuleWithModuleGradingDto>;

            Assert.AreEqual(modulesWithModuleGradings.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0].ModuleGradings.Count(), 2);
            Assert.AreEqual(result[1].ModuleGradings.Count(), 4);
            Assert.AreEqual(result[2].ModuleGradings.Count(), 1);
        }

        [TestMethod]
        [DataRow(1)]
        public void GetTestWithId(int id)
        {
            var expected = _moduleWithModuleGradingsList.FirstOrDefault(x => x.Module.Id == id);
            _mockModuleModuleGradingsService.Setup(x => x.GetModuleWithModuleGradingsByModuleId(It.IsAny<int>())).Returns(expected);

            var modulesWithModuleGradings = _moduleController.Get(id) as OkObjectResult;
            var result = modulesWithModuleGradings.Value as ModuleWithModuleGradingDto;

            Assert.AreEqual(modulesWithModuleGradings.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual(result.Module.Name, _moduleWithModuleGradingsList[0].Module.Name);
            Assert.AreEqual(result.ModuleGradings.Count, _moduleWithModuleGradingsList[0].ModuleGradings.Count);
            Assert.AreEqual(result.ModuleGradings[0].Name, _moduleWithModuleGradingsList[0].ModuleGradings[0].Name);
            Assert.AreEqual(result.Module.Id, _moduleWithModuleGradingsList[0].ModuleGradings[0].ModuleId);
        }

        [TestMethod]
        public void PostTest()
        {
            var newModuleWithModuleGrading = new ModuleWithModuleGradingDto
            {
                Module = new ModuleDto
                {
                    Id = 4,
                    Name = "Module 4 Test"
                },

                ModuleGradings = new List<ModuleGradingDto>
                {
                    new ModuleGradingDto
                    {
                        Id = 8,
                        Name = "Quiz Test",
                        Weight = 1f,
                        ModuleId = 4
                    }
                }
            };

            _mockModuleModuleGradingsService.Setup(x => x.AddNewModuleWithModuleGrading(It.IsAny<ModuleWithModuleGradingDto>())).Returns(newModuleWithModuleGrading);

            var moduleWithModuleGradings = _moduleController.Post(newModuleWithModuleGrading) as ObjectResult;
            var result = moduleWithModuleGradings.Value as ModuleWithModuleGradingDto;

            Assert.AreEqual(moduleWithModuleGradings.StatusCode, (int)HttpStatusCode.Created);
            Assert.AreEqual(newModuleWithModuleGrading.Module.Name, result.Module.Name);
            Assert.AreEqual(newModuleWithModuleGrading.ModuleGradings.Count, result.ModuleGradings.Count);
            Assert.AreEqual(newModuleWithModuleGrading.ModuleGradings[0].Name, result.ModuleGradings[0].Name);
            Assert.AreEqual(newModuleWithModuleGrading.ModuleGradings[0].ModuleId, result.Module.Id);
        }

        [TestMethod]
        [DataRow(2)]
        public void PutTest(int id)
        {
            var moduleWithModuleGradingToUpdate = new ModuleWithModuleGradingDto
            {
                Module = new ModuleDto
                {
                    Id = 2,
                    Name = "xxxxxxxxx"
                },

                ModuleGradings = new List<ModuleGradingDto>
                {
                    new ModuleGradingDto
                    {
                        Id = 3,
                        Name = "Quiz 1",
                        Weight = 0.5f,
                        ModuleId = 2
                    },

                    new ModuleGradingDto
                    {
                        Id = 0,
                        Name = "Quiz 2",
                        Weight = 0.5f,
                        ModuleId = 2
                    }
                }
            };

            _mockModuleModuleGradingsService.Setup(x => x.UpdateModuleWithModuleGrading(id, It.IsAny<ModuleWithModuleGradingDto>())).Returns(moduleWithModuleGradingToUpdate);

            var moduleWithModuleGradings = _moduleController.Put(id, moduleWithModuleGradingToUpdate) as ObjectResult;
            var result = moduleWithModuleGradings.Value as ModuleWithModuleGradingDto;

            Assert.AreEqual(moduleWithModuleGradings.StatusCode, (int)HttpStatusCode.Created);
            Assert.AreEqual(moduleWithModuleGradingToUpdate.Module.Name, result.Module.Name);
            Assert.AreEqual(moduleWithModuleGradingToUpdate.ModuleGradings.Count, result.ModuleGradings.Count);
            Assert.AreEqual(moduleWithModuleGradingToUpdate.ModuleGradings[0].Name, result.ModuleGradings[0].Name);
            Assert.AreEqual(moduleWithModuleGradingToUpdate.ModuleGradings[0].ModuleId, result.Module.Id);
        }
    }
}
