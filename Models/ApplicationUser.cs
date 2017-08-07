using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverFlow.Models
{
    public class ApplicationUser : IdentityUser
    { 
        public virtual ICollection<Question> Question { get; set; }
        
    
    }
}
