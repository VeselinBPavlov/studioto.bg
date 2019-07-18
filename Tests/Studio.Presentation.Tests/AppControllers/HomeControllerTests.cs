namespace Studio.Presentation.Tests.AppControllers
{
    using MyTested.AspNetCore.Mvc;
    using Studio.User.WebApp.Controllers;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void ReturnViewWhenCallingIndexAction()
            => MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingAboutAction()
            => MyMvc
                .Controller<HomeController>()
                .Calling(c => c.About())
                .ShouldReturn()
                .View();
        
        [Fact]
        public void ReturnViewWhenCallingStepsAction()
            => MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Steps())
                .ShouldReturn()
                .View();

        [Fact]
        public void ReturnViewWhenCallingContactsAction()
            => MyMvc
                .Controller<HomeController>()
                .Calling(c => c.Contacts())
                .ShouldReturn()
                .View();
    }
}

