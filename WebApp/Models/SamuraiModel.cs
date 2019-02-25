using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SamuraiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecretIdentityRealName { get; set; }
        public List<QuoteModel> Quotes { get; set; }
    }
}
