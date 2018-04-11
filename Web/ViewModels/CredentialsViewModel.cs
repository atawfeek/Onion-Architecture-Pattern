
using MLS.Web.ViewModels.Validations;
using FluentValidation.Attributes;

namespace MLS.Web.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
