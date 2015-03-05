namespace WhoCanHelpMe.Web.Modules.FormModels
{
    using System.ComponentModel.DataAnnotations;

    using SimpleAuthentication.Core.Annotations;

    public class ProfileFormModel
    {
        [Required]
        [NotNull]
        public string Category { get; set; }

        [Required]
        [NotNull]
        public string Tag { get; set; }
    }
}