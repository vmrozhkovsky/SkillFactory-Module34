﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutheticationService;

public class ExceptonHandler : ActionFilterAttribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        string message = "Произошла непредвиденная ошибка!";
        if (context.Exception is CustomException)
        {
            message = context.Exception.Message;
        }

        context.Result = new BadRequestObjectResult(message);
    }
}