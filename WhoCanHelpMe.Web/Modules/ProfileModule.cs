namespace WhoCanHelpMe.Web.Modules
{
    using System.Linq;

    using AutoMapper;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses;
    using Nancy.Security;
    using Nancy.Validation;

    using Raven.Client;

    using WhoCanHelpMe.Common.Commands;
    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Services.Commands;
    using WhoCanHelpMe.Web.Extensions;
    using WhoCanHelpMe.Web.Modules.FormModels;
    using WhoCanHelpMe.Web.Modules.ViewModels;

    public class ProfileModule : NancyModule
    {
        private readonly IDocumentSession documentSession;

        private readonly ICommandDispatcher dispatcher;

        public ProfileModule(IDocumentSession documentSession, ICommandDispatcher dispatcher)
        {
            this.RequiresAuthentication();

            this.documentSession = documentSession;
            this.dispatcher = dispatcher;

            this.Get["/profile"] = this.GetProfile;
            this.Post["/profile/add"] = this.PostProfileAdd;
            this.Post["/profile/assertion/{id}/delete"]
        }

        private object PostProfileAdd(object arg)
        {
            var model = this.Bind<ProfileFormModel>();

            var validationResult = this.Validate(model);

            if (validationResult.IsValid)
            {
                var command = Mapper.Map<ProfileFormModel, AddAssertionToProfileCommand>(model);
                command.UserId = this.CurrentWchmUser().Id;

                this.dispatcher.Dispatch(command);

                return new RedirectResponse("/profile");
            }

            return new RedirectResponse("/profile");
        }

        private object GetProfile(object arg)
        {
            var user = this.CurrentWchmUser();

            var profile = this.documentSession.Load<User>(user.Id);

            var assertions = profile.Assertions.Select(Mapper.Map<Assertion, ProfileAssertionViewModel>).ToArray();

            var model = new ProfilePageViewModel
                            {
                                Name = profile.GetName(),
                                Assertions = assertions
                            };

            return this.View["Profile", model];
        }
    }
}