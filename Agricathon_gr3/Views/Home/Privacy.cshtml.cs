using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Views.Home
{
    [Authorize]
    public class Privacy : PageModel
    {
    }
}
