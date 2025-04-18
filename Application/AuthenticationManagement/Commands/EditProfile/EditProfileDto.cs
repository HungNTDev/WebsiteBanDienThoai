using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.AuthenticationManagement.Commands.EditProfile
{
    public class EditProfileDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public string? Image { get; set; }
        public IFormFile formFile { get; set; }
    }
}
