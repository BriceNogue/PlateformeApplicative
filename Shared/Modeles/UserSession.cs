using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Modeles
{
    public record class UserSession(int? Id, string? Name, string? Email, string? Role);
    public record class ParcSession(int Id, string Name);
}
