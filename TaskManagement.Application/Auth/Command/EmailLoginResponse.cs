using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Model;

namespace TaskManagement.Application.Auth.Command;
public record EmailLoginResponse(
    UserModel User,
    TokenInfoModel TokenInfo);



