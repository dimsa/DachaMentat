using DachaMentat.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Filters;

namespace DachaMentat.Filters
{
    public class JwtAuthenticationAttribute : AuthorizeAttribute
    {
        
    }
}
