using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Auth.Command;
public class EmailLoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
