namespace WhoCanHelpMe.Web.Configuration
{
    using AutoMapper;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Services.Commands;
    using WhoCanHelpMe.Web.Modules.FormModels;
    using WhoCanHelpMe.Web.Modules.ViewModels;

    public static class AutomapperConfiguration
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<ProfileFormModel, AddAssertionToProfileCommand>();
            Mapper.CreateMap<Assertion, ProfileAssertionViewModel>();
        }
    }
}