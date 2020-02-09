using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.Client.DTO
{
    public class AuthenticationResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
