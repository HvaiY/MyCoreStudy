using System.ComponentModel.DataAnnotations;

namespace MyFirstABPProject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}