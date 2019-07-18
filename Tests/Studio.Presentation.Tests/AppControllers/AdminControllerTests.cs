namespace Studio.Presentation.Tests.AppControllers
{
    using MyTested.AspNetCore.Mvc;
    using Studio.User.WebApp.Areas.Administrator.Controllers;
    using Xunit;

    public class AdminControllerTests
    {
        [Fact]
        public void ReturnViewWhenCallingIndexAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingCountriesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Countries())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingCitiesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Cities())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingAddressesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Addresses())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingLocationsAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Locations())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingIndustriesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Industries())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingEmployeesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Employees())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingServicesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.Services())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingEmployeeServicesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.EmployeeServices())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingLocationIndustriesAction()
            => MyMvc
                .Controller<AdminController>()
                .Calling(c => c.LocationIndustries())
                .ShouldReturn()
                .View();
    }
}