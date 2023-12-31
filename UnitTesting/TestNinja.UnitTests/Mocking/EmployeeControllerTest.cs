﻿using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployerFromDb()
        {
            var storage = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(storage.Object);
            controller.DeleteEmployee(1);
            storage.Verify(s => s.DeleteEmployee(1));
        }
    }
}
