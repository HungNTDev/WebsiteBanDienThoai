﻿using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;

namespace Application.AuthenticationManagement.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordModels Model) : ICommand<ApiResponse< object>>;
}
