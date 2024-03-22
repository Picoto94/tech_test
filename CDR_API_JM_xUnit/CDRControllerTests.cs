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

namespace CDR_API_JM_xUnit
{
    public class CDRControllerTests
    {
        [Fact]
        public async Task GetByReference_Returns_NotFound_When_CDR_Not_Found()
        {
            var reference = "123456";
            var mockService = new Mock<ICDRService>();
            mockService.Setup(service => service.GetByReferenceAsync(reference))
                .Returns(Task.FromResult<CDR>(null));
            var controller = new CDRController(mockService.Object);

            var result = await controller.GetByReference(reference);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetCallCountAndTotalDuration_Returns_BadRequest_On_Exception()
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;
            var mockService = new Mock<ICDRService>();
            mockService.Setup(service => service.GetCallCountAndTotalDurationAsync(startDate, endDate))
                .ThrowsAsync(new Exception("Test Exception"));
            var controller = new CDRController(mockService.Object);

            var request = new GetCallCountAndTotalDurationRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await controller.GetCallCountAndTotalDuration(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

    }
}