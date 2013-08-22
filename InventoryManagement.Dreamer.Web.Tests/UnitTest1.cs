using System;
using InventoryManagement.Dreamer.Domain.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagement.Dreamer.Web.Controllers;
namespace InventoryManagement.Dreamer.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pc = new ProductController(new BasicUnit());
            var companys = pc.GetAllCompanys();

            Assert.AreNotEqual(null,companys);
        }


    }
}
