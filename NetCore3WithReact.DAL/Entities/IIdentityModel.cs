using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3WithReact.DAL.Entities
{
    public interface IIdentityModel
    {
        Guid Id { get; set; }
    }
}
