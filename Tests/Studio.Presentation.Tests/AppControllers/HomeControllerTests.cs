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
    }
}

