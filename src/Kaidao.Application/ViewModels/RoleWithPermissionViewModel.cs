using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaidao.Application.ViewModels
{
    public class RoleWithPermissionViewModel
    {
        public RoleViewModel Role { get; set; }

        public List<CommandInFunctionList> CommandInFunction { get; set; }

    }

    public class CommandInFunctionList
    {
        public FunctionViewModel Function { get; set; }
        public List<Tuple<CommandInFunctionViewModel, bool>> Permissions { get; set; }
    }
}
