using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas
{
    public class DataGeneralPermission : EntityBase<string>
    {
        [Key, MaxLength(127)]
        public override string Id { get; set; } = null!;

        public bool EveryOneCanVisible { get; set; }
                    
        public bool EveryOneCanModified { get; set; }
                    
        public bool EveryOneCanRemoved { get; set; }


        public bool CreatedUserCanVisible { get; set; }

        public bool CreatedUserCanModified { get; set; }

        public bool CreatedUserCanRemoved { get; set; }


        public bool CreatedRoleCanVisible { get; set; }

        public bool CreatedRoleCanModified { get; set; }

        public bool CreatedRoleCanRemoved { get; set; }


        public bool OverCreatedRoleCanVisible { get; set; }

        public bool OverCreatedRoleCanModified { get; set; }

        public bool OverCreatedRoleCanRemoved { get; set; }


        public byte UnderCreatedRoleLevelCanVisible { get; set; }

        public byte UnderCreatedRoleLevelCanModified { get; set; }

        public byte UnderCreatedRoleLevelCanRemoved { get; set; }
    }
}
