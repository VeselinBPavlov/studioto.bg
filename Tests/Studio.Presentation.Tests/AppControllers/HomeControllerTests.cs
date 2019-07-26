namespace Studio.Presentation.Tests.AppControllers
{
    using MyTested.AspNetCore.Mvc;
    using Studio.Application.ContactForms.Commands.Create;
    using Studio.Common;
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

        [Fact]
        public void CreatePostShouldSaveArticleSetModelStateMessageAndRedirectWhenValidModelState()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Contacts(new CreateContactFormCommand
                {
                    FirstName = GConst.ValidName,
                    LastName = GConst.ValidName,
                    Email = "vp_fin@abv.bg",
                    Topic = GConst.ValidName,
                    Message = GConst.ValidName
                }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}

