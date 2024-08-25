using StoreDeLaCruz.Core.Aplication.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Service
{
    public interface IEmailService
    {
        Task SenAsync(EmailRequestDto request);
    }
}
