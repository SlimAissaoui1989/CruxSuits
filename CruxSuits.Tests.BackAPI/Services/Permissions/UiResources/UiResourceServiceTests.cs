using AutoMapper;
using CruxIT.Library.API.Services;
using CruxSuits.BackAPI.Services.Permissions.UiResources;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared.Permissions.UiResources;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.BackAPI.Services.Permissions.UiResources
{
    public class UiResourceServiceTests
    {
        private readonly Mock<ICruxSuitsUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILogger<UiResourceService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UiResourceService _uiResourceService;

        public UiResourceServiceTests()
        {
            _mockUnitOfWork = new Mock<ICruxSuitsUnitOfWork>();
            _mockLogger = new Mock<ILogger<UiResourceService>>();
            _mockMapper = new Mock<IMapper>();
            _uiResourceService = new UiResourceService(_mockUnitOfWork.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetSubUiRecourceByUser_ShouldReturnList_When_UiResourceKeyIsValid()
        {
            List<UiResourcePermissionResponse> permissionsByUser = new()
            {
                new(){Id=101, CanEdit=true, CanSee = true, UiResourceId = 1001},
            };

            List<UiResourcePermissionResponse> permissionsByRole = new()
            {
                new(){Id=202, CanEdit=false, CanSee = true, UiResourceId = 1002},
            };

            _mockUnitOfWork.Setup(s => s.UserUiResourcePermission.GetAllAsync<UiResourcePermissionResponse>(It.IsAny<Expression<Func<UserUiResourcePermission, bool>>>()))
                .ReturnsAsync(permissionsByUser);

            _mockUnitOfWork.Setup(s => s.RoleUiResourcePermission.GetAllAsync<UiResourcePermissionResponse>(It.IsAny<Expression<Func<RoleUiResourcePermission, bool>>>()))
                .ReturnsAsync(permissionsByRole);

            IEnumerable<UiResourcePermissionResponse> results = await _uiResourceService.GetSubUiRecourceByUserAsync(new User(), It.IsAny<string>());

            results.Count().Should().Be(2);
            results.First(c => c.UiResourceId == 1001).CanEdit.Should().BeTrue();
        }

        [Fact]
        public async Task CreateNewUiResourceAsync_CreatesResourceSuccessfully()
        {
            // Arrange
            var request = new UiResourceRequest
            {
                ParentUiResourceKey = "parentKey",
                Key = "resourceKey",
                UiResourceTypeId = "CxTextBox",
                Names = new Dictionary<string, string>
            {
                { "en", "Resource Name" },
                { "fr", "Nom de la ressource" }
            }
            };

            var parentUI = new UiResource { Key = "parentKey" };

            var uiResource = new UiResource
            {
                Key = "parentKey.resourceKey",
                UiResourceTypeId = "CxTextBox"
            };

            var uiResourceNames = new List<UiResourceName>
        {
            new UiResourceName { LanguageId = "en", Resource = uiResource, Name = "Resource Name" },
            new UiResourceName { LanguageId = "fr", Resource = uiResource, Name = "Nom de la ressource" }
        };

            _mockUnitOfWork.Setup(uow => uow.UiResource.GetByKeyAsync("parentKey")).ReturnsAsync(parentUI);
            _mockUnitOfWork.Setup(uow => uow.UiResource.AddAsync(It.IsAny<UiResource>()));
            _mockUnitOfWork.Setup(uow => uow.UiResourceName.AddRangeAsync(It.IsAny<IEnumerable<UiResourceName>>()));
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync());

            _mockMapper.Setup(m => m.Map<UiResourceResponse>(It.IsAny<UiResource>())).Returns(new UiResourceResponse { Key = uiResource.Key });

            // Act
            var result = await _uiResourceService.CreateNewUiResourceAsync(request, "en");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("parentKey.resourceKey", result.Key);
            _mockUnitOfWork.Verify(uow => uow.UiResource.AddAsync(It.IsAny<UiResource>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.UiResourceName.AddRangeAsync(It.IsAny<IEnumerable<UiResourceName>>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
    }
}
