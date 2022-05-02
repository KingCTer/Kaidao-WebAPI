namespace Kaidao.Domain.IdentityEntity
{
    public class CommandInFunction
    {
        public string FunctionId { get; set; }
        public Function Function { get; set; }

        public string CommandId { get; set; }
        public Command Command { get; set; }

        public string Description { get; set; }
    }
}