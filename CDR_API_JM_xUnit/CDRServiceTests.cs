using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application;
using CDR_API_JM.Controllers;
using Domain.Entities;
using CDR_API_JM.Models;
using Infrastructure;

namespace CDR_API_JM_xUnit
{
    public class CDRServiceTests
    {
        [Fact]
        public async Task GetByReferenceAsync_Returns_CDR_When_Found()
        {
            // Arrange
            var reference = "123456";
            var mockRepository = new Mock<ICDRRepository>();
            mockRepository.Setup(repo => repo.GetByReferenceAsync(reference))
                .Returns(Task.FromResult(new CDR { Reference = reference }));

            var service = new CDRService(mockRepository.Object);

            // Act
            var result = await service.GetByReferenceAsync(reference);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reference, result.Reference);
        }

        [Fact]
        public async Task GetCallCountAndTotalDurationAsync_Returns_Dictionary()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;
            var mockRepository = new Mock<ICDRRepository>();
            var service = new CDRService(mockRepository.Object);

            // Act
            var result = await service.GetCallCountAndTotalDurationAsync(startDate, endDate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Dictionary<string, object>>(result);
        }

        // Adicione mais testes conforme necessário para outros métodos do CDRService
    }
}