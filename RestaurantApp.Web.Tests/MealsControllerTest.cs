using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Web.Controllers;

namespace RestaurantApp.Web.Tests
{
    [TestClass]
    public class MealsControllerTest
    {
        [TestMethod]
        public void IndexMethod()
        {
            Assert.AreEqual("HomeController", "HomeController");
        }

        [TestMethod]
        public async void DetailsMethod()
        {
            MealsController hc = new MealsController();
            var result = hc.Details(1) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}
