namespace WhoCanHelpMe.Web.Modules.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using WhoCanHelpMe.Web.Modules.FormModels;

    [DebuggerDisplay("Name")]
    public class ProfilePageViewModel
    {
        public ProfilePageViewModel()
        {
            this.Assertions = new ProfileAssertionViewModel[0];
            this.FormModel = new ProfileFormModel();
            this.Categories = new[]
                                  {
                                      new KeyValuePair<string, string>("Is a", "Use for listing your level / position"),
                                      new KeyValuePair<string, string>(
                                          "Belongs to the",
                                          "Use for the name of the team you work for"),
                                      new KeyValuePair<string, string>(
                                          "Knows about",
                                          "Use for listing topics you know about"),
                                      new KeyValuePair<string, string>(
                                          "Is interested in",
                                          "Use for listing topics you are interested in"),
                                      new KeyValuePair<string, string>(
                                          "Has experience in",
                                          "Use for listing experiences in business verticals"),
                                      new KeyValuePair<string, string>(
                                          "Has worked for",
                                          "Use for listing clients you have worked for"),
                                      new KeyValuePair<string, string>(
                                          "Has previously worked for",
                                          "Use for listing companies you have worked for"),
                                      new KeyValuePair<string, string>(
                                          "Has worked on",
                                          "Use for listing projects you have worked on"),
                                      new KeyValuePair<string, string>(
                                          "Is a member of the",
                                          "Use for listing communities you belong to"),
                                      new KeyValuePair<string, string>(
                                          "Is certified as a",
                                          "Use for listing certifications"),
                                      new KeyValuePair<string, string>(
                                          "Has training for",
                                          "Use for listing training courses you have attended"),
                                      new KeyValuePair<string, string>(
                                          "Has attended",
                                          "Use for listing conferences you have attended"),
                                      new KeyValuePair<string, string>("Blogs at", "Use for your blog Url"),
                                      new KeyValuePair<string, string>("Tweets as", "Use for your Twitter handle")
                                  };
        }

        public string Name { get; set; }

        public ProfileAssertionViewModel[] Assertions { get; set; }

        public KeyValuePair<string, string>[] Categories { get; set; }

        public ProfileFormModel FormModel { get; set; }
    }
}